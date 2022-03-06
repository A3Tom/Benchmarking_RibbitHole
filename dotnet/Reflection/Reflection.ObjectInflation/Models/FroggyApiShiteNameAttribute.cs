namespace Reflection.ObjectInflation.Models;
public class FroggyApiShiteNameAttribute : Attribute
{
    public string Name { get; set; }

    public FroggyApiShiteNameAttribute(string name)
    {
        Name = name;
    }
}

