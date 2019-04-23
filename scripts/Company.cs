using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* this is all you need to know about the model */
/* you are only allowed to use functions in the interface */
interface CompanyInterface
{   // action funcion
    bool take_project(Contract contract, string[] CrewNames);
    void buy_asset(Asset newasset);

    //ask infos
    Dictionary<string,Employee> getEmployees();

}

/* anything else below you don't need to know */

// employee was held in a Dictionary, so that you can find one employee
// by its name
public class Company : CompanyInterface
{   private Dictionary<string,Employee> employees = new Dictionary<string,Employee>();
    private List<Asset> assets = new List<Asset>();
    private List<Project> projects_engaged = new List<Project>();
    private int money;
    private string company_name;

    public Dictionary<string,Employee> getEmployees()
    {   return employees;
    }

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
        money -= e.get_salary();
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
    {   money+=project.get_award();
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

    public void hire_employee(Employee e)
    {   employees.Add(e.get_name(),e);
    }

    public void buy_asset(Asset newasset)
    {   assets.Add(newasset);
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
        s+=e.get_name()+"  ";
        Debug.Log("Your current staff: "+s);
    }

    public string employees_info()
    {   string info = "";
        foreach (Employee e in employees.Values)
        info += "Employee: "+e.get_name()+"\n";
        return info;
    }

    public void print_assets()
    {   foreach (Asset a in assets)
        Debug.Log("Your current assets: "+a.get_detail());
    }

    public string assets_info()
    {   string info = "";
        foreach (Asset a in assets)
        info += "Asset: "+a.get_detail()+"\n";
        return info;
    }

}
