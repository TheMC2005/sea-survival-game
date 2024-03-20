using System.Collections;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class MineEntrance : MonoBehaviour
{
    //GO
    public GameObject mineExit;
    //public GameObject mineEntrance;
    //References
    public Volume ppv;

    public void EnterMine()
    {
        GameManagerSingleton.Instance.player.transform.position = mineExit.transform.position;
        Debug.Log("Ez történik");
    }
   /* private void Update()
    {
        UnityEngine.SceneManagement.Scene currentScene = SceneManager.GetActiveScene();
        UnityEngine.SceneManagement.Scene mainWorld = SceneManager.GetSceneByName("B-test");
        UnityEngine.SceneManagement.Scene mine = SceneManager.GetSceneByName("Mine");
        if(currentScene == mine)
        {
            ppv.weight = 0.75f;
        }
    }
    IEnumerator LoadYourAsyncScene()
    {
        // Set the current Scene to be able to unload it later
        UnityEngine.SceneManagement.Scene currentScene = SceneManager.GetActiveScene();
        UnityEngine.SceneManagement.Scene mainWorld = SceneManager.GetSceneByName("B-test");
        UnityEngine.SceneManagement.Scene mine = SceneManager.GetSceneByName("Mine");
        if(currentScene == mine)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("B-test", LoadSceneMode.Additive);

            // Wait until the last operation fully loads to return anything
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            
            SceneManager.UnloadSceneAsync(currentScene);
            ppv.weight = 0.2f;
        }
        if(currentScene == mainWorld) 
        {
            // The Application loads the Scene in the background at the same time as the current Scene.
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Mine", LoadSceneMode.Additive);

            // Wait until the last operation fully loads to return anything
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            
            // Unload the previous Scene
           // SceneManager.MoveGameObjectToScene(inventory, mine);
            SceneManager.UnloadSceneAsync(currentScene);
            
        }
       
    }
   */
}
