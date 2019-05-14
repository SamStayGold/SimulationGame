using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HireButton : MonoBehaviour
{   GameObject inputfield;
    GameControl control;
    Employee employee;
    GameObject panel;

    void Start()
    {   Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
        control = Control.ConnectToGameModel();
    }

    public void setEmployee(Employee e)
    {   this.employee = e;
    }

    public void setPanel(GameObject panel)
    {   this.panel = panel;
    }

    void TaskOnClick()
    {   if(employee != null)
        {   control.getUserCompany().hire_employee(employee);
            control.DeletePrebuiltEmployee(employee);
            Destroy(panel);
        }
    }

    void DrawPanel()
    {

    }
}
