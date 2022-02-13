using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : InteractableObject
{
    private Vector3 startPos;
    private Quaternion startRotation;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
        startRotation = transform.localRotation;
    }

    protected override IEnumerator Animate()
    {
        float _currentTime = 0.0f;

        Vector3 _finalPosition = startPos;
        Quaternion _finalRotation = startRotation;

        if (!isOpen)
        {
            Renderer _renderer = GetComponent<Renderer>();

            _finalPosition = Vector3.zero;
            _finalPosition.z = _renderer.bounds.extents.z;

            _finalRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }

        isOpen = !isOpen;

        while (_currentTime < animationDuration)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _finalPosition, _currentTime / animationDuration);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, _finalRotation, _currentTime / animationDuration);

            _currentTime += Time.deltaTime;

            yield return null;
        }
    }

}
