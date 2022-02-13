using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeObject : InteractableObject
{
    [Tooltip("Maximum angle after interaction, should be the absolute value")]
    [Range(75.0f, 140.0f)]
    [SerializeField]private float maximumOpenedAngle;

    [SerializeField] private GameObject otherHalf;


    protected override IEnumerator Animate()
    {
        float _currentTime = 0.0f;

        Vector3 _finalRotation = Vector3.zero;

        if (!isOpen)
        {
            _finalRotation.y = maximumOpenedAngle;
        }

        isOpen = !isOpen;

        while (_currentTime < animationDuration)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, 
                                        Quaternion.Euler(_finalRotation), _currentTime / animationDuration);

            otherHalf.transform.localRotation = Quaternion.Slerp(otherHalf.transform.localRotation, 
                                                    Quaternion.Euler(-_finalRotation), _currentTime / animationDuration);

            _currentTime += Time.deltaTime;

            yield return null;
        }
    }
}
