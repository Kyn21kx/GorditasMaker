using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacing : MonoBehaviour
{
    [SerializeField]
    private float radius;
    public bool inRadius;
    [SerializeField]
    private float returnSpeed;
    [SerializeField]
    private float proximitySnap;
    [Header("Object that will be dragged to this")]
    [SerializeField]
    private Rigidbody objRig;
    private Grab grabRef;
    private float dis;

    private void Start() {
        grabRef = GameObject.FindGameObjectWithTag("Player").GetComponent<Grab>();
    }

    private void Update() {
        if (!grabRef.grabbed) {
            RadiusBehaviour();
        }
    }

    private void RadiusBehaviour () {
        dis = Vector3.Distance(transform.position, objRig.transform.position);
        if (dis <= radius) {
            inRadius = true;
        }
        if (dis > radius) {
            inRadius = false;
            objRig.isKinematic = false;
        }
        if (inRadius) {
            objRig.isKinematic = true;
            objRig.transform.rotation = Quaternion.Lerp(objRig.transform.rotation, Quaternion.Euler(new Vector3(-90f, 0f, 0f)), Time.deltaTime * returnSpeed);
            objRig.transform.position = Vector3.Slerp(objRig.transform.position, transform.position, Time.deltaTime * returnSpeed);
            if (dis < radius / proximitySnap) {
                objRig.transform.position = transform.position;
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
