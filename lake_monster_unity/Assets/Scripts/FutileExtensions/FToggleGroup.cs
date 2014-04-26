using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//TODO: would be cool to attach another nodes to specific toggle states so that when that toggle 
//is in that state the content is alive and vice versa

public class FToggleGroup
{
	private List<FToggle> toggles;
	
	public delegate void FToggleGroupChanged(FToggle toggle);
	public event FToggleGroupChanged TabChanged;

	public FToggleGroup()
	{
		toggles = new List<FToggle>();
	}

	public FToggleGroup(List<FToggle> in_toggles)
	{
		toggles = new List<FToggle>();
		foreach(FToggle toggle in in_toggles)
		{
			Add(toggle);
		}
	}

	public void Add(FToggle toggle)
	{
		if(toggles.Count == 0)
		{
			toggle.state = "on";  //on = selected state
		}else{
			toggle.state = "off";  //off = able to be pressed
		}
		
		toggles.Add(toggle);
		toggle.SignalToggle += ToggleToggled;
	}

	public void ToggleToggled(FToggle toggle)
	{	
		//if we hit a tab... turn off the others!
		if(toggle.state == "on")
		{
			foreach(FToggle t in toggles)
			{
				if(t != toggle && t.state == "on")
				{
					t.state = "off";
				}
			}
			
			if(TabChanged != null)
			{
				TabChanged(toggle);
			}
		}else{
			//reject the toggle
			toggle.state = "on";
		}
		
		//refresh state one last time for the one we selected in case any of our siblings turned off shared assets
		toggle.state = toggle.state;
	}
}