using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Weapon", order = 2)]

public class Weapon : ScriptableObject
{
    //alle daten die eine waffe haben m�sste
    public string name;
    public int damage;
    public float range;
    public float handling;
    public float speed;
    public float animspeed;
    //public int type;
}
