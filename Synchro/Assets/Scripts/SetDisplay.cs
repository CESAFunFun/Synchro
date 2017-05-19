using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDisplay : MonoBehaviour {

    [SerializeField]
    private GameObject _mainCam;

    [SerializeField]
    private GameObject _subCam;


    // Use this for initialization
    void Start () {
		
        
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (_mainCam.activeSelf)
            {
                _mainCam.SetActive(false);
                _subCam.SetActive(true);
                GetComponent<CameraRote2>().count = 0;
                transform.rotation = new Quaternion(0, 0, 0,0);
            }
            else
            {
                _mainCam.SetActive(true);
                _subCam.SetActive(false);
                GetComponent<CameraRote2>().count = 0;
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
        }
    }
}
