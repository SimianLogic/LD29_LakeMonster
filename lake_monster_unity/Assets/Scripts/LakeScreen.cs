using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//TODO: MAYBE PASS LEVEL ID?
public delegate void GameOverDelegate();
public delegate void VictoryDelegate();

public class LakeScreen : GameScreen, FSingleTouchableInterface
{
	public GameOverDelegate onGameOver;
	public VictoryDelegate onVictory;

	public const float TENTACLE_GROWTH_SPEED = 0.02f;
	public const float TENTACLE_GROWTH_RATE = 0.02f;
	public const float TENTACLE_MAX_TURN_ANGLE = 45f;
	public const float TENTACLE_BONE_LENGTH = 0.45f;
	public const int MAX_TENTACLE_PIECES = 75;
	
	public List<Enemy> enemies;
	public List<Human> humans;
	
	public Dictionary<Actor,FSprite> debugRects;
	
	public List<FSprite> tentaclePieces;
	
	public bool isDragging;
	public float lastX;
	public float lastY;
	public float lastUpdate;
	public float depthY;
	
	public bool hasAHuman;
	public Human fishFood;

	public FContainer foreground;
	public FContainer midground;
	public FContainer background;
	

	public LakeScreen() : base("monster_pieces")
	{
		EnableSingleTouch();

		foreground = new FContainer ();
		midground  = new FContainer ();
		background = new FContainer ();

		AddChild (background);
		AddChild (midground);
		AddChild (foreground);

		enemies = new List<Enemy> ();
		humans =  new List<Human> ();
		debugRects = new Dictionary<Actor, FSprite>();

		tentaclePieces = new List<FSprite>();
		
		depthY = rootHeight/2 - Futile.screen.height - 50;
	}
	
	public void startLevel(int which)
	{
		clearMe();
		switch(which)
		{
			case 1:
				InitLevel1();
				break;
			case 2:
				InitLevel2();
				break;
			case 3:
				InitLevel3();
				break;
			case 4:
				InitLevel4();
				break;				
			case 5:
				InitLevel5();
				break;	
			default:
				Debug.Log ("NO LEVEL " + which);
				break;
		}
	}
	
	public void clearMe()
	{
		background.RemoveAllChildren ();
		foreground.RemoveAllChildren ();
		midground.RemoveAllChildren ();

		enemies.Clear ();
		humans.Clear ();

		foreach(FSprite tentacle in tentaclePieces)
		{
			tentacle.RemoveFromContainer();
		}
		tentaclePieces.Clear ();
		
		foreach(FSprite debug in debugRects.Values)
		{
			debug.RemoveFromContainer();
		}
		debugRects.Clear();
	}

	public void InitLevel1()
	{
		clearMe ();
		
		Human human1 = new Human ("person_boat", LevelActorPaths.GetLevel1_Humans()[0]);
		foreground.AddChild (human1);
		humans.Add (human1);

		List<List<PatrolStep>> allPaths = LevelActorPaths.GetLevel1_Enemies ();

		Enemy boat1 = new Enemy ("boat2", allPaths[0]);
		midground.AddChild (boat1);
		Enemy boat2 = new Enemy ("boat1", allPaths[1]);
		midground.AddChild (boat2);
		enemies.Add (boat1);
		enemies.Add (boat2);
		
	}

	public void InitLevel2()
	{
		clearMe ();
		
		Human human1 = new Human ("person", new Vector2(300, 820));
		background.AddChild (human1);
		humans.Add (human1);
		Human human2 = new Human ("person_boat", LevelActorPaths.GetLevel2_Humans()[0]);
		foreground.AddChild (human2);
		humans.Add (human2);

		List<List<PatrolStep>> allPaths = LevelActorPaths.GetLevel2_Enemies ();

		Enemy boat1 = new Enemy ("boat2", allPaths[0]);
		midground.AddChild (boat1);
		enemies.Add (boat1);
		Enemy boat2 = new Enemy ("boat2", allPaths[1]);
		background.AddChild (boat2);
		enemies.Add (boat2);
		Enemy boat3 = new Enemy ("sub2", allPaths[2]);
		midground.AddChild (boat3);
		enemies.Add (boat3);
	}

