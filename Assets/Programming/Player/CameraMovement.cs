using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    #region Variables 
    public float mouseSensitivity;
    private float auxSensitivity;
    public Vector2 camInput;
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
        camInput = new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
        player.Rotate(0f, camInput.x * mouseSensitivity * Time.deltaTime, 0f);
        transform.localRotation = Quaternion.AngleAxis(transform.rotation.eulerAngles.x + camInput.y * mouseSensitivity * Time.deltaTime, Vector3.right);
    }

}
