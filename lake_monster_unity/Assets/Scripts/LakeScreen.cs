using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LakeScreen : GameScreen
{
	public List<Enemy> enemies;

	public LakeScreen() : base("monster_pieces")
	{
		Debug.Log("I'M A LAKE");
		images ["hello"].RemoveFromContainer ();
		images ["submarine"].RemoveFromContainer ();
		images["tentacle"].RemoveFromContainer ();
		images["boat"].RemoveFromContainer ();
		images["person"].RemoveFromContainer ();
		    
		enemies = new List<Enemy> ();

		Enemy boat1 = new Enemy ("boat");
		AddChild (boat1);

		enemies.Add (boat1);

	}

	public void update()
	{
		foreach(Enemy enemy in enemies)
		{
			enemy.update();
		}
	}

}

