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
        // ��ȡ��������ֵ
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        jumpInput = Input.GetButtonDown("Jump");
        //checkIsLanding();
        isOnGround = controller.isGrounded;

        CalculateRotation();
        CalculateMove();
        CalculateJump();
    }

    // ����ƽ���ƶ�
    private void CalculateMove()
    {
        // ����������ƶ��ٶȼ����ƶ�ֵ    
        //move = new Vector3(horizontalInput, 0, verticalInput);
        move = transform.forward * verticalInput * moveSpeed * Time.deltaTime;

        // �������Եķ���Ϊ�ƶ�������
        //move = renderCamera.transform.TransformDirection(move);

        // �ƶ���ʱ�������﷽������������һ��
        //if(move.x != 0 || move.z != 0)
        //{
        //    // ����Yֵ������ֵ����������б
        //    move.y = 0;
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(move), rotationSpeed);
        //}

        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed);

        // ��ֱ����
        move += Vector3.up * verticalSpeed * Time.deltaTime;

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

    private void CalculateRotation()
    {

    }

    // �ж��Ƿ��䵽����
    private void checkIsLanding()
    {
       isOnGround = Physics.Raycast(playerFoot.transform.position, Vector3.down, 0.1f,canStandLayerMask);
    }    
}
