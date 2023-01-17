using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerScriptableObject",menuName = "ScriptableObjects/Towers")]
public class TowerScriptableObject : ScriptableObject
{
    public string TowerName;
    public int damage;
    public float shootingSpeed;
    public int price;

    public int[] priceR1;
    public int[] priceR2;

    public float range;

    public bool fireBullet;
    public bool seeCamuf;
    public bool usesRaycast;
    public bool bulletIsDestroyed;

    public Sprite[] sprites;

    public GameObject fireDontDestroyObj;
    public GameObject notFireDestroy;
    public GameObject fireDestroy;
    public GameObject notFireDontDestroy;

    
    
}
