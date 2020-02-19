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
    private void GrabObject () {
        RaycastHit hit;
        Vector3 prevPos = Vector3.zero;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, grabbingDistance)) {
            if (hit.transform != null && hit.transform.CompareTag("Pickup")) {
                if (Input.GetMouseButtonDown(1)) {
                    disToObj = Vector3.Distance(hit.transform.position, cam.transform.position);
                    prevPos = hit.transform.position;
                    obj = hit.transform;
                    grabbed = true;
                }
                
            }
        }
        if (grabbed) {
            obj.GetComponent<Rigidbody>().useGravity = false;
            obj.position = Vector3.Lerp(obj.position, cam.transform.position + cam.transform.forward * disToObj, Time.deltaTime * movingSpeed);
            disToObj = Mathf.Clamp(disToObj, 0.2f, grabbingDistance);
            if (Input.mouseScrollDelta.y > 0) {
                disToObj++;
            }
            else if (Input.mouseScrollDelta.y < 0) {
                disToObj--;
            }
            if (Input.GetMouseButtonUp(1)) {
                grabbed = false;
                obj.GetComponent<Rigidbody>().useGravity = true;
            }
        }
        Debug.DrawRay(cam.transform.position, cam.transform.forward * grabbingDistance);
    }
}
