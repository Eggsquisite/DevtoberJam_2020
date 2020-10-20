using System.Collections.Generic;
using UnityEngine;

public class Potion {
    
    public string potion_name;
    public List<WeightedAttribute> attributes = new List<WeightedAttribute>();

    public Potion() { }

    public Potion(string potion_name) {
        this.potion_name = potion_name;
    }

    public void AddAttribute(WeightedAttribute attribute) {
        attributes.Add(attribute);
    }

    public override string ToString() {
        string s = potion_name;
        for (int i = 0; i < attributes.Count; i++) {
            s += ("\n" + attributes[i].attribute.attribute_name + ": " + attributes[i].weight);
        }

        return s;
    }
}

public struct PotionSolution {
    public Potion potion { get; set; }
    public ushort payment { get; set; }
    public bool isDrunkImmediately { get; set; }
    public string comment { get; set; }
}
