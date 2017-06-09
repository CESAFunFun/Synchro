//-----------------------
// 重力反転をさせる
//-----------------------
using UnityEngine;

public class InversGravityByFloor : MonoBehaviour {
    
    private void OnCollisionStay(Collision collision)
    {
         var child = collision.gameObject.GetComponent<Character>();
         child.ChangeGravity(!child.downGravity);
    }

}
