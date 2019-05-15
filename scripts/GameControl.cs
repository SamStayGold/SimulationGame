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

    private bool newUpdate = false;
    public bool isNewUPdate() { return newUpdate;}
    public void resolveUpdate() { newUpdate = false;}

    public int getDay() { return day;}
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
            //flag the Update
            newUpdate = true;
        }
    }

    void time_update_model()
    {   PlayerCompany.update_company();
        PlayerCompany.print_company();
        day ++;
    }

    void intial_settings()
    {   Employee e1 = new Employee("Lucy", 8, "SpiderMan", "hahaha",new BasicPropertys(50,51,52));
        Employee e2 = new Employee("Tracy", 5, "SpiderMan", "hahaha",new BasicPropertys(50,33,51));
        Employee e3 = new Employee("Lisbon", 10, "SpiderMan", "hahaha",new BasicPropertys(50,33,76));
        Employee e4 = new Employee("Sam", 1, "SpiderMan", "hahaha",new BasicPropertys(20,23,16));
        PrebuiltEmployees.Add(e1);
        PrebuiltEmployees.Add(e2);
        PrebuiltEmployees.Add(e3);
        PrebuiltEmployees.Add(e4);

        PlayerCompany.buy_asset(new Asset("Mercedes C-class",200000,"ccc",0));

        PlayerCompany.print_employees();
        PlayerCompany.print_assets();
        PlayerCompany.print_company();

        PlayerCompany.hire_employee(e1);
        PrebuiltEmployees.Remove(e1);
        PlayerCompany.hire_employee(e3);
        PrebuiltEmployees.Remove(e3);

        PrebuiltContracts.Add(new Contract("Bristol Romantic Website","romance",
            Industry.SpaceEngineering,500,1,100, new BasicPropertys(300,400,250)));

       Contract contract = new Contract("RAF webiste","raf",
            Industry.Military,500,1,170, new BasicPropertys(200,600,50));

       PrebuiltContracts.Add(new Contract("RAF webiste","raf",
            Industry.Military,500,1,170, new BasicPropertys(200,600,50)));

        PlayerCompany.take_project(PrebuiltContracts[0],new string[] {"Lucy"});
        DeletePrebuiltContract(PrebuiltContracts[0]);
        //ontract contract = PrebuiltContracts[1];
        if(contract ==null) Debug.Log("wfwfwfwfwfw");
        PlayerCompany.take_project(contract,new string[] {"Lisbon"});
        DeletePrebuiltContract(PrebuiltContracts[0]);
    }

    public void print_time()
    {   Debug.Log("");
        Debug.Log("//// "+ interval+" seconds passed//// current situation:");
        Debug.Log("Day "+day+":");
    }

    public void PrebuiltEmployee(Employee e)
    {   PrebuiltEmployees.Add(e);
    }

    public void DeletePrebuiltEmployee(Employee e)
    {   PrebuiltEmployees.Remove(e);
    }

    public void DeletePrebuiltContract(Contract c)
    {   PrebuiltContracts.Remove(c);
    }

    public List<Employee> getPrebuiltEmployees()
    {   return PrebuiltEmployees;
    }

    public List<Contract> getPrebuiltContracts()
    {   return PrebuiltContracts;
    }

}
