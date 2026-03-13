using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator anim;

    // 交互与对话锁定
    public bool canMove = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!canMove)
        {
            moveInput = Vector2.zero;
            UpdateAnimation();
            return;
        }

        // 输入获取
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        UpdateAnimation();
    }

    void FixedUpdate()
    {
        // 移动
        rb.velocity = moveInput * moveSpeed;
    }

    void UpdateAnimation()
    {
        if (moveInput != Vector2.zero)
        {
            anim.SetFloat("X", moveInput.x);
            anim.SetFloat("Y", moveInput.y);
            anim.SetBool("IsWalk", true);
        }
        else
        {
            anim.SetBool("IsWalk", false);
        }
    }

    // 外部调用：禁止/允许移动
    public void SetCanMove(bool value)
    {
        canMove = value;
    }
}
