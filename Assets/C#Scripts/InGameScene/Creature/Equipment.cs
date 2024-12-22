using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Equipment : ScriptableObject
{
    private Item weapon = null;
    private Item helmet = null;
    private Item armor = null;
    private Item pants = null;
    private Item shoes = null;
    private Item ring = null;
    private Item necklace = null;
    private Item earring = null;

    public Item Weapon { get; set; }
    public Item Helmet { get; set; }
    public Item Armor { get; set; }
    public Item Pants { get; set; }
    public Item Shoes { get; set; }
    public Item Ring { get; set; }
    public Item Necklace { get; set; }
    public Item Earring { get; set; }

    public float TotalDefense()
    {
        return Helmet?.Defense + Armor?.Defense + Pants?.Defense + Shoes?.Defense ?? 0;
    }
}
