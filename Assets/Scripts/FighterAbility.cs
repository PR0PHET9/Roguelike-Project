using UnityEngine;
using UnityEngine.UI;

public class FighterAbility : MonoBehaviour
{
private GameObject enemy;
private GameObject hero;


[SerializeField]
private GameObject meleePrefab;

[SerializeField]
private GameObject rangePrefab;

[SerializeField]
private Sprite faceIcon;

private GameObject currentAttack;
private GameObject meleeAttack;
private GameObject rangeAttack;

public void SelectAttack(string btn)
{
    
    GameObject victim = tag == "Hero" ? enemy : hero;
    
    if (btn.CompareTo("melee") == 0)
    {
    Debug.Log("Melee Attack");
    } 
    else if (btn.CompareTo("range") == 0)
    {
    Debug.Log("Range Attack");
    } else  
    { 
    Debug.Log("Run");
    }
}
}
