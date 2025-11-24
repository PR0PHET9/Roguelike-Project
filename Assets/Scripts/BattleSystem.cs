using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
     public Transform enemyBattleStation;

     Unit playerUnit;
     Unit enemyUnit;
     
     public Text dialogueText;
    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;


    public BattleState state;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = BattleState.START;
        SetupBattle();
    }

    void SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();
       

          GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();
       
    dialogueText.text = "A Wild" + enemyUnit.unitName + "approaches...";

    playerHUD.SetHUD(playerUnit);
    enemyHUD.SetHUD(enemyUnit);

    }

}
