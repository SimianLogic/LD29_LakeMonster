using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Actor : FContainer
{
	public List<PatrolStep> steps;
	public int stepIndex;
	public FSprite body;

	public Actor(string name, List<PatrolStep> steps):base()
	{
		this.steps = steps;
		body = new FSprite (name);
		AddChild (body);
		stepIndex = -1;
		nextStep ();

	}

	public virtual void update()
	{
		if(steps == null) return;

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
		if(steps == null) return;

		stepIndex = (stepIndex + 1) % steps.Count;
		x = steps [stepIndex].startPos.x;
		y = steps [stepIndex].startPos.y;
		
		if( (scaleX < 0 && steps [stepIndex].facingDirection > 0) || (scaleX > 0 && steps [stepIndex].facingDirection < 0))
		{
			scaleX = scaleX * -1;
		}
	}

}