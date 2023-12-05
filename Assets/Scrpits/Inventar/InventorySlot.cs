//Frontend Script Assigned to individual Slots to change their Occupance(?)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField]
    public bool isRune = false;
    public int slotIndex;
    public Rune item;
    public Sprite itemImage;
    // Start is called before the first frame update

public void OnDrop(PointerEventData eventData){              //Make this able to swap out Runes when they are overlaid over another (List implementation was lacking and we didnt spawn new runes at inventory runtime)
    InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
    Inventory inventory = transform.parent.parent.parent.GetComponent<Inventory>();
    if (transform.childCount == 0){
        inventoryItem.parentAfterDrag = transform;
        int prevIndex = inventoryItem.GetComponent<InventoryItem>().index;
        transform.parent.parent.parent.GetComponent<Inventory>().MovItem(prevIndex, slotIndex);
        inventoryItem.GetComponent<InventoryItem>().SetIndex(slotIndex);
        if(isRune){inventoryItem.GetComponent<Image>().rectTransform.localScale = new Vector3(2,2,2);}
    }
    else if(inventoryItem.GetComponent<InventoryItem>().rune.level == transform.GetChild(0).GetComponent<InventoryItem>().rune.level){
        inventory.UpgradeItem(transform.GetChild(0).GetComponent<InventoryItem>().index,transform.GetChild(0).GetComponent<InventoryItem>().rune.level);
        //inventory.RemItem(inventoryItem.index);           Fix this method
        Debug.Log(transform.GetChild(0).GetComponent<InventoryItem>().rune.level);

    }
}
    public void SetItem(Rune newItem){              //Is this redundant?
        if(newItem == null){
            GetComponent<Image>().color = new Color32(255,255,225,0);
        }
        else{
            item = newItem;
            Debug.Log("Got Item with name: "+item.name);
            GetComponent<Image>().color = new Color32(255,255,225,255);
            itemImage = item.InSlot;
            GetComponent<Image>().sprite = itemImage;
        }
    }

    /*public void SetRune(GameObject newRune){
        if(!isRune){break;}
        
    }*/
}
