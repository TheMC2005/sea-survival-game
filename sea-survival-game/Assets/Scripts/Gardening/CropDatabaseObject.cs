using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using static UnityEditor.Progress;
[CreateAssetMenu(fileName = "NewCropDatabase", menuName = "Database/Create New Crop Database")]
public class CropDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public Crop[] crops;
public Dictionary<Crop, int> GetID = new Dictionary<Crop, int>();
public Dictionary<int, Crop> GetCrop = new Dictionary<int, Crop>();
public void OnAfterDeserialize()
{
    GetID = new Dictionary<Crop, int>();
    GetCrop = new Dictionary<int, Crop>();
    for (int i = 0; i < crops.Length; i++)
    {
        GetID.Add(crops[i], i);
        GetCrop.Add(i, crops[i]);
    }
}
public void OnBeforeSerialize() { }
}
