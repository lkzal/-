using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    // 边界
    public float minX, maxX, minY, maxY;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPos = target.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);

        // 限制边界
        float clampX = Mathf.Clamp(smoothedPos.x, minX, maxX);
        float clampY = Mathf.Clamp(smoothedPos.y, minY, maxY);

        transform.position = new Vector3(clampX, clampY, smoothedPos.z);
    }
}