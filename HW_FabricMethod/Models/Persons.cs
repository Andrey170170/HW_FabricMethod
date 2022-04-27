using System;
using System.Collections.Generic;

namespace HW_FabricMethod.Models;

public abstract class Person
{
    public int Health { get; set; } = 100;
    public double Armor { get; set; } = 0;

    private Weapon _weapon;
    private List<Item> _items;

    public void Hit() =>_weapon.Hit();
    public void GetItem(Item item)
    {
        _items.Add(item);
        item.GetItem();
    }

    protected Person(PersonFactory personFactory)
    {
        _weapon = personFactory.CreateWeapon();
        _items = personFactory.CreateItems();
    }
}

