using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HireButton : MonoBehaviour
{   GameObject inputfield;
    Control control;
    Employee employee;
    void Start()
    {   Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
        control = Control.ConnectToGameModel();
    }

    public void setEmployee(Employee e)
    {   this.employee = e;
    }

    void TaskOnClick()
    {   if(employee!=null)
        control.getUserCompany().hire_employee(employee);
    }

}
