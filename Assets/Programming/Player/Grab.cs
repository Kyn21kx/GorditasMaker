using Assets.Programming;
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
        _Input();
        GrabObject();
    }

    private void FixedUpdate() {
        MoveObject();
    }

    private void _Input () {
        if (grabbed) {
            Vector2 inputRot = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            //Change rotation to be managed with 
            disToObj = Mathf.Clamp(disToObj, 0.5f, grabbingDistance);
            if (Input.mouseScrollDelta.y > 0) {
                disToObj += 0.5f;
            }
            else if (Input.mouseScrollDelta.y < 0) {
                disToObj -= 0.5f;
            }
            if (Input.GetMouseButtonUp(1)) {
                grabbed = false;
                rg.useGravity = true;
            }
            if (inputRot != Vector2.zero) {
                rg.angularVelocity = Vector3.zero;
                obj.Rotate(inputRot.x * Time.deltaTime * 300f, inputRot.y * Time.deltaTime * 300f, 0f);
            }
        }
        
    }

    private void MoveObject () {
        if (grabbed) {
            rg.useGravity = false;
            rg.isKinematic = false;
            PhysicsOperations.LerpVelocity(ref rg, (cam.transform.position + cam.transform.forward * disToObj - rg.position), movingSpeed);
        }
    }

    private void GrabObject () {
        //Setup layermask to make sure that you cannot grab between collisions
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