	public void InitLevel3()
	{
		clearMe ();

		List<List<PatrolStep>> allHumanPaths = LevelActorPaths.GetLevel3_Humans();

		Human human1 = new Human ("person", new Vector2(300, 820));
		background.AddChild (human1);
		humans.Add (human1);	
		Human human2 = new Human ("person_boat", allHumanPaths[0]);
		foreground.AddChild (human2);
		humans.Add (human2);
		Human human3 = new Human ("person_scuba", allHumanPaths[1]);
		foreground.AddChild (human3);
		humans.Add (human3);
		
		List<List<PatrolStep>> allEnemyPaths = LevelActorPaths.GetLevel3_Enemies ();
		
		Enemy boat1 = new Enemy ("boat1", allEnemyPaths[0]);
		midground.AddChild (boat1);
		enemies.Add (boat1);
		Enemy boat2 = new Enemy ("boat2", allEnemyPaths[1]);
		midground.AddChild (boat2);
		enemies.Add (boat2);
		Enemy boat3 = new Enemy ("sub1", allEnemyPaths[2]);
		midground.AddChild (boat3);
		enemies.Add (boat3);
		Enemy boat4 = new Enemy ("sub2", allEnemyPaths[3]);
		midground.AddChild (boat4);
		enemies.Add (boat4);
	}

	public void InitLevel4()
	{
		clearMe ();
		
		List<List<PatrolStep>> allHumanPaths = LevelActorPaths.GetLevel4_Humans();
		
		Human human1 = new Human ("person", new Vector2(300, 820));
		background.AddChild (human1);
		humans.Add (human1);	
		Human human2 = new Human ("person_boat", allHumanPaths[0]);
		foreground.AddChild (human2);
		humans.Add (human2);
		Human human3 = new Human ("person_scuba", allHumanPaths[1]);
		foreground.AddChild (human3);
		humans.Add (human3);
		Human human4 = new Human ("person_scuba", allHumanPaths[2]);
		foreground.AddChild (human4);
		humans.Add (human4);
		
		List<List<PatrolStep>> allEnemyPaths = LevelActorPaths.GetLevel4_Enemies ();
		
		Enemy boat1 = new Enemy ("boat1", allEnemyPaths[0]);
		midground.AddChild (boat1);
		enemies.Add (boat1);
		Enemy boat2 = new Enemy ("boat2", allEnemyPaths[1]);
		midground.AddChild (boat2);
		enemies.Add (boat2);
		Enemy boat3 = new Enemy ("sub1", allEnemyPaths[2]);
		midground.AddChild (boat3);
		enemies.Add (boat3);
		Enemy boat4 = new Enemy ("sub2", allEnemyPaths[3]);
		midground.AddChild (boat4);
		enemies.Add (boat4);
	}

	public void InitLevel5()
	{
		clearMe ();
		
		List<List<PatrolStep>> allHumanPaths = LevelActorPaths.GetLevel5_Humans();
		
		Human human1 = new Human ("person", new Vector2(250, 815));
		background.AddChild (human1);
		humans.Add (human1);	
		Human human2 = new Human ("person_boat", allHumanPaths[0]);
		foreground.AddChild (human2);
		humans.Add (human2);
		Human human3 = new Human ("person_scuba", allHumanPaths[1]);
		foreground.AddChild (human3);
		humans.Add (human3);
		Human human4 = new Human ("person_scuba", allHumanPaths[2]);
		foreground.AddChild (human4);
		humans.Add (human4);
		Human human5 = new Human ("person", new Vector2(400, 820));
		background.AddChild (human5);
		humans.Add (human5);

		List<List<PatrolStep>> allEnemyPaths = LevelActorPaths.GetLevel5_Enemies ();
		
		Enemy boat1 = new Enemy ("boat1", allEnemyPaths[0]);
		midground.AddChild (boat1);
		enemies.Add (boat1);
		Enemy boat2 = new Enemy ("boat2", allEnemyPaths[1]);
		midground.AddChild (boat2);
		enemies.Add (boat2);
		Enemy boat3 = new Enemy ("sub1", allEnemyPaths[2]);
		midground.AddChild (boat3);
		enemies.Add (boat3);
		Enemy boat4 = new Enemy ("sub2", allEnemyPaths[3]);
		midground.AddChild (boat4);
		enemies.Add (boat4);
		Enemy boat5 = new Enemy ("sub1", allEnemyPaths[4]);
		midground.AddChild (boat5);
		enemies.Add (boat5);
	}


