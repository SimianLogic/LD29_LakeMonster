using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LevelActorPaths
{
	public const float TURN_SPEED = 0.5f;
	
	public static List<List<Step>> GetLevel1_Humans()
	{
		List<List<Step>> allPaths = new List<List<Step>> ();
		
		float y = 790f;
		List<Step> steps= new List<Step> ();
		steps.Add (new PatrolStep(new Vector2 (-150, y), new Vector2 (50, y), 5f, -1));
		steps.Add (new TurnStep(TURN_SPEED));
		steps.Add (new PatrolStep(new Vector2 (50, y), new Vector2 (-150, y), 5f, 1));
		steps.Add (new TurnStep(TURN_SPEED));
		
		allPaths.Add (steps);
		
		return allPaths;
	}

	public static List<List<Step>> GetLevel1_Enemies()
	{
		List<List<Step>> allPaths = new List<List<Step>> ();
		
		float y = 795f;
		List<Step> steps= new List<Step> ();
		steps.Add (new PatrolStep (new Vector2 (450, y), new Vector2 (-400, y), 50f, 1));
		steps.Add (new TurnStep(TURN_SPEED));
		steps.Add (new PatrolStep (new Vector2 (-400, y), new Vector2 (450, y), 50f, -1));
		steps.Add (new TurnStep(TURN_SPEED));

		float y2 = 795f;
		List<Step> steps2= new List<Step> ();
		steps2.Add (new PatrolStep (new Vector2 (-200, y2), new Vector2 (450, y2), 30f, -1));
		steps2.Add (new TurnStep(TURN_SPEED));
		steps2.Add (new PatrolStep (new Vector2 (450, y2), new Vector2 (-200, y2), 30f, 1));
		steps2.Add (new TurnStep(TURN_SPEED));


		allPaths.Add (steps);
		allPaths.Add (steps2);
		return allPaths;
	}

	public static List<List<Step>> GetLevel2_Humans()
	{
		List<List<Step>> allPaths = new List<List<Step>> ();
		
		float y = 790f;
		List<Step> steps= new List<Step> ();
		steps.Add (new PatrolStep (new Vector2 (-350, y), new Vector2 (50, y), 15f, -1));
		steps.Add (new TurnStep(TURN_SPEED));
		steps.Add (new PatrolStep (new Vector2 (50, y), new Vector2 (-350, y), 15f, 1));
		steps.Add (new TurnStep(TURN_SPEED));
		
		allPaths.Add (steps);
		
		return allPaths;
	}

	public static List<List<Step>> GetLevel2_Enemies()
	{
		List<List<Step>> allPaths = new List<List<Step>> ();
		
		float y = 790f;
		List<Step> steps= new List<Step> ();
		steps.Add (new PatrolStep (new Vector2 (450, y), new Vector2 (-400, y), 40f, 1));
		steps.Add (new TurnStep(TURN_SPEED));
		steps.Add (new PatrolStep (new Vector2 (-400, y), new Vector2 (450, y), 40f, -1));
		steps.Add (new TurnStep(TURN_SPEED));

		float y2 = 795f;
		List<Step> steps2= new List<Step> ();
		steps2.Add (new PatrolStep (new Vector2 (-300, y2), new Vector2 (150, y2), 50f, -1));
		steps2.Add (new TurnStep(TURN_SPEED));
		steps2.Add (new PatrolStep (new Vector2 (150, y2), new Vector2 (-300, y2), 50f, 1));
		steps2.Add (new TurnStep(TURN_SPEED));

		float y3 = 505f;
		List<Step> steps3= new List<Step> ();
		steps3.Add (new PatrolStep (new Vector2 (-300, y3), new Vector2 (350, y3), 30f, -1));
		steps3.Add (new TurnStep(TURN_SPEED));
		steps3.Add (new PatrolStep (new Vector2 (350, y3), new Vector2 (-300, y3), 30f, 1));
		steps3.Add (new TurnStep(TURN_SPEED));
				
		allPaths.Add (steps);
		allPaths.Add (steps2);
		allPaths.Add (steps3);

		return allPaths;
	}

	public static List<List<Step>> GetLevel3_Humans()
	{
		List<List<Step>> allPaths = new List<List<Step>> ();
		
		float y = 790f;
		List<Step> steps= new List<Step> ();
		steps.Add (new PatrolStep (new Vector2 (-50, y), new Vector2 (-450, y), 10f, 1));
		steps.Add (new TurnStep(TURN_SPEED));
		steps.Add (new PatrolStep (new Vector2 (-450, y), new Vector2 (-50, y), 10f, -1));
		steps.Add (new TurnStep(TURN_SPEED));
		
		float y2 = 680f;
		List<Step> steps2= new List<Step> ();
		steps2.Add (new PatrolStep (new Vector2 (-200, y2), new Vector2 (250, y2), 20f, -1));
		steps2.Add (new TurnStep(TURN_SPEED));
		steps2.Add (new PatrolStep (new Vector2 (250, y2), new Vector2 (-200, y2), 20f, 1));
		steps2.Add (new TurnStep(TURN_SPEED));
		
		allPaths.Add (steps);
		allPaths.Add (steps2);
		return allPaths;
	}
	
	public static List<List<Step>> GetLevel3_Enemies()
	{
		List<List<Step>> allPaths = new List<List<Step>> ();
		
		float y = 795f;
		List<Step> steps= new List<Step> ();
		steps.Add (new PatrolStep (new Vector2 (450, y), new Vector2 (-400, y), 40f, 1));
		steps.Add (new TurnStep(TURN_SPEED));
		steps.Add (new PatrolStep (new Vector2 (-400, y), new Vector2 (450, y), 40f, -1));
		steps.Add (new TurnStep(TURN_SPEED));
		
		float y2 = 790f;
		List<Step> steps2= new List<Step> ();
		steps2.Add (new PatrolStep (new Vector2 (-300, y2), new Vector2 (150, y2), 50f, -1));
		steps2.Add (new TurnStep(TURN_SPEED));
		steps2.Add (new PatrolStep (new Vector2 (150, y2), new Vector2 (-300, y2), 50f, 1));
		steps2.Add (new TurnStep(TURN_SPEED));
		
		float y3 = 570f;
		List<Step> steps3= new List<Step> ();
		steps3.Add (new PatrolStep (new Vector2 (-450, y3+10), new Vector2 (250, y3-30), 30f, -1));
		steps3.Add (new TurnStep(TURN_SPEED));
		steps3.Add (new PatrolStep (new Vector2 (250, y3-30), new Vector2 (-450, y3+10), 30f, 1));
		steps3.Add (new TurnStep(TURN_SPEED));

		float y4 = 425f;
		List<Step> steps4= new List<Step> ();
		steps4.Add (new PatrolStep (new Vector2 (450, y4+40), new Vector2 (-300, y4), 40f, 1));
		steps4.Add (new TurnStep(TURN_SPEED));
		steps4.Add (new PatrolStep (new Vector2 (-300, y4), new Vector2 (450, y4+40), 40f, -1));
		steps4.Add (new TurnStep(TURN_SPEED));

		
		allPaths.Add (steps);
		allPaths.Add (steps2);
		allPaths.Add (steps3);
		allPaths.Add (steps4);

		return allPaths;
	}

	public static List<List<Step>> GetLevel4_Humans()
	{
		List<List<Step>> allPaths = new List<List<Step>> ();
		
		float y = 790f;
		List<Step> steps= new List<Step> ();
		steps.Add (new PatrolStep (new Vector2 (-450, y), new Vector2 (150, y), 5f, -1));
		steps.Add (new TurnStep(TURN_SPEED));
		steps.Add (new PatrolStep (new Vector2 (150, y), new Vector2 (-450, y), 5f, 1));
		steps.Add (new TurnStep(TURN_SPEED));
		
		float y2 = 720f;
		List<Step> steps2= new List<Step> ();
		steps2.Add (new PatrolStep (new Vector2 (-200, y2), new Vector2 (250, y2), 15f, -1));
		steps2.Add (new TurnStep(TURN_SPEED));
		steps2.Add (new PatrolStep (new Vector2 (250, y2), new Vector2 (-200, y2), 15f, 1));
		steps2.Add (new TurnStep(TURN_SPEED));

		float y3 = 590f;
		List<Step> steps3= new List<Step> ();
		steps3.Add (new PatrolStep (new Vector2 (-80, y3+90), new Vector2 (-300, y3-10), 20f, 1));
		steps3.Add (new TurnStep(TURN_SPEED));
		steps3.Add (new PatrolStep (new Vector2 (-300, y3-10), new Vector2 (350, y3), 20f, -1));
		steps3.Add (new TurnStep(TURN_SPEED));
		steps3.Add (new PatrolStep (new Vector2 (350, y3), new Vector2 (-80, y3+90), 20f, 1));
		steps3.Add (new TurnStep(TURN_SPEED));

		allPaths.Add (steps);
		allPaths.Add (steps2);
		allPaths.Add (steps3);
		return allPaths;
	}
	
	public static List<List<Step>> GetLevel4_Enemies()
	{
		List<List<Step>> allPaths = new List<List<Step>> ();
		
		float y = 795f;
		List<Step> steps= new List<Step> ();
		steps.Add (new PatrolStep (new Vector2 (450, y), new Vector2 (-400, y), 30f, 1));
		steps.Add (new TurnStep(TURN_SPEED));
		steps.Add (new PatrolStep (new Vector2 (-400, y), new Vector2 (450, y), 30f, -1));
		steps.Add (new TurnStep(TURN_SPEED));
		
		float y2 = 790f;
		List<Step> steps2= new List<Step> ();
		steps2.Add (new PatrolStep (new Vector2 (-300, y2), new Vector2 (250, y2), 60f, -1));
		steps2.Add (new TurnStep(TURN_SPEED));
		steps2.Add (new PatrolStep (new Vector2 (250, y2), new Vector2 (-300, y2), 60f, 1));
		steps2.Add (new TurnStep(TURN_SPEED));
		
		float y3 = 570f;
		List<Step> steps3= new List<Step> ();
		steps3.Add (new PatrolStep (new Vector2 (-450, y3-50), new Vector2 (450, y3+10), 30f, -1));
		steps3.Add (new TurnStep(TURN_SPEED));
		steps3.Add (new PatrolStep (new Vector2 (450, y3+10),  new Vector2 (400, y3+60), 30f, 1));
		steps3.Add (new PatrolStep (new Vector2 (400, y3+60),  new Vector2 (-400, y3), 30f, 1));
		steps3.Add (new TurnStep(TURN_SPEED));
		steps3.Add (new PatrolStep (new Vector2 (-400, y3),  new Vector2 (-450, y3-50), 30f, -1));
		
		float y4 = 425f;
		List<Step> steps4= new List<Step> ();
		steps4.Add (new PatrolStep (new Vector2 (450, y4+40), new Vector2 (-300, y4), 50f, 1));
		steps4.Add (new TurnStep(TURN_SPEED));
		steps4.Add (new PatrolStep (new Vector2 (-300, y4), new Vector2 (450, y4+40), 50f, -1));
		steps4.Add (new TurnStep(TURN_SPEED));

		
		allPaths.Add (steps);
		allPaths.Add (steps2);
		allPaths.Add (steps3);
		allPaths.Add (steps4);
		
		return allPaths;
	}

	public static List<List<Step>> GetLevel5_Humans()
	{
		List<List<Step>> allPaths = new List<List<Step>> ();
		
		float y = 790f;
		List<Step> steps= new List<Step> ();
		steps.Add (new PatrolStep (new Vector2 (-450, y), new Vector2 (150, y), 5f, -1));
		steps.Add (new TurnStep(TURN_SPEED));
		steps.Add (new PatrolStep (new Vector2 (150, y), new Vector2 (-450, y), 5f, 1));
		
		float y2 = 720f;
		List<Step> steps2= new List<Step> ();
		steps2.Add (new PatrolStep (new Vector2 (-400, y2), new Vector2 (150, y2-40), 15f, -1));
		steps2.Add (new TurnStep(TURN_SPEED));
		steps2.Add (new PatrolStep (new Vector2 (150, y2-40), new Vector2 (-20, y2-100), 15f, 1));
		steps2.Add (new PatrolStep (new Vector2 (-20, y2-100), new Vector2 (-400, y2), 15f, 1));
		steps2.Add (new TurnStep(TURN_SPEED));
		
		float y3 = 590f;
		List<Step> steps3= new List<Step> ();
		steps3.Add (new PatrolStep (new Vector2 (-80, y3+90), new Vector2 (-200, y3-20), 20f, 1));
		steps3.Add (new TurnStep(TURN_SPEED));
		steps3.Add (new PatrolStep (new Vector2 (-200, y3-20), new Vector2 (250, y3), 20f, -1));
		steps3.Add (new TurnStep(TURN_SPEED));
		steps3.Add (new PatrolStep (new Vector2 (250, y3), new Vector2 (-80, y3+90), 20f, 1));
		
		allPaths.Add (steps);
		allPaths.Add (steps2);
		allPaths.Add (steps3);
		return allPaths;
	}
	
	public static List<List<Step>> GetLevel5_Enemies()
	{
		List<List<Step>> allPaths = new List<List<Step>> ();
		
		float y = 795f;
		List<Step> steps= new List<Step> ();
		steps.Add (new PatrolStep (new Vector2 (450, y), new Vector2 (-400, y), 30f, 1));
		steps.Add (new TurnStep(TURN_SPEED));
		steps.Add (new PatrolStep (new Vector2 (-400, y), new Vector2 (450, y), 30f, -1));
		steps.Add (new TurnStep(TURN_SPEED));
		
		float y2 = 790f;
		List<Step> steps2= new List<Step> ();
		steps2.Add (new PatrolStep (new Vector2 (-300, y2), new Vector2 (250, y2), 60f, -1));
		steps2.Add (new TurnStep(TURN_SPEED));
		steps2.Add (new PatrolStep (new Vector2 (250, y2), new Vector2 (-300, y2), 60f, 1));
		steps2.Add (new TurnStep(TURN_SPEED));
		
		float y3 = 670f;
		List<Step> steps3= new List<Step> ();
		steps3.Add (new PatrolStep (new Vector2 (-450, y3-60), new Vector2 (450, y3+10), 30f, -1));
		steps3.Add (new TurnStep(TURN_SPEED));
		steps3.Add (new PatrolStep (new Vector2 (450, y3+10),  new Vector2 (400, y3+90), 30f, 1));
		steps3.Add (new PatrolStep (new Vector2 (400, y3+90),  new Vector2 (-400, y3), 30f, 1));
		steps3.Add (new TurnStep(TURN_SPEED));
		steps3.Add (new PatrolStep (new Vector2 (-400, y3),  new Vector2 (-450, y3-60), 30f, -1));
		
		float y4 = 595f;
		List<Step> steps4= new List<Step> ();
		steps4.Add (new PatrolStep (new Vector2 (450, y4+80), new Vector2 (-350, y4-50), 50f, 1));
		steps4.Add (new TurnStep(TURN_SPEED));
		steps4.Add (new PatrolStep (new Vector2 (-350, y4-50), new Vector2 (-350, y4+90), 50f, -1));
		steps4.Add (new PatrolStep (new Vector2 (-350, y4+90), new Vector2 (450, y4-60), 50f, -1));
		steps4.Add (new TurnStep(TURN_SPEED));
		steps4.Add (new PatrolStep (new Vector2 (450, y4-60), new Vector2 (450, y4+80), 50f, 1));		

		float y5 = 500f;
		List<Step> steps5= new List<Step> ();
		steps5.Add (new PatrolStep (new Vector2 (250, y5-30), new Vector2 (-250, y5+10), 60f, 1));
		steps5.Add (new TurnStep(TURN_SPEED));
		steps5.Add (new PatrolStep (new Vector2 (-250, y5+10), new Vector2 (250, y5-30), 60f, -1));
		steps5.Add (new TurnStep(TURN_SPEED));


		allPaths.Add (steps);
		allPaths.Add (steps2);
		allPaths.Add (steps3);
		allPaths.Add (steps4);
		allPaths.Add (steps5);

		return allPaths;
	}

}

