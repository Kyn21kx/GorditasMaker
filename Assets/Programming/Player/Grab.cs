using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    #region Variables
    Camera cam;
    public float grabbingDistance;
    public float movingSpeed;
    public bool grabbed;
    Transform obj;
    float disToObj = 0f;
    Rigidbody rg = null;
    #endregion

    private void Start() {
        grabbed = false;
        cam = Camera.main;
    }

    private void Update() {
        GrabObject();
        MoveObject();
    }


    private void MoveObject () {
        if (grabbed) {
            rg.useGravity = false;
            rg.isKinematic = false;
            //Get mouse position to the screen
            Vector3 grabPos = cam.transform.position + cam.transform.forward * disToObj - rg.position;
            rg.velocity = grabPos * Time.deltaTime * movingSpeed;

            disToObj = Mathf.Clamp(disToObj, 0.5f, grabbingDistance);
            if (Input.mouseScrollDelta.y > 0) {
                disToObj += 0.5f;
            }
            else if (Input.mouseScrollDelta.y < 0) {
                disToObj -= 0.5f;
            }
            if (Input.GetMouseButtonUp(1)) {
                rg.velocity += 3f * new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
                grabbed = false;
                rg.useGravity = true;
            }
            Vector2 inputRot = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (inputRot.x != 0f) {
                rg.angularVelocity = Vector3.zero;
                obj.Rotate(inputRot.x * Time.deltaTime * 300f, 0f, 0f);
            }
            if (inputRot.y != 0f) {
                rg.angularVelocity = Vector3.zero;
                obj.Rotate(0f, -inputRot.y * Time.deltaTime * 300f, 0f);
            }
        }
    }

    private void GrabObject () {
        //Change Raycast to raycast all
        RaycastHit[] hit = Physics.RaycastAll(cam.transform.position, cam.transform.forward, grabbingDistance);
        if (hit != null) {
            foreach (var item in hit) {
                if (item.transform != null && item.transform.CompareTag("Pickup")) {
                    if (Input.GetMouseButtonDown(1)) {
                        disToObj = Vector3.Distance(item.transform.position, cam.transform.position);
                        obj = item.transform;
                        rg = obj.GetComponent<Rigidbody>();
                        rg.angularVelocity *= Vector2.zero;
                        grabbed = true;
                        break;
                    }

                }
            }
        }
        Debug.DrawRay(cam.transform.position, cam.transform.forward * grabbingDistance);
    }
}
