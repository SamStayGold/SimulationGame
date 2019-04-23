using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Project
{   private Contract contract;
    private List<Employee> manpowers = new List<Employee>();
    private BasicPropertys workload;
    private BasicPropertys progress = new BasicPropertys(0,0,0);
    private BasicPropertys workforce = new BasicPropertys(0,0,0);
    private int award;

    public int get_award() {   return award;}

    public Project(Contract contract, List<Employee> employees)
    {   this.contract = contract;
        this.award = contract.get_award();
        // record the workload requirements of the contract
        workload = contract.get_propertys().givecopy();

        // add employees & record work force
        foreach(Employee e in employees)
        {   manpowers.Add(e);
            e.gotowork();
            BasicPropertys eprop = e.get_propertys();
            workforce.add_propertys(eprop);
        }
    }

// called on everyday
    public bool daily_operation()
    {   projectproceed();
        if(finishing_judge())
        {   free_employees();
            Debug.Log("Project "+contract.get_name()+" finished");
            return true;
        }
        return false;
    }

// currently no work switching between employees,
// just add to all the propertys like 3 bars
    private void projectproceed()
    {   progress.add_propertys(workforce);
        //deal with over work
        if(progress.frontend>workload.frontend) {   progress.frontend = workload.frontend;}
        if(progress.backend>workload.backend) {   progress.backend = workload.backend;}
        if(progress.creativity>workload.creativity) {   progress.creativity = workload.creativity;}
    }

// return true when project finished
    private bool finishing_judge()
    {   if(progress.frontend >= workload.frontend
        && progress.backend >= workload.backend
        && progress.creativity >= workload.creativity)
        {   return true;
        }
        return false;
    }

    private void free_employees()
    {   foreach(Employee e in manpowers)
        e.freefromwork();
    }

    public void print_project()
    {   Debug.Log("Project "+contract.get_name()+"--current Progress--"+"Frontend:"+progress.frontend
        +"/"+workload.frontend+", Backend:"+progress.backend+"/"+workload.backend+
        ", Creativity:"+progress.creativity+"/"+workload.creativity);
    }
}
