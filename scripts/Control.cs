using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class Control : MonoBehaviour
{   private GameControl gameControl;

    // Only fetch information using functions in the Interfaces
    // anythin below interface you don't need to know

// static function to let UI GameObject knows where the controller is
    public static GameControl ConnectToGameModel()
    {   GameObject gamemodel = GameObject.FindWithTag("GameController");
        return gamemodel.GetComponent<Control>().getModel();
    }

    public GameControl getModel()
    {   return gameControl;
    }

    void Start()
    {   gameControl = new GameControl();
        gameControl.start();
        testWriteTo();
    }

    void Update()
    {   gameControl.update();
    }

    private void testWriteTo()
    {   FileHandler handler = new FileHandler();
        handler.saveObject(gameControl,"gameControl");
        GameControl readOut = (GameControl) handler.readObject("gameControl");
        readOut.print_time();
        readOut.print_time();readOut.print_time();
    }

    public void saveGame()
    {   FileHandler handler = new FileHandler();
        handler.saveObject(gameControl,"gameControl");
    }

    public void loadGame(string name)
    {   FileHandler handler = new FileHandler();
        GameControl readOut = (GameControl) handler.readObject(name);
        gameControl = readOut;
    }

}
