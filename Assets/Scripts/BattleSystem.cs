using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

//public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{
    /*
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
     public Transform enemyBattleStation;

     Unit playerUnit;
     Unit enemyUnit;
     
    public TextMeshProUGUI dialogueText;
    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;


    public BattleState state;
   
      void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hero"))
      
        
    
      

        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();
       

          GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();
       
    dialogueText.text = "A Wild" + enemyUnit.unitName + "approaches...";

    playerHUD.SetHUD(playerUnit);
    enemyHUD.SetHUD(enemyUnit);

    yield return new WaitForSeconds(2f);
    state = BattleState.PLAYERTURN;
    PlayerTurn();
    }
    IEnumerator PlayerAttack()
    {
bool isDead = enemyUnit.TakeDamage(playerUnit.damage);  //Damage the enemy
enemyHUD.SetHP(enemyUnit.currentHP);

    

         yield return new WaitForSeconds(2f);
         if(isDead)
         {
        state = BattleState.WON;
        EndBattle();
         }else
         {
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
         }
    }

    IEnumerator EnemyTurn()

    {
        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);
        
         if(isDead)
         {
        state = BattleState.LOST;
        EndBattle();
         }else
         {
        state = BattleState.PLAYERTURN;
        PlayerTurn();
        }
    }
    void EndBattle()
    {
        if(state ==BattleState.WON)
        {
//Function to close the battle after completion
        }else if (state == BattleState.LOST)
        {
            //Function to close the battle after completion

        }

    }
    void PlayerTurn()
        {
            dialogueText.text = "Choose and Action";

        }
    public void OnAttackButton()

    {
        if (state != BattleState.PLAYERTURN)
            return;
            StartCoroutine(PlayerAttack());

    

    } */
  
}
