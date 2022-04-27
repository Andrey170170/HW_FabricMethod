namespace HW_FabricMethod.Models;

abstract class PersonFactory
{
    public abstract Weapon CreateWeapon();
    public abstract Item CreateItem();
}

