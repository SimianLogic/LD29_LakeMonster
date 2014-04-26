using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DataManager
{
	private static Dictionary<string, HashSet<Action<string, float>>> floatListeners = 
		new Dictionary<string, HashSet<Action<string, float>>>();
	private static Dictionary<string, HashSet<Action<string, string>>> stringListeners = 
		new Dictionary<string, HashSet<Action<string, string>>>();
	private static Dictionary<string, HashSet<Action<string, int>>> intListeners = 
		new Dictionary<string, HashSet<Action<string, int>>>();

	public static int GetInt(string key)
	{
		return PlayerPrefs.GetInt(key);
	}

	public static string GetString(string key)
	{
		return PlayerPrefs.GetString(key);
	}

	public static float GetFloat(string key)
	{
		return PlayerPrefs.GetFloat(key);
	}

	public static void ListenToInt(string key, Action<string, int> listener)
	{
		if(!intListeners.ContainsKey(key))
			intListeners.Add(key, new HashSet<Action<string, int>>());
		intListeners[key].Add(listener);
	}

	public static void StopListeningToInt(string key, Action<string, int> listener)
	{
		if(intListeners.ContainsKey(key))
		{
			intListeners[key].Remove(listener);
			if(intListeners[key].Count == 0)
			{
				intListeners.Remove(key);
			}
		}
	}

	public static void ListenToString(string key, Action<string, string> listener)
	{
		if(!stringListeners.ContainsKey(key))
			stringListeners.Add(key, new HashSet<Action<string, string>>());
		stringListeners[key].Add(listener);
	}
	
	public static void StopListeningToInt(string key, Action<string, string> listener)
	{
		if(stringListeners.ContainsKey(key))
		{
			stringListeners[key].Remove(listener);
			if(stringListeners[key].Count == 0)
			{
				stringListeners.Remove(key);
			}
		}
	}

	public static void ListenToFloat(string key, Action<string, float> listener)
	{
		if(!floatListeners.ContainsKey(key))
			floatListeners.Add(key, new HashSet<Action<string, float>>());
		floatListeners[key].Add(listener);
	}
	
	public static void StopListeningToFloat(string key, Action<string, float> listener)
	{
		if(floatListeners.ContainsKey(key))
		{
			floatListeners[key].Remove(listener);
			if(floatListeners[key].Count == 0)
			{
				floatListeners.Remove(key);
			}
		}
	}

	public static void SetInt(string key, int value)
	{
		PlayerPrefs.SetInt(key, value);
		PlayerPrefs.Save();

		if(intListeners.ContainsKey(key))
			foreach(Action<string, int> act in intListeners[key])
				act(key, value);
	}

	public static void SetString(string key, string value)
	{
		PlayerPrefs.SetString(key, value);
		PlayerPrefs.Save();
		
		if(stringListeners.ContainsKey(key))
			foreach(Action<string, string> act in stringListeners[key])
				act(key, value);
	}

	public static void SetFloat(string key, float value)
	{
		PlayerPrefs.SetFloat(key, value);
		PlayerPrefs.Save();
		
		if(floatListeners.ContainsKey(key))
			foreach(Action<string, float> act in floatListeners[key])
				act(key, value);
	}
}