using System;
using System.Collections;
using System.Collections.Generic;


[Serializable]
public class Asset
{   private string name;
    private int price;
    private string description;

//only for server
    private int reward;

    public Asset(string name, int price, string description, int reward)
    {   this.name = name;
        this.price = price;
        this.description = description;
        this.reward = reward;
    }

    public string getName() { return name;}

    public string getDetail()
    {   return name+" worths "+price+"$;";
    }

    public int getPrice()
    {   return price;
    }

    public string getDescription()
    {   return description;
    }

    public int getReward()
    {   return reward;
    }

}
