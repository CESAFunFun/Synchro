using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIAction : MonoBehaviour {

    [SerializeField]
    private Character _character;

    [SerializeField]
    private GameObject _imageJump;
    [SerializeField]
    private GameObject _imageGrav;
    [SerializeField]
    private GameObject _imageBlink;
    // Use this for initialization
    void Start () {

        _imageJump.SetActive(_character.canJump);
        _imageGrav.SetActive(_character.canGravity);
        _imageBlink.SetActive(_character.canBlink);
    }
	
	// Update is called once per frame
	void Update () {


    }
}
