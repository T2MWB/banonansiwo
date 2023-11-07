using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    public WeaponData weapon;
    private int damage;
    private float range;
    // Start is called before the first frame update
    void Start()
    {
        damage = weapon.damage;
        range = weapon.range;
    }

    public void OnTriggerEnter2D(Collider2D collider){
        collider.GetComponent<PlayerMovement>().changeWeapon(damage,range);
        Debug.Log("Schwert geändert | " + damage);
    }
}