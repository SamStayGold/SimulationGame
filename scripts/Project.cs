using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Project
{   private Contract contract;
    Dictionary<Property,List<Employee>> workAssigns = new Dictionary<Property,List<Employee>>();
    private List<Employee> manpowers = new List<Employee>();
    private List<Property> listOfPropertyLeft;

    private int currentDays = 0;

    private BasicPropertys workload;
    private BasicPropertys progress = new BasicPropertys(0,0,0);
    private BasicPropertys workforce;

    public int getAward() {   return contract.getAward();}
    public Industry getIndustry() {  return contract.getIndustry();}
    public string getName() {  return contract.getName();}

    public BasicPropertys getProgress() {  return progress;}
    public int getRemainingTime() { return contract.getTimeLimit()-currentDays;}

//need fail function

    public Project(Contract contract, List<Employee> employees)
    {   this.contract = contract;
        // record the workload requirements of the contract
        workload = contract.getPropertys().givecopy();
        listOfPropertyLeft = workload.getListOfProperty();

        // add employees & record work force
        foreach(Employee e in employees)
        {   manpowers.Add(e);
            e.gotowork();
        }
        workAssignsInit();
    }

    private void workAssignsInit()
    {   foreach (Property p in listOfPropertyLeft)
        {   workAssigns.Add(p,new List<Employee>());
        }
        assignToWorks(manpowers);
        updateWorkForce();
    }

   // ideally max should be null handled;
    private void assignToWorks(List<Employee> availbleMen)
    {   foreach(Employee e in availbleMen)
        {   Property max = e.getPropertys().getMaxBasicProperty(listOfPropertyLeft);
            workAssigns[max].Add(e);
        }
    }

    // called when a property is filled up
    // and employees need to be reAssigned
    private void finishOneTask(Property targetTask)
    {   listOfPropertyLeft.Remove(targetTask);
        if(listOfPropertyLeft.Count!=0)
        {   //assign finished tasks' workers to other tasks
            assignToWorks(workAssigns[targetTask]);
            //clear up the work assigns for the finished task
            workAssigns[targetTask].Clear();
            updateWorkForce();
        }
        //if went over the workload, handled here
        workOverFlowHandling(targetTask);
    }

    private void updateWorkForce()
    {   workforce = new BasicPropertys(0,0,0);
        foreach(KeyValuePair<Property,List<Employee>> entry in workAssigns)
        {   foreach(Employee e in entry.Value)
            {   int hisAbility = e.getPropertys().getPropertyQuant(entry.Key);
                workforce.updatePropertyQuant(entry.Key,hisAbility);
            }
        }
    }

// called on everyday
    public bool daily_operation()
    {   projectproceed();
        projectAdjust();

        if(finishing_judge())
        {   free_employees();
            printFinished();
            return true;
        }
        return false;
    }

// currently no work switching between employees,
// just add to all the propertys like 3 bars
    private void projectproceed()
    {   progress.add_propertys(workforce);
        currentDays ++;
    }

    private void projectAdjust()
    {   List<Property> finishedOnes = new List<Property>();

        // judge then finish in two steps
        // avoid concurrent looping/editing a list
        foreach(Property p in listOfPropertyLeft)
        {   if(progress.getPropertyQuant(p) >= workload.getPropertyQuant(p))
            {   finishedOnes.Add(p);
            }
        }

        foreach(Property p in finishedOnes)
        {   finishOneTask(p);
        }

    }

    private void workOverFlowHandling(Property targetTask)
    {   int maxvalue = workload.getPropertyQuant(targetTask);
        if(progress.getPropertyQuant(targetTask)>maxvalue)
        progress.setPropertyQuant(targetTask,maxvalue);
    }

// return true when project finished
    private bool finishing_judge()
    {   if(listOfPropertyLeft.Count==0) return true;
        return false;
    }


    private void free_employees()
    {   foreach(Employee e in manpowers)
        e.freefromwork();
    }

    public void printProject()
    {   Debug.Log("Project "+contract.getName()+"--current Progress--"+"Frontend:"+progress.Frontend
        +"/"+workload.Frontend+", Backend:"+progress.Backend+"/"+workload.Backend+
        ", Creativity:"+progress.Graphics+"/"+workload.Graphics);
        ;
    }

    public void printFinished()
    {   Debug.Log("Project "+contract.getName()+" finished");
    }
}
