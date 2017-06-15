using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailMove : MonoBehaviour
{

    [SerializeField]
    Vector3 move;

    bool left = false;

    [SerializeField]
    float _waitTime;

    float _curremtTime = 0;
    
    [SerializeField]
    private float _speed;

    public enum State{
        LEFT,
        RIGHT,
        UP,
        DOWN,
    };

    [SerializeField]
    State _state;

    State _pastStat;

    [SerializeField]
    private State _initState;
   
    public State InitState { set { _initState = value; } }


    // Use this for initialization
    public void Start()
    {
        _state = _initState;
        _pastStat = _initState;
        TrailDirection();
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(move * _speed * Time.deltaTime);

        _curremtTime += Time.deltaTime;
        if (_curremtTime > _waitTime)
        {
            TrailDirection();
            _state = (State)Random.Range(0, 4);
            _curremtTime = 0;
        }
    }

    private void TrailDirection()
    {
        if (_state == State.LEFT && _pastStat != State.RIGHT && _initState != State.RIGHT)
        {
            move = new Vector3(-1, 0, 0);
            _pastStat = State.LEFT;
        }
        else if (_state == State.RIGHT && _pastStat != State.LEFT && _initState != State.LEFT)
        {
            move = new Vector3(1, 0, 0);
            _pastStat = State.RIGHT;
        }
        else if (_state == State.DOWN && _pastStat != State.UP && _initState != State.UP)
        {
            move = new Vector3(0, -1, 0);
            _pastStat = State.DOWN;
        }
        else if (_state == State.UP && _pastStat != State.DOWN && _initState != State.DOWN)
        {
            move = new Vector3(0, 1, 0);
            _pastStat = State.UP;
        }
    }
}
