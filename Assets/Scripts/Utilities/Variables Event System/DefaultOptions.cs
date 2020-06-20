using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StringOption
{
    public string name;
    public string description;
    public string value;
}
public class DefaultOptions : MonoBehaviour
{
    public List<StringOption> stringOptions = new List<StringOption>();

    private VariableEventSystem variableEventSystem;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        variableEventSystem = GameObject.FindGameObjectWithTag("VariableEventSystem").GetComponent<VariableEventSystem>();
        variableEventSystem.WaitForInizialisation += SetVariables;
    }

    public void SetVariables(object sender, EventArgs e)
    {
        foreach (StringOption s in stringOptions)
        {
            variableEventSystem.StringEvents.Add(Event<string>.CreateInstance<string>(s.name.ToLower(), s.description.ToLower(), s.value.ToLower()));
        }
    }
}
