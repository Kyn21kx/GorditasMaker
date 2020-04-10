using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class OrderManager : MonoBehaviour {
    //Set up orders by a minimum number of ingredients, core ingredients, and types of food
    public enum Ingredients { Frijoles, Chicharron, Queso, Papas, Crema, Guacamole, Salsa };
    //public enum FoodType {Gordita, Taco};
    #region Variables
    public int numberOfIngredientes;
    private int numberOfProducts;
    public int maxNumberOfProducts;
    [SerializeField]
    List<int> repeatedValues = new List<int>();
    public List<List<Ingredients[]>> orders = new List<List<Ingredients[]>>();
    public TextMeshProUGUI orderText;
    private System.Random ing;
    [SerializeField]
    private int orderIndex = -1;
    #endregion
    /*
     * TO DO SUPER IMPORTAAAAAAAAAAANT
     * Replace the use of global variables for the return of every method, because the functions are returning literally all of the values on the arrays
     */
    float tmr = 0f;
    private void Update() {
        //Replace input event for when a customer walks in to ask for an order
        tmr += Time.deltaTime;
        if (tmr >= 5f) {
            repeatedValues.Clear();
            StartCoroutine(GenerateOrder());
            tmr = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            orderIndex++;
            ViewOrders();
        }
    }

    private IEnumerator GenerateOrder() {
        numberOfProducts = new System.Random().Next(1, maxNumberOfProducts);
        List<Ingredients[]> products = new List<Ingredients[]>();
        for (int i = 0; i < numberOfProducts; i++) {
            orders.Add(GenerateProduct(products));
            yield return new WaitForFixedUpdate();
        }
    }

    private List<Ingredients[]> GenerateProduct(List<Ingredients[]> p) {
        p.Add(GenerateGordita());
        return p;
    }

    private Ingredients[] GenerateGordita() {
        //Return the array of the ingredients selected
        System.Random ingNum = new System.Random();
        ing = new System.Random();
        var values = System.Enum.GetValues(typeof(Ingredients));
        Ingredients[] ingredients;
        ingredients = new Ingredients[ingNum.Next(1, System.Enum.GetNames(typeof(Ingredients)).Length)];
        repeatedValues.Clear();
        for (int i = 0; i < ingredients.Length; i++) {
            int init = ing.Next(ingredients.Length);
            if (!repeatedValues.Contains(init)) {
                var newIng = (Ingredients)values.GetValue(init);
                ingredients.SetValue(newIng, i);
                repeatedValues.Add(init);
            }
            else {
                i--;
            }
        }
        return ingredients;
    }

    private void ViewOrders() {
        orderText.text += "\nOrder #" + (orderIndex + 1) + ": "; 
        for (int j = 0; j < orders[orderIndex].Count; j++) {
            //Iterating products
            orderText.text += "Product #" + (j + 1) + ": ";
            for (int k = 0; k < orders[orderIndex][j].Length; k++) {
                //Iterating ingredients
                orderText.text += orders[orderIndex][j][k] + " ";
            }
        }
        //products.Clear();
    }

}
