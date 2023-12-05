/*
Erstellung: ???
Authoren: Tammo Wiebe
Nutzen: Generalisierung der Gegner. Verantwortlich für Bewegung und Attacke.
Änderungen:
3.6.2023, Tammo Wiebe - beschreibender Programmkopf hinzugefügt, Gegner "Hit" Animation löst statt vom Player nun von Enemy aus
6.6.2023, Tammo Wiebe - Verhalten für Attacken mit Animationen eingeführt
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //default variablen falls es kein EnemyData script daf�r gibt
    public Color defaultColor;
    [SerializeField]
    private int damage = 5;
    [SerializeField]
    private float speed = 0.5f;
    [SerializeField]
    private bool isDummy = false;


//4.12.2023 - Ich bereuhe das was ich hier sehe zutiefst, lasse es aber den Historikern darüber zu urteilen.
    [SerializeField]
    private EnemyData data;

    private GameObject player;

    public Animator animator;
    private SpriteRenderer rend;

    public bool move = true;
    Vector2 bewegung;

    //Checke was der Spieler ist und setze die gew�hlten Werte
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        SetEnemyValues();
    }

    void Update()
    {
        //Folgen();
        if((animator.GetBool("isAttacking"))){
            
        }
        else{
            if (!isDummy){
            bewegung = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            Anim(bewegung);
            if (move == true){transform.position = bewegung;}
            }
        }
    }

    //setze die daten von EnemyData(gesetzt im Inspector bzw <Health>.cs) ein
    private void SetEnemyValues()
    {
        GetComponent<Health>().SetHealth(data.hp, data.hp);
        damage = data.damage;
        speed = data.speed;
    }

    private void Anim(Vector2 bwg)
    {
        //bewegt sich zum spieler; setzt horizontale; schaltet bewegungsanimation an;
        Vector2 direction = this.transform.position - player.transform.position;
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Geschwindigkeit", bwg.sqrMagnitude);
        
    }

    public void BeHit(int damage,Color color)
    {
        if (!(animator.GetBool("Death"))){
            if (!(animator.GetBool("isAttacking"))){
                animator.Play("Hit");
                //rend.color = hitColor;
            }
            GetComponent<Health>().Damage(damage,color);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Attacke(collider);
    }

    private void Attacke(Collider2D collider)
    {
        if (collider.CompareTag("Player") && (collider.GetType() == typeof(BoxCollider2D)) && !animator.GetBool("Death")) //TODO: how does this work?
        {   //give player damage basically
            if(!isDummy){
                animator.Play("ATK");
                if(collider.GetComponent<Health>() != null)
                {
                    Debug.Log(this.gameObject.name + " | " + damage);
                    collider.GetComponent<Health>().Damage(damage,defaultColor);
                }
            }
        }
    }
}
