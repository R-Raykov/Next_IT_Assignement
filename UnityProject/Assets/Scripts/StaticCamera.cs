using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The interactor for the static camera
/// The same implementation is added to the example "SimpleCamera" that comes with the HDRP package
/// </summary>

public class StaticCamera : MonoBehaviour
{
    private RaycastHit hit;

    void Update()
    {
        HandleInteraction();
    }

    /// <summary>
    /// Shoot a raycast that collides with the interactable layer 
    /// If there are any objects that implement the IInteractable interface call their implementation of it
    /// </summary>
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
