using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class ChuckLoader : MonoBehaviour
{
    [SerializeField] public List<GameObject> islands;
    public CharacterController2D characterController;
    public readonly List<Vector3> islandCoordinates = new List<Vector3>();
    private Transform playerTransform;

    void Start()
    {
        foreach (GameObject island in islands)
        {
            islandCoordinates.Add(island.transform.position); //gyorsabb caching miatt
        }
        playerTransform = GameManagerSingleton.Instance.player.transform;
    }

    void Update()
    {
        Vector3 playerPosition = playerTransform.position;
        if(characterController.moving == true) // csak akkor számoljon ha mozog a karakter ne legyen extra számitás
        { 
           for (int i = 0; i < islands.Count; i++)
           {
            float distance = Vector3.Distance(playerPosition, islandCoordinates[i]); // kiszámolja a karakter vs. islandek távolságát
            bool shouldBeActive = distance < 65f;
            islands[i].SetActive(shouldBeActive);
           }
        }
    }
}
