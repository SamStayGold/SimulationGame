using System;
using System.Collections;
using System.Collections.Generic;


/* this is all you need to know about the model */
/* you are only allowed to use functions in the interface */
/*
interface CompanyInterface
{   // action funcion
    bool take_project(Contract contract, string[] CrewNames);
    void hire_employee(Employee e);

    //ask infos
    Dictionary<string,Employee> getEmployees();

}*/

/* anything else below you don't need to know */

// employee was held in a Dictionary, so that you can find one employee
// by its name
[Serializable]
public class Company //: CompanyInterface
{   private Dictionary<string,Employee> employees = new Dictionary<string,Employee>();
    private List<Asset> assets = new List<Asset>();
    private List<Project> projects_engaged = new List<Project>();
    private int money = 100000000;
    private string company_name;

    //empty string = no alert
    private Alert alert = new Alert();

    public Dictionary<string,Employee> getEmployees()
    {   return employees;
    }
    public List<Project> getCurrentProjects()
    {   return projects_engaged;
    }
    public int getMoney() { return money;}


    public Company(string name)
    {   this.company_name = name;
    }

// called on each day
    public void update_company()
    {   update_projects();
        update_employees();
    }

// pay employees
    public void update_employees()
    {   foreach (Employee e in employees.Values)
        money -= e.getSalary();
    }

// proceed every project
    public void update_projects()
    {   if(projects_engaged.Count==0) return;
        //avoid concurrency problem of delete and access List
        //at the same time
        List<Project> finished = new List<Project>();
        foreach (Project p in projects_engaged)
        {   if(p.daily_operation())
            finished.Add(p);
        }
        if(finished.Count!=0)
        {   foreach(Project p in finished)
            finish_project(p);
        }
    }

//  get paid from and remove finishing project
    private void finish_project(Project project)
    {   money+=project.getAward();
        projects_engaged.Remove(project);
    }

// sign up a project, asign employee by name
// if no employee has correspondent name return false
// if employee is busy return false;
    public bool take_project(Contract contract, string[] CrewNames)
    {   List<Employee> crew = new List<Employee>();
        foreach (string name in CrewNames)
        {   Employee e;
            if(!employees.TryGetValue(name, out e)) return false;
            if(e.get_status()) return false;
            crew.Add(e);
        }
        Project newproject = new Project(contract,crew);
        projects_engaged.Add(newproject);
        return true;
    }

    public bool hire_employee(Employee e)
    {   if (check_hireable(e)) employees.Add(e.getName(),e);
        else
        {  alert.setMessage("Can't hire now, you don't have required assets yet");
           return false;
        }
        return true;
    }

    public bool buy_asset(Asset newasset)
    {   if (money < newasset.getPrice())
        {   alert.setMessage("You don't have enough money to buy this assets!");
            return false;
        }

        if (newasset.getName().Contains("Server Clusters"))
        {   if(!hasAssetWithName("Server Room"))
            alert.setMessage("You need a Server Room to buy server");
            return false;
        }
        assets.Add(newasset);
        money -= newasset.getPrice();
        return true;
    }

    private bool check_buyable()
    {   return true;
    }

    private bool check_hireable(Employee e)
    {   string[] titleKeywords = e.getJobTitle().Split(' ');
        foreach(string s in titleKeywords)
        {    if(!checkKeyTitle(s)) return false;
        }
        return true;
    }

    private bool checkKeyTitle(string keywords)
    {   switch (keywords)
        {    case "Operation":
                if(hasAssetWithName("Server Room")) return true;
                return false;
             case "Senior":
                if(hasAssetWithName("Office LV2")) return true;
                return false;
             case "Full":
                if(hasAssetWithName("Office LV3")) return true;
                if(hasAssetWithName("Office LV2"))
                {   if(countEmployeeWithKeyword("Full")<3) return true;
                }
                return false;
             default:
                return true;
       }
     }

     private int countEmployeeWithKeyword(string keyword)
     {   int count = 0;
         foreach(Employee e in employees.Values)
         {   if(e.getName().Contains(keyword))
             count++;
         }
         return count;
     }

     private bool hasAssetWithName(string name)
     {   foreach(Asset a in assets)
         {   if(a.getName().Equals(name))
             return true;
         }
         return false;
     }

    public string get_alert_message()
    {   string message = alert.getMessage();
        //reset alert
        alert.setMessage("");
        return message;
    }


// trivial functions, not important
// some were used to cooperate with the old UI
    public void print_company()
    {   Debug.Log("Company: "+company_name+"---current account---:  "+money+"$");
        foreach(Project p in projects_engaged)
        p.print_project();
    }

    public void print_employees()
    {   String s = "";
        foreach (Employee e in employees.Values)
        s+=e.getName()+"  ";
        Debug.Log("Your current staff: "+s);
    }

    public string employees_info()
    {   string info = "";
        foreach (Employee e in employees.Values)
        info += "Employee: "+e.getName()+"\n";
        return info;
    }

    public void print_assets()
    {   foreach (Asset a in assets)
        Debug.Log("Your current assets: "+a.getDetail());
    }

    public string assets_info()
    {   string info = "";
        foreach (Asset a in assets)
        info += "Asset: "+a.getDetail()+"\n";
        return info;
    }

    public static void Main(string[] args)
    {   Company cmp = new Company("haa");
        Asset a1 = new Asset("Office LV3",100,"haa",300);
        if(cmp.buy_asset(a1)==false) Console.WriteLine("test fails");
        Employee e1 = new Employee("22",1,"Full Stacker","sd",new BasicPropertys(0,0,0));
        if(cmp.hire_employee(e1)==false) Console.WriteLine("test fails");
    }
}
