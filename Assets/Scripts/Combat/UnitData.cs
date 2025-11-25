using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "Combat/UnitData")]
public class UnitData : ScriptableObject
{
    [Header("Identity")]
    public string unitName = "New Unit";

    [Header("Prefab (must have CharacterBattle on root)")]
    public GameObject prefab;

    [Header("Stats")]
    public int maxHP = 30;
    public int attack = 8;
    public int defense = 2;
    public int speed = 5;

    [Header("Misc")]
    public bool isPlayerUnit = true;
}