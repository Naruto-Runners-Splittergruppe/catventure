using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableEventSystem : MonoBehaviour
{
    public List<Event<bool>> boolEvents;
    public List<Event<int>> intEvents { get; set; }
    public List<Event<float>> floatEvents { get; set; }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
