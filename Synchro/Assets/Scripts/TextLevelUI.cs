using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLevelUI : MonoBehaviour {

    private Text levelText;

	// Use this for initialization
	void Start () {
        levelText = GetComponent<Text>();
		
	}
	
	// Update is called once per frame
	void Update () {
        levelText.text = "Stage " + GameController.Instance.mapLevel.ToString();
		
	}
}
