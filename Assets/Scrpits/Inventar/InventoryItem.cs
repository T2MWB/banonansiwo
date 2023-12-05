using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Rune rune;
    public int index;
    public Image image;
    public GameObject itemDescriptor;
    public GameObject newDescriptor;
    [HideInInspector] public Transform parentAfterDrag;
[HideInInspector] public Transform parentPrev;
    private void Start(){
        InitialiseItem(rune);

    }

    public void InitialiseItem(Rune newItem) {
        rune = newItem;
        image.sprite = newItem.InSlot;
        index = transform.parent.GetComponent<InventorySlot>().slotIndex;
    }

    public void SetIndex(int nextIndex){
        this.index = nextIndex;
    }

    public void OnPointerEnter(PointerEventData eventData){
        Debug.Log("Enter");
        newDescriptor = Instantiate(itemDescriptor, transform);
        newDescriptor.GetComponent<InventoryDescriptor>().SetUp(rune);
    }

    public void OnPointerExit(PointerEventData eventData){
        Debug.Log("Exit");
        newDescriptor.GetComponent<InventoryDescriptor>().End();
    }
    public void OnBeginDrag(PointerEventData eventData){
        image.raycastTarget = false;
        image.rectTransform.localScale = new Vector3(1,1,1);
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData){
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData){
        image.raycastTarget = true;
        parentPrev = parentAfterDrag;
        transform.SetParent(parentAfterDrag);
        if(parentAfterDrag == parentPrev && parentAfterDrag.GetComponent<InventorySlot>().isRune){
            image.rectTransform.localScale = new Vector3(2,2,2);
        }
    }
}