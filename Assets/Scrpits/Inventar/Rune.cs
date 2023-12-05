using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Rune", order = 3)]

public class Rune : ScriptableObject
{
    //alle daten die eine rune haben m�sste
    public string itemType;
    public string name;
    //strength was removed because it will update over the course of the game and the base is 0 for all runes
    public int baseDmg;
    public Sprite Icon;
    public Sprite InSlot;
    public int level = 0;
    public Color color;
    public string description;
}
