using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public abstract class BaseModel<T> where T : class, new()
{
	private static bool SHOW_DEBUGGING_MESSAGES = false;

	protected static Dictionary<string, Dictionary<object, T>> cache;

	/**
	 * Populates the static cache of class T with values interpreted
	 * from the CSV file at "filename."  Expects CSV to be unquoted,
	 * whitespace sensitive, and composed of only ints, floats, bools,
	 * strings, and enums.
	 */
	public static void LoadFromData(string filename)
	{
		cache = new Dictionary<string, Dictionary<object, T>>();
		cache.Add("id", new Dictionary<object, T>());

		string[] text = (Resources.Load("Models/" + filename, typeof(TextAsset)) as TextAsset).text.Split('\n');
		
		if(SHOW_DEBUGGING_MESSAGES) Debug.Log("Preparing to load " + (text.Length-2) + " models from " + filename + ".csv");
		
		string[] props = text[0].Trim().Split(',');
		string[] types = text[1].Trim().Split(',');
		string[] row;
		
		List<int> ignoreCols = new List<int>();

		for(int i = 0; i < props.Length; i++)
		{
			if(CheckForField(props[i], types[i]))
			{
				if(SHOW_DEBUGGING_MESSAGES) Debug.Log("Including column " + i + " named " + props[i] + " in import.");
			}
			else
			{
				ignoreCols.Add(i);
				if(SHOW_DEBUGGING_MESSAGES) Debug.Log("Error parsing column " + i + ", named \"" + props[i] + "\" of type " + types[i] + "; column will be ignored.");
			}
		}

		T item;
		for(int j = 2; j < text.Length; j++)
		{
			item = new T();
			row = text[j].Trim().Split(',');

			if(row.Length >= props.Length)
			{
				for(int i = 0; i < props.Length; i++)
				{
					if(!ignoreCols.Contains(i))
					{
						PopulateField(typeof(T).GetField(props[i]), row[i], item);
					}
				}

				cache["id"].Add((int)typeof(T).GetField("id").GetValue(item), item);
			}
			
			MethodInfo onCreationComplete = typeof(T).GetMethod("OnCreationComplete");
			onCreationComplete.Invoke(item, null);
		}
	}
	
	//OVERRIDE ME TO DO POST-INIT FUNCTIONALITY
	//using the constructor, you won't have access to any of the loaded data yet
	virtual public void OnCreationComplete()
	{
	}

	/**
	 * Returns the item of the cache with the given id, if such an
	 * item exists.  Can return a single T item or can fail with an
	 * invalid dictionary call.
	 */
	public static T Find(int id)
	{
		if(SHOW_DEBUGGING_MESSAGES) Debug.Log("Found " + cache["id"].Count + " things.");
		return cache["id"][id];
	}

	/**
	 * Returns the item whose field named "prop" has the value
	 * provided as "value" iff such an item exists AND all items
	 * in the cache have unique values for "prop."  If these 
	 * conditions are met, builds a cache on the provided "prop"
	 * and returns the requested value.  Can return null or a
	 * single T object, or can throw an error.
	 */
	public static T FindByProperty(string prop, object value)
	{
		// Filter invalid queries.
		if(typeof(T).GetField(prop) == null || typeof(T).GetField(prop).FieldType != value.GetType())
			return null;

		// Return the query value, if available.
		if(cache.ContainsKey(prop))
			return cache[prop][value];

		// Build the new cache lazily, if necessary.
		cache.Add(prop, new Dictionary<object, T>());
		foreach(T item in cache["id"].Values)
		{
			object key = typeof(T).GetField(prop).GetValue(item);
			if(key == null || cache[prop].ContainsKey(key))
				throw new Exception("Illegal attempt to fetch models on non-unique or non-ubiquitous property \"" + prop + "\" (value = '" + key + "'.");
			else
				cache[prop].Add(key, item);
		}

		return cache[prop][value];
	}

	/**
	 * Returns a list of all items in the cache that have "value"
	 * in their field "prop."  Can return null, an empty list, or
	 * a list of values.
	 */
	public static List<T> FindAllByProperty(string prop, object value)
	{
		// Filter invalid queries.
		System.Reflection.FieldInfo field = typeof(T).GetField(prop);
		if(field == null || field.FieldType != value.GetType())
			return null;
		
		List<T> matches = new List<T>();
		
		// Return the query value, if available.  If cache exists, then there is certainly at most one match.
		if(cache.ContainsKey(prop))
		{
			matches.Add(cache[prop][value]);
			return matches;
		}
		
		foreach(T item in cache["id"].Values)
		{
			if(field.GetValue(item).Equals(value))
				matches.Add(item);
		}
		return matches;
	}

	private static bool CheckForField(string prop, string type)
	{
		return typeof(T).GetField(prop) != null && typeof(T).GetField(prop).FieldType == ParseType(type, prop);
	}

	private static System.Type ParseType(string type, string prop)
	{
		if(type == "int")
			return typeof(int);
		if(type == "string")
			return typeof(string);
		if(type == "float")
			return typeof(float);
		if(type == "bool")
			return typeof(bool);
		if(type == "enum" && typeof(T).GetField(prop) != null && typeof(T).GetField(prop).FieldType.IsEnum)
			return typeof(T).GetField(prop).FieldType;
		return System.Type.GetType(type);
	}

	private static void PopulateField(System.Reflection.FieldInfo field, string value, T target)
	{
		if(field.FieldType == typeof(int))
			field.SetValue(target, ParseInt(value));
		else if(field.FieldType == typeof(string))
			field.SetValue(target, value);
		else if(field.FieldType == typeof(float))
			field.SetValue(target, ParseFloat(value));
		else if(field.FieldType == typeof(bool))
			field.SetValue(target, value == "true");
		else if(field.FieldType.IsEnum)
		{
			System.Reflection.FieldInfo val = field.FieldType.GetField(value.Replace(' ', '_'));
			if(val != null)
				field.SetValue(target, val.GetRawConstantValue());
			else
				field.SetValue(target, field.FieldType.GetField("nil").GetRawConstantValue());
		}
	}

	private static int ParseInt(string value)
	{
		int ret = -1;
		try {
			ret = Convert.ToInt32(value);
		} catch(Exception) {
			if(SHOW_DEBUGGING_MESSAGES) Debug.Log("Parse error attempting to import value " + value + " as int; reverting to default of -1.");
			ret = -1;
		}
		return ret;
	}

	private static float ParseFloat(string value)
	{
		float ret = -1f;
		try {
			ret = Convert.ToSingle(value);
		} catch(Exception) {
			if(SHOW_DEBUGGING_MESSAGES) Debug.Log("Parse error attempting to import value " + value + " as float; reverting to default of -1f.");
			ret = -1f;
		}
		return ret;
	}
}