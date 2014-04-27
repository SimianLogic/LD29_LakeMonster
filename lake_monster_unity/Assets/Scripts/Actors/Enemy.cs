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
				sonar.y = -165f;
				sonar.x = -14f;
				break;
			case "boat2":
				sonar.y = -91f;
				sonar.x = -7f;
				break;
			case "sub1":
				sonar.y = 4f;
				sonar.x = -175f;
				break;
			case "sub2":
				sonar.y = 4f;
				sonar.x = -105f;
				break;
			default:
				break;
		}
	}





}




