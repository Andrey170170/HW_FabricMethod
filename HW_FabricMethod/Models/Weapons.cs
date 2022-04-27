using System;

namespace HW_FabricMethod.Models;

public abstract class Weapon : Item
{
    public string Name => this.GetType().ToString();
    
    public double Damage { get; set; }
    public double Range { get; set; }
    public double Cooldown { get; set; }

    public abstract void Hit();
}

public class Axe : Weapon
{
    private Random _random = new Random();

    public override void Hit()
    {
        Console.WriteLine("Атакуем топором");
    }
    
    public override Item GetItem()
    {
        Console.WriteLine("Топор получен");
        return this;
    }

    public Axe()
    {
        Cost = _random.Next(100,150);
        Weight = _random.Next(6, 9);
        Rarity = _random.Next(35,50);
        
        Damage = _random.Next(40,60);
        Range = _random.Next(2,4);
        Cooldown = _random.Next(3,6);
    }
}

public class Sword : Weapon
{
    private Random _random = new Random();
    
    public override void Hit()
    {
        Console.WriteLine("Атакуем мечом");
    }
    
    public override Item GetItem()
    {
        Console.WriteLine("Меч получен");
        return this;
    }
    
    public Sword()
    {
        Cost = _random.Next(75,100);
        Weight = _random.Next(4,6);
        Rarity = _random.Next(40,70);
        
        Damage = _random.Next(30,50);
        Range = _random.Next(1,2);
        Cooldown = _random.Next(2,4);
    }
}

public class Bow : Weapon
{
    private Random _random = new Random();
    
    public override void Hit()
    {
        Console.WriteLine("Стреляем из лука");
    }
    
    public override Item GetItem()
    {
        Console.WriteLine("Лук получен");
        return this;
    }
    
    public Bow()
    {
        Cost = _random.Next(150,200);
        Weight = _random.Next(4,7);
        Rarity = _random.Next(20,40);
        
        Damage = _random.Next(20,30);
        Range = _random.Next(20,30);
        Cooldown = _random.Next(6,8);
    }
}

