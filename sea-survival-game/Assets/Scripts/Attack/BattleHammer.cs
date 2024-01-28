using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHammer : MonoBehaviour
{
    GameObject player;
    CharacterController2D cc2d;
    ToolsPlayerController tpc;

    void Start()
    {
        player = GameManagerSingleton.Instance.player;
        cc2d = player.GetComponent<CharacterController2D>();
        tpc = player.GetComponent<ToolsPlayerController>();
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.velocity=new Vector3(0,0,0);
        cc2d.enabled=false;
        tpc.enabled=false;

        Collider2D coll = GetComponent<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D().NoFilter();
        List<Collider2D> results = new List<Collider2D>();
        int n = Physics2D.OverlapCollider(coll, filter, results);

        for(int i=0; i<n; i++){
            if(results[i].gameObject.tag=="enemy"){
                results[i].gameObject.GetComponent<Hitpoints>().hp-=5;
            }
        }

        StartCoroutine(End());
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(1);
        cc2d.enabled=true;
        tpc.enabled=true;
        Destroy(this.gameObject);
    }
}
