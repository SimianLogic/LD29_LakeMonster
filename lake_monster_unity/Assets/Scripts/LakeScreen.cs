using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LakeScreen : GameScreen, FSingleTouchableInterface
{
	public const float TENTACLE_GROWTH_SPEED = 0.02f;
	public const float TENTACLE_GROWTH_RATE = 0.02f;
	public const float TENTACLE_MAX_TURN_ANGLE = 45f;
	public const float TENTACLE_BONE_LENGTH = 0.45f;
	public const int MAX_TENTACLE_PIECES = 75;
	
	public List<Enemy> enemies;
	public List<Human> humans;
	
	public Dictionary<Enemy,FSprite> debugRects;
	
	public List<FSprite> tentaclePieces;
	
	public bool isDragging;
	public float lastX;
	public float lastY;
	public float lastUpdate;
	public float depthY;

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

		tentaclePieces = new List<FSprite>();
		
		depthY = rootHeight/2 - Futile.screen.height - 50;

		InitHumans ();
		InitEnemies();

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

		foreach(Human man in humans)
		{
			man.update();
		}
		
		if(isDragging)
		{
			UpdateDrag();
		}else{
			UpdateRetract();
		}
		
		if(TestForCollisions())
		{
			isDragging = false;
			Debug.Log ("GAME OVER");
		}
	}
	
	public bool TestForCollisions()
	{
	
		Dictionary<Enemy, Rect> cached_rects = new Dictionary<Enemy, Rect>();
		foreach(FSprite tentacle in tentaclePieces)
		{
			foreach(Enemy enemy in enemies)
			{
				if(!cached_rects.ContainsKey(enemy))
				{
					//first get the sonar's rect, which at least we know isn't rotated
					Rect sonar_rect = enemy.sonar.GetTextureRectRelativeToContainer();
					//now get the sonar_rect relative to the container
					Vector2 xy = enemy.LocalToOther(new Vector2(sonar_rect.x, sonar_rect.y), this);
					Vector2 wh = enemy.LocalToOther (new Vector2(sonar_rect.width, sonar_rect.height), this);
					
					cached_rects[enemy] = new Rect(xy.x, xy.y, wh.x, wh.y);
					
					if(!debugRects.ContainsKey(enemy))
					{
						debugRects[enemy] = new FSprite("debug_rect");
					}
				}
				
				if(TestCircleRect(tentacle.x, tentacle.y, tentacle.width/2, cached_rects[enemy]))
				{
					Debug.Log ("HIT HIT HIT HIT HIT");
					return true;
				}
				
			}
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

