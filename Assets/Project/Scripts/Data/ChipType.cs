using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chip Type", menuName = "Configs/Chip/Chip Type")]
public class ChipType : ScriptableObject
{
    [SerializeField] string id;
    [SerializeField] Sprite sprite;

    public string ID => id;
    public Sprite Sprite => sprite;
}
