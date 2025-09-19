using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : UnitController
{
    private PlayerMoveComponent playerMove;


    private float currentSpeed = 0f;
    private Vector3 currentDir = Vector3.zero;
    private Vector3 lastDir = Vector3.zero;

    // Cac bien tam thoi
    [SerializeField] private Camera mainCamera;


    private void Awake()
    {
        base.Awake();
        playerMove = moveComponent as PlayerMoveComponent;
    }

    protected override void HandleIdle()
    {
        

    }


    //private bool GetDirectionFromDevices(out Vector3 direction, out float speedIntensity)
    //{
    //    Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) +
    //    new Vector3(joystick.Horizontal, 0, joystick.Vertical);
    //    if (input.sqrMagnitude < inputVectorSqrMin)
    //    {
    //        direction = Vector3.zero;
    //        speedIntensity = 0f;
    //        return false;
    //    }
    //    speedIntensity = Mathf.Clamp01(input.magnitude);
    //    direction = ConvertInputToDirectionByCamera(input);
    //    return true;
    //}

    private Vector3 ConvertInputToDirectionByCamera(Vector3 input)
    {
        // Chuyen doi input theo huong nhin trai phai tu Camera. Rat quan trong
        Vector3 camForward = mainCamera.transform.forward;
        camForward.y = 0;
        camForward.Normalize();
        Vector3 camRight = mainCamera.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        // Tinh huong di chuyen theo camera
        return (camForward * input.z + camRight * input.x).normalized;

        ////Lay theo toa do dia phuong cua nhan vat. Nay khong dung nua
        //Vector3 moveDir = input.sqrMagnitude > 0.01f ? input.normalized : Vector3.zero;
        //return moveDir;
    }

}
