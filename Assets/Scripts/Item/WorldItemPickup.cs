using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItemPickup : MonoBehaviour
{
    [SerializeField] private ItemData itemData;

    private void OnTriggerEnter(Collider other)
    {

        var itemUser = other.GetComponentInParent<ItemUser>();

        itemUser.UseItem(itemData);
        StartCoroutine(DelayedDestroy());
    }

    private IEnumerator DelayedDestroy()
    {
        yield return null; // 1 프레임 대기
        Destroy(gameObject);
    }
 }
