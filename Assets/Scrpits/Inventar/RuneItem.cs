using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneItem : MonoBehaviour
{
    [SerializeField]
    public Rune rune;
    private Inventory inventory;// = GameObject.FindGameObjectsWithTag("Inventory").GetComponent<Inventory>();

    public string itemType;
    public string name;
    public int damage;
    public Sprite icon;
    public Sprite slot;


    //var dropDown = itemType.Pickup;
    // Start is called before the first frame update
    void Start(){
        inventory = GameObject.Find("UI").GetComponent<Inventory>();
        itemType = rune.itemType;
        name = rune.name;
        damage = rune.baseDmg;
        icon = rune.Icon;
        slot = rune.InSlot;
    }

    public void OnTriggerEnter2D(Collider2D collider){
        //Debug.Log(name);
            inventory.AddItem(rune); //rune
        //GameObject.Find.GetComponent<Inventory>().OnInventoryToggle();
        
        //collider.GetComponent<PlayerMovement>().changeWeapon(damage,range,speed,name);
        //Debug.Log("Schwert geändert | " + damage + " | " + speed);
    }
}