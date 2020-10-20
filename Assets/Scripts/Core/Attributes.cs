
public struct Attribute {

    public string attribute_name;

    public Attribute(string attribute_name) {
        this.attribute_name = attribute_name;
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
}
