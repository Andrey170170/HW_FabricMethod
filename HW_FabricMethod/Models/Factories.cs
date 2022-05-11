using System.Collections.Generic;

namespace HW_FabricMethod.Models;

public abstract class PersonFactory
{
    public abstract string CreateRace();
    public abstract Weapon CreateWeapon();
    public abstract Armor CreateArmor();
    public abstract List<Item> CreateItems();
}

public class HumanFactory : PersonFactory
{
    private Weapon _weapon;
    private Armor _armor;
    private List<Item> _items;
    public override string CreateRace() => "Human";
    public override Weapon CreateWeapon() => _weapon;
    public override Armor CreateArmor() => _armor;
    public override List<Item> CreateItems() => _items;

    public HumanFactory(Weapon weapon, Armor armor, List<Item> items)
    {
        _weapon = weapon;
        _armor = armor;
        _items = items;
    }
}

public class TauFactory : PersonFactory
{
    public override string CreateRace() => "Tau";
    public override Weapon CreateWeapon() => new Sword();
    public override Armor CreateArmor() => new Breastplate();
    public override List<Item> CreateItems() => new()
    {
        new Ring(), CreateWeapon()
    };
}

public class OrcFactory : PersonFactory
{
    public override string CreateRace() => "Orc";
    public override Weapon CreateWeapon() => new Axe();
    public override Armor CreateArmor() => new Breastplate();
    public override List<Item> CreateItems() => new()
    {
        CreateWeapon()
    };
}

public class AeldariFactory : PersonFactory
{
    public override string CreateRace() => "Aeldari";
    public override Weapon CreateWeapon() => new Bow();
    public override Armor CreateArmor() => new Chain();
    public override List<Item> CreateItems() => new()
    {
        new Brilliant(), CreateWeapon()
    };
}