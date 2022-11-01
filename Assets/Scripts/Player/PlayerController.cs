using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private float horizontalInput;
    private float verticalInput;
    private bool jumpInput;
    private Vector3 move;
    private Vector3 jumpSpeed;
    private Animator animator;

    //private float diffAngle;
    //private RaycastHit hitInfo;

    private LayerMask canStandLayerMask;

    public bool isOnGround = true;

    public GameObject playerFoot;
    public GameObject renderCamera;

    public float moveSpeed;
    public float gravity;
    public float jumpHeight;
    public float rotationSpeed;
    public string[] canStandLayerNames;
    public float checkRaduis;
 

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        canStandLayerMask = LayerMask.GetMask(canStandLayerNames);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 获取键盘输入值
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        jumpInput = Input.GetButtonDown("Jump");
        
        checkIsLanding();
        //isOnGround = controller.isGrounded;
        CalculateMove();
        CalculateJump();

        animator.SetFloat("MoveSpeed", controller.velocity.sqrMagnitude);
    }

    // 计算平面移动
    private void CalculateMove()
    {
        // 根据输入和移动速度计算移动值    
        move = transform.forward * verticalInput * moveSpeed * Time.deltaTime;

        // 交由CharatorController执行移动操作
        controller.Move(move);

        //move = new Vector3(horizontalInput, 0, verticalInput);

        // 摄像机面对的方向即为移动的正向
        //move = renderCamera.transform.TransformDirection(move);

        // 移动的时候让人物方向和摄像机方向一致
        //if(move.x != 0 || move.z != 0)
        //{
        //    // 避免Y值方向有值导致人物倾斜
        //    move.y = 0;
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(move), rotationSpeed);
        //}

        // 转向
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed);
    }

    // 计算跳跃
    private void CalculateJump()
    {
        if (isOnGround)
        {
            if (jumpInput)
            {
                jumpSpeed.y = Mathf.Sqrt(-2 * jumpHeight * gravity);
            }
        }
        else
        {
            // 如果跳跃上升过程中按住空格键则能跳得更高
            //if( verticalSpeed > 0 && jumpInput)
            //{
            //    verticalSpeed += jumpSpeed * 0.5f * Time.deltaTime;
            //}
            jumpSpeed.y += gravity * Time.deltaTime;
        }

        // 竖直方向
        controller.Move(jumpSpeed * Time.deltaTime);
    }


    // 判断是否落到地上.落到地面上时把Y方向速度设置为0
    private void checkIsLanding()
    {
        // 球形检测
        isOnGround = Physics.CheckSphere(playerFoot.transform.position, checkRaduis, canStandLayerMask);
        if (isOnGround && jumpSpeed.y < 0)
        {
            jumpSpeed.y = 0;
        }
    }
}
