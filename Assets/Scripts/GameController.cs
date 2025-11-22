using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Transactions;
using UnityEngine.SocialPlatforms;



public class GameController : MonoBehaviour
{
 private List<FighterStats> fighterStats;

 private GameObject battleMenu;


public Text battleText;

private void Awake()
{
    battleMenu = GameObject.Find("ActionMenu");
}

 void Start()

 { //Determining who starts first based on character speed
    fighterStats = new List<FighterStats>();
    GameObject hero = GameObject.FindGameObjectWithTag("Hero");
    FighterStats currentFighterStats = hero.GetComponent<FighterStats>();
    currentFighterStats.CalculateNextTurn(0);
    fighterStats.Add(currentFighterStats);

    GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
    FighterStats currentEnemyStats = enemy.GetComponent<FighterStats>();
    currentEnemyStats.CalculateNextTurn(0);
    fighterStats.Add(currentEnemyStats);

    fighterStats.Sort();
    this.battleMenu.SetActive(false);

    NextTurn();

 }

    public void NextTurn()
    {
        FighterStats currentFighterStats = fighterStats[0];
        fighterStats.Remove(currentFighterStats);
       if(!currentFighterStats.GetDead()) 
       {
        GameObject currentUnit = currentFighterStats.gameObject;
        currentFighterStats.CalculateNextTurn(currentFighterStats.nextActTurn);
        fighterStats.Add(currentFighterStats);
        fighterStats.Sort(); //sorts based on speed value
    if(currentUnit.tag == "Hero")
    {
    this.battleMenu.SetActive(true);
    } else
    {
        string attackType = Random.Range(0, 2) == 1 ? "melee" : "range"; //currently only 2 attack profiles, this to be updated with more later
        currentUnit.GetComponent<FighterAbility>().SelectAttack(attackType);
    }
       } else
       {
        NextTurn();
       }
    }
}
