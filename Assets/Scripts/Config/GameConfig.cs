using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoBehaviour
{
    static GameConfig _Instance;
    public static GameConfig Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new GameConfig();
            }
            return _Instance;
        }
    }

    public static readonly float _MeterToUnit = 46;
}
