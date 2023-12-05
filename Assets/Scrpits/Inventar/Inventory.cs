using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryItemPrefab;
    public PlayerMovement player;
    public InventorySlot[] inventorySlots;
    public Rune[] items = new Rune[12];

    public void OnInventoryToggle(){
           //Wenn Inventar aktiv ist, schließe es (default state)
           if(transform.GetChild(1).gameObject.activeSelf){
            Debug.Log("Gameplay");
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
            for(int i = 0; i < 2; i++){
                Debug.Log("Iterating:" +i);
                InventorySlot slot = transform.GetChild(0).GetChild(0).GetChild(i).GetComponent<InventorySlot>();
                int ie = i+10;
                Debug.Log("Children: "+slot.transform.childCount+" at "+i+" | index in items: "+ie+" | item at i: "+items[i+10]);
                //Gegenstand ist in Liste aber keiner ist im Slot
                if(!(items[i+10]==null) && slot.transform.childCount == 0){
                    SpawnNewItem(items[i+10], slot);
                }
                //Kein Gegenstand ist in der Liste aber einer im Slot
                else if(items[i+10] ==null && slot.transform.childCount > 0){
                    Destroy(slot.transform.GetChild(0).gameObject);            //das löschen/die referenz auf das objekt funktioniert nicht
                }
                //Ein Gegenstand ist in der Liste aber der falsche im Slot
                else if (items[i+10] != null && items[i+10] != slot.transform.GetChild(0).GetComponent<InventoryItem>().rune){
                    Destroy(slot.transform.GetChild(0).gameObject);
                    SpawnNewItem(items[i+10], slot);
                }
            }
            Debug.Log("TimeScale resumed");
            Time.timeScale = 1f;
           }
            //ansonsten öffne es
           else{
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
            Debug.Log("Inventory | "+inventorySlots.Length);
            for(int i = 0; i < inventorySlots.Length; i++){
                //Rufe SpawnNewItem auf bis alle Elemente aus (Array welches gesammelte Items speichert) vollständig drin ist
                if(!(items[i]==null)){
                InventorySlot slot = inventorySlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if (itemInSlot == null && !slot.isRune){
                    SpawnNewItem(items[i], slot);
                }
                }
            }
            Time.timeScale = 0f;
           }
    }

    public void UpgradeItem(int index, int level){
        inventorySlots[index].transform.GetChild(0).GetComponent<InventoryItem>().rune.level = level++;
    }

    public void RemItem(int index){
        Destroy(inventorySlots[index].transform.GetChild(0));
        inventorySlots[index] = null;
    }

    public void AddItem(Rune item){
        for(int i = 0; i < items.Length;i++){
            if(items[i] == null){
                items[i] = item;
                break;
            }
        }
        Debug.Log("Added item");
    }

    public void MovItem(int prevIndex, int nextIndex){
        Debug.Log(prevIndex+ " | "+nextIndex);
        items[nextIndex] = items[prevIndex];
        if(prevIndex != nextIndex){items[prevIndex] = null;}
        for(int i = 0; i<2;i++){
            Debug.Log("Transferring Runes");
            player.runes[i] = items[i+10];
        }
        Debug.Log("Moved Item from "+prevIndex+" to "+nextIndex+" ("+items[nextIndex].name+")");
    }
    void SpawnNewItem (Rune rune, InventorySlot slot){
        Debug.Log("Trying to spawn item "+rune.name+" at "+slot.transform.name);
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(rune);
        //Debug.Log("Spawned Item "+rune.name+" at "+slot.name);
    }
}

/*  public void AddRune(Rune rune, int slot){
        if(isFull()){return;}
        Debug.Log("Adding Item "+rune.name);
        items[next] = rune;
        if(next < (size-1)){next++;}
    }
    public bool isFull(){
        for (int i = 0; i < size;i++){
            if(items[i]==null){
                return false;
                Debug.Log("Full");
                break;
            }
        }
        Debug.Log("Empty");
        return true;
    }

    private void ShowInventory(){

        if(transform.GetChild(1).gameObject.activeSelf){
            for(int i = 0; i < size; i++){
                searchedSlot = transform.GetChild(1).GetChild(i).gameObject;
                Debug.Log($"Found: {searchedSlot}", searchedSlot);
                //send null if there is no item saved in items[i], else add item
                if(items[i]==null){searchedSlot.GetComponent<InventorySlot>().SetItem(null);}
                else{searchedSlot.GetComponent<InventorySlot>().SetItem(items[i]);
                Debug.Log(searchedSlot.name+ " | "+i+" | "+ items[i].name);}
            }
            for(int i = 0; i < 2; i++){
                searchedSlot = transform.GetChild(1).GetChild(i+10).gameObject;

            }
        }
    }*/
