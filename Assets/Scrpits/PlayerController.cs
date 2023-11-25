//This is not used anymore, I don't know why we have it in our project

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D myRB; 

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
