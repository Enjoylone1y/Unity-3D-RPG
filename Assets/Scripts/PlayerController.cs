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
    private float verticalSpeed;
    private float diffAngle;

    private RaycastHit hitInfo;
    private LayerMask canStandLayerMask;

    public bool isOnGround = true;

    public GameObject playerFoot;
    public GameObject renderCamera;

    public float moveSpeed;
    public float gravity;
    public float jumpSpeed;
    public float rotationSpeed;
    public string[] canStandLayerNames;
 

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
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
        //checkIsLanding();
        isOnGround = controller.isGrounded;

        CalculateRotation();
        CalculateMove();
        CalculateJump();
    }

    // 计算平面移动
    private void CalculateMove()
    {
        // 根据输入和移动速度计算移动值    
        //move = new Vector3(horizontalInput, 0, verticalInput);
        move = transform.forward * verticalInput * moveSpeed * Time.deltaTime;

        // 摄像机面对的方向即为移动的正向
        //move = renderCamera.transform.TransformDirection(move);

        // 移动的时候让人物方向和摄像机方向一致
        //if(move.x != 0 || move.z != 0)
        //{
        //    // 避免Y值方向有值导致人物倾斜
        //    move.y = 0;
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(move), rotationSpeed);
        //}

        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed);

        // 竖直方向
        move += Vector3.up * verticalSpeed * Time.deltaTime;

        // 交由CharatorController执行移动操作
        controller.Move(move);
    }

    // 计算跳跃
    private void CalculateJump()
    {
        if (isOnGround)
        {   if (jumpInput)
            {
                isOnGround = false;
                verticalSpeed = jumpSpeed;
            }
        }
        else
        {
            // 如果跳跃上升过程中按住空格键则能跳得更高
            if( verticalSpeed > 0 && jumpInput)
            {
                verticalSpeed += jumpSpeed * 0.5f * Time.deltaTime;
            }
            verticalSpeed += gravity * Time.deltaTime;
        }
    }

    private void CalculateRotation()
    {

    }

    // 判断是否落到地上
    private void checkIsLanding()
    {
       isOnGround = Physics.Raycast(playerFoot.transform.position, Vector3.down, 0.1f,canStandLayerMask);
    }    
}
