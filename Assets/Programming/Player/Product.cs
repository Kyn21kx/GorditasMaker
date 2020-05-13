using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Programming.Player {
    public class Product {
        public enum Ingredients { Frijoles, Chicharron, Queso, Papas, Crema, Guacamole, Salsa };
        public Ingredients[] ingredients;
        private List<int> repeatedValues = new List<int>();
        public int NumberOfIngredients { get; }
        public Product () {
            Task.Run(async () => {
                await RandomGeneration();
            });
        }
        private async Task RandomGeneration() {
            var values = Enum.GetValues(typeof(Ingredients));
            int numberOfIngredients = new System.Random().Next(1, Enum.GetNames(typeof(Ingredients)).Length);
            ingredients = new Ingredients[numberOfIngredients];
            int init = new System.Random().Next(ingredients.Length);
            for (int i = 0; i < numberOfIngredients; i++) {
                var newIng = (Ingredients)values.GetValue(init);
                if (!repeatedValues.Contains(init)) {
                    ingredients.SetValue(newIng, i);
                    repeatedValues.Add(init);
                }
                else {
                    i--;
                }
            }
        }
    }
}
