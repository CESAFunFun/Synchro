using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    private void Awake() {
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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void isClear() {
        Debug.Log("Clear");
    }
    private void isFail(GameObject failObject) {
        Debug.Log(failObject + ": Faild");
        failObject.GetComponent<Player>().Restart();
    }
}
