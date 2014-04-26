using UnityEngine;
using System.Collections;

public class GameScreen : MetaContainer
{

	public GameScreen(string metadata) : base(metadata) {

	}

	public GameScreen() : base()
	{

	}

	//update any text fields or dynamic content... about to come on screen!
	virtual public void willShow()
	{
		
	}
	//screen "live" -- do any animations or whatever
	virtual public void didShow()
	{
		
	}
	
	//stop any animations, clean up, etc
	virtual public void willHide()
	{
	}
	
	//do any cleanup we want
	virtual public void didHide()
	{
	}
}

