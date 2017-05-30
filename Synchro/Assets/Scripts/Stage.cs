using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour {

    public int stageLevel;
    public GameObject[] stageObjects;

    private void Start()
    {
        stageLevel = GameManager.Instance.mapLevel;
    }
}
