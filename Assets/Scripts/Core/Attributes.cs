using System.Collections.Generic;

public class Attribute {

    public string attribute_name;
    public static List<Attribute> goodAttributes;
    public static List<Attribute> badAttributes;

    public Attribute(string attribute_name) {
        this.attribute_name = attribute_name;
    }
    
    public static Attribute FindAttribute(string attribute) {
        for (int i = 0; i < goodAttributes.Count; i++) { 
            if (goodAttributes[i].attribute_name == attribute) return goodAttributes[i];
        }
        for (int i = 0; i < badAttributes.Count; i++) { 
            if (badAttributes[i].attribute_name == attribute) return badAttributes[i];
        }

        return null;
    }
}

[System.Serializable]
public struct WeightedAttribute {
    
    public Attribute attribute;
    public byte weight;

    public WeightedAttribute(Attribute attribute, byte weight) {
        this.attribute = attribute;
        this.weight = weight;
    }

    public override string ToString() {
        string s = attribute.attribute_name + "  (" + weight + ")";
        return s;
    }
}
