#define _DEBUG_

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

public class LakeMonster : MonoBehaviour
{
//	private WillsLakeScreen lake;
	public LakeScreen lake;
	
	public const int STATE_GAMEPLAY = 1;
	public const int STATE_GAMEOVER = 2;
	
	public int state;

	void Start()
	{
		state = STATE_GAMEPLAY;
		
		FutileParams fparams = new FutileParams(true, true, false, false); //landscape left, right, portrait, portraitUpsideDown

		fparams.AddResolutionLevel(1024.0f, 1.0f, 1.0f, ""); //max width, displayScale, resourceScale, resourceSuffix
		
		fparams.backgroundColor = RXUtils.GetColorFromHex("555555");
		fparams.origin = new Vector2(0.5f, 0.5f);
		
		Futile.instance.Init(fparams);
		
		Futile.atlasManager.LoadAtlas("Atlases/lake_monster");
		Futile.atlasManager.LogAllElementNames();
		
		MetaContainer.loadFont("Arial-Black",120,"arial_black_120","Atlases/arial_black_120",0f,0f);

		ScreenManager.init(this, "");

		lake = new LakeScreen();
//		lake.y = lake.rootHeight/2 - Futile.screen.halfHeight - 500;
		lake.y = Futile.screen.halfHeight - lake.rootHeight/2;
		Futile.stage.AddChild(lake);
		
//		Go.to(lake, 5.0f, new TweenConfig().floatProp("y", Futile.screen.halfHeight - lake.rootHeight/2).setEaseType(EaseType.ExpoInOut).setDelay(0.1f));

		
		
		lake.onGameOver += handleGameOver;
	}
	
	void Update()
	{
		switch(state)
		{
			case STATE_GAMEPLAY:
				lake.Update ();	
				break;
			case STATE_GAMEOVER:
				//do nothing
				break;
			default:
				//do nothing
				break;
		}
		
	}
	
	private GameScreen gameOverScreen;
	public void handleGameOver()
	{
		if(gameOverScreen == null)
		{
			gameOverScreen = new GameScreen("newspaper");
			gameOverScreen.buttons["continue"].SignalRelease += handleContinue;
		}
		
		state = STATE_GAMEOVER;	
		ScreenManager.loadScreen(gameOverScreen, ScreenSourceDirection.Right);
	}
	
	public void handleContinue(FButton button)
	{
		lake.clearMe();
		lake.InitLevel1();
		
		state = STATE_GAMEPLAY;
		
		ScreenManager.loadScreen(null, ScreenSourceDirection.Left);
	}

}
