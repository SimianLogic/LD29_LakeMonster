using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Human : Actor
{

	public Human(string name, List<Step> steps):base(name, steps)
	{
	}

	public Human(string name, Vector2 startPos):base(name, null)
	{
		x = startPos.x;
		y = startPos.y;
	}


}
