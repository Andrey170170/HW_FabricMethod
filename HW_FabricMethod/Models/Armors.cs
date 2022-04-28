using System;

namespace HW_FabricMethod.Models;

public abstract class Armor : Item
{
    public int Durability { get; set; } = 100;
    public abstract int Class { get; set; }
}

public class Chain : Armor
{
    private Random _random = new Random();
    
    public override int Class { get; set; } = 2;

    public Chain()
    {
        Cost = _random.Next(100,150);
        Weight = _random.Next(6, 7);
        Rarity = _random.Next(35,50);

        Durability = _random.Next(50, 100);
    }
    public override Item GetItem()
    {
        Console.WriteLine("Кольчуга получена");
        return this;
    } 
}

public class Breastplate : Armor
{
    private Random _random = new Random();
    public override int Class { get; set; } = 4;

    public Breastplate()
    {
        Cost = _random.Next(200,350);
        Weight = _random.Next(12, 15);
        Rarity = _random.Next(20,30);

        Durability = _random.Next(50, 100);
    }
    public override Item GetItem()
    {
        Console.WriteLine("Нагрудник получен");
        return this;
    } 
}