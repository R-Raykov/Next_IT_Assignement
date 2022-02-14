using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct UIElement
{
	public string Name;
	public GameObject UIObject;
}

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

	private void DisableAllScreens()
	{
		foreach (UIElement UI in UIMenus)
		{
			if (UI.UIObject != null)
				UI.UIObject.SetActive(false);
		}
	}

	//shows a menu by name
	private void ShowMenu(string name)
	{
		DisableAllScreens();

		MenuManager.Instance.LastScreen = MenuManager.Instance.CurrentScreen;

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
