using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class Ingredient : MonoBehaviour {
    public string name;
    public ushort cost;
    public ushort quantity = 0;

    public Ingredient(string name, ushort cost) {
        this.name = name;
        this.cost = cost;
    }

    public void AddOne() {
        quantity++; 
    }

    public void RemoveOne() {
        quantity--;
    }

}

