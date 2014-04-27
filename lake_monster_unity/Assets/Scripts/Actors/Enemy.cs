using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Enemy : Actor
{
	public FSprite sonar;
	
	public Vector2 sonar_vert_1;
	public Vector2 sonar_vert_2;
	public Vector2 sonar_vert_3;

	public Enemy(string name, List<PatrolStep> steps):base(name, steps)
	{

		sonar = new FSprite (name + "_sonar");
		AddChild (sonar);

		switch(name)
		{
			case "boat1":
				sonar.y = -165f;
				sonar.x = -14f;
				sonar_vert_1 = new Vector2(sonar.x, sonar.y + sonar.height/2);
				sonar_vert_2 = new Vector2(sonar.x + sonar.width/2, sonar.y - sonar.height/2);
				sonar_vert_3 = new Vector2(sonar.x - sonar.width/2, sonar.y - sonar.height/2);
				break;
			case "boat2":
				sonar.y = -91f;
				sonar.x = -7f;
				sonar_vert_1 = new Vector2(sonar.x, sonar.y + sonar.height/2);
				sonar_vert_2 = new Vector2(sonar.x + sonar.width/2, sonar.y - sonar.height/2);
				sonar_vert_3 = new Vector2(sonar.x - sonar.width/2, sonar.y - sonar.height/2);
				break;
			case "sub1":
				sonar.y = 4f;
				sonar.x = -175f;
				sonar_vert_1 = new Vector2(sonar.x + sonar.width/2, sonar.y);
				sonar_vert_2 = new Vector2(sonar.x - sonar.width/2, sonar.y - sonar.height/2);
				sonar_vert_3 = new Vector2(sonar.x - sonar.width/2, sonar.y + sonar.height/2);
				break;
			case "sub2":
				sonar.y = 4f;
				sonar.x = -105f;
				sonar_vert_1 = new Vector2(sonar.x + sonar.width/2, sonar.y);
				sonar_vert_2 = new Vector2(sonar.x - sonar.width/2, sonar.y - sonar.height/2);
				sonar_vert_3 = new Vector2(sonar.x - sonar.width/2, sonar.y + sonar.height/2);
				break;
			default:
				break;
		}
		
		
	}





}




