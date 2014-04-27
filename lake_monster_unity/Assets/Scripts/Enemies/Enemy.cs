using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Enemy : FContainer
{

	private List<PatrolStep> steps;
	private int stepIndex;
	public FSprite body;
	public FSprite sonar;

	public Enemy(string name, List<PatrolStep> steps):base()
	{
		this.steps = steps;
		body = new FSprite (name);
		sonar = new FSprite (name + "_sonar");
		AddChild (body);
		AddChild (sonar);

		switch(name)
		{
			case "boat1":
				sonar.y = -235f;
				sonar.x = -20f;
				break;
			case "boat2":
				sonar.y = -130f;
				sonar.x = -10f;
				break;
			case "sub1":
				sonar.y = 5f;
				sonar.x = -250f;
				break;
			case "sub2":
				sonar.y = 5f;
				sonar.x = -150f;
				break;
			default:
				break;
		}

		Debug.Log ("Enemy constructed " + name);

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

		if( (scaleX < 0 && steps [stepIndex].facingDirection > 0) || (scaleX > 0 && steps [stepIndex].facingDirection < 0))
		{
			scaleX = scaleX * -1;
		}
	}

}




