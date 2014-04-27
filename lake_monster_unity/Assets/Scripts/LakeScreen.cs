using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LakeScreen : GameScreen
{
	public List<Enemy> enemies;

	public LakeScreen() : base("monster_pieces")
	{
		Debug.Log("I'M A LAKE");

		images ["submarine"].RemoveFromContainer ();
		images["tentacle"].RemoveFromContainer ();
		images["boat"].RemoveFromContainer ();
		images["person"].RemoveFromContainer ();
		    
		enemies = new List<Enemy> ();

		float boat1Y = 800f;
		List<PatrolStep> steps1= new List<PatrolStep> ();
		steps1.Add (new PatrolStep (new Vector2 (-400, boat1Y), new Vector2 (400, boat1Y), 30f, -1));
		steps1.Add (new PatrolStep (new Vector2 (400, boat1Y), new Vector2 (-400, boat1Y), 30f, 1));

		Enemy boat1 = new Enemy ("boat1", steps1);
		AddChild (boat1);

		float boat2Y = 785f;
		List<PatrolStep> steps2 = new List<PatrolStep> ();
		steps2.Add (new PatrolStep (new Vector2 (200, boat2Y), new Vector2 (-400, boat2Y), 40f, 1));
		steps2.Add (new PatrolStep (new Vector2 (-400, boat2Y), new Vector2 (200, boat2Y), 40f, -1));
		
		Enemy boat2 = new Enemy ("boat2", steps2);
		AddChild (boat2);

		float sub1Y = 405f;
		List<PatrolStep> steps3 = new List<PatrolStep> ();
		steps3.Add (new PatrolStep (new Vector2 (450, sub1Y+50), new Vector2 (-50, sub1Y-100), 20f, 1));
		steps3.Add (new PatrolStep (new Vector2 (-50, sub1Y-100), new Vector2 (450, sub1Y+50), 20f, -1));
		
		Enemy sub1 = new Enemy ("sub1", steps3);
		AddChild (sub1);

		float sub2Y = 400f;
		List<PatrolStep> steps4= new List<PatrolStep> ();
		steps4.Add (new PatrolStep (new Vector2 (-440, sub2Y-50), new Vector2 (100, sub2Y+100), 50f, -1));
		steps4.Add (new PatrolStep (new Vector2 (100, sub2Y+100), new Vector2 (-440, sub2Y-50), 50f, 1));
		
		Enemy sub2 = new Enemy ("sub2", steps4);
		AddChild (sub2);

		float myScale = 0.7f;
		boat1.scaleX = boat1.scaleX* myScale;
		boat2.scaleX = boat2.scaleX * myScale;
		sub1.scaleX = sub1.scaleX * myScale;
		sub2.scaleX = sub2.scaleX * myScale;

		boat1.scaleY =  myScale;
		boat2.scaleY =  myScale;
		sub1.scaleY = myScale;
		sub2.scaleY = myScale;

		enemies.Add (boat1);
		enemies.Add (boat2);
		enemies.Add (sub1);
		enemies.Add (sub2);


	}

	public void Update()
	{
		foreach(Enemy enemy in enemies)
		{
			enemy.update();
		}
	}

}

