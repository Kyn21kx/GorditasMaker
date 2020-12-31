using Assets.Programming;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Grab : MonoBehaviour {
    #region Variables
    Camera cam;
    public float grabbingDistance;
    public float movingSpeed;
    public bool Grabbed { get; private set; }
    [SerializeField]
    private bool rotating;
    private Transform obj;
    private float disToObj = 0f;
    private Rigidbody rg = null;
    private TextMeshProUGUI interactionText;
    private Movement movRef;
    [SerializeField]
    LayerMask targetMask;
    #endregion

    private void Start() {
        interactionText = GameObject.Find("GameManager").GetComponent<GameManager>().canvasUI.transform.Find("InteractionText").GetComponent<TextMeshProUGUI>();
        interactionText.gameObject.SetActive(false);
        Grabbed = false;
        rotating = false;
        movRef = GetComponent<Movement>();
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
        if (Grabbed) {
            //Change rotation to be managed with 
            disToObj = Mathf.Clamp(disToObj, 0.5f, grabbingDistance / 2.5f);
            if (Input.mouseScrollDelta.y > 0) {
                disToObj += 0.5f;
            }
            else if (Input.mouseScrollDelta.y < 0) {
                disToObj -= 0.5f;
            }
            if (Input.GetMouseButtonUp(1)) {
                Grabbed = false;
                rg.useGravity = true;
            }

            if (!rotating && Input.GetKeyDown(KeyCode.R)) {
                rotating = true;
                movRef.canMove = false;
            }
            else if (rotating && Input.GetKeyDown(KeyCode.R)) {
                rotating = false;
                movRef.canMove = true;
            
            }

            if (InputManager.MovementInput != Vector2.zero && rotating) {
                rg.angularVelocity = Vector3.zero;
                obj.Rotate(InputManager.MovementInput.x * Time.deltaTime * 300f, InputManager.MovementInput.y * Time.deltaTime * 300f, 0f);
            }
        }
        
    }

    private void MoveObject () {
        if (Grabbed) {
            rg.useGravity = false;
            rg.isKinematic = false;
            PhysicsOperations.LerpVelocity(ref rg, (cam.transform.position + cam.transform.forward * disToObj - rg.position), movingSpeed);
        }
    }

    private void GrabObject () {
        //Setup layermask to make sure that you cannot grab between collisions
        RaycastHit item;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out item, grabbingDistance, targetMask)) {
            interactionText.gameObject.SetActive(true);
            interactionText.SetText("[E]");
            if (Input.GetMouseButtonDown(1)) {
                disToObj = Vector3.Distance(item.transform.position, cam.transform.position);
                obj = item.transform;
                rg = obj.GetComponent<Rigidbody>();
                rg.angularVelocity *= Vector2.zero;
                Grabbed = true;
            }
        }
        else if (!Grabbed)
            interactionText.gameObject.SetActive(false);
        Debug.DrawRay(cam.transform.position, cam.transform.forward * grabbingDistance);
    }
}
