#define _DEBUG_

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

public class LakeMonster : MonoBehaviour
{
	private FContainer screen;
	
	void Start()
	{
		FutileParams fparams = new FutileParams(true, true, false, false); //landscape left, right, portrait, portraitUpsideDown

		fparams.AddResolutionLevel(768.0f, 1.0f, 1.0f, ""); //max height, displayScale, resourceScale, resourceSuffix
		
		fparams.backgroundColor = RXUtils.GetColorFromHex("555555");
		fparams.origin = new Vector2(0.5f, 0.5f);
		
		Futile.instance.Init(fparams);
		
//		Futile.atlasManager.LoadAtlases("Atlases/dtd_atlas");
//		MetaContainer.loadFont("BarutaBlack",48,"barutablack_48","Atlases/barutablack_48",0f,0f);

//		ScreenManager.init(this, "popup_backdrop");
		
//		AnimationManager.init(this);
//		AnimationManager.loadAnimationSet("Animation/flump_test");

//		CreepModel.LoadFromData("creeps");
		
		Debug.Log("HELLO WORLD");
	}

}
