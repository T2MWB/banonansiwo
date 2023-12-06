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
    //Stats
    public ImgStatSlider levelBar;
    public ImgStatSlider staminaBar;
    public ImgStatSlider healthBar;
    public ImgStatSlider coolBar;
    private float playerXP = 0;
    private int level = 1;
    private float stamina = 1f;
    private float cooldownProgress;
    public GameObject gameOver;
    public GameObject levelUp;
    //Remove this after Presentation
    public GameObject Boss;

    //Bewegung
    public float Geschw = 5f;

    public Rigidbody2D rb;
    public Animator animation;
    Vector2 bewegung;
    Vector2 playerPos;

    //Angriff
    public Weapon weapondata;
    public Collider2D radius;

    private int damage = 5;
    public Color defaultColor;

    public Transform attackPoint;
    private float range = 1f;
    private float handling = 0.1f;
    private float wpnspeed = 1f;
    private float animspeed = 1f;
    private string weaponname;
    public LayerMask enemyLayer;
    
    //weiteres
    public GameObject UI; 
    private float lastAtk;
    public Rune[] runes = new Rune[2];

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(radius.GetType().ToString());
        //importiere die werte vom ScriptableObject
        damage = weapondata.damage;
        range = weapondata.range;
        wpnspeed = weapondata.speed;
        handling = weapondata.handling;
        animspeed = weapondata.animspeed;
        weaponname = weapondata.name;

        levelBar.SetSlider(0);
        healthBar.SetSlider(1);
        staminaBar.SetSlider(1);
        coolBar.SetSlider(0);
        //Debug.Log("Width "+Screen.width+" | Height "+Screen.height);
        playerPos.Set(Screen.width/2,Screen.height/2);        //This is really risky because it will not update for screen resizing in start
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("tab")){
            UI.GetComponent<Inventory>().OnInventoryToggle();
        }
        //wenn am attackieren dann wir langsamer
        //if(Time.time-lastAtk<wpnspeed){Geschw = 1f;}else{Geschw=5f;}
        if(stamina <= 1){
        if((Time.time-lastAtk)>(wpnspeed+1)){stamina = stamina + (1.0f / 6 * Time.deltaTime);}
        else{stamina = stamina + (1.0f / 20 * Time.deltaTime);}}
        staminaBar.SetSlider((stamina));
        if((wpnspeed-(Time.time-lastAtk))>=0){cooldownProgress = (wpnspeed-(Time.time-lastAtk))/wpnspeed;}else{cooldownProgress=0;}
        if((Time.time-lastAtk <= wpnspeed)){
            if(cooldownProgress<=0.05f){coolBar.SetSlider(0);}
            else{coolBar.SetSlider(cooldownProgress);}
            }//:coolBar.SetSlider((Time.time-lastAtk)/wpnspeed);}
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

    public void changeWeapon(int damage, float range, float handling,float speed, float animspeed, string name){
        this.damage = damage;
        this.range = range;
        this.wpnspeed = speed;
        this.handling = handling;
        this.weaponname = name;
        this.animspeed = animspeed;
    }
    private void Attacke()
    {
        Debug.Log(Time.time-lastAtk+" | "+wpnspeed+" | "+animspeed);
        if(Time.time-lastAtk<wpnspeed || (stamina - handling) <= 0){return;}
        lastAtk = Time.time;
        Debug.Log(handling);
        stamina = stamina - handling;
        animation.speed = animspeed;
        animation.Play("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, range, enemyLayer);
        
        foreach(Collider2D enemy in hitEnemies)
        {
            if(enemy is BoxCollider2D){
            /*enemy.GetComponent<Enemy>().GetComponent<Animator>().Play("Hit");
            enemy.GetComponent<Health>().Damage(damage);*/
                enemy.GetComponent<Enemy>().BeHit(damage,defaultColor);
                //Debug.Log(enemy.name + " Ist getroffen");
            }
        }
    }

    public void AddXP(int xp){
        //Initialisation
        this.playerXP += xp;
        float maxXP = level*0.9f*100;
        float percXP = playerXP/maxXP;
        //Level-Up
        if(playerXP>=((maxXP))){
            int savedXP = Mathf.RoundToInt(playerXP-maxXP);
            
            playerXP = 0;
            level++;
            levelBar.SetSlider(0);
            Debug.Log("Level-Up!"+level);
            if(level == 5){
                Boss.SetActive(true);
                Boss.GetComponent<Enemy>().BossSetup();
            }
            levelUp.SetActive(true);
            levelUp.GetComponent<ImgDeactiveAfterTime>().Setup(2);
            Debug.Log("Saved "+savedXP);
            if(savedXP == 0){levelBar.SetSlider(0);Debug.Log(savedXP == 0);}
            else{AddXP(savedXP);}
            return;
        }
        //Debug.Log("MaxXP "+maxXP+" | added % "+percXP);
        levelBar.SetSlider(percXP);
        //Debug.Log("PlayerXP: "+playerXP);
    }
    public void BeHit(float amount){
        //Debug.Log("Decreasing by "+amount);
        healthBar.DecSlider(amount);
    }

    public void Die(){
        
        if(!gameOver.activeSelf){
            gameOver.SetActive(true);
        }
        Time.timeScale = 0f;
    }
}
