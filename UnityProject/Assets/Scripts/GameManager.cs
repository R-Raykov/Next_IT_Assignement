using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gameManagerObject = new GameObject();
                gameManagerObject.name = "GameManager";
                gameManagerObject.AddComponent<GameManager>();

                DontDestroyOnLoad(gameManagerObject);

            }
            return instance;
        }
    }

    public bool FreeCamera { get; set; }
    public bool RandomRoom { get; set; }

    [SerializeField] private GameObject presetRoomRoot;
    [SerializeField] private GameObject randomRoomRoot;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
        FreeCamera = true;
        RandomRoom = false;
    }

    /// <summary>
    /// Set which type of room to use and disable the other's root
    /// </summary>
    /// <param name="pFlag"></param>
    public void SetRandomRoom(bool pFlag)
    {
        if (pFlag)
        {
            RandomRoom = true;
            presetRoomRoot.SetActive(false);
            randomRoomRoot.SetActive(true);
        }
        else
        {
            RandomRoom = false;
            presetRoomRoot.SetActive(true);
            randomRoomRoot.SetActive(false);
        }
    }

    /// <summary>
    /// Get a random number from 0-100, used for randomly generating objects based on a % chance
    /// </summary>
    public int GetRandomNumber()
    {
        int _rng = Random.Range(0, 101);
        return _rng;
    }
}
