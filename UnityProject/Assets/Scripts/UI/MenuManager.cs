using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuManager : MonoBehaviour
{
    #region Events
    public delegate void ScreenHandler(string pScreen);
    public event ScreenHandler OnShowScreen;

    public delegate void EventHandler();
    public event EventHandler OnGameStart;
    #endregion

    public string CurrentScreen;
    public string LastScreen;

    private static MenuManager instance;
    public static MenuManager Instance { get => instance; }

    [SerializeField] private GameObject fpsCamera;
    [SerializeField] private GameObject staticCamera;
    [SerializeField] private GameObject menuCamera;

    [SerializeField] private GameObject canvas;


    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        menuCamera.SetActive(true);
        fpsCamera.SetActive(false);
        staticCamera.SetActive(false);
    }

    private void OnEnable()
    {
        OnGameStart += SetCorrectCameras;
    }

    private void OnDisable()
    {
        OnGameStart -= SetCorrectCameras;
    }

    public void ChangeScreen(string pScreen)
    {
        if (OnShowScreen != null)
            OnShowScreen.Invoke(pScreen);
    }

    public void ShowLastScreen()
    {
        ChangeScreen(LastScreen);
        SetCorrectCameras();
    }

    public void StartGame()
    {
        if (OnGameStart != null)
            OnGameStart.Invoke();
    }

    private void SetCorrectCameras()
    {
        if(GameManager.Instance.FPSCamera)
        {
            menuCamera.SetActive(false);
            fpsCamera.SetActive(true);
            staticCamera.SetActive(false);
        }
        else
        {
            menuCamera.SetActive(false);
            fpsCamera.SetActive(false);
            staticCamera.SetActive(true);
        }
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
