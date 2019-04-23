using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HireButton : MonoBehaviour
{   GameObject inputfield;
    Control control;
    Text text;
    void Start()
    {   Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
        inputfield = this.transform.parent.gameObject;
        text = inputfield.GetComponentInChildren<Text>();
        control = Control.ConnectToGameModel();
    }

    void handle_input()
    {   string name = text.text;
        control.PlayerCompany.hire_employee(name, 0,new BasicPropertys(0,0,0));
    }

    void TaskOnClick()
    {    handle_input();
         Destroy(inputfield.transform.parent.gameObject);
    }
}
