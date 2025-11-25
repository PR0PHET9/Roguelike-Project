using System;
using System.Collections;
using UnityEngine;

public class CharacterBattle : MonoBehaviour
{
    [Header("Runtime")]
    public string unitName;
    public bool isPlayerTeam;

    [Header("Stats (runtime)")]
    public int maxHP;
    public int currentHP;
    public int attackStat;
    public int defenseStat;
    public int speed;

    public bool IsDead => currentHP <= 0;

    // Setup from UnitData
    public void Setup(UnitData data, bool isPlayer)
    {
        if (data != null)
        {
            unitName = data.unitName;
            maxHP = data.maxHP;
            currentHP = maxHP;
            attackStat = data.attack;
            defenseStat = data.defense;
            speed = data.speed;
            isPlayerTeam = isPlayer;
        }
        else
        {
            // fallback defaults
            unitName = "Unknown";
            maxHP = 10;
            currentHP = maxHP;
            attackStat = 5;
            defenseStat = 1;
            speed = 1;
            isPlayerTeam = isPlayer;
        }
    }

    // Simple attack coroutine, calls callback when complete
    public IEnumerator Attack(CharacterBattle target, Action onComplete = null)
    {
        if (target == null || target.IsDead)
        {
            onComplete?.Invoke();
            yield break;
        }

        Debug.Log($"{unitName} attacks {target.unitName}");

        // wind-up / animation placeholder
        yield return new WaitForSeconds(0.3f);

        int damage = CalculateDamage(target);
        target.TakeDamage(damage);

        // hit reaction placeholder
        yield return new WaitForSeconds(0.25f);

        onComplete?.Invoke();
    }

    private int CalculateDamage(CharacterBattle target)
    {
        int raw = attackStat - target.defenseStat;
        return Mathf.Max(1, raw);
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(0, currentHP);
        Debug.Log($"{unitName} takes {damage} damage ({currentHP}/{maxHP})");

        if (IsDead)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        Debug.Log($"{unitName} has died.");
        // play death animation, disable, etc. For now disable after short delay
        StartCoroutine(DelayedDisable());
    }

    private IEnumerator DelayedDisable()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}