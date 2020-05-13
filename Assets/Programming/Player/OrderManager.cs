using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using Assets.Programming.Player;

public class OrderManager : MonoBehaviour {
    //Set up orders by a minimum number of ingredients, core ingredients, and types of food
    //public enum FoodType {Gordita, Taco};
    #region Variables
    public float generationTime;
    public int numberOfIngredientes;
    private int numberOfProducts;
    public int maxNumberOfProducts;
    [SerializeField]
    List<int> repeatedValues = new List<int>();
    public TextMeshProUGUI orderText;
    public GameObject orderPanel;
    private System.Random ing;
    private int orderIndex = 0;
    public bool generating;
    Product product;
    #endregion
    /*
     * TO DO SUPER IMPORTAAAAAAAAAAANT
     * Replace the use of global variables for the return of every method, because the functions are returning literally all of the values on the arrays
     */
    float tmr = 0f;

    private void Start() {
        generating = false;
    }

    private void Update() {
        //Replace input event for when a customer walks in to ask for an order
        if (!generating) {
            tmr += Time.deltaTime;
            if (tmr >= generationTime) {
                tmr = 0f;
            }
        }
        if (Input.GetKeyDown(KeyCode.F)) {
            product = new Product();
            Camera.main.GetComponent<CameraMovement>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            orderPanel.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.F)) {
            Camera.main.GetComponent<CameraMovement>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            orderPanel.SetActive(false);
        }
    }
}
