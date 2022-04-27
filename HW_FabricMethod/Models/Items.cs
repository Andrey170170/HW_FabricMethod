using System;

namespace HW_FabricMethod.Models;

public abstract class Item
{
    public string Name => this.GetType().ToString();
    
    public int Cost { get; set; }
    public double Weight { get; set; }
    public double Rarity { get; set; }

    public abstract Item GetItem();
}

public class Ring : Item
{
    private Random _random = new Random();

    public override Item GetItem()
    {
        Console.WriteLine("Кольцо получено!");
        return this;
    }
    
    public Ring()
    {
        Cost = _random.Next(10,20);
        Weight = _random.Next(1, 2);
        Rarity = _random.Next(40,60);
    }
}

public class Brilliant : Item
{
    private Random _random = new Random();
    
    public override Item GetItem()
    {
        Console.WriteLine("Бриллиант получен!");
        return this;
    }
    
    public Brilliant()
    {
        Cost = _random.Next(150,300);
        Weight = _random.Next(2,3);
        Rarity = _random.Next(5,10);
    }
}