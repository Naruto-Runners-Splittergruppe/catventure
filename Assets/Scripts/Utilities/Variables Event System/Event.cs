using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event<T>
{
	private T eventVariable;
	private EventHandler eventVariableSet;
	private EventHandler eventVariableGet;

	public EventHandler EventVariableGet
	{
		get { return eventVariableGet; }
		set { eventVariableGet = value; }
	}


	public EventHandler EventVariableSet
	{
		get { return eventVariableSet; }
		set { eventVariableSet = value; }
	}


	public T EventVariable
	{
		get
		{
			eventVariableGet.Invoke(this, null);
			return eventVariable;
		}
		set
		{
			eventVariableSet.Invoke(this, null);
			eventVariable = value;
		}
	}

	public Guid Id { get; internal set; }
	public string Name { get; internal set; }

	public string Description { get; internal set; }

	public static Event<S> CreateInstance<S>(string name, string description, S value)
	{
		Event <S> createdEvent = new Event<S>() {Id = new Guid(), Name = name, Description = description, EventVariable = value};
		return createdEvent;
	}
}
