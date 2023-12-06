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

    public GameObject xpPrefab;
    public TextMeshPro indicator;
    //This doesnt work (shows no text)
    //public GameObject DamageIndicator;

    // Update is called once per frame
    void Start()
    {

    }
    
    public int ShowHealth()
    {
        return health;
    }
    //M�glichkeit der Entit�t von ausser Health ab zu ziehen
    public void Damage(int amount,Color color)
    {
        if(transform.CompareTag("Player")){
            
            float percHit = (float) amount/ (float)MAX_HEALTH;
            //Debug.Log(amount+" , "+MAX_HEALTH+" | "+percHit);
            transform.GetComponent<PlayerMovement>().BeHit(percHit);
            
        }
        //Does all the showy shit now
        int current_damage;
        int.TryParse(indicator.text, out current_damage);
        var spawnOffset = new Vector3(Random.Range(-0.3f,0.3f),Random.Range(-0.1f,0.5f),0f);
        Debug.Log(spawnOffset);
        indicator.transform.position = new Vector3(transform.position.x + spawnOffset.x,transform.position.y + spawnOffset.y,indicator.transform.position.z);
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
        //WILL REPLACE
        /*var spawnOffset = new Vector3(Random.Range(-0.3f,0.3f),Random.Range(-0.1f,0.5f),0f);
        GameObject newDMG = Instantiate(DamageIndicator, transform.position+spawnOffset, Quaternion.identity, transform);
        //Debug.Log(transform.GetChild(2).name);
        //newDMG.transform.parent = transform.GetChild(3);
        newDMG.GetComponent<DamageIndicator>().SetUp(amount.ToString(),color,spawnOffset);
        //newDMG.transform.parent = .transform;*/
        /* This will be the health indicator for the hero, rn it's showing alot of weird stuff though
        else{
            current_damage = health - current_damage;
            indicator.text = current_damage.ToString();
        }*/
        if(amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("(negativer schaden) Bruder du heilst dich mit Damage, falsche Methode");
            return;
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
    public void GetHealth(out int health,out int MAX_HEALTH){
        health = this.health;
        MAX_HEALTH = this.MAX_HEALTH;
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
        if(this.CompareTag("Player")){
            transform.GetComponent<PlayerMovement>().Die();
        }
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
            int xpAmount = MAX_HEALTH/10;
            for(int i=0;i<xpAmount;i++){
                var spawnOffset = new Vector3(Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f),0);
                GameObject newXP = Instantiate(xpPrefab, transform.position+spawnOffset, Quaternion.identity, transform);
                newXP.GetComponent<XPOrb>().SetUp(10);
                newXP.transform.parent = transform.parent;
            }
            //StartCoroutine(DieDelay());
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
