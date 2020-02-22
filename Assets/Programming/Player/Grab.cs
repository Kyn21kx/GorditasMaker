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
    #endregion

    private void Start() {
        grabbed = false;
        cam = Camera.main;
    }

    private void Update() {
        GrabObject();
    }
    float disToObj = 0f;
    Rigidbody rg = null;
    private void GrabObject () {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, grabbingDistance)) {
            if (hit.transform != null && hit.transform.CompareTag("Pickup")) {
                if (Input.GetMouseButtonDown(1)) {
                    disToObj = Vector3.Distance(hit.transform.position, cam.transform.position);
                    obj = hit.transform;
                    rg = obj.GetComponent<Rigidbody>();
                    rg.angularVelocity *= Vector2.zero;
                    grabbed = true;
                }
                
            }
        }
        if (grabbed) {
            rg.useGravity = false;
            rg.isKinematic = false;
            rg.angularVelocity = Vector3.Cross(rg.angularVelocity, Vector3.zero);
            //Idea del orbe gravitatorio que tiene como child al objeto
            rg.MovePosition(Vector3.Lerp(obj.position, cam.transform.position + cam.transform.forward * disToObj, Time.deltaTime * movingSpeed));
            disToObj = Mathf.Clamp(disToObj, 1f, grabbingDistance);
            if (Input.mouseScrollDelta.y > 0) {
                disToObj+= 0.5f;
            }
            else if (Input.mouseScrollDelta.y < 0) {
                disToObj -= 0.5f;
            }
            if (Input.GetMouseButtonUp(1)) {
                grabbed = false;
                rg.useGravity = true;
                rg.velocity += 5f * new Vector3(cam.GetComponent<CameraMovement>().camInput.x, cam.GetComponent<CameraMovement>().camInput.y, rg.velocity.z);
            }
            Vector2 inputRot = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (inputRot.x != 0f) {
                obj.Rotate(inputRot.x * Time.deltaTime * 300f, 0f, 0f);
            }
            if (inputRot.y != 0f) {
                obj.Rotate(0f, -inputRot.y * Time.deltaTime * 300f, 0f);
            }
        }
        Debug.DrawRay(cam.transform.position, cam.transform.forward * grabbingDistance);
    }

    private void OnCollisionStay(Collision collision) {
        if (grabbed) {

         
        }
    }

}
