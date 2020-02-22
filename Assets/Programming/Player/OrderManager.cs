using UnityEngine;
using System.Collections.Generic;

public class OrderManager : MonoBehaviour
{
    //Set up orders by a minimum number of ingredients, core ingredients, and types of food
    public enum Ingredients {Frijoles, Chicharron, Queso, Papas, Crema, Guacamole, Salsa};
    //public enum FoodType {Gordita, Taco};
    #region Variables
    public int numberOfIngredientes;
    [SerializeField]
    Ingredients[] ingredients;
    [SerializeField]
    List<int> repeatedValues = new List<int>();
    #endregion

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            GenerateOrder();
        }
    }

    private void GenerateOrder () {
        //Return the array of the ingredients selected
        System.Random ingNum = new System.Random();
        System.Random ing = new System.Random();
        numberOfIngredientes = ingNum.Next(1, System.Enum.GetNames(typeof(Ingredients)).Length);
        var values = System.Enum.GetValues(typeof(Ingredients));
        ingredients = new Ingredients[numberOfIngredientes];
        repeatedValues.Clear();
        for (int i = 0; i < ingredients.Length; i++) {
            int init = ing.Next(ingredients.Length);
            if (!repeatedValues.Contains(init)) {
                var newIng = (Ingredients)values.GetValue(init);
                ingredients.SetValue(newIng, i);
                repeatedValues.Add(init);
                Debug.Log(init);
            }
            else {
                i--;
            }
        }
    }

}
