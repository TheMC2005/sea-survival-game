using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craftingtable : MonoBehaviour
{
    void Update()
    {
        // Check if the left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            // Check if the ray hits any collider
            if (hit.collider != null)
            {
                // Check if the collider belongs to the game object you want to detect clicks on
                if (hit.collider.gameObject == gameObject)
                {
                    // This object has been clicked
                    GameManagerSingleton.Instance.OpenMenu();

                    // Here you can perform any actions you want when the object is clicked
                }
            }
        }
    }
}
