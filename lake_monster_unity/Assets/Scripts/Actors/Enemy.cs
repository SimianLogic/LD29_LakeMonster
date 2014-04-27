using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Enemy : Actor
{
	public FSprite sonar;

	public Enemy(string name, List<PatrolStep> steps):base(name, steps)
	{

		sonar = new FSprite (name + "_sonar");
		AddChild (sonar);

		switch(name)
		{
			case "boat1":
				sonar.y = -235f;
				sonar.x = -20f;
				break;
			case "boat2":
				sonar.y = -130f;
				sonar.x = -10f;
				break;
			case "sub1":
				sonar.y = 5f;
				sonar.x = -250f;
				break;
			case "sub2":
				sonar.y = 5f;
				sonar.x = -150f;
				break;
			default:
				break;
		}
	}





}




