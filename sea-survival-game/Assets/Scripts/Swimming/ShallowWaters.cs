using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShallowWaters : MonoBehaviour
{
    [SerializeField] CharacterController2D characterController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered the shallowWaters!");
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

    }
}