	public void InitHumans()
	{
		Human human1 = new Human ("person", new Vector2(300, 820));
		background.AddChild (human1);

		float human2Y = 790f;
		List<PatrolStep> steps1= new List<PatrolStep> ();
		steps1.Add (new PatrolStep (new Vector2 (-150, human2Y), new Vector2 (50, human2Y), 5f, -1));
		steps1.Add (new PatrolStep (new Vector2 (50, human2Y), new Vector2 (-150, human2Y), 5f, 1));

		Human human2 = new Human ("person_boat", steps1);
		foreground.AddChild (human2);

		float human3Y = 500f;
		List<PatrolStep> steps2= new List<PatrolStep> ();
		steps2.Add (new PatrolStep (new Vector2 (150, human3Y+10), new Vector2 (-100, human3Y-10), 15f, 1));
		steps2.Add (new PatrolStep (new Vector2 (-100, human3Y-10), new Vector2 (150, human3Y+10), 15f, -1));

		Human human3 = new Human ("person_scuba", steps2);
		background.AddChild (human3);


		humans.Add (human1);
		humans.Add (human2);
		humans.Add (human3);
	}


	public void InitEnemies()
	{
		float boat1Y = 800f;
		List<PatrolStep> steps1= new List<PatrolStep> ();
		steps1.Add (new PatrolStep (new Vector2 (-400, boat1Y), new Vector2 (400, boat1Y), 30f, -1));
		steps1.Add (new PatrolStep (new Vector2 (400, boat1Y), new Vector2 (-400, boat1Y), 30f, 1));
		
		Enemy boat1 = new Enemy ("boat1", steps1);
		midground.AddChild (boat1);
		
		float boat2Y = 785f;
		List<PatrolStep> steps2 = new List<PatrolStep> ();
		steps2.Add (new PatrolStep (new Vector2 (200, boat2Y), new Vector2 (-400, boat2Y), 40f, 1));
		steps2.Add (new PatrolStep (new Vector2 (-400, boat2Y), new Vector2 (200, boat2Y), 40f, -1));
		
		Enemy boat2 = new Enemy ("boat2", steps2);
		midground.AddChild (boat2);
		
		float sub1Y = 405f;
		List<PatrolStep> steps3 = new List<PatrolStep> ();
		steps3.Add (new PatrolStep (new Vector2 (450, sub1Y+50), new Vector2 (-50, sub1Y-100), 20f, 1));
		steps3.Add (new PatrolStep (new Vector2 (-50, sub1Y-100), new Vector2 (450, sub1Y+50), 20f, -1));
		
		Enemy sub1 = new Enemy ("sub1", steps3);
		midground.AddChild (sub1);
		
		float sub2Y = 400f;
		List<PatrolStep> steps4= new List<PatrolStep> ();
		steps4.Add (new PatrolStep (new Vector2 (-440, sub2Y-50), new Vector2 (100, sub2Y+100), 50f, -1));
		steps4.Add (new PatrolStep (new Vector2 (100, sub2Y+100), new Vector2 (-440, sub2Y-50), 50f, 1));
		
		Enemy sub2 = new Enemy ("sub2", steps4);
		midground.AddChild (sub2);
		
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

		foreach(Human man in humans)
		{
			man.update();
		}
		
		if(isDragging)
		{
			UpdateDrag();
		}else{
			UpdateRetract();
			
			if(hasAHuman)
			{
				if(tentaclePieces.Count > 0)
				{
					FSprite tip = tentaclePieces.GetLastObject();
					fishFood.x = tip.x;
					fishFood.y = tip.y;
				}else{
					hasAHuman = false;
					fishFood = null;
					if(humans.Count == 0)
					{
						if(onVictory != null)
						{
							onVictory();
						}
					}
				}
			}
		}
		
		if(TestForCollisions())
		{
			isDragging = false;
			if(onGameOver != null)
			{
				onGameOver();
			}
		}
		
		if(TestForHumans())
		{
			isDragging = false;
		}
	}
	
	public bool TestForHumans()
	{
		//no need to cache rects, we're comparing many:1 not many:many
		foreach(Human human in humans)
		{
			Rect test = new Rect(human.x - human.body.width/2, human.y - human.body.height/2, human.body.width, human.body.height);
			
			if(tentaclePieces.Count > 0)
			{
				//just the tip of the tentacle
				FSprite tentacle = tentaclePieces.GetLastObject();
				if(TestCircleRect(tentacle.x, tentacle.y, tentacle.width/2, test))
				{
					tentacle.color = RXUtils.GetColorFromHex("ff0000");
					hasAHuman = true;
					fishFood = human;
					humans.Remove(human);
					return true;
				}else{
					tentacle.color = RXUtils.GetColorFromHex("ffffff");
				}
			}
		}

		return false;
	}
	
