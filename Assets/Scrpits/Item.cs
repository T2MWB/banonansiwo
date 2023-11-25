using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    public WeaponData weapon;
    private int damage;
    private float range;
    private float speed;
    public enum itemType{
        Pickup,
        Weapon,
    }
    var dropDown = itemType.Pickup;
    // Start is called before the first frame update
    void Start()
    {
        damage = weapon.damage;
        range = weapon.range;
        speed = weapon.speed;
    }

    public void OnTriggerEnter2D(Collider2D collider){
        collider.GetComponent<PlayerMovement>().changeWeapon(damage,range,speed);
        Debug.Log("Schwert geändert | " + damage + " | " + speed);
    }
}