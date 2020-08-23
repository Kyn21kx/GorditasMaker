using Assets.Programming;
using Assets.Programming.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class IngredientID : MonoBehaviour {

    public Product.Ingredients ingredientType;
    public LayerMask targetLayerMask;
    public bool Stitched { get; private set; }
    private Rigidbody attachedRig;
    private void Start() {
        Stitched = false;
    }

    private void Update() {
        StitchTogether();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.tag == "Pickup") {
            attachedRig = collision.transform.GetComponent<Rigidbody>();
            Stitched = true;
        }
    }

    private void StitchTogether () {
        //Stitch and shit
        
    }
}
