using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //default variablen falls es kein EnemyData script dafür gibt
    [SerializeField]
    private int damage = 5;
    [SerializeField]
    private float speed = 0.5f;

    [SerializeField]
    private EnemyData data;

    private GameObject player;

    public Animator animator;

    public bool move = true;
    Vector2 bewegung;

    //Checke was der Spieler ist und setze die gewählten Werte
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SetEnemyValues();
    }

    void Update()
    {
        //Folgen();
        bewegung = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        Anim(bewegung);
        if (move == true)
        {
            transform.position = bewegung;
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
        
        Vector2 direction = this.transform.position - player.transform.position;
        animator.SetFloat("Horizontal", direction.x);
        //Debug.Log(this.gameObject.name+": "+direction.x+" | "+this.transform.position);
        animator.SetFloat("Geschwindigkeit", bwg.sqrMagnitude);
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && (collider.GetType() == typeof(BoxCollider2D)))
        {
            if(collider.GetComponent<Health>() != null)
            {
                Debug.Log(this.gameObject.name + " | " + damage);
                collider.GetComponent<Health>().Damage(damage);
                //this.GetComponent<Health>().Damage(10000);
            }
        }
    }
}
