//---------------------------------
// 横に進んだり戻ったりするギミック
//---------------------------------
using UnityEngine;

public class MovingFloor : MonoBehaviour {

    [SerializeField]
    private float speed = 1;

    private float posX;
	// Use this for initialization
	void Start () {

        posX = gameObject.transform.position.x;
		
	}
	
	// Update is called once per frame
	void Update () {

        posX += speed;

        gameObject.transform.position = new Vector3(posX, transform.position.y, transform.position.z);
		
	}

    private void OnCollisionEnter(Collision collision)
    {        
        if(collision.gameObject.tag == "Map")
        {
            speed *= -1;
        }
    }

}
