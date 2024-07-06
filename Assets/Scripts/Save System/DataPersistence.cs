using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistance : MonoBehaviour
{
    [Header("File Config name")]
    [SerializeField] private string fileName;

    [Header("Debugging")]
    [SerializeField] private bool disableDataPersistence = false;
    [SerializeField] private bool initializeDataIfNull = false;

    public static DataPersistance Instance;
    private FileDataHandler fileDataHandler;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("Check DataPersistence Singleton");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);


        if (disableDataPersistence)
        {
            Debug.Log("Save system is currently disabled");
        }

        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
       SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        Debug.Log("New Data");
        this.gameData = new GameData();
        foreach (IDataPersistence persistence in dataPersistenceObjects)
        {
            persistence.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        if (disableDataPersistence)
        {
            return;
        }

        if (this.gameData == null)
        {
            Debug.Log("No Initial Game Data Found");
            return;
        }

        this.dataPersistenceObjects = FindAllDataPersistenceObjects();

        foreach (IDataPersistence persistence in dataPersistenceObjects)
        {
            persistence.SavaData(gameData);
        }
        fileDataHandler.Save(gameData);
        Debug.Log("Saved Data");
    }

    public void LoadGame()
    {
        if (disableDataPersistence)
        {
            return;
        }

        this.gameData = fileDataHandler.Load();

        if(this.gameData == null && initializeDataIfNull)
        {
            NewGame();
        }

        if(this.gameData == null)
        {
            Debug.Log("No Data to load");
            return;
        }

        foreach (IDataPersistence persistence in dataPersistenceObjects)
        {
            persistence.LoadData(gameData);
        }
        Debug.Log("Loaded Data");
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true).OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
