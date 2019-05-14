using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class GameControl
{   private Company PlayerCompany;
    private List<Employee> PrebuiltEmployees = new List<Employee>();
    private List<Contract> PrebuiltContracts = new List<Contract>();
    private float time0 = 0.0f;
    private float interval = 5.0f;  // the real time for each day in seconds
    private int day = 0;
    private bool pause = false;
    public void pauseGame() {   pause = true;}
    public void resumeGame() {   pause = false;}

    // Only fetch information using functions in the Interfaces
    // anythin below interface you don't need to know

    public Company getUserCompany()
    {   return PlayerCompany;
    }

    public void start()
    {   PlayerCompany = new Company("California Technology");
        intial_settings();
        testPause();
    }

    private void testPause()
    {   pauseGame();
        resumeGame();
    }

    public void update()
    {   if(Time.time-time0>interval && !pause)
        {   time0 = Time.time;
            print_time();
            time_update_model();
        }
    }

    void time_update_model()
    {   PlayerCompany.update_company();
        PlayerCompany.print_company();
        day ++;
    }

    void intial_settings()
    {   PrebuiltEmployee("Lucy", 8, new BasicPropertys(50,51,52));
        PrebuiltEmployee("Tracy", 5, new BasicPropertys(50,33,51));
        PrebuiltEmployee("Lisbon", 10, new BasicPropertys(50,33,76));
        PrebuiltEmployee("Sam", 1, new BasicPropertys(20,23,16));
        PlayerCompany.buy_asset(new Asset("Mercedes C-class",200000f));
        PlayerCompany.print_employees();
        PlayerCompany.print_assets();
        PlayerCompany.print_company();

        PlayerCompany.hire_employee(PrebuiltEmployees[0]);
        PlayerCompany.hire_employee(PrebuiltEmployees[1]);

        PrebuiltContracts.Add(new Contract("Bristol Romantic Website",
        500,1,new BasicPropertys(300,400,250)));
        PrebuiltContracts.Add(new Contract("RAF webiste",
            500,1,new BasicPropertys(200,600,50)));
        PlayerCompany.take_project(PrebuiltContracts[0],new string[] {"Lucy","Tracy"});
        PlayerCompany.take_project(PrebuiltContracts[1],new string[] {"Lisbon"});
    }

    public void print_time()
    {   Debug.Log("");
        Debug.Log("//// "+ interval+" seconds passed//// current situation:");
        Debug.Log("Day "+day+":");
    }

    public void PrebuiltEmployee(string s, int salary, BasicPropertys propertys)
    {   PrebuiltEmployees.Add(new Employee(s,salary,propertys));
    }

    public void DeletePrebuiltEmployee(Employee e)
    {   PrebuiltEmployees.Remove(e);
    }

    public List<Employee> getPrebuiltEmployees()
    {   return PrebuiltEmployees;
    }

}
