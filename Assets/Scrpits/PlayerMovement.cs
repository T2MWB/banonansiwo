using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float Geschw = 5f;

    public Rigidbody2D rb;
    public Animator animation;

    Vector2 bewegung;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bewegung.x = Input.GetAxisRaw("Horizontal");
        bewegung.y = Input.GetAxisRaw("Vertical");

        animation.SetFloat("Vertikal", bewegung.y);
        animation.SetFloat("Horizontal", bewegung.x);
        animation.SetFloat("Geschwindigkeit", bewegung.sqrMagnitude);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + bewegung.normalized * Geschw * Time.fixedDeltaTime);
    }
}
