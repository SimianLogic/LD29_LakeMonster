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
	public const int STATE_SPLASH = 3;
	public const int STATE_LEVELS = 4;
	
	public int state;
	public int currentLevel;
	
	private GameScreen splash;
	
	void Start()
	{
		state = STATE_SPLASH;
		
		FutileParams fparams = new FutileParams(true, true, false, false); //landscape left, right, portrait, portraitUpsideDown

		fparams.AddResolutionLevel(1024.0f, 1.0f, 1.0f, ""); //max width, displayScale, resourceScale, resourceSuffix
		
		fparams.backgroundColor = RXUtils.GetColorFromHex("555555");
		fparams.origin = new Vector2(0.5f, 0.5f);
		
		Futile.instance.Init(fparams);
		
		Futile.atlasManager.LoadAtlas("Atlases/lake_monster");
		Futile.atlasManager.LogAllElementNames();
		
		MetaContainer.loadFont("Arial-Black",120,"arial_black_120","Atlases/arial_black_120",0f,0f);
		MetaContainer.loadFont("BrushScriptStd",16,"BrushScriptStd_64","Atlases/BrushScriptStd_64",4f,-12f);

		ScreenManager.init(this, "");
		
		splash = new GameScreen("greetings_lochness_cleanup");
		splash.buttons["start"].SignalRelease += handleLevels;
		ScreenManager.loadScreen(splash, ScreenSourceDirection.Instant);
	}
	
	void Update()
	{
		switch(state)
		{
			case STATE_GAMEPLAY:
				lake.Update ();	
				break;
			default:
				//do nothing
				break;
		}
		
	}
	
	
	
	private GameScreen levels;
	public void handleLevels(FButton button)
	{
		if(levels == null)
		{
			levels = new GameScreen("levelChooser");
			levels.buttons["play1"].SignalRelease += handleLevelStart;
			levels.buttons["play2"].SignalRelease += handleLevelStart;
			levels.buttons["play3"].SignalRelease += handleLevelStart;
			levels.buttons["play4"].SignalRelease += handleLevelStart;
			levels.buttons["play5"].SignalRelease += handleLevelStart;
			levels.buttons["play6"].SignalRelease += handleLevelStart;
			levels.buttons["play7"].SignalRelease += handleLevelStart;
			levels.buttons["play8"].SignalRelease += handleLevelStart;
		}
		
		state = STATE_LEVELS;
		ScreenManager.loadScreen(levels, ScreenSourceDirection.Right);
	}
	
	public void handleLevelStart(FButton button)
	{
		if(button == levels.buttons["play1"])
		{
			playGame(1);
		}else if(button == levels.buttons["play2"]){
			playGame(2);
		}else if(button == levels.buttons["play3"]){
			playGame(3);
		}else if(button == levels.buttons["play4"]){
			playGame(4);
		}else if(button == levels.buttons["play5"]){
			playGame(5);
		}else if(button == levels.buttons["play6"]){
			playGame(6);
		}else if(button == levels.buttons["play7"]){
			playGame(7);
		}else if(button == levels.buttons["play8"]){
			playGame(8);
		}
	}
	
	public void playGame(int level)
	{
		if(lake == null)
		{
			lake = new LakeScreen();
			lake.onGameOver += handleGameOver;
			lake.onVictory += handleVictory;
		}
		
		state = STATE_GAMEPLAY;
		
		currentLevel = level;
		lake.startLevel(level);
		
		lake.y = lake.rootHeight/2 - Futile.screen.halfHeight - 200;

//		lake.y = Futile.screen.halfHeight - lake.rootHeight/2;

		Futile.stage.AddChild(lake);
		lake.MoveToBack();
		
		Go.to(lake, 5.0f, new TweenConfig().floatProp("y", Futile.screen.halfHeight - lake.rootHeight/2).setEaseType(EaseType.ExpoInOut).setDelay(0.1f));
		
		ScreenManager.loadScreen(null, ScreenSourceDirection.Left);
	}
	
	private GameScreen gameOverScreen;
	public void handleGameOver()
	{
		if(gameOverScreen == null)
		{
			gameOverScreen = new GameScreen("newspaper");
			gameOverScreen.buttons["back"].SignalRelease += handleContinue;
			gameOverScreen.buttons["continue"].SignalRelease += handleRestart;
		}
		
		state = STATE_GAMEOVER;	
		ScreenManager.loadScreen(gameOverScreen, ScreenSourceDirection.Right);
	}
	
	private GameScreen victoryScreen;
	public void handleVictory()
	{
		if(victoryScreen == null)
		{
			victoryScreen = new GameScreen("youwin");
			victoryScreen.buttons["continue"].SignalRelease += handleContinue;
		}
		
		state = STATE_GAMEOVER;	
		ScreenManager.loadScreen(victoryScreen, ScreenSourceDirection.Right);
	}
	
	public void handleContinue(FButton button)
	{
		lake.RemoveFromContainer();
		state = STATE_LEVELS;
		
		ScreenManager.loadScreen(levels, ScreenSourceDirection.Left);
	}
	
	public void handleRestart(FButton button)
	{
		playGame(currentLevel);
		
		ScreenManager.loadScreen(null, ScreenSourceDirection.Right);
	}

}
