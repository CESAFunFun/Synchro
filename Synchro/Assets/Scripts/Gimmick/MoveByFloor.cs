//-------------------------------
// NPCをスピードにより進ませる
//-------------------------------
using UnityEngine;

public class MoveByFloor : MonoBehaviour {

    [SerializeField]
    private float movementSpeed = 0.025f ;

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Child")
        {
            //動かせる
            collision.transform.Translate(movementSpeed, 0, 0);
        }
    }
}
