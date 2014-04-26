using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FToggle : FStateButton
{
	public List<string> toggleStates;
	
	public delegate void FToggleSignalDelegate(FToggle toggle);
	public event FToggleSignalDelegate SignalToggle;
	
	public static FToggle WrapAndReplace(FStateButton button)
	{
		List<string> toggle_states = new List<string>();
		toggle_states.Add("on");
		toggle_states.Add("off");
		return WrapAndReplace(button, toggle_states);
	}
	
	public static FToggle WrapAndReplace(FStateButton button, List<string> toggle_states)
	{
		FToggle toggle = new FToggle(toggle_states);
		foreach(string key in button.buttons.Keys)
		{
			toggle.addState(key, button.buttons[key]);
		}
		toggle.finalize();
		
		toggle.x = button.x;
		toggle.y = button.y;
		
		if(button.container != null)
		{
			button.container.AddChildAtIndex(toggle, button.container.IndexOf(button.container));
			button.RemoveFromContainer();
			button.cleanUp();
		}
		return toggle;
	}
	
	override public void finalize()
	{
		base.finalize();
		
		SignalRelease += onRelease;
		state = toggleStates[0];
	}
	
	//TODO: change SignalToggle to SignalState and move it into the setter for state!
	//that will let us listen for state changes not dependent on touches
	public void onRelease(FStateButton button)
	{
		int which = toggleStates.IndexOf(state);
		if(which >= 0)
		{
			state = toggleStates[(which + 1) % toggleStates.Count];
			if(SignalToggle != null)
			{
				SignalToggle(this);
			}
		}else{
			Debug.Log ("DOING MY OWN THING");
		}
	}
	
	//defaults, yay!
	public FToggle():base()
	{
		toggleStates = new List<string>();
		toggleStates.Add("on");
		toggleStates.Add("off");
	}
	
	public FToggle(List<string> toggle_states):base()
	{
		toggleStates = toggle_states;
	}

	

}

