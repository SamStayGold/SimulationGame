using System;
[Serializable]
public class Employee
{   private string name;
    private BasicPropertys propertys;
    private string image_filepath;
    private bool occupied = false;
    private int salary;
    private string jobTitle;
    private string description;

    public static void Main(string[] args)
    {   Console.WriteLine("Hello World");
    }

    public Employee(string name, int salary, string jobTitle, string description, BasicPropertys propertys)
    {   this.name = name;
        this.salary = salary;
        this.propertys = propertys;
        this.jobTitle = jobTitle;
        this.description = description;
    }

    public string getName(){   return name;}
    public int getSalary(){   return salary;}
    public string getJobTitle(){   return jobTitle;}
    public string getDescription(){   return description;}

    public BasicPropertys getPropertys()
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
