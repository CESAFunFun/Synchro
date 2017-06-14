//---------------------------
// 秒数で自分を破棄する
//---------------------------
using UnityEngine;

public class DestroyInSeconds : MonoBehaviour {
    

    bool destroyFlag = false;

    float currentTime = 0;

    private Vector3 pos;

    [SerializeField]
    float waitTime = 2;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Map")
        {
            destroyFlag = true;
        }
    }

    private void Start()
    {
        pos = gameObject.transform.position;
    }
    private void Update()
    {
        if (!destroyFlag) return;

        currentTime += Time.deltaTime;

        if(currentTime > waitTime)
        {
            transform.position = new Vector3(10000000, 0, 0);
            if(currentTime > 5)
            {
                Reposition();
                currentTime = 0;
            }
        }



    }
    private void Reposition()
    {
        destroyFlag = false;
        transform.position = pos;
    }

}
