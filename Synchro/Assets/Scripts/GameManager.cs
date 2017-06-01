using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public Gamepad gamePad;

    private static GameManager instance;

    public static GameManager Instance { get { return instance; } }

    public int mapLevel = 1;

    private void Awake()
    {

        gamePad = GetComponent<Gamepad>();
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        //gamePad = GetComponent<Gamepad>();
    }

    private void Update()
    {
    }

    private void isClear()
    {
        Debug.Log("Clear");
    }

    private void isFail(GameObject failObject)
    {
        failObject.GetComponent<Character>().Restart();
    }
}
