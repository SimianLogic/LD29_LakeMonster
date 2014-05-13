using UnityEngine;
using System.Collections;

public class TurnStep : Step
{
	public float durationInSeconds;
	public TurnStep(float duration_in_seconds ) : base()
	{
		durationInSeconds = duration_in_seconds;
	}
}

