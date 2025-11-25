using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{
    [Header("Stats")]
    private Rigidbody2D rb;
public string unitName;
public int unitLevel;
public int damage;
public int maxHP;
//public int health;
public int currentHP;

public float health;
public float startMagic;
 public float magic;
 public float melee;
 public float magicRange;
 public float defense;
  public float speed;

 //public float experience;
public Slider hpSlider;

[SerializeField] FloatingHealthBar healthBar;

private void Awake()
{
    rb = GetComponent<Rigidbody2D>();
    healthBar = GetComponentInChildren<FloatingHealthBar>();
}

void Start()
{
      currentHP = maxHP; //sets hp to max hp
        hpSlider.maxValue = maxHP;
        hpSlider.value = currentHP;
}

public bool TakeDamage(int dmg)
{
    currentHP -= dmg;
    healthBar.UpdateHealthBar(currentHP, maxHP);
    if(currentHP <= 0)
    return true;
    else
    
        return false;
    
}

}
 
