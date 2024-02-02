using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private GameObject spike;
    [SerializeField] private GameObject laser;
    void Start()
    {
        player = GameManagerSingleton.Instance.player;
        StartCoroutine(SpikeAttack());
    }
    IEnumerator SpikeAttack(){
        yield return new WaitForSeconds(5);
        for(int i=0; i<10; i++){
            GameObject spikeattack = Instantiate(this.spike, player.transform.position, new Quaternion(0,0,0,1));
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(LaserAttack());
    }
    IEnumerator LaserAttack(){
        yield return new WaitForSeconds(5);
        for(int i=0; i<5; i++){
            GameObject laserattack = Instantiate(this.laser, this.gameObject.transform.position, new Quaternion(0,0,0,1));
            Vector2 attack = player.transform.position - gameObject.transform.position;
            attack.Normalize();
            laserattack.gameObject.GetComponent<Rigidbody2D>().velocity=attack*10;
            yield return new WaitForSeconds(3);
            Destroy(laserattack);
        }
        StartCoroutine(SpikeAttack());
    }
}
