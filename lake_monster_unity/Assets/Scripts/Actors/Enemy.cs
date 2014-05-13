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

	public Enemy(string name, List<Step> steps):base(name, steps)
	{
		sonarName = name + "_sonar";
		full_sonar = new MetaContainer(sonarName);
		AddChild (sonar);

		switch(name)
		{
			case "boat1":
				sonar.y = -165f;
				sonar.x = -14f;
				sonar_vert_1 = new Vector2(sonar.x, sonar.y + sonar.height/2);
				sonar_vert_2 = new Vector2(sonar.x - sonar.width/2, sonar.y - sonar.height/2);
				sonar_vert_3 = new Vector2(sonar.x + sonar.width/2, sonar.y - sonar.height/2);
				break;
			case "boat2":
				sonar.y = -91f;
				sonar.x = -7f;
				sonar_vert_1 = new Vector2(sonar.x, sonar.y + sonar.height/2);
				sonar_vert_2 = new Vector2(sonar.x - sonar.width/2, sonar.y - sonar.height/2);
				sonar_vert_3 = new Vector2(sonar.x + sonar.width/2, sonar.y - sonar.height/2);
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




