using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zambi : MonoBehaviour
{
    GameObject player;
    [SerializeField] private float speed;
    [SerializeField] private float eyesight;
    private bool seeplayer;
    void Start()
    {
        seeplayer=false;
        player = GameManagerSingleton.Instance.player;
        StartCoroutine(DetectPlayer());
    }

    void Update()
    {
        if(seeplayer){
            Debug.Log("fasz");
        }
    }

    IEnumerator DetectPlayer(){
        float d=Vector2.Distance(gameObject.transform.position, player.transform.position);
        if (d<=eyesight){seeplayer=true;}
        else{seeplayer=false;}
        yield return new WaitForSeconds(1);
        StartCoroutine(DetectPlayer());
    }
}
