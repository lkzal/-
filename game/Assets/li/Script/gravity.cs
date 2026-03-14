using UnityEngine;

public class ZAxisGravity : MonoBehaviour
{
    public Rigidbody rb;
    private float gravity = 9.81f;

    void FixedUpdate()
    {
        // 在 Z 轴施加向下的力
        rb.AddForce(0, 0, gravity, ForceMode.Acceleration);
    }
}
