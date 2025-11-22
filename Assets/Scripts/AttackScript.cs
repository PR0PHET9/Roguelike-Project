using UnityEngine;

public class AttackScript : MonoBehaviour
{

public GameObject owner;

//Future Animation Scripts/Classing
// [SerializeField]
// private string animationName;

[SerializeField]
private bool magicAttack;

[SerializeField]
private float magicCost;

[SerializeField]
private float minAttackMultiplier;

[SerializeField]
private float maxAttackMultiplier;

[SerializeField]
private float minDefenseMultiplier;

[SerializeField]
private float maxDefenseMultiplier;

private FighterStats attackerStats;
private FighterStats targetStats;
private float damage = 0.0f;


 //Magic Mana bar system 

public void Attack(GameObject victim)

{
  
    attackerStats = owner.GetComponent<FighterStats>();
    targetStats = victim.GetComponent<FighterStats>();
  
    if(attackerStats.magic >= magicCost); //Checks mana cost of spell even if zero
    {
        float multiplier = Random.Range(minAttackMultiplier, maxAttackMultiplier);
        if(magicCost > 0)

        {
        attackerStats.updateMagicFill(magicCost);
        }
        damage = multiplier * attackerStats.melee; //Magic costs
        if (magicAttack)
        {
            damage = multiplier * attackerStats.magicRange;
            attackerStats.magic = attackerStats.magic - magicCost;

        }
        float defenseMultiplier = Random.Range(minDefenseMultiplier, maxDefenseMultiplier);
        damage =Mathf.Max(0, damage - (defenseMultiplier * targetStats.defense));

        //Future Animation script (remove this line later)
        //owner.GetComponent<Animator>().Play(animationName); 
        targetStats.ReceiveDamage(damage); //Target takes damage
        attackerStats.updateMagicFill(magicCost);
    }

}

}
