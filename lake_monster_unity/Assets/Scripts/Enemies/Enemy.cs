using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Enemy : FSprite
{

	private List<PatrolStep> steps;
	private int stepIndex;

	public Enemy(string name):base(name)
	{
		float boatY = 800f;
		Debug.Log ("Enemy constructed " + name);
		steps = new List<PatrolStep> ();
		steps.Add (new PatrolStep (new Vector2 (-400, boatY), new Vector2 (400, boatY), 50f, -1));
		steps.Add (new PatrolStep (new Vector2 (400, boatY), new Vector2 (-400, boatY), 20f, 1));
		stepIndex = -1;

		nextStep ();

	}

	public virtual void update()
	{
		float dt = Time.deltaTime;
		PatrolStep step = steps [stepIndex];
		float vx = step.velocityVector.x * dt;
		float vy = step.velocityVector.y * dt;

		if (step.startPos.x < step.endPos.x) 
		{
			x = Mathf.Min (x + vx, step.endPos.x);
		} else 
		{
			x = Mathf.Max (x + vx, step.endPos.x);
		}

		if (step.startPos.y < step.endPos.y)
		{
			y = Mathf.Min (y + vy, step.endPos.y);
		} else
		{
			y = Mathf.Max (y + vy, step.endPos.y);
		}


		if (Mathf.Abs(x-step.endPos.x) <=1 && Mathf.Abs(y-step.endPos.y) <=1){
			nextStep ();
		}
		
	}

	public void nextStep()
	{
		stepIndex = (stepIndex + 1) % steps.Count;
		x = steps [stepIndex].startPos.x;
		y = steps [stepIndex].startPos.y;

		scaleX = steps [stepIndex].facingDirection;
	}

}

class PatrolStep
{
	public Vector2 startPos;
	public Vector2 endPos;
	public float velocity;
	public int facingDirection;
	public Vector2 velocityVector;

	public PatrolStep(Vector2 start_pos, Vector2 end_pos, float velocity, int facing )
	{
		startPos = start_pos;
		endPos = end_pos;
		this.velocity = velocity;
		facingDirection = facing;
		velocityVector = Vector2.ClampMagnitude( new Vector2(endPos.x - startPos.x, endPos.y - startPos.y), this.velocity);
	}

}


