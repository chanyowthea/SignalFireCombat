using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class NewBehaviourScript : MonoBehaviour
{

    bool _IsUsed;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Profiler.BeginSample("Judge");
        for (int i = 0; i < 100; i++)
        {
            if (_IsUsed)
            {
                _IsUsed = false;
            }
        }
        Profiler.EndSample();

        Profiler.BeginSample("Assign");
        for (int i = 0; i < 100; i++)
        {
            _IsUsed = false;
        }
        Profiler.EndSample();
    }
}
