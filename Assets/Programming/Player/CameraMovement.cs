using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    #region Variables 
    public float mouseSensitivity;
    private float auxSensitivity;
    private Vector2 camInput;
    private Transform player;
    #endregion

    private void Start() {
        auxSensitivity = mouseSensitivity;
        Cursor.lockState = CursorLockMode.Locked;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update() {
        LookAround();
    }

    private void LookAround () {
        camInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")).normalized;
        transform.Rotate(-camInput.y * mouseSensitivity * Time.deltaTime, 0f, 0f);
        Quaternion rot = new Quaternion(Mathf.Clamp(transform.rotation.x, -0.6f, 0.6f), transform.rotation.y, transform.rotation.z, transform.rotation.w);
        transform.rotation = rot;
        player.Rotate(0f, camInput.x * mouseSensitivity * Time.deltaTime, 0f);
        Debug.Log(transform.rotation.x);
    }

}
