using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeObject : MonoBehaviour, IInteractable
{
    [Tooltip("The duration of the open/close animation in seconds")]
    [Range(1.0f, 5.0f)]
    [SerializeField] private float animationDuration;

    [Tooltip("Maximum angle after interaction, should be the absolute value")]
    [Range(75.0f, 140.0f)]
    [SerializeField]private float maximumOpenedAngle;

    [SerializeField] private GameObject otherHalf;

    RaycastHit hit;

    private bool isOpen;

    private Coroutine animation;

    private void Start()
    {
        
    }

    private void OnMouseDown()
    {
        Interact();
    }

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        if (Physics.Raycast(ray:_ray, out hit, 100, 1 << 10))
    //        {
    //            Destroy(hit.transform.gameObject);
    //        }
    //    }

    public void Interact()
    {
        if(animation != null)
        {
            StopCoroutine(animation);
        }
        animation = StartCoroutine(Animate());
    }

    private IEnumerator Animate()
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
