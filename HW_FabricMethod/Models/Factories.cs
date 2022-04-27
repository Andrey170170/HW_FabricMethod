using System.Collections.Generic;

namespace HW_FabricMethod.Models;

public abstract class PersonFactory
{
    public abstract Weapon CreateWeapon();
    public abstract List<Item> CreateItems();
}

public class HumanFactory : PersonFactory
{
    public override Weapon CreateWeapon() => new Sword();
    public override List<Item> CreateItems() => new()
    {
        new Ring(), CreateWeapon()
    };
}

public class OrcFactory : PersonFactory
{
    public override Weapon CreateWeapon() => new Axe();
    public override List<Item> CreateItems() => new()
    {
        CreateWeapon()
    };
}

public class AeldariFactory : PersonFactory
{
    public override Weapon CreateWeapon() => new Bow();
    public override List<Item> CreateItems() => new()
    {
        new Brilliant(), CreateWeapon()
    };
}