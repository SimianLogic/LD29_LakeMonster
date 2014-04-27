using UnityEngine;
using System.Collections;

public enum ScreenSourceDirection
{
	Top,
	Bottom,
	Left,
	Right,
	TopCover,
	BottomCover,
	LeftCover,
	RightCover,
	TopUncover,
	BottomUncover,
	LeftUncover,
	RightUncover,
	Instant  //no animation
}

public enum AnimationType
{
	Linear,
	Interpolated
}

/*
 *	This is a super basic starter implementation, but eventually we'll want multiple layers to help keep things clean...
 *		-> Screen/UI Layer
 *		-> Popups Layer
 *		-> FX Layer
 */
public class ScreenManager
{
	private static GameScreen lastScreen; //to clean up once it's off screen
	private static GameScreen currentScreen;
	
	public static FContainer screenLayer; //single object only!
	public static FContainer popupLayer; //stack of popups which can be on top of screens
	public static FContainer fxLayer; //holds flashies and fun stuff that needs to be very very top

	public static MonoBehaviour coroutineHandler;

	public static FSprite backdrop;

	public static AnimationType defaultAnimationType = AnimationType.Interpolated;
	private const float PUSH_SPEED_X = 12f, PUSH_SPEED_Y = 18f;
	
	private const float INTERPOLATION = 4.0f; //move distance/4 on each frame
	private const float CLOSE_ENOUGH = 1.0f;  //how close til we're done?
	
	
	/*
	 * This is required to prevent errors when the buttons that trigger the animation coroutines
	 * are double-tapped.  There are a few other ways to deal with this, I think, but this is the 
	 * most simplistic and is effective.
	 */
	private static bool isAnimating = false;

	public static void init(MonoBehaviour handler, string backdropSprite = "")
	{
		coroutineHandler = handler;

		backdrop = null;
		if(backdropSprite != "")
		{
			backdrop = new FSprite(backdropSprite);
			backdrop.scaleX = Futile.screen.width / backdrop.width;
			backdrop.scaleY = Futile.screen.height / backdrop.height;
			// NOTE: The following two lines recolor the backdrop sprite
			backdrop.shader = FShader.SolidColored;
			backdrop.color = RXUtils.GetColorFromHex("555555");
		}

		screenLayer = new FContainer();
		
		Futile.stage.AddChild(screenLayer);		
	}
	
	//adapted a bit from the Banana demo in Futile
	public static void loadScreen(GameScreen screen, ScreenSourceDirection direction=ScreenSourceDirection.Instant)
	{
		if(isAnimating)
			return;

		lastScreen = currentScreen;
		currentScreen = screen;
		
		if(currentScreen == null)
		{
			//use an empty one if we weren't given one!
			currentScreen = new GameScreen();
		}
		
		if(direction == ScreenSourceDirection.Instant) 
		{
			if(lastScreen != null)
			{
				lastScreen.RemoveFromContainer();
				lastScreen.willHide();
				lastScreen.didHide();
			}
			
			currentScreen.willShow();
			Futile.stage.AddChild(currentScreen);
			currentScreen.didShow();
			
			return;
		}
		else
		{
			switch(direction)
			{
			case ScreenSourceDirection.Top:
				currentScreen.x = 0f;
				currentScreen.y = Futile.screen.height;
	 			coroutineHandler.StartCoroutine(AnimateScreens());
				break;
			case ScreenSourceDirection.Bottom:
				currentScreen.x = 0f;
				currentScreen.y = -1f * Futile.screen.height;
				coroutineHandler.StartCoroutine(AnimateScreens());
				break;
			case ScreenSourceDirection.Left:
				currentScreen.x = -1f * Futile.screen.width;
				currentScreen.y = 0f;
				coroutineHandler.StartCoroutine(AnimateScreens());
				break;
			case ScreenSourceDirection.Right:
				currentScreen.x = Futile.screen.width;
				currentScreen.y = 0f;
				coroutineHandler.StartCoroutine(AnimateScreens());
				break;
			case ScreenSourceDirection.TopCover:
				currentScreen.x = 0f;
				currentScreen.y = Futile.screen.height;
				coroutineHandler.StartCoroutine(AnimateScreens(true));
				break;
			case ScreenSourceDirection.BottomCover:
				currentScreen.x = 0f;
				currentScreen.y = -1f * Futile.screen.height;
				coroutineHandler.StartCoroutine(AnimateScreens(true));
				break;
			case ScreenSourceDirection.LeftCover:
				currentScreen.x = -1f * Futile.screen.width;
				currentScreen.y = 0f;
				coroutineHandler.StartCoroutine(AnimateScreens(true));
				break;
			case ScreenSourceDirection.RightCover:
				currentScreen.x = Futile.screen.width;
				currentScreen.y = 0f;
				coroutineHandler.StartCoroutine(AnimateScreens(true));
				break;
			case ScreenSourceDirection.TopUncover:
				coroutineHandler.StartCoroutine(UncoverScreen(0f, Futile.screen.height));
				break;
			case ScreenSourceDirection.BottomUncover:
				coroutineHandler.StartCoroutine(UncoverScreen(0f, -1f * Futile.screen.height));
				break;
			case ScreenSourceDirection.LeftUncover:
				coroutineHandler.StartCoroutine(UncoverScreen(-1f * Futile.screen.height, 0f));
				break;
			case ScreenSourceDirection.RightUncover:
				coroutineHandler.StartCoroutine(UncoverScreen(Futile.screen.height, 0f));
				break;
			default:
				throw new System.Exception("Unrecognized transition type; newly-created transitions may not have been implemented.");
			}

			return;
		}
	}

