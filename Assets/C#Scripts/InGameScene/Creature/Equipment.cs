using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment
{
    private Creature creature;

    public Equipment(Creature creature)
    {
        this.creature = creature;
    }

    private Item weapon = null;
    public Item Weapon 
    { 
        get { return weapon; }
        set { weapon = value;
            
        } 
    }

    private Item helmet = null;
    public Item Helmet
    {
        get { return helmet; }
        set { helmet = value; }
    }

    private Item armor = null;
    public Item Armor
    {
        get { return armor; }
        set { armor = value; }
    }

    private Item pants = null;
    public Item Pants
    {
        get { return pants; }
        set { pants = value; }
    }

    private Item shoes = null;
    public Item Shoes
    {
        get { return shoes; }
        set { shoes = value; }
    }

    private Item ring1 = null;
    public Item Ring1
    {
        get { return ring1; }
        set { ring1 = value; }
    }

    private Item ring2 = null;
    public Item Ring2
    {
        get { return ring2; }
        set { ring2 = value; }
    }

    private Item necklace = null;
    public Item Necklace
    {
        get { return necklace; }
        set { necklace = value; }
    }

    private Item earring = null;
    public Item Earring
    {
        get { return earring; }
        set { earring = value; }
    }
}
