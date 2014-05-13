
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PatrolStep : Step
{
	public Vector2 startPos;
	public Vector2 endPos;
	public float velocity;
	public int facingDirection;
	public Vector2 velocityVector;
	
	public PatrolStep(Vector2 start_pos, Vector2 end_pos, float velocity, int facing ) : base()
	{
		startPos = start_pos;
		endPos = end_pos;
		this.velocity = velocity;
		facingDirection = facing;
		velocityVector = Vector2.ClampMagnitude( new Vector2(endPos.x - startPos.x, endPos.y - startPos.y), this.velocity);
	}
	
}