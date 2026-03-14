using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private CharacterController cc;
    private Animator anim;

    public bool canMove = true;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!canMove)
        {
            anim.SetBool("Walk", false);
            return;
        }

        // 输入：A、D 控制 X，W、S 控制 Y
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // 纯 XY 平面移动，Z=0 固定不动
        Vector3 moveDir = new Vector3(h, v, 0).normalized;

        if (moveDir.magnitude > 0.1f)
        {
            // 角色面朝移动方向
            transform.forward = new Vector3(0, 0, 1);
            transform.right = moveDir;

            cc.Move(moveDir * moveSpeed * Time.deltaTime);
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
    }
}
