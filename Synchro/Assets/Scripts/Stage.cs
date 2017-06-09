using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour {

    [HideInInspector]
    public int stageLevel;
    public GameObject[] stageObjects;

    private void Awake()
    {
        stageLevel = GameObject.Find("GameManager").GetComponent<GameManager>().mapLevel;
    }
    private void Start()
    {

    }
}