	public static void StartCoroutine(IEnumerator coroutine)
	{
		if(coroutineHandler != null)
		{
			coroutineHandler.StartCoroutine(coroutine);
		}
		else
		{
			throw new System.Exception("ScreenManager has no coroutine handler!  Did the initial MonoBehaviour get destroyed?");
		}
	}

	private static IEnumerator AnimateScreens(bool cover = false)
	{
		if(isAnimating)
			yield return null;
		isAnimating = true;

		if(lastScreen != null)
			lastScreen.willHide();
		currentScreen.willShow();

		if(cover && backdrop != null)
		{
			currentScreen.AddChild(backdrop);
			backdrop.MoveToBack();
		}
		
		Futile.stage.AddChild(currentScreen);
		
		//ideally would put these in sub-functions, but not sure how that plays with the C# yields
		//would also prefer to rewrite this to not assume(0,0) as the target, but zoom to any target
		if(defaultAnimationType == AnimationType.Linear)
		{
			while(currentScreen.x != 0f || currentScreen.y != 0f)
			{
				if(currentScreen.x > 0f)
				{
					if(lastScreen != null && !cover)
						lastScreen.x -= Mathf.Min(currentScreen.x, PUSH_SPEED_X);
					currentScreen.x -= Mathf.Min(currentScreen.x, PUSH_SPEED_X);
				}
				else if(currentScreen.x < 0f)
				{
					if(lastScreen != null && !cover)
						lastScreen.x -= Mathf.Max(currentScreen.x, -1f * PUSH_SPEED_X);
					currentScreen.x -= Mathf.Max(currentScreen.x, -1f * PUSH_SPEED_X);
				}
				
				if(currentScreen.y > 0f)
				{
					if(lastScreen != null && !cover)
						lastScreen.y -= Mathf.Min(currentScreen.y, PUSH_SPEED_Y);
					currentScreen.y -= Mathf.Min(currentScreen.y, PUSH_SPEED_Y);
				}
				else if(currentScreen.y < 0f)
				{
					if(lastScreen != null && !cover)
						lastScreen.y -= Mathf.Max(currentScreen.y, -1f * PUSH_SPEED_Y);
					currentScreen.y -= Mathf.Max(currentScreen.y, -1f * PUSH_SPEED_Y);
				}
				
				yield return new WaitForEndOfFrame();
			}		
		}
		else if(defaultAnimationType == AnimationType.Interpolated)
		{
			while(currentScreen.x != 0.0f || currentScreen.y != 0.0f)
			{
				if(Mathf.Abs(currentScreen.x) > 0.0f)
				{
					float dx = (0.0f - currentScreen.x) / INTERPOLATION;
					// Debug.Log ("DX = " + dx);
					
					currentScreen.x += dx;
					if(lastScreen != null && !cover) lastScreen.x += dx; 
					
					if(Mathf.Abs(currentScreen.x) < CLOSE_ENOUGH)
					{
						currentScreen.x = 0.0f;
					}
				}
				
				if(Mathf.Abs(currentScreen.y) > 0.0f)
				{
					float dy = (0.0f - currentScreen.y) / INTERPOLATION;
					currentScreen.y += dy;
					if(lastScreen != null && !cover) lastScreen.y += dy; 
					
					if(Mathf.Abs (currentScreen.y) < CLOSE_ENOUGH)
					{
						currentScreen.y = 0.0f;
					}
				}
								
				yield return new WaitForEndOfFrame();
			}
		}
		else
		{
			throw new FutileException("Invalid animation type set in ScreenManager!");
		}

		if(lastScreen != null)
		{
			lastScreen.RemoveFromContainer();
			lastScreen.didHide();
		}
		if(backdrop != null)
			currentScreen.RemoveChild(backdrop);
		currentScreen.didShow();
		
		isAnimating = false;
	}