	public bool TestForCollisions()
	{
		Dictionary<Enemy, Rect> cached_rects = new Dictionary<Enemy, Rect>();
		foreach(Enemy enemy in enemies)
		{
			if(!cached_rects.ContainsKey(enemy))
			{
				//this code puts bright orange debug rects over the rect of the sonar bits
				
				if(!debugRects.ContainsKey(enemy))
				{
					debugRects[enemy] = new FSprite("debug_rect");
//					AddChild(debugRects[enemy]);
				}
				
				Vector2 sonar_pos = enemy.LocalToOther(enemy.sonar.GetPosition(), this);
				cached_rects[enemy] = new Rect(sonar_pos.x - enemy.sonar.width/2, 
				                               sonar_pos.y - enemy.sonar.height/2, 
				                               enemy.sonar.width, 
				                               enemy.sonar.height);
				                               								
//				debugRects[enemy].x = cached_rects[enemy].center.x;
//				debugRects[enemy].y = cached_rects[enemy].center.y;
//				debugRects[enemy].width = cached_rects[enemy].width;
//				debugRects[enemy].height = cached_rects[enemy].height;
		
				//use this to see if our enemy verts are in reasonable spots		
//				Vector2 vertex = enemy.LocalToOther(enemy.sonar_vert_3, this);
//				debugRects[enemy].x = vertex.x;
//				debugRects[enemy].y = vertex.y;
//				debugRects[enemy].width = 25;
//				debugRects[enemy].height = 25;
			}
		}
		
		
		foreach(FSprite tentacle in tentaclePieces)
		{
			foreach(Enemy enemy in enemies)
			{
				//first see if we're even in the rect...
				if(TestCircleRect(tentacle.x, tentacle.y, tentacle.width/2, cached_rects[enemy]))
				{		
					return true;				
					Vector2 vertex_a = enemy.LocalToOther(enemy.sonar_vert_1, this);
					Vector2 vertex_b = enemy.LocalToOther(enemy.sonar_vert_2, this);
					Vector2 vertex_c = enemy.LocalToOther(enemy.sonar_vert_2, this);
					
					if(TestPointInTriangle(new Vector2(tentacle.x, tentacle.y), vertex_a, vertex_b, vertex_c))
					{
						tentacle.color = RXUtils.GetColorFromHex("ff0000");
						
					}else{
						Debug.Log("HIT THE RECT BUT NOT THE TRI");
					}
				}else{
					tentacle.color = RXUtils.GetColorFromHex("ffffff");
				}
				
			}
		}
		return false;
	}
	
	
	private bool TestPointInTriangle(Vector2 s_float, Vector2 a_float, Vector2 b_float, Vector2 c_float)
	{
		Vector2 s = new Vector2(Mathf.Round(s_float.x), Mathf.Round(s_float.y));
		Vector2 a = new Vector2(Mathf.Round(a_float.x), Mathf.Round(a_float.y));
		Vector2 b = new Vector2(Mathf.Round(b_float.x), Mathf.Round(b_float.y));
		Vector2 c = new Vector2(Mathf.Round(c_float.x), Mathf.Round(c_float.y));
		
		float as_x = s.x-a.x;
		float as_y = s.y-a.y;
		
		bool s_ab = (b.x-a.x)*as_y-(b.y-a.y)*as_x > 0;
		
		if((c.x-a.x)*as_y-(c.y-a.y)*as_x > 0 == s_ab) return false;
		
		if((c.x-b.x)*(s.y-b.y)-(c.y-b.y)*(s.x-b.x) > 0 != s_ab) return false;
		
		return true;
	}
	
	
	//adapted from http://processing.org/discourse/beta/num_1259957186.html
	private bool TestLineCircle(Vector2 point1, Vector2 point2, float circle_x, float circle_y, float circle_r)
	{
		float dx = point2.x - point1.x;
		float dy = point2.y - point1.y;
		
		float dx_dx = dx*dx;
		float dy_dy = dy*dy;
		
		float long_squared = dx_dx + dy_dy;
		float r_squared = circle_r*circle_r;
		
		float cdx1 = circle_x - point1.x;
		float cdx2 = circle_x - point2.x;
		
		float cdy1 = circle_y - point1.y;
		float cdy2 = circle_y - point2.y;
		
		float dot1 = dx*cdy1 - dy*cdx1;
		float root = circle_r*circle_r*long_squared - dot1*dot1;
		
		if(root >= 0)
		{
			float dot2 = dx*cdx1 + dy*cdy1;
			float t = dot2 / long_squared;
			
			return ((t >= 0 && t <= 1) || (cdx1*cdx1 + cdy1*cdy1 < r_squared) || (cdx2*cdx2 + cdy2*cdy2 < r_squared) );
		}
		return false;
	}
	
