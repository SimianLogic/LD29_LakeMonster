using UnityEngine;
using System.Collections;

public class TemporalSprite : FContainer
{
	int lifespan, fullLife;
	bool beam;
	FContainer parent;
	FSprite sprite;
	Vector2 velocity;

	private static FContainer targetScreen = null;

	public static void SetTargetScreen(FContainer targetScreen)
	{
		TemporalSprite.targetScreen = targetScreen;
	}

	public static void Create(string sprite, int lifespan, FContainer parent)
	{
		Create(sprite, lifespan, parent, 0f, 0f);
	}
	
	public static void Create(string sprite, int lifespan, FContainer parent, float xOff, float yOff,
	                          float scaleX = 1f, float scaleY = 1f, bool beam = false, float xVelocity = 0f, float yVelocity = 0f)
	{
		TemporalSprite temp = new TemporalSprite(sprite, lifespan, parent, xOff, yOff, scaleX, scaleY, beam, new Vector2(xVelocity, yVelocity));
		ScreenManager.StartCoroutine(temp.LiveOutAndDie());
	}

	private TemporalSprite(string sprite, int lifespan, FContainer parent, float xOff, float yOff,
	                       float scaleX, float scaleY, bool beam, Vector2 velocity) : base()
	{
		this.sprite = new FSprite(sprite);
		this.AddChild(this.sprite);
		
		this.lifespan = lifespan;
		this.fullLife = lifespan;
		this.parent = parent;
		this.beam = beam;
		this.x += xOff;
		this.y += yOff;
		this.scaleX = scaleX;
		this.scaleY = scaleY;
		this.velocity = new Vector2();
	}

	public static void Create(FNode content, int lifespan, FContainer parent)
	{
		Create(content, lifespan, parent, 0f, 0f);
	}

	public static void Create(FNode content, int lifespan, FContainer parent, float xOff, float yOff,
	                          float scaleX = 1f, float scaleY = 1f, bool beam = false, float xVelocity = 0f, float yVelocity = 0f)
	{
		TemporalSprite temp = new TemporalSprite(content, lifespan, parent, xOff, yOff, scaleX, scaleY, beam, new Vector2(xVelocity, yVelocity));
		ScreenManager.StartCoroutine(temp.LiveOutAndDie());
	}

	private TemporalSprite(FNode content, int lifespan, FContainer parent, float xOff, float yOff,
	                       float scaleX, float scaleY, bool beam, Vector2 velocity) : base()
	{
		this.AddChild(content);
		
		this.lifespan = lifespan;
		this.fullLife = lifespan;
		this.parent = parent;
		this.beam = beam;
		this.x += xOff;
		this.y += yOff;
		this.scaleX = scaleX;
		this.scaleY = scaleY;
		this.velocity = velocity;
	}

	private IEnumerator LiveOutAndDie()
	{
		if(parent != null)
			parent.AddChild(this);
		else if(targetScreen != null)
			targetScreen.AddChild(this);

		while(lifespan-- > 0)
		{
			if(beam)
			{
				this.alpha = (float)lifespan / (float)fullLife;
				this.scaleX *= 1.01f;
			}
			this.x += velocity.x;
			this.y += velocity.y;
			yield return null;
		}

		if(parent != null)
			parent.RemoveChild(this);
		else
			targetScreen.RemoveChild(this);
	}
}