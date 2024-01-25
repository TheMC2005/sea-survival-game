using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShallowWaters : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered the shallowWaters!");
            GameManagerSingleton.Instance.isSwimming = false;
            GameManagerSingleton.Instance.inShallow = true;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player exited the shallowWaters!");
            GameManagerSingleton.Instance.inShallow = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Debug.Log("Ez van");
        // GameManagerSingleton.Instance.inShallow = true;
        // GameManagerSingleton.Instance.isSwimming = false; azert vettem ki mert bajok voltak amikor hajoztam kozel a parthoz
    }
}
