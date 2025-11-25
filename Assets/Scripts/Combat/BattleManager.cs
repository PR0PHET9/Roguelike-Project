using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Central manager: spawns player party (from selection) and 1-4 enemies randomly
// then runs a simple turn-based loop.
public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    [Header("Prefab fallback (if UnitData.prefab is empty)")]
    public GameObject fallbackPrefab; // must have CharacterBattle on root

    [Header("Enemy templates (UnitData assets for enemies)")]
    public List<UnitData> enemyTemplates = new List<UnitData>();

    [Header("Spawn settings")]
    public Vector3[] playerSpawnPositions = new Vector3[4]
    {
        new Vector3(-4f, 0f, 0f),
        new Vector3(-5.5f, -0.3f, 0f),
        new Vector3(-2.5f, -0.3f, 0f),
        new Vector3(-7f, -0.6f, 0f)
    };
    public Vector3[] enemySpawnPositions = new Vector3[4]
    {
        new Vector3(4f, 0f, 0f),
        new Vector3(5.5f, -0.3f, 0f),
        new Vector3(2.5f, -0.3f, 0f),
        new Vector3(7f, -0.6f, 0f)
    };

    [Header("Gameplay")]
    public float endOfTurnDelay = 0.15f;

    private List<CharacterBattle> playerTeam = new List<CharacterBattle>();
    private List<CharacterBattle> enemyTeam = new List<CharacterBattle>();

    private enum BattleState { Idle, Running, Finished }
    private BattleState battleState = BattleState.Idle;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // entry point: called by PartySelectionManager. playerUnits is list of UnitData selected by player
    public void StartBattle(List<UnitData> playerUnits)
    {
        if (battleState == BattleState.Running) return;

        // clear any previous
        foreach (var go in playerTeam) if (go) Destroy(go.gameObject);
        foreach (var go in enemyTeam) if (go) Destroy(go.gameObject);
        playerTeam.Clear();
        enemyTeam.Clear();

        // spawn players
        for (int i = 0; i < playerUnits.Count && i < playerSpawnPositions.Length; i++)
        {
            var data = playerUnits[i];
            var cb = SpawnUnit(data, playerSpawnPositions[i], true);
            cb.unitName = data.unitName;
            playerTeam.Add(cb);
        }

        // spawn random number of enemies between 1 and 4
        int enemyCount = Random.Range(1, Mathf.Min(4, enemyTemplates.Count) + 1);
        for (int i = 0; i < enemyCount && i < enemySpawnPositions.Length; i++)
        {
            // pick random enemy template
            var template = enemyTemplates[Random.Range(0, enemyTemplates.Count)];
            var cb = SpawnUnit(template, enemySpawnPositions[i], false);
            cb.unitName = template.unitName;
            enemyTeam.Add(cb);
        }

        // start battle loop
        StartCoroutine(BattleLoop());
    }

    private CharacterBattle SpawnUnit(UnitData data, Vector3 position, bool isPlayer)
    {
        GameObject prefab = (data != null && data.prefab != null) ? data.prefab : fallbackPrefab;
        if (prefab == null)
        {
            Debug.LogError("No prefab set for UnitData and no fallbackPrefab assigned.");
            var placeholder = new GameObject("PlaceholderUnit");
            var cbPlaceholder = placeholder.AddComponent<CharacterBattle>();
            cbPlaceholder.Setup(data, isPlayer);
            placeholder.transform.position = position;
            return cbPlaceholder;
        }

        var go = Instantiate(prefab, position, Quaternion.identity);
        var cb = go.GetComponent<CharacterBattle>();
        if (cb == null)
        {
            Debug.LogError("Prefab does not have CharacterBattle on root!");
            // attach one so system can continue
            cb = go.AddComponent<CharacterBattle>();
        }

        cb.Setup(data, isPlayer);
        return cb;
    }

    private IEnumerator BattleLoop()
    {
        battleState = BattleState.Running;
        Debug.Log("Battle start!");

        while (!IsBattleOver())
        {
            // order alive units by speed descending
            var units = new List<CharacterBattle>();
            units.AddRange(playerTeam.Where(u => u != null && !u.IsDead));
            units.AddRange(enemyTeam.Where(u => u != null && !u.IsDead));
            units = units.OrderByDescending(u => u.speed).ToList();

            foreach (var unit in units)
            {
                if (IsBattleOver()) break;
                if (unit == null || unit.IsDead) continue;

                if (unit.isPlayerTeam)
                {
                    yield return StartCoroutine(PlayerTurn(unit));
                }
                else
                {
                    yield return StartCoroutine(EnemyTurn(unit));
                }

                yield return new WaitForSeconds(endOfTurnDelay);
            }

            yield return null; // next round
        }

        battleState = BattleState.Finished;
        Debug.Log(GetBattleResult());
    }

    private bool IsBattleOver()
    {
        bool playersAlive = playerTeam.Any(x => x != null && !x.IsDead);
        bool enemiesAlive = enemyTeam.Any(x => x != null && !x.IsDead);
        return !(playersAlive && enemiesAlive);
    }

    private string GetBattleResult()
    {
        bool playersAlive = playerTeam.Any(x => x != null && !x.IsDead);
        bool enemiesAlive = enemyTeam.Any(x => x != null && !x.IsDead);
        if (playersAlive && !enemiesAlive) return "Players win!";
        if (!playersAlive && enemiesAlive) return "Enemies win!";
        return "Draw";
    }

    // Simple player turn: pick first alive enemy automatically (replace with UI input)
    private IEnumerator PlayerTurn(CharacterBattle unit)
    {
        Debug.Log($"Player turn: {unit.unitName}");

        var targets = enemyTeam.Where(e => e != null && !e.IsDead).ToList();
        if (targets.Count == 0) yield break;

        // TODO: replace this with UI-based target selection.
        var target = targets[0];

        bool actionDone = false;
        yield return StartCoroutine(unit.Attack(target, () => actionDone = true));
        while (!actionDone) yield return null;
    }

    // Simple enemy AI: pick random alive player
    private IEnumerator EnemyTurn(CharacterBattle unit)
    {
        Debug.Log($"Enemy turn: {unit.unitName}");

        var targets = playerTeam.Where(p => p != null && !p.IsDead).ToList();
        if (targets.Count == 0) yield break;

        var target = targets[Random.Range(0, targets.Count)];
        bool actionDone = false;
        yield return StartCoroutine(unit.Attack(target, () => actionDone = true));
        while (!actionDone) yield return null;
    }
}