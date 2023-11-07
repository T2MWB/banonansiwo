/*
Erstellung: ???
Authoren: Tammo Wiebe
Nutzen: Generalisierung von Lebenspunkten. Dient zur Manipulation der Lebenspunkte und bietet ein Defaultfeld an.
Änderungen:
3.6.2023, Tammo Wiebe - beschreibender Programmkopf hinzugefügt
8.6.2023, Tammo Wiebe - Schadensanzeige für Gegner eingebaut
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private bool singleHit = false;

    private int MAX_HEALTH = 100;

    public TMP_Text indicator;

    // Update is called once per frame
    void Start()
    {
        indicator.text = "";
    }
    
    public int ShowHealth()
    {
        return health;
    }
    //M�glichkeit der Entit�t von ausser Health ab zu ziehen
    public void Damage(int amount)
    {
        int current_damage;
        int.TryParse(indicator.text, out current_damage);
        if (this.CompareTag("Enemy")){
            if (singleHit){
                current_damage = amount;
                indicator.text = current_damage.ToString();
            }
            else{
                current_damage = current_damage + amount;
                indicator.text = current_damage.ToString();
            }

        }
        /* This will be the health indicator for the hero, rn it's showing alot of weird stuff though
        else{
            current_damage = health - current_damage;
            indicator.text = current_damage.ToString();
        }*/
        if(amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("(negativer schaden) Bruder du heilst dich mit Damage, falsche Methode");
        }

        this.health -= amount;

        if(health <= 0)
        {
            Die();
        }
    }

    public void SetHealth(int maxHealth, int health)
    {
        this.MAX_HEALTH = maxHealth;
        this.health = health;
    }

    //M�glichkeit der Entit�t von aussen mehr Health zu geben
    public void Heal(int amount)
    {
        if(amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("(negative heilung) So schlecht im heilen dass du dir weh tust");
        }

        bool wouldBeOverMaxHealth = health + amount > MAX_HEALTH;

        //Heile angemessen nach aktuellen HP (System um �berheilung zu verhindern)
        if (wouldBeOverMaxHealth)
        {
            this.health = MAX_HEALTH;
        }
        else
        {
            this.health += amount;
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name.ToString() + " ist gestorben!");
        //Das muss noch in eine anpassbare Referenz zu ner Methode gemacht werden damit man mit verschiedenen Toden unterschiedlich umgehen kann                         !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        /*if (this.CompareTag("Player"))
        {

        }*/
        if (this.CompareTag("Enemy"))
        {
            this.GetComponent<Enemy>().animator.SetBool("Death", true);
            this.GetComponent<Enemy>().animator.Play("Die");
            this.GetComponent<Enemy>().move = false;
            //StartCoroutine(DieDelay());
        }
        else
        {
            Heal(MAX_HEALTH);
        }
        //Destroy(gameObject);
        //this.close();
    }

    /*IEnumerator DieDelay()
    {
        yield return new WaitForSeconds(this.GetComponent<Enemy>().animator.GetComponent<Animation>().clip.length);
        Destroy(gameObject);
    }*/
}
