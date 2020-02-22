using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class OrderManager : MonoBehaviour
{
    //Set up orders by a minimum number of ingredients, core ingredients, and types of food
    public enum Ingredients {Frijoles, Chicharron, Queso, Papas, Crema, Guacamole, Salsa};
    //public enum FoodType {Gordita, Taco};
    #region Variables
    public int numberOfIngredientes;
    [SerializeField]
    List<int> repeatedValues = new List<int>();
    public List<Ingredients[]> orders = new List<Ingredients[]>();
    public TextMeshProUGUI orderText;
    #endregion

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            GenerateOrder();
        }
    }

    private void GenerateOrder () {
        orders.Add(GenerateGordita());
        orderText.SetText("");
        for (int j = 0; j < orders.Count; j++) {
            var item = orders[j];
            orderText.text +="\nOrden #" + (j + 1) + ". Deme una gordita con: ";
            for (int i = 0; i < item.Length; i++) {
                orderText.text += (item.GetValue(i) + " ");
            }
        }
    }

    private Ingredients[] GenerateGordita () {
        //Return the array of the ingredients selected
        System.Random ingNum = new System.Random();
        System.Random ing = new System.Random();
        numberOfIngredientes = ingNum.Next(1, System.Enum.GetNames(typeof(Ingredients)).Length);
        var values = System.Enum.GetValues(typeof(Ingredients));
        Ingredients[] ingredients;
        ingredients = new Ingredients[numberOfIngredientes];
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
