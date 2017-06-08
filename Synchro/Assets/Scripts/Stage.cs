using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour {

    public int stageLevel;
    public GameObject[] stageObjects;

    public bool useJump = false;
    public bool usePlayerChanger = false;
    public bool useGravity = false;
    public bool usePlayerFloorFlip = false;


    private void Awake()
    {
        stageLevel = GameManager.Instance.mapLevel;
    }
    private void Start()
    {

    }
}
