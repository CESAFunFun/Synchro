//-----------------------
// ブリングするギミック
//-----------------------
using UnityEngine;

public class Blink : MonoBehaviour {

    [SerializeField]
    private float _interval = 2;

    private float _time = 0;

    private bool _enabled = true;

    private SpriteRenderer _spriteRend;
    private BoxCollider _boxCol;

	// Use this for initialization
	void Start () {

        _spriteRend = gameObject.GetComponent<SpriteRenderer>();
        _boxCol = gameObject.GetComponent<BoxCollider>();

		
	}
	
	// Update is called once per frame
	void Update () {
        _time++;
        
        //時間たつと消したり出したりする
        if(_time >= 60 * _interval)
        {
            _time = 0;
            if(_enabled)
            {
                _enabled = false;
            }
            else
            {
                _enabled = true;
            }
        }

        EnablingDisabling(_enabled);

	}

    private void EnablingDisabling(bool flag)
    {
        _spriteRend.enabled = flag;
        _boxCol.enabled = flag;
    }
}
