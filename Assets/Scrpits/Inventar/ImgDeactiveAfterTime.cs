using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImgDeactiveAfterTime : MonoBehaviour
{
    private float spawnTime;
    private float lifeTime;
    // Start is called before the first frame update
    public void Setup(float time)
    {
        spawnTime = Time.time;
        lifeTime = time;
    }

    // Update is called once per frame
    void Update()
    {
        if((Time.time-spawnTime)>=lifeTime){
            gameObject.SetActive(false);
        }
    }
}
