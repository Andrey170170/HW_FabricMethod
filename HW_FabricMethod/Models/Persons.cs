using System;
using System.Collections.Generic;

namespace HW_FabricMethod.Models;

public class Person
{
    public int Health { get; set; } = 100;
    public string Race { get; set; }

    private Weapon _weapon;
    private Armor _armor;
    private List<Item> _items;

    public void Hit() =>_weapon.Hit();
    public void GetItem(Item item)
    {
        _items.Add(item);
        item.GetItem();
    }

    public Person(PersonFactory personFactory)
    {
        _weapon = personFactory.CreateWeapon();
        _items = personFactory.CreateItems();
    }
}

