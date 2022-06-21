using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public float MouseLookSpeed = 15f;
    public Transform Player;

    Vector2 rotation = Vector2.zero;
    //float xRotation = 0f;
    //float yRotation = 0f;
    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {

        rotation.x += Input.GetAxis("Mouse X") * MouseLookSpeed * Time.deltaTime;
        rotation.y += Input.GetAxis("Mouse Y") * MouseLookSpeed * Time.deltaTime;
        rotation.y = Mathf.Clamp(rotation.y,-90,90);

        var xQuat = Quaternion.AngleAxis(rotation.x,Vector3.up);
        var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

        //transform.rotation = xQuat * yQuat;
        Player.rotation = xQuat * yQuat;
        

    }
}
