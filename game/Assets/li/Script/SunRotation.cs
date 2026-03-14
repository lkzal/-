using UnityEngine;

public class SunRotation : MonoBehaviour
{
    public float rotationSpeed = 0.5f; // 太阳旋转速度

    void Update()
    {
        // 绕 Y 轴旋转，模拟太阳东升西落
        transform.RotateAround(Vector3.zero, Vector3.up, rotationSpeed * Time.deltaTime);
        // 始终保持光照方向指向场景中心
        transform.LookAt(Vector3.zero);
    }
}
