using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    #region Variables
    Camera cam;
    public float grabbingDistance;
    #endregion

    private void Update() {
        cam = Camera.main;
        GrabObject();
    }

    private void GrabObject () {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, grabbingDistance)) {
            if (hit.transform != null) {
                Debug.Log(hit.transform.name);
            }
        }
        Debug.DrawRay(cam.transform.position, cam.transform.forward * grabbingDistance);
    }
}
