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
    private Ingredients[] ingredients;
    private List<Ingredients[]> products = new List<Ingredients[]>();
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
            tmr = 0f;
            StartCoroutine(GenerateOrder());
        }
    }

    private IEnumerator GenerateOrder() {
        numberOfProducts = new System.Random().Next(1, maxNumberOfProducts);
        System.Random ingNum = new System.Random();
        ing = new System.Random();
        var values = System.Enum.GetValues(typeof(Ingredients));
        repeatedValues.Clear();
        yield return new WaitForFixedUpdate();
        //For controlling order generation
        for (int k = 0; k < numberOfProducts; k++) {
            //For controlling product generation
            for (int j = 0; j < numberOfProducts; j++) {
                //For controlling ingredient generation
                ingredients = new Ingredients[ingNum.Next(1, System.Enum.GetNames(typeof(Ingredients)).Length)];
                for (int i = 0; i < ingredients.Length; i++) {
                    yield return new WaitForFixedUpdate();
                    int init = ing.Next(ingredients.Length);
                    if (!repeatedValues.Contains(init)) {
                        var newIng = (Ingredients)values.GetValue(init);
                        yield return new WaitForFixedUpdate();
                        ingredients.SetValue(newIng, i);
                        yield return new WaitForFixedUpdate();
                        repeatedValues.Add(init);
                    }
                    else {
                        i--;
                    }
                }
                //Here the ingredients are finished
                yield return new WaitForFixedUpdate();
                products.Add(ingredients);
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForFixedUpdate();
            orders.Add(products);
            yield return new WaitForFixedUpdate();
            ViewOrders();
            orderIndex++;
        }
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
