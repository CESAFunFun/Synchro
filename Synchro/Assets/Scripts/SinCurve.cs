using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinCurve : MonoBehaviour {

    [SerializeField]
    private Vector3 anchor;

    [SerializeField]
    private float width = 1F;
    [SerializeField]
    private float speed = 1F;
    [SerializeField]
    private bool adb = false;
    [SerializeField]
    private bool sinX = false;
    [SerializeField]
    private bool sinY = false;

    private float value = 0.0f;

    void Update () {
        value += Time.deltaTime * speed;
        var sin = Mathf.Sin(value) * width;
        if (adb) sin = Mathf.Abs(sin);

        if(sinX) transform.localPosition = anchor + Vector3.right * sin;
        if(sinY) transform.localPosition = anchor + Vector3.up * sin;
	}
}
