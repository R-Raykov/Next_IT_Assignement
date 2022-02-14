using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCamera : MonoBehaviour
{
    private RaycastHit hit;

    void Update()
    {
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray: _ray, out hit, 100, 1 << 10))
            {
                hit.transform.GetComponentInChildren<IInteractable>()?.Interact();
            }
        }
    }
}
