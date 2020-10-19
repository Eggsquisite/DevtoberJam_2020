
public struct Attribute {

    public string name;

    public Attribute(string name) {
        this.name = name;
    }

}

public struct WeightedAttribute {
    
    public Attribute attribute;
    public byte weight;

    public WeightedAttribute(Attribute attribute, byte weight) {
        this.attribute = attribute;
        this.weight = weight;
    }
}
