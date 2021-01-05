using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour {

    #region Variables
    [SerializeField]
    private float speed = 1f;
    private Rigidbody rig;
    private float auxHeight;
    public bool canMove;
    public bool Crouched { get; private set; }
    #endregion

    private void Start() {
        rig = GetComponent<Rigidbody>();
        auxHeight = transform.localScale.y;
        canMove = true;
    }


    private void Update() {
        if (canMove)
            Move();
        _Input();
    }

    private void _Input() {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !Crouched)
            Crouch();
        else if (Input.GetKeyDown(KeyCode.LeftControl) && Crouched)
            StandUp();
    }


    private void Move() {
        Vector3 dir = transform.forward * InputManager.MovementInput.y + transform.right * InputManager.MovementInput.x;
        rig.MovePosition(transform.position + dir * speed * Time.deltaTime);
    }

    private void Crouch() {
        transform.localScale = new Vector3(transform.localScale.x, 0.6f, transform.localScale.z);
        Crouched = true;
    }

    private void StandUp() {
        transform.localScale = new Vector3(transform.localScale.x, auxHeight, transform.localScale.z); 
        Crouched = false;
    }
    
}
