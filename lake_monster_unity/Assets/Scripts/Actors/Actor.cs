using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Actor : FContainer
{
	public List<Step> steps;
	public int stepIndex;
	public FSprite body;

	public Actor(string name, List<Step> steps):base()
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
		Step step = steps [stepIndex];
		
		if(step is TurnStep)
		{	
			handleTurnStep(step as TurnStep, dt);
		}else if(step is PatrolStep){
			handlePatrolStep(step as PatrolStep, dt);
		}
	}
	
	//relative to current stepIndex
	private Step GetStepWithIndex(int index)
	{
		int which = stepIndex + index;
		while(which < 0) which += steps.Count;
		while(which >= steps.Count) which -= steps.Count;
		
		return steps[which];
	}
	
	
	public virtual float prevScale
	{
		get
		{
			if(GetStepWithIndex(-1) is PatrolStep)
			{
				return (float)(GetStepWithIndex(-1) as PatrolStep).facingDirection;
			}else{
				return 0f;
			}
		}
	}
	
	public virtual float nextScale
	{
		get
		{
			if(GetStepWithIndex(1) is PatrolStep)
			{
				return (float)(GetStepWithIndex(1) as PatrolStep).facingDirection;
			}else{
				return 0f;
			}
		}
	}
	
	public virtual void handleTurnStep(TurnStep step, float dt)
	{
	
		if(prevScale > nextScale)
		{
			this.scaleX = Mathf.Max(nextScale, this.scaleX - dt/step.durationInSeconds);
		}else{
		 	this.scaleX = Mathf.Min(nextScale, this.scaleX + dt/step.durationInSeconds);
		}
		
		if(this.scaleX == nextScale)
		{
			nextStep();
		}
		
	}
	
	public virtual void handlePatrolStep(PatrolStep step, float dt)
	{
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
		
		
		if (Mathf.Abs(x-step.endPos.x) <=1 && Mathf.Abs(y-step.endPos.y) <=1)
		{
			nextStep ();
		}
	}

	public void nextStep()
	{
		if(steps == null) return;

		stepIndex = (stepIndex + 1) % steps.Count;
		
		Step step = steps[stepIndex];
		if(step is PatrolStep)
		{
			PatrolStep patrol = step as PatrolStep;
			x = patrol.startPos.x;
			y = patrol.startPos.y;
			
			if( (scaleX < 0 && patrol.facingDirection > 0) || (scaleX > 0 && patrol.facingDirection < 0))
			{
				scaleX = scaleX * -1;
			}
		}
		
		
	}

}