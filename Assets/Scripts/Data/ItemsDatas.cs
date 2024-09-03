using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType
    {
        Melee,
        Range,
        Glove,
        Shoe,
        Heal,
    }
    
    public int id;
    public string type;
    public ItemType itemType;
    public int level;
    public string name;
    public string icon;
    public float damage;
    public float[] damages;
    public int count;
    public int[] counts;

    //  hand
    public string hand;
    public Sprite handSprite;
    // levelMax
    public bool maxLevel = false;
    
    public WeaponController WeaponController;
    public GearController GearController;
}
[Serializable]
public class ItemsDatasLoad : ILoader<int, Item>
{
    public List<Item> Items = new List<Item>();
    
    public Dictionary<int, Item> MakeDict()
    {
     
        Dictionary<int, Item> dic = new Dictionary<int, Item>();

        foreach (Item item in Items)
        {
            switch (item.type)
            {
                case "weapon":
                    item.itemType = Item.ItemType.Melee;
                    break;
                case "range":
                    item.itemType = Item.ItemType.Range;
                    break;
                case "glove":
                    item.itemType = Item.ItemType.Glove;
                    break;
                case "shoe":
                    item.itemType = Item.ItemType.Shoe;
                    break;
                case "heal":
                    item.itemType = Item.ItemType.Heal;
                    break;
            }

            // if (item.hand.Length > 0)
            //     item.handSprite = Utils.FindSprite("Props", item.hand);
            dic.Add(item.id, item);
        }

        return dic;
    }
}
