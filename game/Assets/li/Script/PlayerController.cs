using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("基础移动")]
    public float moveSpeed = 5f;
    public bool canMove = true;

    [Header("Z轴重力（正方向）")]
    public float gravity = 9.81f;
    public float groundCheckDistance = 0.15f;
    public LayerMask groundLayer;

    [Header("跳跃设置")]
    public float jumpHeight = 3f;
    public float jumpCooldown = 0.5f;
    private bool canJump = true;

    [Header("翻滚动画设置")]
    public float rollSpeed = 720f; // 每秒翻滚角度
    private bool isRolling = false;
    private Vector3 rollDirection;

    private CharacterController cc;
    private Animator anim;
    private Vector3 velocity;
    private bool isGrounded;
    private float jumpTimer;

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
            anim.SetBool("Jump", false);
            anim.SetBool("Roll", false);
            ApplyGravity();
            return;
        }

        // 1. 地面检测（Z轴正方向）
        isGrounded = Physics.CheckSphere(
            transform.position + new Vector3(0, 0, groundCheckDistance),
            0.1f,
            groundLayer
        );

        // 2. 跳跃冷却计时
        if (jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;
            canJump = false;
        }
        else
        {
            canJump = true;
        }

        // 3. 处理输入
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 moveDir = new Vector3(h, v, 0).normalized;

        // 4. 跳跃输入（空格键）
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canJump)
        {
            Jump();
        }

        // 5. 翻滚输入（左Shift键）
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded && moveDir.magnitude > 0.1f && !isRolling)
        {
            StartRoll(moveDir);
        }

        // 6. 应用移动（非翻滚状态下）
        if (moveDir.magnitude > 0.1f && !isRolling)
        {
            transform.forward = new Vector3(0, 0, 1);
            transform.right = moveDir;
            cc.Move(moveDir * moveSpeed * Time.deltaTime);
            anim.SetBool("Walk", true);
        }
        else if (!isRolling)
        {
            anim.SetBool("Walk", false);
        }

        // 7. 应用重力与跳跃
        ApplyGravity();

        // 8. 应用翻滚动画
        ApplyRoll();
    }

    /// <summary>
    /// 跳跃逻辑：沿Z轴正方向起跳
    /// </summary>
    void Jump()
    {
        velocity.z = Mathf.Sqrt(jumpHeight * 2 * gravity);
        jumpTimer = jumpCooldown;
        anim.SetBool("Jump", true);
    }

    /// <summary>
    /// 开始翻滚：记录方向并触发动画
    /// </summary>
    void StartRoll(Vector3 direction)
    {
        isRolling = true;
        rollDirection = direction;
        anim.SetBool("Roll", true);
    }

    /// <summary>
    /// 应用翻滚：让方块沿移动方向旋转
    /// </summary>
    void ApplyRoll()
    {
        if (isRolling)
        {
            // 沿移动方向的垂直轴翻滚（方块滚动效果）
            Vector3 rollAxis = Vector3.Cross(rollDirection, Vector3.forward);
            transform.Rotate(rollAxis, rollSpeed * Time.deltaTime);

            // 翻滚时额外加速移动
            cc.Move(rollDirection * moveSpeed * 1.5f * Time.deltaTime);

            // 翻滚结束条件：旋转接近一圈或落地后停止
            if (isGrounded && transform.rotation.eulerAngles.x % 360 < 10)
            {
                isRolling = false;
                anim.SetBool("Roll", false);
                // 重置旋转到直立
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    /// <summary>
    /// Z轴正方向重力与落地处理
    /// </summary>
    void ApplyGravity()
    {
        if (isGrounded && velocity.z > 0)
        {
            velocity.z = 2f; // 轻微吸附地面
            anim.SetBool("Jump", false);
        }
        else
        {
            velocity.z += gravity * Time.deltaTime;
        }

        cc.Move(velocity * Time.deltaTime);
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
    }
}
