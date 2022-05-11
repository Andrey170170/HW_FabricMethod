using System;
using System.Collections.Generic;

namespace HW_FabricMethod.Models;

public class Person
{
    public int Health { get; set; } = 100;
    public string Race { get; set; }

    public Weapon Weapon;
    public Armor Armor;
    private List<Item> _items;

    public void Hit() =>Weapon.Hit();
    public void GetItem(Item item)
    {
        _items.Add(item);
        item.GetItem();
    }

    public Person(PersonFactory personFactory)
    {
        Race = personFactory.CreateRace();
        Weapon = personFactory.CreateWeapon();
        Armor = personFactory.CreateArmor();
        _items = personFactory.CreateItems();
    }
}

