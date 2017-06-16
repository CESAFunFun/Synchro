using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailLineCreater : MonoBehaviour {

    [SerializeField]
    private GameObject _trailPrefab;

    private GameObject trail;

    [SerializeField]
    private TrailMove.State _state;
    

	// Use this for initialization
	void Start () {
        
        trail = Instantiate(_trailPrefab, transform.position, Quaternion.identity);
        trail.GetComponent<TrailMove>().InitState = _state;
	}
	
	// Update is called once per frame
	void Update () {

        if (trail.transform.position.x > transform.position.x + 25.0f || trail.transform.position.x < transform.position.x + -25.0f ||
            trail.transform.position.y > transform.position.y + 25.0f || trail.transform.position.y < transform.position.y + -25.0f)
        {
            Destroy(trail);
            trail = Instantiate(_trailPrefab, transform.position, Quaternion.identity);
            trail.GetComponent<TrailMove>().InitState = _state;
        }
             

	}
}
