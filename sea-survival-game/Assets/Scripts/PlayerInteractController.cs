using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerInteractController : MonoBehaviour
{
     CharacterController2D CC2D;
    Rigidbody2D rb;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractableArea = 1.2f;
    Character character;
    private void Awake()
    {
        CC2D = GetComponent<CharacterController2D>();
        rb = GetComponent<Rigidbody2D>();
        character = GetComponent<Character>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !GameManagerSingleton.Instance.IsPaused)
        {
            Interact();
        }
    }

    private void Interact()
    {
        //ez ugyanaz a kod mint a toolhitnél megnézi, hogy van e elotte dolog es mehet az interakcio
        Vector2 position = rb.position + CC2D.LastMotionVector * offsetDistance;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);
        foreach (Collider2D collider in colliders)
        {
            Interactable hit = collider.GetComponent<Interactable>();
            if (hit != null)
            {
                hit.Interact(character);
                break;
            }
        }
    }
}
