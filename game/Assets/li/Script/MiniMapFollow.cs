using UnityEngine;

public class MiniMapFollow : MonoBehaviour
{
    public Transform player;      // 拖你的角色
    public float cameraHeight = -20f;

    // 这里拖小地图上的 PlayerIcon
    public RectTransform playerIcon;

    void LateUpdate()
    {
        // 小地图相机只跟随 X、Y，Z固定
        transform.position = new Vector3(
            player.position.x,
            player.position.y,
            cameraHeight
        );

        // 保持垂直向下看XY平面
        transform.rotation = Quaternion.Euler(0, 0, 0);

        // 让图标跟着角色在XY平面的朝向转
        if (playerIcon != null)
        {
            playerIcon.rotation = Quaternion.Euler(0, 0, -player.eulerAngles.z);
        }
    }
}
