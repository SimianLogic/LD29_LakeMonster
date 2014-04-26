using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MetaContainer : FContainer
{
	
	private static Dictionary<string, int> LoadedFonts;

	public int rootWidth;
	public int rootHeight;
	
	public Dictionary<string, Vector2> positions;
	public Dictionary<string, FLabel> labels;
	public Dictionary<string, FButton> buttons;
	public Dictionary<string, FSprite> progress;
	public Dictionary<string, FSprite> images;
	public Dictionary<string, FToggle> toggles;

	public MetaContainer(string metadata_filename) : base()
	{	
		string text;
		string path = "Metadata/" + metadata_filename + metadataTag;
		try
		{
			TextAsset asset = Resources.Load(path, typeof(TextAsset)) as TextAsset;
			text = asset.text;
		}catch(Exception){
			throw new FutileException("UNABLE TO LOAD METACONTAINER " + path);
		}
		processMetadata(text);
	}

	public MetaContainer() : base()
	{
		processMetadata("");
	}
	
	//Futile.atlasManager.LoadFont("Monaco","monaco_48", "monaco_48", 0.0f, 0.0f);
	public static void loadFont(string fontName, int fontSize, string elementName, string configPath, float offsetX, float offsetY)
	{
		if(LoadedFonts == null)
		{
			LoadedFonts = new Dictionary<string, int>();
		}
		
		if(LoadedFonts.ContainsKey(fontName))
		{
			// Debug.LogWarning("ALREADY LOADED " + fontName);
		}else{
			Futile.atlasManager.LoadFont(fontName, elementName, configPath, offsetX, offsetY);
			LoadedFonts.Add (fontName, fontSize);
		}
	}
	
	public static string metadataTag
	{
		get
		{
			switch(Futile.resourceSuffix)
			{
				case("_ipad"):
					return "_ipad";
				case("_ipad_retina"):
					return "_ipad";
				case("_iphone"):
					return "_iphone";
				case("_iphone_retina"):
					return "_iphone";
				case("_iphone5"):
					return "_iphone5";
				case("_iphone5_retina"):
					return "_iphone5";
				default:
					throw new FutileException("Unrecognized resource suffix!");
			}
		}
	}
		
	//useful for debugging whether all the labels we find are there or not... won't throw an error		
	public void setText(string field, string text)
	{
		if(labels.ContainsKey(field))
		{
			labels[field].text = text;
			labels[field].x = positions[field].x;
			labels[field].y = positions[field].y;
		}else{
			// Debug.LogWarning("CANNOT FIND TEXT FIELD NAMED: " + field);
		}
		
	}
	
	internal void addProgressBar(string name, string fill_name)
	{		
		FSprite progress_bar = images[fill_name];

		progress_bar.anchorX = 0.0f;
		progress_bar.x -= progress_bar.width/2;
		
		progress[name] = progress_bar;
	}
	
	string colorToHex(Color32 color)
	{
		string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
		return hex;
	}
	
	Color hexToColor(string hex)
	{
		byte r = byte.Parse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
		return new Color32(r,g,b, 255);
	}	
	
	internal void processMetadata(string metadata)
	{
		//master list of all name -> coordinates
		positions = new Dictionary<string,Vector2>();
		
		//lists of our buttons, labels, progress bars, and images
		labels = new Dictionary<string, FLabel>();
		buttons = new Dictionary<string, FButton>();
		progress = new Dictionary<string, FSprite>();
		images = new Dictionary<string, FSprite>();
		toggles = new Dictionary<string, FToggle>();

		string[] objects = metadata.Split("+"[0]);
		
		List<string> things_to_toggle = new List<string>();
		foreach(string obj in objects)
		{
			//ignore the empty string
			if(obj == "")
				continue;

			string[] data = obj.Split("|"[0]);
			string type = data[0].Split("_"[0])[0];
			
			//these two don't have an x & a y!
			if(data[0] == "root_width"){
				rootWidth = System.Int32.Parse(data[1]);
				continue;
			}else if(data[0] == "root_height"){
				rootHeight = System.Int32.Parse(data[1]);				
				continue;
			}
			
			int x = System.Int32.Parse(data[1]);
			int y = System.Int32.Parse(data[2]);
			
//			 Debug.Log(data[0] + " - " + x + "," + y);

			if(type == "btn"){
				if(data[0].IndexOf("_down") >= 0)
				{
					//no need to process a button twice!
					continue;
				}

				string button_name = data[0].Replace("btn_","").Replace("_up","");
				FButton button = new FButton(data[0], "btn_" + button_name + "_down");
				
				button.data = button_name;
				
				if(button_name.Contains("tab_"))
				{
					things_to_toggle.Add(button_name);
				}

				this.AddChild(button);
				button.x = x;
				button.y = y;

				buttons[button_name] = button;
				positions[button_name] = new Vector2(x,y);
			}else if(type == "text"){
			  //TODO: font size
			  			  
			  	// Debug.Log("ADDING LABEL: " + data[0].Substring(5));
			  
			  	//might be more later?
				string clean_text = data[9].Replace("/r","/n");
				FLabel label = new FLabel(data[4],clean_text);
				this.AddChild(label);
				
				float text_size = (float)System.Double.Parse(data[6]);
				// Debug.Log ("TEXT SIZE: " + text_size);
				int font_size = LoadedFonts[data[4]];
				
				float scale_mod = text_size / font_size;
				label.scale = scale_mod;
				
				
				if(data[5] == "center")
				{
          			label.anchorX = 0.5f;
				}else if(data[5] == "left"){
					label.anchorX = 0.0f;
				}else if(data[5] == "right"){
					label.anchorX = 1.0f;
				}
				
				label.x = x;
				label.y = y;
				
				label.color = hexToColor(data[3]);

				labels[data[0].Substring(5)] = label;
				positions[data[0].Substring(5)] = new Vector2(x,y);
			}else{
				//x,y in this point are assuming y is at the top left...
				positions[data[0]] = new Vector2(x,y);
			
				FSprite sprite = new FSprite(data[0]);
				this.AddChild(sprite);

				// Debug.Log("   -> anchorX = " + sprite.anchorX);
				
				sprite.x = x;
				sprite.y = y;
				
				images[data[0]] = sprite;
				positions[data[0]] = new Vector2(x,y);
			}
		}
		
		//CONVERT things with tab_buttonname_state into buttons with state!
		foreach(string button_name in things_to_toggle)
		{
			string[] toggle_data = button_name.Split("_"[0]);
			string state = toggle_data[toggle_data.Length - 1];
			
			//pop off the tab_ and the _state
			string toggle_name = toggle_data[1];
			for(int i = 2; i < toggle_data.Length - 1; i++)
			{
				toggle_name = toggle_name + "_" + toggle_data[i];
			}
			
			if(!toggles.ContainsKey(toggle_name))
			{
				toggles[toggle_name] = new FToggle();
				toggles[toggle_name].data = toggle_name;
			}
			
			toggles[toggle_name].addState(state, buttons[button_name]);
		}
		
		foreach(FToggle toggle in toggles.Values)
		{
			Debug.Log ("ADDED TOGGLE " + toggle.data);
			toggle.finalize();
		}
	}
}

