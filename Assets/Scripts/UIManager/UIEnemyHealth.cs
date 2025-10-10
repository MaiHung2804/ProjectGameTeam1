using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEnemyHealth : MonoBehaviour
{
    public Transform camera;
    public HealthCompentTest healthCompentTest;


    // Update is called once per frame


    private void LateUpdate()
    {
        transform.LookAt(transform.position + camera.forward);

    }
}
