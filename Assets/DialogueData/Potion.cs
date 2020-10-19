using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class Potion : MonoBehaviour {
    
    public string name;
    public List<Attributes.Attribute> attributes = new List<Attributes.Attribute>();
    public List<byte> attribute_weights = new List<byte>();

    public Potion() { }

    public Potion(string name) {
        this.name = name;
    }

    public void AddAttribute(Attributes.Attribute attribute, byte weight ) {
        attributes.Add(attribute);
        attribute_weights.Add(weight);
    }
}

public struct PotionSolution {
    public Potion potion { get; set; }
    public ushort payment { get; set; }
    public bool isDrunkImmediately { get; set; }
    public string comment { get; set; }
    
    
}
