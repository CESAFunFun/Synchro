using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour {

    public int stageLevel;
    public GameObject[] stageObjects;

    private void Awake()
    {
        stageLevel = GameManager.Instance.mapLevel;
    }
    private void Start()
    {

    }
}
