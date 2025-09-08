using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    private Vector3 cameraOffset = new Vector3(1, 2.5f, -8);
    private float mouseSensitivity = 3.0f;
    private float zoomSpeed = 2f;
    private float minZoom = 3f;
    private float maxZoom = 15f;
    private float yaw = 0; // goc xoay trai - phai, xoay theo truc y (truc dung)
    private float pitch = 12; // goc xoay len xuong, xoay theo truc x (truc ngang x)
    private float minPitch = -20;
    private float maxPitch = 60;

    private float followThreshold = 0.5f*0.5f; // Khoang cach toi thieu de bat dau di chuyen
    private Vector3 vectorDistance;
    private Vector3 lastTargetPostion;


    void Start()
    {
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
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f)
        {
            cameraOffset.z += scroll * zoomSpeed;
            cameraOffset.z = Mathf.Clamp(cameraOffset.z, -maxZoom, -minZoom);
        }
    }

    private void GetRotateInfor()
    {
        // Chi xoay khi nhan Ctrl
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
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
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 desiredPosition = lastTargetPostion + rotation * cameraOffset;
        transform.position = desiredPosition;
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
