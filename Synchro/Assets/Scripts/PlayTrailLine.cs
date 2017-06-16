using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTrailLine : MonoBehaviour {

    [SerializeField]
    private Player _player;

    [SerializeField]
    private GameObject _trailPrefab;

    private GameObject[] _trail = new GameObject[10];
    
    private TrailMove.State _state;

    // Use this for initialization
    void Start () {

        float _initY = 20.0f;
        if (_player.downGravity)
        {
            _state = TrailMove.State.DOWN;

        }
        else
        {
            _state = TrailMove.State.UP;
            _initY *= -1;
        }
        //背景のパーティクル
        for (int i = 0; i < _trail.Length; i++)
        {
            //Instantiate(_trailPrefab);
            _trail[i] = Instantiate(_trailPrefab, new Vector3(transform.position.x + (i * 2), _initY, 0),Quaternion.identity);
            _trail[i].transform.SetParent(transform);
            _trail[i].GetComponent<TrailMove>().InitState = _state;
        }

    }
	
	// Update is called once per frame
	void Update () {

        float _initY = 20.0f;
        if (_player.downGravity)
        {
            _state = TrailMove.State.DOWN;
        }
        else
        {
            _state = TrailMove.State.UP;
            _initY *= -1;
        }
        //範囲外に出たら再生成
        for (int i = 0; i < _trail.Length; i++)
        {
            if (_trail[i].transform.position.x > transform.position.x + 25.0f || _trail[i].transform.position.x < transform.position.x + -25.0f ||
                _trail[i].transform.position.y > transform.position.y + 25.0f || _trail[i].transform.position.y < transform.position.y + -25.0f)
            {
                Destroy(_trail[i]);
                _trail[i] = Instantiate(_trailPrefab, new Vector3(transform.position.x + (i * 2), _initY, 0), Quaternion.identity);
                _trail[i].transform.SetParent(transform);
                _trail[i].GetComponent<TrailMove>().InitState = _state;
            }

        }
    }
}
