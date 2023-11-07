/*
Erstellung: ???
Authoren: Tammo Wiebe
Nutzen: Blaupause für Scriptables der "Gegner" Klasse. Wird in Scriptables eingesetzt um die Eintragung der unten gegebenen Variablen zu ermöglichen.
Änderungen:
3.6.2023, Tammo Wiebe - beschreibender Programmkopf hinzugefügt
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Enemy", order = 1)]

public class EnemyData : ScriptableObject
{
    public int hp;
    public int damage;
    public float speed;
}
