using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageIndicator : MonoBehaviour
{
    public TextMeshPro text;
    public int deathLimit = 1;
    public float spawnTime;
    //public GameObject host;
    public Vector3 spawnOffset;

void Update(){
    //Debug.Log(Time.time+" | "+spawnTime+" | "+(Time.time-spawnTime));
    /*if(deathLimit >= Time.time-spawnTime){
        Destroy(gameObject);
    }*/
    //gameObject.transform.position = host.transform.position + spawnOffset;

}
public void SetUp(string input,Color color,Vector3 offset){
    spawnTime = Time.time;
    //host = transform.parent;
    this.spawnOffset = offset;
    Debug.Log("My Position"+transform.position);
    Debug.Log("Offset"+spawnOffset);
    transform.position = new Vector3(transform.position.x+spawnOffset.x,transform.position.y+spawnOffset.y,0.14f);
    text.text = input;
    text.color = color;
    
    spawnOffset = offset;


}
}
