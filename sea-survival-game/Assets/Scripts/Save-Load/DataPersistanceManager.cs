using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataPersistanceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;
    [Header("Movement to Save")]
    public bool readyToSave;


    [Header("Debug")]
    [SerializeField] private bool initalizeDataIfNull = false;
    [SerializeField] private bool disableDataPersistance = false;
    [SerializeField] private bool overrideSelectedProfileID = false;
    [SerializeField] private string overwrittenProfileID;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    public string selectedProfileID = "";
    public static DataPersistanceManager instance { get; private set; }
    
    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("Found more than one Data Persistance Manager in the scene.Destroying the newest one");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        if(disableDataPersistance)
        {
            Debug.LogWarning("Datapersistance is disabled");
        }
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        this.selectedProfileID = dataHandler.GetMostRecentlyUpdatedProfileId();
        if(overrideSelectedProfileID)
        {
            this.selectedProfileID = overwrittenProfileID;
            Debug.LogWarning("Overrode the selected profileid to this:" + overwrittenProfileID);
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnLoaded;
    }

    public void OnSceneLoaded(Scene scene,LoadSceneMode mode)
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }
    public void OnSceneUnLoaded(Scene scene)
    {
        SaveGame();
    }

    public void ChangeSelectedProfileID(string newprofileID)
    {
        this.selectedProfileID = newprofileID;
        LoadGame();
    }
    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        if (disableDataPersistance) 
        { 
            return;
        }
        this.gameData = dataHandler.Load(selectedProfileID);

        if(this.gameData == null && initalizeDataIfNull)
        {
            NewGame();
        }
        if (this.gameData == null)
        {
            Debug.Log("No data was found. New Game Needs to be created!");
            return;
        }
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        if (disableDataPersistance)
        {
            return;
        }
        if (this.gameData == null)
        {
            Debug.LogWarning("No data found. New game needs to be started before data can be saved");
            return;
        }
        // pass the data to other scripts so they can update it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            if (dataPersistenceObj.GetType() == typeof(CharacterController2D) && readyToSave)
            {
                dataPersistenceObj.SaveData(gameData);
                readyToSave = false;
            }
            if (!(dataPersistenceObj.GetType() == typeof(CharacterController2D)))
            {
                dataPersistenceObj.SaveData(gameData);
            }
        }
        gameData.lastUpdated = System.DateTime.Now.ToBinary();
        dataHandler.Save(gameData, selectedProfileID);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
    public bool HasGameData()
    {
        return this.gameData != null;
    }
    public Dictionary<string, GameData> GetAllProfilesGameData()
    {
        return dataHandler.LoadAllProfiles();
    }
}
