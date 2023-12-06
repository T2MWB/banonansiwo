using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    [SerializeField]
    public Weapon weapon;
    private int damage;
    private float range;
    private float handling;
    private float speed;
    private float animspeed;
    private string name;
    

    //var dropDown = itemType.Pickup;
    // Start is called before the first frame update
    void Start()
    {
        damage = weapon.damage;
        range = weapon.range;
        handling = weapon.handling;
        speed = weapon.speed;
        name = weapon.name;
        animspeed = weapon.animspeed;
    }

    public void OnTriggerEnter2D(Collider2D collider){
        collider.GetComponent<PlayerMovement>().changeWeapon(damage,range,handling,speed,animspeed,name);
        Debug.Log("Schwert geändert | " + damage + " | " + speed+" | "+animspeed);
    }
}