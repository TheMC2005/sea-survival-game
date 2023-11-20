using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class ChunkLoader : MonoBehaviour
{
    [SerializeField] private List<GameObject> islands;
    [SerializeField] private CharacterController2D characterController;
    [SerializeField] private List<Transform> islandPosition;
    float distanceLimitSquared = 65f*65f;
    private Transform playerTransform;

    private IEnumerator UpdateIslandsCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f); // hány secenként updatelje

        while (true)
        {
            if (characterController.moving)
            {
                UpdateIslands();
            }

            yield return wait;
        }
    }

    private void UpdateIslands()
    {
        Vector3 playerPosition = playerTransform.position;
        for (int i = 0; i < islands.Count; i++)
        {
            float distanceSquared = (playerPosition - islandPosition[i].position).sqrMagnitude;
            bool shouldBeActive = distanceSquared < distanceLimitSquared;
            islands[i].SetActive(shouldBeActive);
        }
    }

    private void Start()
    {
        playerTransform = GameManagerSingleton.Instance.player.transform;
        StartCoroutine(UpdateIslandsCoroutine());
    }
}
