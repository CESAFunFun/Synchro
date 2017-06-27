using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinCurve : MonoBehaviour {

    [SerializeField]
    private Vector3 anchor;
    [SerializeField]
    private Transform point;

    [SerializeField]
    private float width = 1F;
    [SerializeField]
    private float speed = 1F;
    [SerializeField]
    private bool sinX = false;
    [SerializeField]
    private bool sinY = false;

    private float value = 0.0f;

    void Update () {
        value += Time.deltaTime * speed;
        var sin = Mathf.Abs(Mathf.Sin(value) * width);

        if(sinX) transform.position = anchor + point.position + Vector3.right * sin;
        if(sinY) transform.position = anchor + point.position + Vector3.up * sin;
	}
}
