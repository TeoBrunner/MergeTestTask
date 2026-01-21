using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chip Type Database", menuName = "Configs/Chip/Chip Type Database")]
public class ChipTypeDatabase : ScriptableObject
{
    [SerializeField] List<ChipType> chipTypes;
    public ChipType GetRandom()
    {
        if(chipTypes == null || chipTypes.Count == 0)
        {
            Debug.LogError("ChipTypeDatabase: No chip types available.");
            return null;
        }
        int index = Random.Range(0, chipTypes.Count);
        return chipTypes[index];
    }
    public ChipType GetByID(string id)
    {
        ChipType chipType = chipTypes.Find(chipType => chipType.ID == id);
        if(chipType == null)
        {
            Debug.LogError($"ChipTypeDatabase: No chip type found with ID {id}.");
        }
        return chipType;
    }
}
