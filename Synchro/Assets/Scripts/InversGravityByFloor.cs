//-----------------------
// 重力反転をさせる
//-----------------------
using UnityEngine;

public class InversGravityByFloor : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Child")
        {
            if (collision.gameObject.GetComponent<Character>().downGravity)
            {
                collision.gameObject.GetComponent<Character>().downGravity = false;
            }
            else
            {
                collision.gameObject.GetComponent<Character>().downGravity = true;
            }

        }
    }

}
