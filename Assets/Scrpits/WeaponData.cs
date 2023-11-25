using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Weapon", order = 2)]

public class WeaponData : ScriptableObject
{
    //alle daten die eine waffe haben m�sste
    public int damage;
    public float range;
    public float speed;
    //public int type;
}
