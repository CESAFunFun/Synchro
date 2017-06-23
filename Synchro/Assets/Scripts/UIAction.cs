using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIAction : MonoBehaviour {

    [HideInInspector]
    public static bool mapMoveBy;
    [HideInInspector]
    public static bool mapChangeGravity;
    [HideInInspector]
    public static bool mapBlinkGimmick;
    [HideInInspector]
    public static bool mapBrokenGimmick;

    [SerializeField]
    private Character _character;

    [SerializeField]
    private GameObject _imageJump;
    [SerializeField]
    private GameObject _imageGrav;
    //[SerializeField]
    //private GameObject _imageBlink;

    [SerializeField]
    private GameObject _imageMoveBy;
    [SerializeField]
    private GameObject _imageChangeGravity;
    [SerializeField]
    private GameObject _imageBlinkGimmick;
    [SerializeField]
    private GameObject _imageBrokenGimmick;

    // Use this for initialization
    void Start () {
        //_imageJump.SetActive(_character.canJump);
        //_imageGrav.SetActive(_character.canGravity);
        //_imageBlink.SetActive(_character.canBlink);
    }
	
	// Update is called once per frame
	void Update () {
        _imageJump.SetActive(_character.canJump);
        _imageGrav.SetActive(_character.canGravity);

        _imageMoveBy.SetActive(mapMoveBy);
        _imageChangeGravity.SetActive(mapChangeGravity);
        _imageBlinkGimmick.SetActive(mapBlinkGimmick);
        _imageBrokenGimmick.SetActive(mapBrokenGimmick);
    }
}
