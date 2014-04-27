using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LevelActorPaths
{
	
	public static List<List<PatrolStep>> GetLevel1_Humans()
	{
		List<List<PatrolStep>> allPaths = new List<List<PatrolStep>> ();
		
		float y = 790f;
		List<PatrolStep> steps= new List<PatrolStep> ();
		steps.Add (new PatrolStep (new Vector2 (-150, y), new Vector2 (50, y), 5f, -1));
		steps.Add (new PatrolStep (new Vector2 (50, y), new Vector2 (-150, y), 5f, 1));
		
		allPaths.Add (steps);
		
		return allPaths;
	}

	public static List<List<PatrolStep>> GetLevel1_Enemies()
	{
		List<List<PatrolStep>> allPaths = new List<List<PatrolStep>> ();
		
		float y = 795f;
		List<PatrolStep> steps= new List<PatrolStep> ();
		steps.Add (new PatrolStep (new Vector2 (450, y), new Vector2 (-400, y), 50f, 1));
		steps.Add (new PatrolStep (new Vector2 (-400, y), new Vector2 (450, y), 50f, -1));

		float y2 = 795f;
		List<PatrolStep> steps2= new List<PatrolStep> ();
		steps2.Add (new PatrolStep (new Vector2 (-200, y2), new Vector2 (450, y2), 30f, -1));
		steps2.Add (new PatrolStep (new Vector2 (450, y2), new Vector2 (-200, y2), 30f, 1));


		allPaths.Add (steps);
		allPaths.Add (steps2);
		return allPaths;
	}

	public static List<List<PatrolStep>> GetLevel2_Humans()
	{
		List<List<PatrolStep>> allPaths = new List<List<PatrolStep>> ();
		
		float y = 790f;
		List<PatrolStep> steps= new List<PatrolStep> ();
		steps.Add (new PatrolStep (new Vector2 (-350, y), new Vector2 (50, y), 15f, -1));
		steps.Add (new PatrolStep (new Vector2 (50, y), new Vector2 (-350, y), 15f, 1));
		
		allPaths.Add (steps);
		
		return allPaths;
	}

	public static List<List<PatrolStep>> GetLevel2_Enemies()
	{
		List<List<PatrolStep>> allPaths = new List<List<PatrolStep>> ();
		
		float y = 790f;
		List<PatrolStep> steps= new List<PatrolStep> ();
		steps.Add (new PatrolStep (new Vector2 (450, y), new Vector2 (-400, y), 40f, 1));
		steps.Add (new PatrolStep (new Vector2 (-400, y), new Vector2 (450, y), 40f, -1));

		float y2 = 795f;
		List<PatrolStep> steps2= new List<PatrolStep> ();
		steps2.Add (new PatrolStep (new Vector2 (-300, y2), new Vector2 (150, y2), 50f, -1));
		steps2.Add (new PatrolStep (new Vector2 (150, y2), new Vector2 (-300, y2), 50f, 1));

		float y3 = 505f;
		List<PatrolStep> steps3= new List<PatrolStep> ();
		steps3.Add (new PatrolStep (new Vector2 (-300, y3), new Vector2 (350, y3), 30f, -1));
		steps3.Add (new PatrolStep (new Vector2 (350, y3), new Vector2 (-300, y3), 30f, 1));
				
		allPaths.Add (steps);
		allPaths.Add (steps2);
		allPaths.Add (steps3);

		return allPaths;
	}

	public static List<List<PatrolStep>> GetLevel3_Humans()
	{
		List<List<PatrolStep>> allPaths = new List<List<PatrolStep>> ();
		
		float y = 790f;
		List<PatrolStep> steps= new List<PatrolStep> ();
		steps.Add (new PatrolStep (new Vector2 (-50, y), new Vector2 (-450, y), 10f, 1));
		steps.Add (new PatrolStep (new Vector2 (-450, y), new Vector2 (-50, y), 10f, -1));
		
		float y2 = 680f;
		List<PatrolStep> steps2= new List<PatrolStep> ();
		steps2.Add (new PatrolStep (new Vector2 (-200, y2), new Vector2 (250, y2), 20f, -1));
		steps2.Add (new PatrolStep (new Vector2 (250, y2), new Vector2 (-200, y2), 20f, 1));
		
		allPaths.Add (steps);
		allPaths.Add (steps2);
		return allPaths;
	}
	
	public static List<List<PatrolStep>> GetLevel3_Enemies()
	{
		List<List<PatrolStep>> allPaths = new List<List<PatrolStep>> ();
		
		float y = 795f;
		List<PatrolStep> steps= new List<PatrolStep> ();
		steps.Add (new PatrolStep (new Vector2 (450, y), new Vector2 (-400, y), 40f, 1));
		steps.Add (new PatrolStep (new Vector2 (-400, y), new Vector2 (450, y), 40f, -1));
		
		float y2 = 790f;
		List<PatrolStep> steps2= new List<PatrolStep> ();
		steps2.Add (new PatrolStep (new Vector2 (-300, y2), new Vector2 (150, y2), 50f, -1));
		steps2.Add (new PatrolStep (new Vector2 (150, y2), new Vector2 (-300, y2), 50f, 1));
		
		float y3 = 570f;
		List<PatrolStep> steps3= new List<PatrolStep> ();
		steps3.Add (new PatrolStep (new Vector2 (-450, y3+10), new Vector2 (250, y3-30), 30f, -1));
		steps3.Add (new PatrolStep (new Vector2 (250, y3-30), new Vector2 (-450, y3+10), 30f, 1));

		float y4 = 425f;
		List<PatrolStep> steps4= new List<PatrolStep> ();
		steps4.Add (new PatrolStep (new Vector2 (450, y4+40), new Vector2 (-300, y4), 40f, 1));
		steps4.Add (new PatrolStep (new Vector2 (-300, y4), new Vector2 (450, y4+40), 40f, -1));

		
		allPaths.Add (steps);
		allPaths.Add (steps2);
		allPaths.Add (steps3);
		allPaths.Add (steps4);

		return allPaths;
	}

	public static List<List<PatrolStep>> GetLevel4_Humans()
	{
		List<List<PatrolStep>> allPaths = new List<List<PatrolStep>> ();
		
		float y = 790f;
		List<PatrolStep> steps= new List<PatrolStep> ();
		steps.Add (new PatrolStep (new Vector2 (-450, y), new Vector2 (150, y), 5f, -1));
		steps.Add (new PatrolStep (new Vector2 (150, y), new Vector2 (-450, y), 5f, 1));
		
		float y2 = 720f;
		List<PatrolStep> steps2= new List<PatrolStep> ();
		steps2.Add (new PatrolStep (new Vector2 (-200, y2), new Vector2 (250, y2), 15f, -1));
		steps2.Add (new PatrolStep (new Vector2 (250, y2), new Vector2 (-200, y2), 15f, 1));

		float y3 = 590f;
		List<PatrolStep> steps3= new List<PatrolStep> ();
		steps3.Add (new PatrolStep (new Vector2 (-80, y3+90), new Vector2 (-300, y3-10), 20f, 1));
		steps3.Add (new PatrolStep (new Vector2 (-300, y3-10), new Vector2 (350, y3), 20f, -1));
		steps3.Add (new PatrolStep (new Vector2 (350, y3), new Vector2 (-80, y3+90), 20f, 1));

		allPaths.Add (steps);
		allPaths.Add (steps2);
		allPaths.Add (steps3);
		return allPaths;
	}
	
	public static List<List<PatrolStep>> GetLevel4_Enemies()
	{
		List<List<PatrolStep>> allPaths = new List<List<PatrolStep>> ();
		
		float y = 795f;
		List<PatrolStep> steps= new List<PatrolStep> ();
		steps.Add (new PatrolStep (new Vector2 (450, y), new Vector2 (-400, y), 30f, 1));
		steps.Add (new PatrolStep (new Vector2 (-400, y), new Vector2 (450, y), 30f, -1));
		
		float y2 = 790f;
		List<PatrolStep> steps2= new List<PatrolStep> ();
		steps2.Add (new PatrolStep (new Vector2 (-300, y2), new Vector2 (250, y2), 60f, -1));
		steps2.Add (new PatrolStep (new Vector2 (250, y2), new Vector2 (-300, y2), 60f, 1));
		
		float y3 = 570f;
		List<PatrolStep> steps3= new List<PatrolStep> ();
		steps3.Add (new PatrolStep (new Vector2 (-450, y3-50), new Vector2 (450, y3+10), 30f, -1));
		steps3.Add (new PatrolStep (new Vector2 (450, y3+10),  new Vector2 (400, y3+60), 30f, 1));
		steps3.Add (new PatrolStep (new Vector2 (400, y3+60),  new Vector2 (-400, y3), 30f, 1));
		steps3.Add (new PatrolStep (new Vector2 (-400, y3),  new Vector2 (-450, y3-50), 30f, -1));
		
		float y4 = 425f;
		List<PatrolStep> steps4= new List<PatrolStep> ();
		steps4.Add (new PatrolStep (new Vector2 (450, y4+40), new Vector2 (-300, y4), 50f, 1));
		steps4.Add (new PatrolStep (new Vector2 (-300, y4), new Vector2 (450, y4+40), 50f, -1));

		
		allPaths.Add (steps);
		allPaths.Add (steps2);
		allPaths.Add (steps3);
		allPaths.Add (steps4);
		
		return allPaths;
	}

}

