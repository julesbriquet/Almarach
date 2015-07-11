using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager
{
    #region Singleton

    private static GameManager _instance;

    private GameManager()
    {
        //Initialize();
    }

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }
            return _instance;
        }
    }

    #endregion

    public List<Player> Players;

    public void Reset()
    {

    }

}
