using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class zambi : MonoBehaviour
{
    GameObject player;
    PlayerHP playerhp;
    [SerializeField] private float speed;
    [SerializeField] private float wanderingspeed;
    [SerializeField] private float eyesight;
    [SerializeField] private float attackRange;
    [SerializeField] private int damage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackReadyTime;
    private bool seeplayer;
    private int angle;
    private Vector3 muve;
    private Rigidbody2D rb2D;
    private float timer;
    void Start()
    {
        seeplayer=false;
        angle=UnityEngine.Random.Range(0,359);
        player = GameManagerSingleton.Instance.player;
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        playerhp = player.GetComponent<PlayerHP>();
        StartCoroutine(DetectPlayer());
        StartCoroutine(Wondering());
    }

    void Update()
    {
        if(seeplayer){
            Vector2 attack = player.transform.position - gameObject.transform.position;
            attack.Normalize();
            rb2D.velocity = attack*speed;
            if(timer>attackSpeed && Vector2.Distance(gameObject.transform.position, player.transform.position)<attackRange){
                StartCoroutine(DamagePlayer());
                timer = 0;
            }
        }
        else{rb2D.velocity = muve*wanderingspeed;}
        if(timer<attackSpeed){
            timer += Time.deltaTime;
        }
    }

    IEnumerator DetectPlayer(){
        while(true){
            float d=Vector2.Distance(gameObject.transform.position, player.transform.position);
            if (d<=eyesight){seeplayer=true;}
            else{seeplayer=false;}
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator Wondering(){
        while(true){
            int d = UnityEngine.Random.Range(-45,45);
            angle += d;
            double a = (angle*Math.PI)/180;
            double x = Math.Sin(a);
            double y = Math.Cos(a);
            muve = new Vector3((float)x,(float)y, 0);
            yield return new WaitForSeconds(1);
        }
    }
    IEnumerator DamagePlayer(){
        yield return new WaitForSeconds(attackReadyTime);
        if(Vector2.Distance(gameObject.transform.position, player.transform.position)<attackRange){
            playerhp.DealDamage(damage);
            Debug.Log("hit");
        }
    }
}
