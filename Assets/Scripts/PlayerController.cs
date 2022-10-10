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
        // ��ȡ��������ֵ
        horizontalInput = Input.GetAxis("Horizontal");
        verticleInput = Input.GetAxis("Vertical");
        jumpInput = Input.GetButtonDown("Jump");
        //checkIsLanding();
        isOnGround = controller.isGrounded;

        CalculateMove();
        CalculateJump();
    }

    // ����ƽ���ƶ�
    private void CalculateMove()
    {
        // ����������ƶ��ٶȼ����ƶ�ֵ    
        move = new Vector3(horizontalInput, 0, verticleInput);
        move *= moveSpeed * Time.deltaTime;
        move += Vector3.up * verticalSpeed * Time.deltaTime;

        // ���ݽ�ɫ������ı��ƶ�����
        move = transform.TransformDirection(move);

        // ����CharatorControllerִ���ƶ�����
        controller.Move(move);
    }

    // ������Ծ
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
            // �����Ծ���������а�ס�ո���������ø���
            if( verticalSpeed > 0 && jumpInput)
            {
                verticalSpeed += jumpSpeed * 0.5f * Time.deltaTime;
            }
            verticalSpeed += gravity * Time.deltaTime;
        }
    }

    // �ж��Ƿ��䵽����
    private void checkIsLanding()
    {
       isOnGround = Physics.Raycast(playerFoot.transform.position, Vector3.down, 0.1f,canStandLayerMask);
    }    
}
