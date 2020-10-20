using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DataLoader : MonoBehaviour {
    
    private static StreamReader inputFile;
    private static string line;
    
    public static List<Potion> potions = new List<Potion>();
    public static List<Attribute> goodAttributes = new List<Attribute>();
    public static List<Attribute> badAttributes = new List<Attribute>();

    void Start() {
        
    }

    public static void LoadDataFromFile() {
        inputFile = new StreamReader("Assets/DialogueData/PotionDialogues.txt");
        int counter = 0;
        while ((line = inputFile.ReadLine()) != null) {
            if (line.Contains("//")) line = line.Split('/')[0];
            //Debug.Log(line);
            while ((line != null) && line.Contains("[") && line.Contains("]")) {
                if (line.Contains("GoodAttributes")) LoadAttributes(true);
                else if (line.Contains("BadAttributes")) LoadAttributes(false);
                else if (line.Contains("Potion")) LoadPotion();
                else line = inputFile.ReadLine();
            }

            counter++;
            if (counter == 5000) break;
        }

        Debug.Log("We found " + potions.Count + " potions");
        for (int i = 0; i < potions.Count; i++) {
            Debug.Log(potions[i]);
        }

        Debug.Log("We found " + goodAttributes.Count + " attributes");
        for (int i = 0; i < goodAttributes.Count; i++) {
            Debug.Log(goodAttributes[i].attribute_name);
        }
        
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

                    Attribute? a = null;
                    for (int j = 0; j < goodAttributes.Count; j++) {
                        if (attribute == goodAttributes[j].attribute_name) { a = goodAttributes[j]; break; }
                    }

                    if (a == null) {
                        for (int j = 0; j < badAttributes.Count; j++) {
                            if (attribute == badAttributes[j].attribute_name) { a = badAttributes[j]; break; }
                        }
                    }
                    
                    if (a == null) Debug.LogError("Error: The attribute '" + attribute + "' from potion '" + potion.potion_name + "' could not be " +
                                                  "located in the database.  Please check the data file.");
                    
                    WeightedAttribute wa = new WeightedAttribute((Attribute) a, strength);
                    potion.AddAttribute(wa);
                }
            }
        }
        potions.Add(potion);
    }
}
