using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VariableEventSystem : MonoBehaviour
{
    public List<Event<bool>> BoolEvents { get; set; }
    public List<Event<int>> IntEvents { get; set; }
    public List<Event<float>> FloatEvents { get; set; }

    public List<Event<string>> StringEvents { get; set; }

    public EventHandler WaitForInizialisation { get; set; }

    private void Start()
    {
        BoolEvents = new List<Event<bool>>();
        IntEvents = new List<Event<int>>();
        FloatEvents = new List<Event<float>>();
        StringEvents = new List<Event<string>>();

        if (WaitForInizialisation != null)
        {
            WaitForInizialisation.Invoke(this, null);
        }

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

        return new Event<bool>();
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

        return new Event<bool>();
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
        return new Event<int>();
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
        return new Event<int>();
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
        return new Event<float>();
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
        return new Event<float>();
    }

    public Event<string> FindEventInStrings(Guid name)
    {
        return StringEvents.Any(x => x.Id == name) ? StringEvents.FirstOrDefault(x => x.Id == name) : new Event<string>();
    }

    public Event<string> FindEventInStrings(string name)
    {
        return StringEvents.Any(x => x.Name == name) ? StringEvents.FirstOrDefault(x => x.Name == name) : new Event<string>();
    }
}
