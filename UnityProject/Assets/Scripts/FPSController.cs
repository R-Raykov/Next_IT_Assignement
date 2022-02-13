using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
	[SerializeField] private float mouseSensitivity = 5.0f;
	[SerializeField] private float mouseSmoothing = 2.0f;
	[Tooltip("Speed of the player")]
	[Range(1,20)]
	[SerializeField] private float Speed;

	private Vector2 mouseLook;
	private Vector2 smoothing;

	private float translation;
	private float straffe;

	private RaycastHit hit;


	// Update is called once per frame
	void Update()
	{
		HandleRotation();
		HandleMovement();
		HandleInteraction();
	}

	private void HandleRotation()
    {
		Vector2 mouseDirection = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

		Vector2.Scale(mouseDirection, new Vector2(mouseSensitivity * mouseSmoothing, mouseSensitivity * mouseSmoothing));
		smoothing.x = Mathf.Lerp(smoothing.x, mouseDirection.x, 1f / mouseSmoothing);
		smoothing.y = Mathf.Lerp(smoothing.y, mouseDirection.y, 1f / mouseSmoothing);
		mouseLook += smoothing;
		mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);


		transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
		transform.localRotation = Quaternion.AngleAxis(mouseLook.x, Vector3.up);
	}

	private void HandleMovement()
    {
		translation = Input.GetAxis("Vertical") * Speed;
		straffe = Input.GetAxis("Horizontal") * Speed;

		translation *= Time.deltaTime;
		straffe *= Time.deltaTime;

		transform.Translate(straffe, 0, translation);
	}

	private void HandleInteraction()
    {
		if (Input.GetMouseButtonDown(0))
        {
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray: _ray, out hit, 5, 1 << 10))
            {
				hit.transform.GetComponentInChildren<IInteractable>()?.Interact();
            }
        }
    }
}
