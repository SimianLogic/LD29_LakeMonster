#define _DEBUG_

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

public class LakeMonster : MonoBehaviour
{
	private FContainer screen;
	public LakeScreen lake;

	void Start()
	{
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
		lake.y = Futile.screen.halfHeight - lake.rootHeight / 2;
		//lake.y = lake.rootHeight/2 - Futile.screen.halfHeight - 500;
		ScreenManager.loadScreen(lake, ScreenSourceDirection.Instant);
		
		//Go.to(lake, 5.0f, new TweenConfig().floatProp("y", Futile.screen.halfHeight - lake.rootHeight/2).setEaseType(EaseType.ExpoInOut).setDelay(0.1f));
		
		
		
//		AnimationManager.init(this);
//		AnimationManager.loadAnimationSet("Animation/flump_test");

//		CreepModel.LoadFromData("creeps");
		
		Debug.Log("HELLO WORLD");
	}

	void Update()
	{
		lake.update ();
	}
}
