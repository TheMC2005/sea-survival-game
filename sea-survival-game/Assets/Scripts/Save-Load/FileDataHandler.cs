using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";
    private bool useEncryption = false;
    private const string KEY = "VwBDhEQbWCHSMkQNqrgx31nTWVsGhGp0OmvsCdA83rc=";
    private const string IV = "RkrDta4Rmr8xff7XdWL1OQ==";
    public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
        this.useEncryption = useEncryption;
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
                if (useEncryption)
                {
                    dataToLoad = Decrypt(dataToLoad);
                }
                // deserialize the data from Json back into the C# object
                JsonSerializerSettings jsonSettings = new JsonSerializerSettings();
                jsonSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                jsonSettings.TypeNameHandling = TypeNameHandling.Auto;
                jsonSettings.MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead;
                jsonSettings.Converters.Add(new DictionaryVector2IntJsonConverter());
                jsonSettings.Converters.Add(new TileConverter());
                jsonSettings.Converters.Add(new GameObjectConverter());
                //jsonSettings.Converters.Add(new SpriteRendererConverter());
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
            //jsonSettings.Converters.Add(new SpriteRendererConverter());
            string dataToStore = JsonConvert.SerializeObject(data, Formatting.Indented, jsonSettings);
            if (useEncryption)
            {
                dataToStore = Encrypt(dataToStore);
            }
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
    private string Encrypt(string data)
    {
        byte[] clearBytes = Encoding.Unicode.GetBytes(data);
        using Aes aes = Aes.Create();
        aes.Key = Convert.FromBase64String(KEY);
        aes.IV = Convert.FromBase64String(IV);
        using MemoryStream memoryStream = new MemoryStream();
        using CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
        cryptoStream.Write(clearBytes, 0, clearBytes.Length);
        cryptoStream.Close();
        byte[] encryptedBytes = memoryStream.ToArray();
        return Convert.ToBase64String(encryptedBytes);
    }

    private string Decrypt(string data)
    {
        byte[] cipherBytes = Convert.FromBase64String(data);
        using Aes aes = Aes.Create();
        aes.Key = Convert.FromBase64String(KEY);
        aes.IV = Convert.FromBase64String(IV);  
        using MemoryStream memoryStream = new MemoryStream();
        using CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
        cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);
        cryptoStream.Close();
        byte[] decryptedBytes = memoryStream.ToArray();
        return Encoding.Unicode.GetString(decryptedBytes);
    }
}

