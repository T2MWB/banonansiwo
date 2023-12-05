using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDescriptor : MonoBehaviour
{
    public GameObject header;
    public GameObject description;
    public Image image;
    public Transform parentAfterDrag;
    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
        if (Input.GetKeyDown("tab")){End();}
        image.rectTransform.localScale = new Vector3(1,1,1);
        
    }
    public void SetUp(Rune rune){
        transform.position = Input.mousePosition;
        image.rectTransform.localScale = new Vector3(1,1,1);
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        if(rune.level>0){header.GetComponent<Text>().text = rune.name+" Rune Lvl."+rune.level;}
        else{header.GetComponent<Text>().text = rune.name+" Rune";}
        description.GetComponent<Text>().text = rune.description;
    }
    public void End(){
        Destroy(gameObject);
    }
}
