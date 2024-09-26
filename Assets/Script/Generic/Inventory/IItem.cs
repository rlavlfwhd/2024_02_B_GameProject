using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    string Name { get; }
    int ID {  get; }
    void Use();
}

public class Weapon : IItem
{
    public string Name { get; private set; }
    public int ID { get; private set; }
    public int Damage { get; private set; }

    public Weapon(string name, int id, int damage)
    {
        Name = name;
        ID = id;
        Damage = damage;
    }
    public void Use()
    {
        Debug.Log($"Using weapon {Name} with damage {Damage}");
    }

}

public class HealthPotion : IItem
{
    public string Name { get; private set; }
    public int ID { get; private set; }
    public int HealAmount { get; private set; }

    public HealthPotion(string name, int iD, int healAmount)
    {
        Name = name;
        ID = iD;
        HealAmount = healAmount;
    }

    public void Use()
    {
        Debug.Log($"Using weapon {Name} with damage {HealAmount}");
    }
}

public class Inventory<T> where T : IItem
{
    private List<T> items = new List<T>();

    public void AddItem(T item)
    {
        items.Add(item);
        Debug.Log($"Add {item.Name} to inventory");
    }

    public void UseItem(int index)
    {
        if(index >= 0 && index < items.Count)
        {
            items[index].Use();
        }
        else
        {
            Debug.Log("Invalid item index");
        }
    }
    public void ListItems()
    {
        foreach(var item in items)
        {
            Debug.Log($"Item: {item.Name}, ID : {item.ID}");
        }    
    }
}
