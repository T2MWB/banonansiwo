using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Bewegung
    public float Geschw = 5f;

    public Rigidbody2D rb;
    public Animator animation;
    Vector2 bewegung;

    //Angriff
    public WeaponData weapon;
    public Collider2D radius;

    private int damage = 5;

    public Transform attackPoint;
    public float range = 1f;
    public LayerMask enemyLayer;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(radius.GetType().ToString());
        //importiere die werte vom ScriptableObject
        damage = weapon.damage;
        range = weapon.range;
    }

    // Update is called once per frame
    void Update()
    {
        bewegung.x = Input.GetAxisRaw("Horizontal");
        bewegung.y = Input.GetAxisRaw("Vertical");

        animation.SetFloat("Vertikal", bewegung.y);
        animation.SetFloat("Horizontal", bewegung.x);
        animation.SetFloat("Geschwindigkeit", bewegung.sqrMagnitude);
        
        if (Input.GetButtonDown("Fire1")){
            Attacke();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + bewegung.normalized * Geschw * Time.fixedDeltaTime);
    }

    private void Attacke()
    {
        animation.Play("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, range, enemyLayer);
        
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().GetComponent<Animator>().Play("Hit");
            enemy.GetComponent<Health>().Damage(damage);
            Debug.Log(enemy.name + " Ist getroffen");
        }
        
        /*if (collider.CompareTag("Enemy")){
            Debug.Log("Tag ist Enemy");
            if (collider.GetComponent<Health>() != null)
            {
                Debug.Log("Attacke bet�tigt");
                collider.GetComponent<Health>().Damage(damage);
                Debug.Log(this.GetComponent<Health>().ShowHealth());
            }
        }*/
    }
    /*void OnDrawGizmoSelected()
    {
        if(attackPoint == null)
        {
            Debug.Log("AttackPoint = 0");
            return;
        }

        Gizmos.color = new Color(1, 1, 0, 0.75f);
        Gizmos.DrawWireSphere(attackPoint.position, range);
    }*/
}
