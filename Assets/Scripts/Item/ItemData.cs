using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public float duration;
    public float effectValue;
    public ItemEffectType effectType;

    [Header("Visual")]
    public GameObject prefab;
    public Material material;

    public enum ItemEffectType
    {
        SpeedBoost,
        JumpBoost,
        HealthRegen
    }
}
