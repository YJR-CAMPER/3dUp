using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemUser : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    private Coroutine activeEffect;

    public void UseItem(ItemData item)
    {
        Debug.Log($"[ItemUser] UseItem called by {gameObject.name} with item: {item?.itemName}");

        if (item == null)
        {
            Debug.LogError("[ItemUser] ERROR: item is null!");
            return;
        }

        if (player == null)
        {
            player = GetComponent<PlayerController>();
            if (player == null)
            {
                Debug.LogError("[ItemUser] ERROR: PlayerController가 null입니다!");
                return;
            }
        }

        if (activeEffect != null)
            StopCoroutine(activeEffect);

        activeEffect = StartCoroutine(ApplyEffect(item));
    }

    private void Awake()
    {
        player = GetComponent<PlayerController>();
        Debug.Log("[ItemUser] PlayerController 연결됨: " + player);
    }

    private IEnumerator ApplyEffect(ItemData item)
    {
        Debug.Log($"[ItemUser] ApplyEffect 시작: {item?.itemName}, 효과: {item?.effectType}");

        if (item == null)
        {
            Debug.LogError("[ItemUser] ApplyEffect에서 item이 null입니다!");
            yield break;
        }
        switch (item.effectType)
        {
            case ItemData.ItemEffectType.SpeedBoost:
                float originalSpeed = player.moveSpeed;
                player.moveSpeed += item.effectValue;
                yield return new WaitForSeconds(item.duration);
                player.moveSpeed = originalSpeed;
                break;

            case ItemData.ItemEffectType.JumpBoost:
                float originalJump = player.jumpPower;
                player.jumpPower += item.effectValue;
                yield return new WaitForSeconds(item.duration);
                break;

            case ItemData.ItemEffectType.HealthRegen:
                var health = GetComponent<HealthSystem>();
                if (health != null)
                    health.Heal(item.effectValue);
                break;
        }

        activeEffect = null;
    }
}
