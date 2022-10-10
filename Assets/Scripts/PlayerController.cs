using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private float horizontalInput;
    private float verticleInput;
    private bool jumpInput;
    private Vector3 move;
    private float verticalSpeed;
    private RaycastHit hitInfo;
    private LayerMask canStandLayerMask;

    public bool isOnGround = true;

    public float moveSpeed;
    public float gravity;
    public float jumpSpeed;
    public GameObject playerFoot;
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
        verticleInput = Input.GetAxis("Vertical");
        jumpInput = Input.GetButtonDown("Jump");
        //checkIsLanding();
        isOnGround = controller.isGrounded;

        CalculateMove();
        CalculateJump();
    }

    // 计算平面移动
    private void CalculateMove()
    {
        // 根据输入和移动速度计算移动值    
        move = new Vector3(horizontalInput, 0, verticleInput);
        move *= moveSpeed * Time.deltaTime;
        move += Vector3.up * verticalSpeed * Time.deltaTime;

        // 根据角色面向方向改变移动方向
        move = transform.TransformDirection(move);

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

    // 判断是否落到地上
    private void checkIsLanding()
    {
       isOnGround = Physics.Raycast(playerFoot.transform.position, Vector3.down, 0.1f,canStandLayerMask);
    }    
}
