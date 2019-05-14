using System;
using System.Collections;
using System.Collections.Generic;
[Serializable]
public class Asset
{   private string name;
    private float price;
    public Asset(string name, float price)
    {   this.name = name;
        this.price = price;
    }
    public string get_detail()
    {   return name+" worths "+price+"$;";
    }
}