	private bool TestCircleRect(float circle_x, float circle_y, float circle_r, Rect rect)
	{
		//http://stackoverflow.com/questions/401847/circle-rectangle-collision-detection-intersection
		
		// Find the closest point to the circle within the rectangle
		float closest_x = Mathf.Clamp(circle_x, rect.xMin, rect.xMax);
		float closest_y = Mathf.Clamp (circle_y, rect.yMin, rect.yMax);
		
		// Calculate the distance between the circle's center and this closest point
		float dx = circle_x - closest_x;
		float dy = circle_y - closest_y;
		
		// If the distance is less than the circle's radius, an intersection occurs
		float d_squared = dx*dx + dy*dy;
		
		return (d_squared < circle_r*circle_r);
	}
	
	
	public void UpdateRetract()
	{
		if(tentaclePieces.Count == 0)
		{
			return;
		}
		if(Time.time - lastUpdate < TENTACLE_GROWTH_SPEED)
		{
			return;
		}
		lastUpdate = Time.time;
		
		FSprite last = tentaclePieces[tentaclePieces.Count - 1];
		tentaclePieces.Remove (last);
		last.RemoveFromContainer();
		
		UpdateTentacle();
	}
	
	public void UpdateDrag()
	{
		if(Time.time - lastUpdate < TENTACLE_GROWTH_SPEED)
		{
			return;
		}
		lastUpdate = Time.time;
		
		float tip_x, tip_y;
		
		FAtlasElement tentacle = Futile.atlasManager.GetElementWithName("tentacle");
		float bone_length = tentacle.sourceSize.y * TENTACLE_BONE_LENGTH;
		
		if(tentaclePieces.Count == 0)
		{
			tip_x = lastX;
			tip_y = depthY;
		}else{
			float tx = tentaclePieces.GetLastObject().x;
			float ty = tentaclePieces.GetLastObject().y;
			
			float rotation = tentaclePieces.GetLastObject().rotation * RXMath.DTOR;
			
			tip_x = tx + Mathf.Cos(rotation)*bone_length;
			tip_y = ty + -1*Mathf.Sin(rotation)*bone_length;
		}
		
		//might also want to add a time component so we don't insta-grow
		Vector2 delta = new Vector2(lastX - tip_x, lastY - tip_y);
		if(delta.magnitude > bone_length)
		{
			if(tentaclePieces.Count < MAX_TENTACLE_PIECES)
			{
				//add a new bone and point it at lastX/lastY
				FSprite bone = new FSprite("tentacle");
				AddChild(bone);
				
				tentaclePieces.Add(bone);
				bone.x = tip_x;
				bone.y = tip_y;
				bone.anchorX = 0.0f;
				
				UpdateTentacle();
				
				//y-positive, invert the y
				float angle = Mathf.Atan2(delta.y*-1, delta.x)*RXMath.RTOD;
				
				if(tentaclePieces.Count > 1)
				{
					float angle_diff = angle - tentaclePieces[tentaclePieces.Count - 2].rotation;
					while(angle_diff > 180f) angle_diff -= 360f;
					while(angle_diff < -180f) angle_diff += 360f;
					
					if(Mathf.Abs (angle_diff)  > TENTACLE_MAX_TURN_ANGLE)
					{
						float dampening = (1 - Mathf.Abs(TENTACLE_MAX_TURN_ANGLE / angle_diff)) * angle_diff;
						angle -= dampening;
					}
				}
				
				bone.rotation = angle;
			}
		}
	}
	
	public void UpdateTentacle()
	{
		float growth = tentaclePieces.Count;
		foreach(FSprite sprite in tentaclePieces)
		{
			sprite.scale = (1.0f + growth*TENTACLE_GROWTH_RATE);
			growth--;
		}
	}
	
	
	//SINGLE TOUCH DELEGATE
	
	public bool HandleSingleTouchBegan(FTouch touch)
	{
		isDragging = true;
		lastX = GetLocalTouchPosition(touch).x;
		lastY = GetLocalTouchPosition(touch).y;
		
		return true;
	}
	
	public void HandleSingleTouchMoved(FTouch touch)
	{
		lastX = GetLocalTouchPosition(touch).x;
		lastY = GetLocalTouchPosition(touch).y;
	}
	
	public void HandleSingleTouchEnded(FTouch touch)
	{
		isDragging = false;
	}
	
	public void HandleSingleTouchCanceled(FTouch touch)
	{
		isDragging = false;
	}

}

