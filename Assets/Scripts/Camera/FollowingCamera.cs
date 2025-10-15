using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    private Transform target;
    private Vector3 cameraOffset = new Vector3(1, 2.5f, -8.5f);
    private float mouseSensitivity = 3.0f;
    private float zoomSpeed = 2f;
    private float minZoom = 3f;
    private float maxZoom = 15f;
    private float yaw = 0; // goc xoay trai - phai, xoay theo truc y (truc dung)
    private float pitch = 12; // goc xoay len xuong, xoay theo truc x (truc ngang x)
    private float minPitch = -20;
    private float maxPitch = 60;

    private float followThreshold = 0.5f * 0.5f; // Khoang cach toi thieu de bat dau di chuyen
    private Vector3 vectorDistance;
    private Vector3 lastTargetPostion;

    public static FollowingCamera Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public void Init()
    {
        target = PlayerManager.Instance.PlayerBase.transform;
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
        if (target != null)
        {
            lastTargetPostion = target.position;
        }


    }

    private void LateUpdate()
    {
        GetZoomInfor();
        GetRotateInfor();
        FollowTarget();
    }

    private void GetZoomInfor()
    {
        // Zoom bang chuot giua
        float scroll = InputManager.Instance.GetZoomFromMouseInput();
        if (Mathf.Abs(scroll) > 0.01f)
        {
            cameraOffset.z += scroll * zoomSpeed;
            cameraOffset.z = Mathf.Clamp(cameraOffset.z, -maxZoom, -minZoom);
        }
    }

    private void GetRotateInfor()
    {
        // Chi xoay khi nhan Ctrl
        if (InputManager.Instance.GetControlInput())
        {
            Vector2 input = InputManager.Instance.GetRotateFromMouseInput();
            yaw += input.x * mouseSensitivity;
            pitch -= input.y * mouseSensitivity;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        }
    }

    private void FollowTarget()
    {
        vectorDistance = target.position - lastTargetPostion;
        if (vectorDistance.sqrMagnitude > followThreshold)
        {
            float moveSpeed = Mathf.Clamp(vectorDistance.magnitude, 0.1f, 10f);
            lastTargetPostion = Vector3.MoveTowards(lastTargetPostion, target.position, moveSpeed * Time.deltaTime);
        }
        else
        {   // CAI NAY RAT HAY: Neu khong co cai nay, thi Camera se van hoi di chuyen 
            // mot chut khi don vi dung yen, do no chay trong LateUpdate, con don vi chay trong Update
            lastTargetPostion = target.position;
        }
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 desiredPosition = lastTargetPostion + rotation * cameraOffset;
        transform.position = desiredPosition;
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }

    public Vector3 ConvertVectorAsCameraCordination(Vector3 input)
    {
        // Chuyen doi input theo huong nhin trai phai tu Camera. Rat quan trong
        Vector3 camForward = Instance.transform.forward;
        camForward.y = 0;
        camForward.Normalize();
        Vector3 camRight = Instance.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        // Tinh huong di chuyen theo camera
        return (camForward * input.z + camRight * input.x).normalized;

        ////Lay theo toa do dia phuong cua nhan vat. Nay khong dung nua
        //Vector3 moveDir = input.sqrMagnitude > 0.01f ? input.normalized : Vector3.zero;
        //return moveDir;
    }
}
