using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 第一人称控制
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public float XSensitivity = 2f;
    public float YSensitivity = 2f;
    private float currentXS = 2f;
    private float currentYS = 2f;

    private CharacterController controller;
    private Transform myTrans;
    public Vector3 moveDirection = Vector3.zero;
    private MouseLook mouseLook;
    private Camera myCamera;
    private HeroInfo heroInfo;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        myTrans = this.transform;
        myCamera = GetComponentInChildren<Camera>();
        mouseLook = new MouseLook(XSensitivity, YSensitivity);
        mouseLook.Init(myTrans, myCamera.transform);
        currentXS = XSensitivity;
        currentYS = YSensitivity;
        heroInfo = GetComponent<HeroInfo>();
    }
    private void Update()
    {
        bool lockMouse = heroInfo.state == HeroState.Normal || heroInfo.state == HeroState.UltimateSkill;
        //转向
        RotateView(lockMouse);
        if (lockMouse)
        {
            //移动
            if (controller.isGrounded)
            {
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= speed;
                if (Input.GetButton("Jump"))
                    moveDirection.y = jumpSpeed;
            }
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        }
        

    }
    /// <summary>
    /// 旋转视角
    /// </summary>
    private void RotateView(bool lockMouse)
    {
        mouseLook.LookRotation(myTrans, myCamera.transform, lockMouse);
    }
    /// <summary>
    /// 修改鼠标灵敏度
    /// </summary>
    private void CheckSensitivity()
    {
        if(Mathf.Abs(XSensitivity - currentXS) >= 0.001f)
        {
            mouseLook.XSensitivity = XSensitivity;
            currentXS = XSensitivity;
        }
        if(Mathf.Abs(YSensitivity - currentYS) >= 0.001f)
        {
            mouseLook.YSensitivity = YSensitivity;
            currentYS = YSensitivity;
        }
    }
}
