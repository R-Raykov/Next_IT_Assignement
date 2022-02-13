using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothObject : InteractableObject
{
    private Vector3 startPos;
    private Vector3 startScale;

    private Coroutine playAnimation;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
        startScale = transform.localScale;
    }

    protected override IEnumerator Animate()
    {
        float _currentTime = 0.0f;

        Vector3 _finalPosition = startPos;
        Vector3 _finalScale = startScale;

        if (!isOpen)
        {
            _finalPosition.x = 0.325f;
            _finalScale.x = startScale.x / 4.0f;
        }

        isOpen = !isOpen;

        while (_currentTime < animationDuration)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _finalPosition, _currentTime / animationDuration);
            transform.localScale = Vector3.Lerp(transform.localScale, _finalScale, _currentTime / animationDuration);

            _currentTime += Time.deltaTime;

            yield return null;
        }
    }

}
