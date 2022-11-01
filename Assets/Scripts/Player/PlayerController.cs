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
        // ��ȡ��������ֵ
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        jumpInput = Input.GetButtonDown("Jump");
        
        checkIsLanding();
        //isOnGround = controller.isGrounded;
        CalculateMove();
        CalculateJump();

        animator.SetFloat("MoveSpeed", controller.velocity.sqrMagnitude);
    }

    // ����ƽ���ƶ�
    private void CalculateMove()
    {
        // ����������ƶ��ٶȼ����ƶ�ֵ    
        move = transform.forward * verticalInput * moveSpeed * Time.deltaTime;

        // ����CharatorControllerִ���ƶ�����
        controller.Move(move);

        //move = new Vector3(horizontalInput, 0, verticalInput);

        // �������Եķ���Ϊ�ƶ�������
        //move = renderCamera.transform.TransformDirection(move);

        // �ƶ���ʱ�������﷽������������һ��
        //if(move.x != 0 || move.z != 0)
        //{
        //    // ����Yֵ������ֵ����������б
        //    move.y = 0;
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(move), rotationSpeed);
        //}

        // ת��
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed);
    }

    // ������Ծ
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
            // �����Ծ���������а�ס�ո���������ø���
            //if( verticalSpeed > 0 && jumpInput)
            //{
            //    verticalSpeed += jumpSpeed * 0.5f * Time.deltaTime;
            //}
            jumpSpeed.y += gravity * Time.deltaTime;
        }

        // ��ֱ����
        controller.Move(jumpSpeed * Time.deltaTime);
    }


    // �ж��Ƿ��䵽����.�䵽������ʱ��Y�����ٶ�����Ϊ0
    private void checkIsLanding()
    {
        // ���μ��
        isOnGround = Physics.CheckSphere(playerFoot.transform.position, checkRaduis, canStandLayerMask);
        if (isOnGround && jumpSpeed.y < 0)
        {
            jumpSpeed.y = 0;
        }
    }
}
