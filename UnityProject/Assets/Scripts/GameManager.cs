using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance {
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

    public bool FPSCamera { get; set; }
 

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
        FPSCamera = true;
    }
}
