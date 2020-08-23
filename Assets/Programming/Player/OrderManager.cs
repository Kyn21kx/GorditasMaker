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
    public int maxNumberOfProducts;
    public TextMeshProUGUI orderText;
    public GameObject orderPanel;
    public bool generating;
    List<Order> orders;
    #endregion
    /*
     * TO DO SUPER IMPORTAAAAAAAAAAANT
     * Replace the use of global variables for the return of every method, because the functions are returning literally all of the values on the arrays
     */
    [SerializeField]
    float tmr = 0f;

    private void Start() {
        orders = new List<Order>();
        generating = false;
        tmr += generationTime / 1.5f;
    }

    private void Update() {
        //Replace input event for when a customer walks in to ask for an order
        if (!generating) {
            tmr += Time.deltaTime;
            if (tmr >= generationTime) {
                orders.Add(new Order());
                tmr = 0f;
            }
        }
        if (Input.GetKeyDown(KeyCode.F)) {
            Camera.main.GetComponent<CameraMovement>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            DisplayOrders();
            orderPanel.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.F)) {
            Camera.main.GetComponent<CameraMovement>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            orderPanel.SetActive(false);
        }
    }

    private void DisplayOrders () {
        string concat = "";
        for (int i = 0; i < orders.Count; i++) {
            concat += "Orden #" + (i + 1) + ":\n" + orders[i].ToString() + "\n";
        }
        orderText.text = concat;
    }

}
