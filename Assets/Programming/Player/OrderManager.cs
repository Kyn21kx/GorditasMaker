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
    private int orderIndex = 0;
    private List<Ingredients[]> products = new List<Ingredients[]>();
    public bool generating;
    private Ingredients[] ingredients;
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
            if (tmr >= 5f) {
                tmr = 0f;
                StartCoroutine(Ingredient_Generation());
            }
        }
    }



    private IEnumerator Ingredient_Generation() {
        generating = true;
        int n_Products = new System.Random().Next(1, maxNumberOfProducts);
        for (int j = 0; j < n_Products; j++) {
            System.Random ingNum = new System.Random();
            ing = new System.Random();
            var values = System.Enum.GetValues(typeof(Ingredients));
            repeatedValues.Clear();
            ingredients = new Ingredients[ingNum.Next(1, System.Enum.GetNames(typeof(Ingredients)).Length)];
            for (int i = 0; i < ingredients.Length; i++) {
                yield return new WaitForSecondsRealtime(0.2f);
                int init = ing.Next(ingredients.Length);
                yield return new WaitForSecondsRealtime(0.2f);
                if (!repeatedValues.Contains(init)) {
                    var newIng = (Ingredients)values.GetValue(init);
                    yield return new WaitForSecondsRealtime(0.2f);
                    ingredients.SetValue(newIng, i);
                    yield return new WaitForSecondsRealtime(0.2f);
                    repeatedValues.Add(init);
                }
                else {
                    i--;
                }
            }
            products.Add(ingredients);
            yield return new WaitForSecondsRealtime(0.2f);
        }
        orders.Add(products);
        ViewOrders();
        generating = false;
    }

    private void ViewOrders() {
        for (int i = 0; i < orders.Count; i++) {

        }
        foreach (var item in products) {
            orderText.text += "\nProducto: ";
            foreach (var ingr in item) {
                orderText.text += ingr + ", ";
            }
        }
        //products.Clear();
    }

}
