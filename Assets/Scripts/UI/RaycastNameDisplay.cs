using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaycastNameDisplay : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private float rayDistance = 5f;
    [SerializeField] private LayerMask interactLayerMask;

    private void Update()
    {
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, rayDistance, interactLayerMask))
        {
            var interactable = hit.collider.GetComponent<InteractableName>();
            if (interactable != null)
            {
                nameText.text = interactable.objectName;
                nameText.enabled = true;
                return;
            }
        }

        // 아무것도 안 보고 있다면
        nameText.enabled = false;
    }
}
