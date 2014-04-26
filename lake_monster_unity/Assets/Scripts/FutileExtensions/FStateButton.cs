using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FStateButton : FContainer
{
	public delegate void FStateButtonSignalDelegate(FStateButton button);
	
	public event FStateButtonSignalDelegate SignalPress;
	public event FStateButtonSignalDelegate SignalRelease;
	public event FStateButtonSignalDelegate SignalReleaseOutside;
	
	private bool _isEnabled;
	private string _state;
	
	public Dictionary<string, FButton> buttons;
	private Dictionary<string, List<FNode>> linkedNodes;
	
	//INSTANTIATION PROCESS:
	//   make an empty FToggle
	//   call addState a bunch of times
	//   call finalize, which will "consume" the buttons, parent them to this node, and place itself in the display tree
	public FStateButton()
	{
		buttons = new Dictionary<string, FButton>();
		linkedNodes = new Dictionary<string, List<FNode>>();
		_isEnabled = true; //default ON
	}
	
	public virtual void addState(string state_name, FButton button)
	{
		buttons[state_name] = button;
		buttons[state_name].SignalPress += OnSignalPress;
		buttons[state_name].SignalRelease += OnSignalRelease;
		buttons[state_name].SignalReleaseOutside += OnSignalReleaseOutside;
		
		if(isFinalized)
		{
			button.x -= offsetX;
			button.y -= offsetY;
			AddChild(button);
		}
	}
	
	public virtual void linkNodeToState(FNode node, string linked_state)
	{
		if(!linkedNodes.ContainsKey(linked_state))
		{
			linkedNodes[linked_state] = new List<FNode>();
		}
		
		//don't double add...
		if(linkedNodes[linked_state].Contains(node))
		{
			return;
		}
		
		linkedNodes[linked_state].Add(node);
		node.isVisible = (state == linked_state);
	}
	
	public virtual void cleanUp()
	{
		foreach(string state_name in buttons.Keys)
		{
			buttons[state_name].SignalPress -= OnSignalPress;
			buttons[state_name].SignalRelease -= OnSignalRelease;
			buttons[state_name].SignalReleaseOutside -= OnSignalReleaseOutside;
		}
	}
	
	private float offsetX, offsetY;
	private bool isFinalized = false;
	public virtual void finalize()
	{
		if(buttons.Keys.Count < 1)
		{
			throw new FutileException("You need to add more states to your toggle first!");
		}
		
		isFinalized = true;
		
		//kind of arbitrary... just pick one of the button states to offset the others by...
		offsetX = -1;
		offsetY = -1;
		int lowestChild = -1;
		FContainer parent = null;
		foreach(FButton button in buttons.Values)
		{
			if(offsetX == -1f) offsetX = button.x;
			if(offsetY == -1f) offsetY = button.y;
			
			if(button.container != null)
			{
				if(parent == null) parent = button.container;
				if(lowestChild == -1 || parent.IndexOf(button) < lowestChild) lowestChild = parent.IndexOf(button);
			}
			
			AddChild(button);
			button.x -= offsetX;
			button.y -= offsetY;
		}
		
		//place us back in the parent display tree
		parent.AddChildAtIndex(this, lowestChild);
		x = offsetX;
		y = offsetY;
	}
	
	
	public virtual string state
	{
		get { return _state; }
		set 
		{ 
			string old_state = _state;
			_state = value;
			foreach(string key in buttons.Keys)
			{
				buttons[key].isEnabled = (key == _state);
				buttons[key].isVisible = (key == _state);
			}
			
			handleStateChange(old_state, _state);
			
		}
	}
	
	public virtual void handleStateChange(string old_state, string new_state)
	{
		if(linkedNodes == null) return;
		
		if(old_state != null && linkedNodes.ContainsKey(old_state))
		{
			foreach(FNode node in linkedNodes[old_state])
			{
				node.isVisible = false;
			}
		}
		
		if(new_state != null && linkedNodes.ContainsKey(new_state))
		{
			foreach(FNode node in linkedNodes[new_state])
			{
				node.isVisible = true;
			}
		}
		
	}
	
	public void OnSignalPress(FButton button)
	{
		if(SignalPress != null)
		{
			SignalPress(this);
		}
	}
	
	public void OnSignalRelease(FButton button)
	{
		if(SignalRelease != null)
		{
			SignalRelease(this);
		}
	}
	
	public void OnSignalReleaseOutside(FButton button)
	{
		if(SignalReleaseOutside != null)
		{
			SignalReleaseOutside(this);
		}
	}
	
	
	public bool isEnabled
	{
		get {return _isEnabled;}
		set 
		{
			if(_isEnabled != value)
			{
				_isEnabled = value;
				foreach(string key in buttons.Keys)
				{
					FButton button = buttons[key];
					button.isEnabled = _isEnabled && key == state;
				}
			}
		}
	}
	
}

