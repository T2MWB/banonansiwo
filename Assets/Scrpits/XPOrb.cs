using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPOrb : MonoBehaviour
{
    public int xp;
    public SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetUp(int xp){
        this.xp = xp;
        float scale = (xp/10)*0.25f;
        sprite.size = new Vector3(scale,scale,scale);
    }

    public void OnTriggerEnter2D(Collider2D collider){
        if(!(collider.CompareTag("Enemy"))){
        collider.GetComponent<PlayerMovement>().AddXP(xp);
        Destroy(gameObject);
        }
    }
}
