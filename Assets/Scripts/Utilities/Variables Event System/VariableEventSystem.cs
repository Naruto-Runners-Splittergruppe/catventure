using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableEventSystem : MonoBehaviour
{
    public List<Event<bool>> BoolEvents { get; set; }
    public List<Event<int>> IntEvents { get; set; }
    public List<Event<float>> FloatEvents { get; set; }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public Event<bool> FindEventInBools(Guid id)
    {
        foreach(Event<bool> b in BoolEvents)
        {
            if(b.Id.Equals(id))
            {
                return b;
            }
        }

        return null;
    }

    public Event<bool> FindEventInBools(string name)
    {
        foreach (Event<bool> b in BoolEvents)
        {
            if (b.Name.Equals(name))
            {
                return b;
            }
        }

        return null;
    }

    public Event<int> FindEventInInts(Guid id)
    {
        foreach (Event<int> i in IntEvents)
        {
            if (i.Id.Equals(id))
            {
                return i;
            }
        }
        return null;
    }

    public Event<int> FindEventInInts(string name)
    {
        foreach (Event<int> i in IntEvents)
        {
            if (i.Name.Equals(name))
            {
                return i;
            }
        }
        return null;
    }

    public Event<float> FindEventInFloats(Guid id)
    {
        foreach (Event<float> f in FloatEvents)
        {
            if (f.Id.Equals(id))
            {
                return f;
            }
        }
        return null;
    }

    public Event<float> FindEventInFloats(string name)
    {
        foreach (Event<float> f in FloatEvents)
        {
            if (f.Name.Equals(name))
            {
                return f;
            }
        }
        return null;
    }
}
