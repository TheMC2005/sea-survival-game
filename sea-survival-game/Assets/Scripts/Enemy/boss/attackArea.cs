using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackArea : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private Sprite spike;
    private SpriteRenderer sr;
    void Start()
    {
        StartCoroutine(End());
        sr=GetComponent<SpriteRenderer>();
    }
    IEnumerator End(){
        yield return new WaitForSeconds(1);
        Debug.Log("fasz");
        Collider2D coll = GetComponent<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D().NoFilter();
        List<Collider2D> results = new List<Collider2D>();
        int n = Physics2D.OverlapCollider(coll, filter, results);

        sr.sprite = spike;

        for(int i=0; i<n; i++){
            if(results[i].gameObject.tag=="Player"){
                results[i].gameObject.GetComponent<PlayerHP>().DealDamage(damage);
            }
        }

        yield return new WaitForSeconds((float)0.5);
        Destroy(this.gameObject);
    }
}
