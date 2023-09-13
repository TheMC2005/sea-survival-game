using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";       

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        // use Path.Combine to account for different OS's having different path separators
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                // load the serialized data from the file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                // deserialize the data from Json back into the C# object
                JsonSerializerSettings jsonSettings = new JsonSerializerSettings();
                jsonSettings.Converters.Add(new DictionaryVector2IntJsonConverter());
                jsonSettings.Converters.Add(new TileConverter());
                jsonSettings.Converters.Add(new GameObjectConverter());
                loadedData = JsonConvert.DeserializeObject<GameData>(dataToLoad, jsonSettings);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }

    public void Save(GameData data)
    {
        // use Path.Combine to account for different OS's having different path separators
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            // create the directory the file will be written to if it doesn't already exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // serialize the C# game data object into Json
            JsonSerializerSettings jsonSettings = new JsonSerializerSettings();
            jsonSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonSettings.Converters.Add(new DictionaryVector2IntJsonConverter());
            jsonSettings.Converters.Add(new TileConverter());
            jsonSettings.Converters.Add(new GameObjectConverter());
            string dataToStore = JsonConvert.SerializeObject(data, Formatting.Indented, jsonSettings);
            // write the serialized data to the file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }
    }
}

