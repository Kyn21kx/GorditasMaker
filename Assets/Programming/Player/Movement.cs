using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour {

    #region Variables
    [SerializeField]
    private float speed = 1f;
    private Rigidbody rig;
    public bool canMove;
    #endregion

    private void Start() {
        rig = GetComponent<Rigidbody>();
        canMove = true;
    }

    private void Update() {
        if (canMove)
            Move();
    }

    private void Move() {
        Vector3 dir = transform.forward * InputManager.MovementInput.y + transform.right * InputManager.MovementInput.x;
        rig.MovePosition(transform.position + dir * speed * Time.deltaTime);
    }
    
}
