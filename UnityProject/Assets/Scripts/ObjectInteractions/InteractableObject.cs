using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parent class for all interactors
/// </summary>
public class InteractableObject : MonoBehaviour, IInteractable
{
    [Tooltip("The duration of the open/close animation in seconds")]
    [Range(1.0f, 10.0f)]
    [SerializeField] protected float animationDuration = 5.0f;

    protected bool isOpen;

    private Coroutine playAnimation;

    /// <summary>
    /// Implementation of the interface
    /// </summary>
    public void Interact()
    {
        //check if it's animating and start an animate coroutine
        if (playAnimation != null)
        {
            StopCoroutine(playAnimation);
        }
        playAnimation = StartCoroutine(Animate());
    }

    /// <summary>
    /// Each child class has a different version of Animate
    /// </summary>
    protected virtual IEnumerator Animate()
    {
        yield return null;
    }
}
