using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct UIElement
{
	public string Name;
	public GameObject UIObject;
}
// This could have been handled by the menu manager, but I didn't want to add too much to it when it can be split
public class UIElementHandler : MonoBehaviour
{
	[SerializeField] private List<UIElement> UIMenus;

	private void OnEnable()
	{
		MenuManager.Instance.OnShowScreen += ShowMenu;
	}

	private void OnDisable()
	{
		MenuManager.Instance.OnShowScreen -= ShowMenu;
	}

	/// <summary>
	/// Disables all screen
	/// </summary>
	private void DisableAllScreens()
	{
		foreach (UIElement UI in UIMenus)
		{
			if (UI.UIObject != null)
				UI.UIObject.SetActive(false);
		}
	}

	/// <summary>
	/// Show a menu by name
	/// </summary>
	/// <param name="name">Menu name</param>
	private void ShowMenu(string name)
	{
		// first disable other screen
		DisableAllScreens();

		MenuManager.Instance.LastScreen = MenuManager.Instance.CurrentScreen;

		// then loop over the available screens
		// coulbe be a faster way of doing this, but it's still an O(n) function, which is exeptable for this demo
		foreach (UIElement UI in UIMenus)
		{
			if (UI.Name == name)
			{
				if (UI.UIObject != null)
				{
					UI.UIObject.SetActive(true);
				}
			}
		}

		MenuManager.Instance.CurrentScreen = name;
	}
}
