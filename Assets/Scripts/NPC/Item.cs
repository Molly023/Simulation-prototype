using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int Id;

    new string name;
    string description;
    Sprite sprite;
    int price;

    public string Name {
        get {
            name = Utilities.GetLine("ItemNames", Id);

            return name;
        }
    }

    public string Description {
        get{
            description = Utilities.GetLine("ItemDescription", Id);

            return description; 
        }
    }

    public Sprite Sprite {
        get {
            sprite = Utilities.GetSprite(Id.ToString());
            return sprite;
        }

    }

    public int Price {
        get {
            price = int.Parse(Utilities.GetLine("ItemPrices", Id));
            
            return price;
        }
    }

}
