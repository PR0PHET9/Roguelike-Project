using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnBasedBattleSystem : MonoBehaviour
{
    /*
    [Header("Prefabs / Spawning")]
    [Tooltip("Prefab with CharacterBattle component on root")]
    public Transform pfCharacterCombat;
    public int playerCount = 2;
    public int enemyCount = 2;

    [Header("Spawn positions")]
    public Vector3[] playerSpawnPositions;
    public Vector3[] enemySpawnPositions;

    [Header("Gameplay")]
    public float endOfTurnDelay = 0.15f;

    private List<CharacterBattle> playerTeam = new List<CharacterBattle>();
    private List<CharacterBattle> enemyTeam = new List<CharacterBattle>();

    private enum BattleState { Starting, Running, Finished }
    private BattleState battleState = BattleState.Starting;

    private void Start()
    {
        // if no positions set, provide defaults
        if (playerSpawnPositions == null || playerSpawnPositions.Length == 0)
        {
            playerSpawnPositions = new Vector3[] {
                new Vector3(-3f, -1f, 0f),
                new Vector3(-4.5f, -1.4f, 0f),
                new Vector3(-1.5f, -0.6f, 0f)
            };
        }
        if (enemySpawnPositions == null || enemySpawnPositions.Length == 0)
        {
            enemySpawnPositions = new Vector3[] {
                new Vector3(3f, -1f, 0f),
                new Vector3(4.5f, -1.4f, 0f),
                new Vector3(1.5f, -0.6f, 0f)
            };
        }

        SpawnTeams();
        StartCoroutine(BattleLoop());
    }

    private void SpawnTeams()
    {
        for (int i = 0; i < playerCount; i++)
        {
            Vector3 pos = playerSpawnPositions[Mathf.Min(i, playerSpawnPositions.Length - 1)];
            var cb = SpawnCharacter(true, pos);
            cb.unitName = $"Player {i + 1}";
            playerTeam.Add(cb);
        }

        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 pos = enemySpawnPositions[Mathf.Min(i, enemySpawnPositions.Length - 1)];
            var cb = SpawnCharacter(false, pos);
            cb.unitName = $"Enemy {i + 1}";
            enemyTeam.Add(cb);
        }
    }

    private CharacterBattle SpawnCharacter(bool isPlayerTeam, Vector3 position)
    {
        Transform t = Instantiate(pfCharacterCombat, position, Quaternion.identity);
        CharacterBattle cb = t.GetComponent<CharacterBattle>();
        if (cb == null)
            Debug.LogError("pfCharacterCombat must have a CharacterBattle component on its root!");
        cb.Setup(isPlayerTeam);
        return cb;
    }

    private IEnumerator BattleLoop()
    {
        battleState = BattleState.Running;
        Debug.Log("Battle start!");

        while (!IsBattleOver())
        {
            // Build a list of all alive combatants and order by speed descending
            List<CharacterBattle> turnOrder = new List<CharacterBattle>();
            turnOrder.AddRange(playerTeam.Where(x => !x.IsDead));
            turnOrder.AddRange(enemyTeam.Where(x => !x.IsDead));
            turnOrder = turnOrder.OrderByDescending(x => x.speed).ToList();

            foreach (var unit in turnOrder)
            {
                if (IsBattleOver()) break; // check again mid-round

                if (unit.IsDead) continue;

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
        bool playersAlive = playerTeam.Any(x => !x.IsDead);
        bool enemiesAlive = enemyTeam.Any(x => !x.IsDead);
        return !(playersAlive && enemiesAlive);
    }

    private string GetBattleResult()
    {
        bool playersAlive = playerTeam.Any(x => !x.IsDead);
        bool enemiesAlive = enemyTeam.Any(x => !x.IsDead);
        if (playersAlive && !enemiesAlive) return "Players win!";
        if (!playersAlive && enemiesAlive) return "Enemies win!";
        return "Draw";
    }

    // --- Player Turn: simple target selection + attack
    private IEnumerator PlayerTurn(CharacterBattle unit)
    {
        Debug.Log($"Player turn: {unit.unitName}");
        // build list of alive enemy targets
        List<CharacterBattle> targets = enemyTeam.Where(e => !e.IsDead).ToList();
        if (targets.Count == 0) yield break;

        int selectedIndex = 0;
        bool confirmed = false;

        Debug.Log($"Select a target with Left/Right and press Space to attack. Current target: {targets[selectedIndex].unitName}");

        // simple selection loop
        while (!confirmed)
        {
            // show current target each frame (could be replaced with UI highlight)
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                selectedIndex = (selectedIndex + 1) % targets.Count;
                Debug.Log($"Selected: {targets[selectedIndex].unitName}");
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                selectedIndex = (selectedIndex - 1 + targets.Count) % targets.Count;
                Debug.Log($"Selected: {targets[selectedIndex].unitName}");
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                confirmed = true;
            }

            // if the selected target died while selecting, refresh targets
            targets = enemyTeam.Where(e => !e.IsDead).ToList();
            if (targets.Count == 0) yield break;
            selectedIndex = Mathf.Clamp(selectedIndex, 0, targets.Count - 1);

            yield return null;
        }

        var target = targets[selectedIndex];
        bool actionDone = false;
        yield return StartCoroutine(unit.Attack(target, () => actionDone = true));
        while (!actionDone) yield return null; // safety, though Attack coroutine yields properly
    }

    // --- Enemy Turn: simple AI - pick random player target and attack
    private IEnumerator EnemyTurn(CharacterBattle unit)
    {
        Debug.Log($"Enemy turn: {unit.unitName}");
        List<CharacterBattle> targets = playerTeam.Where(p => !p.IsDead).ToList();
        if (targets.Count == 0) yield break;

        var target = targets[Random.Range(0, targets.Count)];
        bool actionDone = false;
        yield return StartCoroutine(unit.Attack(target, () => actionDone = true));
        while (!actionDone) yield return null;
    }
    */
}