/*
Erstellung: ???
Authoren: Tammo Wiebe
Nutzen: Hauptskript für den Spieler(Player). Die wichtigsten Funktionen des Spielers sind hier vertreten.
Änderungen:
3.6.2023, Tammo Wiebe - beschreibender Programmkopf hinzugefügt, "Hit" Animation auf den Gegner ausgelagert
8.6.2023, Tammo Wiebe - Blickrichtung wird jetzt basierend auf Mausposition mit setOrientation() festgestellt
22.6.2023, Tammo Wiebe - vollendung von Blickrichtung basierend auf Mausposition
*/
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
    Vector2 playerPos;

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
        playerPos.Set(Screen.width/2,Screen.height/2);        //This is really risky because it will not update for screen resizing in start
    }

    // Update is called once per frame
    void Update()
    {
        bewegung.x = Input.GetAxisRaw("Horizontal");
        bewegung.y = Input.GetAxisRaw("Vertical");

        
        setOrientation();
        //animation.SetFloat("Vertikal", bewegung.y);
        //animation.SetFloat("Horizontal", bewegung.x);
        animation.SetFloat("Geschwindigkeit", bewegung.sqrMagnitude);
        
        if (Input.GetButtonDown("Fire1")){
            Attacke();
        }
    }

    private void setOrientation(){
        Vector2 mousePos = Input.mousePosition;
        Vector2 distance = mousePos - playerPos;
        distance.Normalize();
        //Vector2 Vplayer = transform.position;
        //Vector2 Vplayer = GetComponent<Camera>().WorldToScreenPoint(transform.position);

        //Debug.Log("Distance: " + distance);
        
        animation.SetFloat("Horizontal", distance.x);
        animation.SetFloat("Vertikal", distance.y);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + bewegung.normalized * Geschw * Time.fixedDeltaTime);
    }

    public void changeWeapon(int damage, float range){
        this.damage = damage;
        this.range = range;
    }
    private void Attacke()
    {
        animation.Play("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, range, enemyLayer);
        
        foreach(Collider2D enemy in hitEnemies)
        {
            if(enemy is BoxCollider2D){
            /*enemy.GetComponent<Enemy>().GetComponent<Animator>().Play("Hit");
            enemy.GetComponent<Health>().Damage(damage);*/
                enemy.GetComponent<Enemy>().BeHit(damage);
                Debug.Log(enemy.name + " Ist getroffen");
            }
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
