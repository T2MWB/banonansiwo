using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 100;

    private int MAX_HEALTH = 100;

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public int ShowHealth()
    {
        return health;
    }
    //M�glichkeit der Entit�t von ausser Health ab zu ziehen
    public void Damage(int amount)
    {
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
            this.GetComponent<Enemy>().animator.Play("Death");
            this.GetComponent<Enemy>().move = false;
            StartCoroutine(DieDelay());
        }
        else
        {
            Heal(MAX_HEALTH);
        }
        //Destroy(gameObject);
        //this.close();
    }

    IEnumerator DieDelay()
    {
        yield return new WaitForSeconds(this.GetComponent<Enemy>().animator.GetComponent<Animation>().clip.length);
        Destroy(gameObject);
    }
}
