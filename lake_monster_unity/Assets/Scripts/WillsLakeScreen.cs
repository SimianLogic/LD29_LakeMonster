using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WillsLakeScreen : GameScreen, FSingleTouchableInterface
{
	public const float TENTACLE_GROWTH_SPEED = 0.05f;
	public const float TENTACLE_GROWTH_RATE = 0.03f;
	public const float TENTACLE_MAX_TURN_ANGLE = 45f;

	public List<FSprite> tentaclePieces;

		public bool isDragging;
	public float lastX;
	public float lastY;
	public float lastUpdate;
	public float depthY;
	public FAtlasElement tentacle;
	
	public WillsLakeScreen() : base("monster_pieces")
	{
		EnableSingleTouch();
		tentaclePieces = new List<FSprite>();
		tentacle = images["tentacle"].element;
		depthY = rootHeight/2 - Futile.screen.height - 50;
	}
	
	public void Update()
	{
		if(isDragging)
		{
			UpdateDrag();
		}else{
			UpdateRetract();
		}
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
		float bone_length = tentacle.sourceSize.y * 0.8f;
		
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
			if(tentaclePieces.Count < 50)
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

