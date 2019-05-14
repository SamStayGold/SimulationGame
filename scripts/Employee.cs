using System;
[Serializable]
public class Employee
{   private string name;
    private BasicPropertys propertys;
    private string image_filepath;
    private bool occupied = false;
    private int salary;

    public static void Main(string[] args)
    {   Console.WriteLine("Hello World");
    }

    public Employee(string name, int salary, BasicPropertys propertys)
    {   this.name = name;
        this.salary = salary;
        this.propertys = propertys;
    }
    public string get_name(){   return name;}
    public int get_salary(){   return salary;}

    public BasicPropertys get_propertys()
    {   return propertys;
    }

// assign employee to work, or free him
    public void gotowork()
    {   occupied = true;
    }
    public void freefromwork()
    {   occupied = false;
    }
    public bool get_status()
    {   return occupied;
    }
}
