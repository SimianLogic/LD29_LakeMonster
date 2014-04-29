using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Enemy : Actor
{
	public string sonarName;
	public MetaContainer full_sonar;
	public FSprite sonar
	{
		get
		{
			return full_sonar.images[sonarName];
		}
	}

	public Enemy(string name, List<PatrolStep> steps):base(name, steps)
	{
		sonarName = name + "_sonar";
		full_sonar = new MetaContainer(sonarName);
		AddChild (sonar);

		switch(name)
		{
			case "boat1":
				full_sonar.y = -165f;
				full_sonar.x = -14f;
				break;
			case "boat2":
				full_sonar.y = -91f;
				full_sonar.x = -7f;
				break;
			case "sub1":
				full_sonar.y = 4f;
				full_sonar.x = -175f;
				break;
			case "sub2":
				full_sonar.y = 4f;
				full_sonar.x = -105f;
				break;
			default:
				break;
		}
		
		
	}





}




