using UnityEngine;
using TMPro;
using System.Collections.Generic;

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
    public List<Ingredients[]> products = new List<Ingredients[]>();
    public List<List<Ingredients[]>> orders = new List<List<Ingredients[]>>();
    public TextMeshProUGUI orderText;
    private System.Random ing;
    #endregion

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            numberOfProducts = new System.Random().Next(1, maxNumberOfProducts);
            for (int i = 0; i < numberOfProducts; i++) {
                GenerateOrder();
            }
        }
    }

    private void GenerateOrder() {
        orders.Add(GenerateProduct());
    }

    private List<Ingredients[]> GenerateProduct () {
        products.Add(GenerateGordita());
        orderText.SetText("");
        for (int i = 0; i < products.Count; i++) {
            orderText.text += "\nProducto: ";
            var item = products[i];
            for (int j = 0; j < item.Length; j++) {
                orderText.text += item.GetValue(j) + " ";
            }
        }
        return products;
    }

    private Ingredients[] GenerateGordita () {
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

}
