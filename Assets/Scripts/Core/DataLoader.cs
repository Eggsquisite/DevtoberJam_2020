using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataLoader {
    
    private static StreamReader inputFile;
    private static string line;
    
    private static List<Potion> potions = new List<Potion>();
    private static List<Ingredient> ingredients = new List<Ingredient>();
    private static List<Attribute> goodAttributes = new List<Attribute>();
    private static List<Attribute> badAttributes = new List<Attribute>();
    
    public static void LoadDataFromFile() {

        potions = new List<Potion>();
        Potion.potions = potions;
        ingredients = new List<Ingredient>();
        Ingredient.ingredients = ingredients;
        goodAttributes = new List<Attribute>();
        Attribute.goodAttributes = goodAttributes;
        badAttributes = new List<Attribute>();
        Attribute.badAttributes = badAttributes;
        
        
        inputFile = new StreamReader("Assets/Scripts/Core/Input.txt");

        while ((line = inputFile.ReadLine()) != null) {
            if (line.Contains("//")) line = line.Split('/')[0];
            while ((line != null) && line.Contains("[") && line.Contains("]")) {
                if (line.Contains("GoodAttributes")) LoadAttributes(true);
                else if (line.Contains("BadAttributes")) LoadAttributes(false);
                else if (line.Contains("Potion")) LoadPotion();
                else if (line.Contains("Ingredients")) LoadIngredients();
                else if (line.Contains("StartingInventory")) LoadInventory();
                else line = inputFile.ReadLine();
            }
        }

        String s = "We loaded the following: ";

        s += ("\n\n" + potions.Count + " potions:");
        for (int i = 0; i < potions.Count; i++) {
            s += ("\n" + potions[i]);
        }
        
        s += ("\n\n" + ingredients.Count + " ingredients:");
        for (int i = 0; i < ingredients.Count; i++) {
            //s += ("\n" + ingredients[i].ingredient_name);
            s += "\n" + ingredients[i];
        }

        s += ("\n\n" + goodAttributes.Count + " good attributes:");
        for (int i = 0; i < goodAttributes.Count; i++) {
            s += ("\n" + goodAttributes[i].attribute_name);
        }
        
        s += ("\n\n" + badAttributes.Count + " bad attributes:");
        for (int i = 0; i < badAttributes.Count; i++) {
            s += ("\n" + badAttributes[i].attribute_name);
        }
        
        s += ("\n\n" + "Inventory:\n");
        s += Inventory.ToString();

        Debug.Log(s);

        Potion.potions = potions;
        Ingredient.ingredients = ingredients;
        Attribute.goodAttributes = goodAttributes;
        Attribute.badAttributes = badAttributes;
        
        inputFile.Close();
    }

    private static void LoadAttributes(bool good) {
        while (((line = inputFile.ReadLine()) != null) && !line.Contains("[") && !line.Contains("]")) {
            if (line.Contains("//")) line = line.Split('/')[0];
            if (line.Trim().Length > 0) {
                if (good) goodAttributes.Add(new Attribute(line));
                else badAttributes.Add(new Attribute(line));
            }
        }
    }

    private static void LoadPotion() {
        //Debug.Log("LOADING POTION");
        Potion potion = new Potion();
        
        while (((line = inputFile.ReadLine()) != null) && !line.Contains("[") && !line.Contains("]")){
            if (line.Contains("//")) line = line.Split('/')[0];
            if (line.Contains("Name")) potion.potion_name = line.Split('=')[1].Trim();
            else if (line.Contains("Attribute")) {
                line = line.Split('=')[1];
                string[] s = line.Split(',');

                for (int i = 0; i < s.Length; i++) {
                    string[] s2 = s[i].Trim().Split(' ');
                    byte strength = Byte.Parse(s2[0]);
                    string attribute = s2[1];

                    
                    Attribute a = Attribute.FindAttribute(attribute);
                    if (a == null) Debug.LogError("Error: The attribute '" + attribute + "' from potion '" + potion.potion_name + "' could not be " +
                                                  "located in the database.  Please check the data file.");
                    
                    WeightedAttribute wa = new WeightedAttribute((Attribute) a, strength);
                    potion.AddAttribute(wa);
                }
            }
            else if (line.Contains("Ingredients")) {
                line = line.Split('=')[1];
                string[] s = line.Split(',');

                
                for (int i = 0; i < s.Length; i++) {
                    s[i] = s[i].Trim();
                    int index = 0;
                    string quantity = "";
                    for (int j = 0; j < s[i].Length; j++) {
                        if (s[i][j] == ' ') { index = j; break; }
                        quantity += s[i][j];
                    }
                    potion.AddRecipeIngredient(new WeightedIngredient(Ingredient.FindIngredient(s[i].Remove(0,index+1)), ushort.Parse(quantity)));
                }
            }
            else if (line.Contains("Craftable")) {
                line = line.Split('=')[1].Trim();
                if (line.Contains("yes")) potion.isInRecipeBook = true;
            }
        }
        potions.Add(potion);
        if (potion.isInRecipeBook) RecipeBook.potionsInRecipeBook.Add(potion);
    }

    private static void LoadIngredients() {
        while (((line = inputFile.ReadLine()) != null) && !line.Contains("[") && !line.Contains("]")) {
            if (line.Contains("//")) line = line.Split('/')[0];
            if (line.Trim().Length > 0) {
                string[] s = line.Split(',');
                ingredients.Add(new Ingredient(s[0].Trim(), Byte.Parse(s[1].Trim())));
                if (s.Length == 3) {
                    if (s[2].Trim().Contains("illicit")) ingredients[ingredients.Count - 1].illicit = true;
                }
            }
        }
    }

    private static void LoadInventory() {

        while (((line = inputFile.ReadLine()) != null) && !line.Contains("[") && !line.Contains("]")) {
            if (line.Contains("//")) line = line.Split('/')[0];
            if (line.Trim().Length > 0) {
                string[] s = line.Split(',');
                if (s[0].Trim() == ("Money")) Inventory.AddFunds(int.Parse(s[1].Trim()));
                else {
                    string ingredient_name = s[0].Trim();
                    Inventory.AddItem(Ingredient.FindIngredient(ingredient_name),ushort.Parse(s[1].Trim()));
                }
            }
        }
    }
}
