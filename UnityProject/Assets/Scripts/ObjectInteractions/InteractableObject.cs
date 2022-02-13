using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [Tooltip("The duration of the open/close animation in seconds")]
    [Range(1.0f, 10.0f)]
    [SerializeField] protected float animationDuration = 5.0f;

    protected bool isOpen;

    private Coroutine playAnimation;

    public void Interact()
    {
        if (playAnimation != null)
        {
            StopCoroutine(playAnimation);
        }
        playAnimation = StartCoroutine(Animate());
    }

    protected virtual IEnumerator Animate()
    {
        yield return null;
    }
}
