using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Try : MonoBehaviour
{
    public float ver;
    public float hor;
    public Vector3 direction;
    public bool isMove = false;
    public CharacterController controller;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        ver = Input.GetAxis("Vertical");
        hor = Input.GetAxis("Horizontal");
        if (isMove)
        {
            controller.Move(direction * Time.deltaTime);
        }
    }
    private void OnGUI()
    {
        if (GUILayout.Button("shift"))
        {
            if (!isMove)
            {
                direction = this.transform.forward;
                if (ver < 0)
                {
                    direction = -direction;
                }
                if (hor != 0)
                {
                    direction *= ver == 0 ? 0 : 1;
                }
                if (hor > 0)
                {
                    direction = (direction + this.transform.right).normalized;
                }
                else if (hor < 0)
                {
                    direction = (direction - this.transform.right).normalized;
                }
            }
            isMove = !isMove;
        }
    }
}
