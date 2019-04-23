using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{   public Company PlayerCompany;
    private List<Employee> PrebuiltEmployees = new List<Employee>();
    private List<Contract> PrebuiltContracts = new List<Contract>();
    private float time0 = 0.0f;
    private float interval = 3.0f;  // the real time for each day in seconds
    private int day = 0;

    void Start()
    {   PlayerCompany = new Company("California Technology");
        intial_settings();
    }

    void Update()
    {   if(Time.time-time0>interval)
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
    {   PlayerCompany.hire_employee("Lucy", 8, new BasicPropertys(50,50,50));
        PlayerCompany.hire_employee("Tracy", 5, new BasicPropertys(50,33,50));
        PlayerCompany.hire_employee("Lisbon", 10, new BasicPropertys(50,33,76));
        PlayerCompany.buy_asset(new Asset("Mercedes C-class",200000f));
        PlayerCompany.print_employees();
        PlayerCompany.print_assets();
        PlayerCompany.print_company();

        PrebuiltContracts.Add(new Contract("Bristol Romantic Website",
        500,1,new BasicPropertys(300,400,250)));
        PrebuiltContracts.Add(new Contract("RAF webiste",
            500,1,new BasicPropertys(200,600,50)));
        PlayerCompany.take_project(PrebuiltContracts[0],new string[] {"Lucy","Tracy"});
        PlayerCompany.take_project(PrebuiltContracts[1],new string[] {"Lisbon"});
    }

    void print_time()
    {   Debug.Log("");
        Debug.Log("//// "+ interval+" seconds passed//// current situation:");
        Debug.Log("Day "+day+":");
    }

// static function to let UI GameObject knows where the controller is
    public static Control ConnectToGameModel()
    {   GameObject gamemodel = GameObject.FindWithTag("GameController");
        return gamemodel.GetComponent<Control>();
    }

}