	private static IEnumerator UncoverScreen(float destX, float destY)
	{
		if(lastScreen == null || isAnimating)
			yield return null;
		isAnimating = true;

		lastScreen.willHide();
		currentScreen.willShow();

		if(backdrop != null)
		{
			lastScreen.AddChild(backdrop);
			backdrop.MoveToBack();
		}
		
		Futile.stage.AddChild(currentScreen);
		lastScreen.MoveToFront();

		switch(defaultAnimationType)
		{
		case AnimationType.Linear:
			while(lastScreen.x != destX || lastScreen.y != destY)
			{
				if(lastScreen.x > destX)
				{
					lastScreen.x -= Mathf.Min(lastScreen.x - destX, PUSH_SPEED_X);
				}
				else if(currentScreen.x < destX)
				{
					lastScreen.x -= Mathf.Max(lastScreen.x - destX, -1f * PUSH_SPEED_X);
				}
				
				if(currentScreen.y > destY)
				{
					lastScreen.y -= Mathf.Min(lastScreen.y - destY, PUSH_SPEED_Y);
				}
				else if(currentScreen.y < destY)
				{
					lastScreen.y -= Mathf.Max(lastScreen.y - destY, -1f * PUSH_SPEED_Y);
				}

				yield return new WaitForEndOfFrame();
			}
			break;
		case AnimationType.Interpolated:
			while(lastScreen.x != destX || lastScreen.y != destY)
			{
				if(lastScreen.x == 0 && destX != 0)
				{
					lastScreen.x += (destX < 0 ? -1f : 1f) * PUSH_SPEED_X;
				}
				else if(Mathf.Abs(lastScreen.x) < Mathf.Abs(destX))
				{
					lastScreen.x += (destX - lastScreen.x) / INTERPOLATION;
				}
				
				if(lastScreen.y == 0 && destY != 0)
				{
					lastScreen.y += (destY < 0 ? -1f : 1f) * PUSH_SPEED_Y;
				}
				else if(Mathf.Abs(lastScreen.y) < Mathf.Abs(destY))
				{
					lastScreen.y += (destY - lastScreen.y) / INTERPOLATION;
				}

				if(Mathf.Abs(lastScreen.x - destX) < CLOSE_ENOUGH) lastScreen.x = destX;
				if(Mathf.Abs(lastScreen.y - destY) < CLOSE_ENOUGH) lastScreen.y = destY;
				
				yield return new WaitForEndOfFrame();
			}
			break;
		default:
			throw new FutileException("Invalid animation type set in ScreenManager!");
		}

		lastScreen.RemoveFromContainer();
		lastScreen.didHide();

		currentScreen.didShow();

		isAnimating = false;
	}
}