//-----------------------
// ブリングするギミック
//-----------------------
using UnityEngine;

public class Blink : MonoBehaviour {

    [SerializeField]
    private float _interval = 2;

    private float _currentTime = 0;

    private bool _enabled = true;

    private Vector3 pos;

	// Use this for initialization
	void Start () {

        pos = gameObject.transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {

        _currentTime += Time.deltaTime;

        if (_currentTime > _interval)
        {
            transform.position = new Vector3(10000000, 0, 0);
            _enabled = false;

            if (_currentTime > _interval * 2)
            {
                Reposition();
                _currentTime = 0;
            }
        }

	}
    private void Reposition()
    {
        transform.position = pos;
        _enabled = true;
    }

}
