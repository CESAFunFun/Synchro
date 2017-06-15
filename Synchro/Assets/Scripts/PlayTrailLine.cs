using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTrailLine : MonoBehaviour {

    [SerializeField]
    private Player _player;

    [SerializeField]
    private GameObject _trailPrefab;

    private GameObject trail;
    
    private TrailMove.State _state;

    private float currentTime = 0;

    private float waitTime = 5;

    // Use this for initialization
    void Start () {

        if (_player.downGravity) _state = TrailMove.State.DOWN;
        else _state = TrailMove.State.UP;

        trail = Instantiate(_trailPrefab, transform.position, Quaternion.identity);
        trail.GetComponent<TrailMove>().InitState = _state;
    }
	
	// Update is called once per frame
	void Update () {

        if (_player.downGravity) _state = TrailMove.State.DOWN;
        else _state = TrailMove.State.UP;
        currentTime += Time.deltaTime;
        if (currentTime > waitTime)
        {
            Destroy(trail);
            trail = Instantiate(_trailPrefab, transform.position, Quaternion.identity);
            trail.GetComponent<TrailMove>().InitState = _state;
            currentTime = 0;
        }

	}
}
