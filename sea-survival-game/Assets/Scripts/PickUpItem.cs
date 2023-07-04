using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
   //Ez itt a pickup system de a fejemben müködik egyenlore csak
   Transform player;
    [SerializeField] float speed = 5f; // ezt meg lehet valtoztatom de lehet jo
    [SerializeField] float pickUpDistance = 1.15f; // Ez szerintem jol be van love
    [SerializeField] float TimeToLeave = 10f; //mennyi ido alatt tunik el a cucc a foldrol item despawn time roviden
    private void Awake()
    {
        player = GameManagerSingleton.Instance.player.transform;
    }
    private void Update()
    {
        TimeToLeave -= Time.deltaTime;
        if( TimeToLeave < 0 )
        {
        Destroy(gameObject);
        }
        float distance = Vector3.Distance(transform.position, player.position); //object es player kozotti distance
        if (distance > pickUpDistance)
        {
            return; // ha nagyobb a distance nem veszi fel csak vissza kuldi
        }
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        if (distance < 0.1f)
        { 
         Destroy(gameObject);
        }
    }

}
