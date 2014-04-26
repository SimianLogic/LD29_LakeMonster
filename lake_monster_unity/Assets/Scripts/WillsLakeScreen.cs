using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WillsLakeScreen : GameScreen, FSingleTouchableInterface
{
	public const float TENTACLE_GROWTH_SPEED = 0.25f;
	public const float TENTACLE_GROWTH_RATE = 0.02f;

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
		}
	}
	
	public void UpdateDrag()
	{
		if(Time.time - lastUpdate < TENTACLE_GROWTH_SPEED)
		{
			return;
		}
		float tip_x, tip_y;
		float bone_length = tentacle.sourceSize.y;
		
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
			//add a new bone and point it at lastX/lastY
			FSprite bone = new FSprite("tentacle");
			AddChild(bone);
			
			tentaclePieces.Add(bone);
			
			bone.x = tip_x;
			bone.y = tip_y;
			bone.anchorX = 0.0f;
			
			float growth = tentaclePieces.Count;
			foreach(FSprite sprite in tentaclePieces)
			{
				sprite.scale = (1.0f + growth*TENTACLE_GROWTH_RATE);
				growth--;
			}
			
			//y-positive, invert the y
			float angle = Mathf.Atan2(delta.y*-1, delta.x)*RXMath.RTOD;
			
			bone.rotation = angle;
		}
	}
	
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

