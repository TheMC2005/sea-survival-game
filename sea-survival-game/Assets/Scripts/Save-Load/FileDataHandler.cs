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

    public GameData Load(string profileID)
    {
        //ha a profileid null
        if(profileID == null)
        {
            Debug.LogWarning("A profileid null");
            return null;
        }
        string fullPath = Path.Combine(dataDirPath, profileID, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
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
                JsonSerializerSettings jsonSettings = new JsonSerializerSettings();
                jsonSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                jsonSettings.TypeNameHandling = TypeNameHandling.Auto;
                jsonSettings.MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead;
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

    public void Save(GameData data, string profileID)
    {
        //ha a profileid null
        if (profileID == null)
        {
            Debug.LogWarning("A profileid null");
            return;
        }
        string fullPath = Path.Combine(dataDirPath,profileID,dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            JsonSerializerSettings jsonSettings = new JsonSerializerSettings();
            jsonSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonSettings.Converters.Add(new DictionaryVector2IntJsonConverter());
            jsonSettings.Converters.Add(new TileConverter());
            jsonSettings.Converters.Add(new GameObjectConverter());
            string dataToStore = JsonConvert.SerializeObject(data, Formatting.Indented, jsonSettings);
            if (useEncryption)
            {
                dataToStore = Encrypt(dataToStore);
            }
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
    public Dictionary<string, GameData> LoadAllProfiles()
    {
        Dictionary<string, GameData> profileDictionary = new Dictionary<string, GameData>();

        // loop over all directory names in the data directory path
        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(dataDirPath).EnumerateDirectories();
        foreach (DirectoryInfo dirInfo in dirInfos)
        {
            string profileId = dirInfo.Name;

            string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
            if (!File.Exists(fullPath))
            {
                Debug.LogWarning("Skipping directory when loading all profiles because it does not contain data: "
                    + profileId);
                continue;
            }

            GameData profileData = Load(profileId);

            if (profileData != null)
            {
                profileDictionary.Add(profileId, profileData);
            }
            else
            {
                Debug.LogError("Tried to load profile but something went wrong. ProfileId: " + profileId);
            }
        }

        return profileDictionary;
    }

    public string GetMostRecentlyUpdatedProfileId()
    {
        string mostRecentProfileId = null;

        Dictionary<string, GameData> profilesGameData = LoadAllProfiles();
        foreach (KeyValuePair<string, GameData> pair in profilesGameData)
        {
            string profileId = pair.Key;
            GameData gameData = pair.Value;

            if (gameData == null)
            {
                continue;
            }

            if (mostRecentProfileId == null)
            {
                mostRecentProfileId = profileId;
            }
            else
            {
                DateTime mostRecentDateTime = DateTime.FromBinary(profilesGameData[mostRecentProfileId].lastUpdated);
                DateTime newDateTime = DateTime.FromBinary(gameData.lastUpdated);
                if (newDateTime > mostRecentDateTime)
                {
                    mostRecentProfileId = profileId;
                }
            }
        }
        return mostRecentProfileId;
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

