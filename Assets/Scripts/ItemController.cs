


using UnityEngine;
using System.Collections;


public class ItemController : MonoBehaviour 
{
	#region Variables
	
		#region Public
		
		
		
		#endregion
		
		#region Scripts
		
		
		
		#endregion
		
		#region Sprites
		
			#region Head
			
			private tk2dSprite idle1HeadSprite;
			private tk2dSprite idle2HeadSprite;
			private tk2dSprite jump1HeadSprite;
			private tk2dSprite jump2HeadSprite;
			private tk2dSprite jump3HeadSprite;
			private tk2dSprite jump4HeadSprite;
			private tk2dSprite jump5HeadSprite;
			private tk2dSprite jump6HeadSprite;
			private tk2dSprite landHeadSprite;
			private tk2dSprite run1HeadSprite;
			private tk2dSprite run2HeadSprite;
			
			#endregion

			#region Santa Hat
			
			private tk2dSprite idle1HeadSpriteSH;
			private tk2dSprite idle2HeadSpriteSH;
			private tk2dSprite jump1HeadSpriteSH;
			private tk2dSprite jump2HeadSpriteSH;
			private tk2dSprite jump3HeadSpriteSH;
			private tk2dSprite jump4HeadSpriteSH;
			private tk2dSprite jump5HeadSpriteSH;
			private tk2dSprite jump6HeadSpriteSH;
			private tk2dSprite landHeadSpriteSH;
			private tk2dSprite run1HeadSpriteSH;
			private tk2dSprite run2HeadSpriteSH;
			
			#endregion
		
		#endregion
		
		#region Private
		
		//
		private bool headSpriteIsOn = false;
		private bool _santaHatIsOn = false;
		private bool bodySpriteIsOn = false;
		private bool feetSpriteIsOn = false;
		private bool handsSpriteIsOn = false;
		
		#endregion
	
	#endregion
	
	
	#region Frame Change
	
	//
	//
	public void SetFrame (int f)
	{
		// If we need to set the head sprite frame
		if (headSpriteIsOn)
		{
			// First, deactivate all other frames
			DisableHeadSprites ();
		
			// Now, activate only the specified frame
			switch (f)
			{
				case 1: idle1HeadSprite.renderer.enabled = true; break;
				case 2: idle2HeadSprite.renderer.enabled = true; break;
				case 3: jump1HeadSprite.renderer.enabled = true; break;
				case 4: jump2HeadSprite.renderer.enabled = true; break;
				case 5: jump3HeadSprite.renderer.enabled = true; break;
				case 6: jump4HeadSprite.renderer.enabled = true; break;
				case 7: jump5HeadSprite.renderer.enabled = true; break;
				case 8: jump6HeadSprite.renderer.enabled = true; break;
				case 9: landHeadSprite.renderer.enabled = true; break;
				case 10: run1HeadSprite.renderer.enabled = true; break;
				case 11: run2HeadSprite.renderer.enabled = true; break;
			}
		} 

		//
		if (_santaHatIsOn)
		{
			// First, deactivate all other frames
			DisableSantaHatSprites ();
			
			// Now, activate only the specified frame
			switch (f)
			{
				case 1: idle1HeadSpriteSH.renderer.enabled = true; break;
				case 2: idle2HeadSpriteSH.renderer.enabled = true; break;
				case 3: jump1HeadSpriteSH.renderer.enabled = true; break;
				case 4: jump2HeadSpriteSH.renderer.enabled = true; break;
				case 5: jump3HeadSpriteSH.renderer.enabled = true; break;
				case 6: jump4HeadSpriteSH.renderer.enabled = true; break;
				case 7: jump5HeadSpriteSH.renderer.enabled = true; break;
				case 8: jump6HeadSpriteSH.renderer.enabled = true; break;
				case 9: landHeadSpriteSH.renderer.enabled = true; break;
				case 10: run1HeadSpriteSH.renderer.enabled = true; break;
				case 11: run2HeadSpriteSH.renderer.enabled = true; break;
			}
		}
	}
	
	#endregion
	
	
	#region Sprite Activation
	
	//
	//
	public void SetHeadSprite (string itemName)
	{
		idle1HeadSprite.SetSprite ("1_head_" + itemName + "_slothIdle1");
		idle1HeadSprite.SetSprite ("1_head_" + itemName + "_slothIdle2");
		jump1HeadSprite.SetSprite ("1_head_" + itemName + "_slothJump1");
		jump2HeadSprite.SetSprite ("1_head_" + itemName + "_slothJump2");
		jump3HeadSprite.SetSprite ("1_head_" + itemName + "_slothJump3");
		jump4HeadSprite.SetSprite ("1_head_" + itemName + "_slothJump4");
		jump5HeadSprite.SetSprite ("1_head_" + itemName + "_slothJump5");
		jump6HeadSprite.SetSprite ("1_head_" + itemName + "_slothJump6");
		landHeadSprite.SetSprite ("1_head_" + itemName + "_slothLand");
		run1HeadSprite.SetSprite ("1_head_" + itemName + "_slothRun1");
		run2HeadSprite.SetSprite ("1_head_" + itemName + "_slothRun2");
	}


	//
	//
	/*public void SetSantaHatSprite ()
	{
		idle1HeadSpriteSH.SetSprite ("1_head_" + itemName + "_slothIdle1");
		idle1HeadSpriteSH.SetSprite ("1_head_" + itemName + "_slothIdle2");
		jump1HeadSpriteSH.SetSprite ("1_head_" + itemName + "_slothJump1");
		jump2HeadSpriteSH.SetSprite ("1_head_" + itemName + "_slothJump2");
		jump3HeadSpriteSH.SetSprite ("1_head_" + itemName + "_slothJump3");
		jump4HeadSpriteSH.SetSprite ("1_head_" + itemName + "_slothJump4");
		jump5HeadSpriteSH.SetSprite ("1_head_" + itemName + "_slothJump5");
		jump6HeadSpriteSH.SetSprite ("1_head_" + itemName + "_slothJump6");
		landHeadSpriteSH.SetSprite ("1_head_" + itemName + "_slothLand");
		run1HeadSpriteSH.SetSprite ("1_head_" + itemName + "_slothRun1");
		run2HeadSpriteSH.SetSprite ("1_head_" + itemName + "_slothRun2");
	}*/
	
	
	//
	//
	public void SetHeadSpriteActive (bool b)
	{
		headSpriteIsOn = b;
		if (!b)
			DisableHeadSprites ();
	}

	public void SetSantaHatSpriteActive (bool b)
	{
		_santaHatIsOn = b;
		if (!b)
			DisableSantaHatSprites ();
	}
	
	
	// Disables all of the head sprites
	// Called from SetFrame (int f)
	void DisableHeadSprites ()
	{
		idle1HeadSprite.renderer.enabled = false;
		idle2HeadSprite.renderer.enabled = false;
		jump1HeadSprite.renderer.enabled = false;
		jump2HeadSprite.renderer.enabled = false;
		jump3HeadSprite.renderer.enabled = false;
		jump4HeadSprite.renderer.enabled = false;
		jump5HeadSprite.renderer.enabled = false;
		jump6HeadSprite.renderer.enabled = false;
		landHeadSprite.renderer.enabled = false;
		run1HeadSprite.renderer.enabled = false;
		run2HeadSprite.renderer.enabled = false;
	}


	// Disables all of the head sprites
	// Called from SetFrame (int f)
	void DisableSantaHatSprites ()
	{
		idle1HeadSpriteSH.renderer.enabled = false;
		idle2HeadSpriteSH.renderer.enabled = false;
		jump1HeadSpriteSH.renderer.enabled = false;
		jump2HeadSpriteSH.renderer.enabled = false;
		jump3HeadSpriteSH.renderer.enabled = false;
		jump4HeadSpriteSH.renderer.enabled = false;
		jump5HeadSpriteSH.renderer.enabled = false;
		jump6HeadSpriteSH.renderer.enabled = false;
		landHeadSpriteSH.renderer.enabled = false;
		run1HeadSpriteSH.renderer.enabled = false;
		run2HeadSpriteSH.renderer.enabled = false;
	}
	
	#endregion


	#region Layers

	//
	public void BringSantaHatsToForeground ()
	{
		idle1HeadSpriteSH.renderer.sortingLayerName = "HighScoreGlasses";
		idle2HeadSpriteSH.renderer.sortingLayerName = "HighScoreGlasses";
		jump1HeadSpriteSH.renderer.sortingLayerName = "HighScoreGlasses";
		jump2HeadSpriteSH.renderer.sortingLayerName = "HighScoreGlasses";
		jump3HeadSpriteSH.renderer.sortingLayerName = "HighScoreGlasses";
		jump4HeadSpriteSH.renderer.sortingLayerName = "HighScoreGlasses";
		jump5HeadSpriteSH.renderer.sortingLayerName = "HighScoreGlasses";
		jump6HeadSpriteSH.renderer.sortingLayerName = "HighScoreGlasses";
		landHeadSpriteSH.renderer.sortingLayerName = "HighScoreGlasses";
		run1HeadSpriteSH.renderer.sortingLayerName = "HighScoreGlasses";
		run2HeadSpriteSH.renderer.sortingLayerName = "HighScoreGlasses";
	}


	//
	public void BringHeadToForeground ()
	{
		idle1HeadSprite.renderer.sortingLayerName = "HighScoreGlasses";
		idle2HeadSprite.renderer.sortingLayerName = "HighScoreGlasses";
		jump1HeadSprite.renderer.sortingLayerName = "HighScoreGlasses";
		jump2HeadSprite.renderer.sortingLayerName = "HighScoreGlasses";
		jump3HeadSprite.renderer.sortingLayerName = "HighScoreGlasses";
		jump4HeadSprite.renderer.sortingLayerName = "HighScoreGlasses";
		jump5HeadSprite.renderer.sortingLayerName = "HighScoreGlasses";
		jump6HeadSprite.renderer.sortingLayerName = "HighScoreGlasses";
		landHeadSprite.renderer.sortingLayerName = "HighScoreGlasses";
		run1HeadSprite.renderer.sortingLayerName = "HighScoreGlasses";
		run2HeadSprite.renderer.sortingLayerName = "HighScoreGlasses";
	}


	//
	public void BringSantaHatsToBackground ()
	{
		idle1HeadSpriteSH.renderer.sortingLayerName = "Player";
		idle2HeadSpriteSH.renderer.sortingLayerName = "Player";
		jump1HeadSpriteSH.renderer.sortingLayerName = "Player";
		jump2HeadSpriteSH.renderer.sortingLayerName = "Player";
		jump3HeadSpriteSH.renderer.sortingLayerName = "Player";
		jump4HeadSpriteSH.renderer.sortingLayerName = "Player";
		jump5HeadSpriteSH.renderer.sortingLayerName = "Player";
		jump6HeadSpriteSH.renderer.sortingLayerName = "Player";
		landHeadSpriteSH.renderer.sortingLayerName = "Player";
		run1HeadSpriteSH.renderer.sortingLayerName = "Player";
		run2HeadSpriteSH.renderer.sortingLayerName = "Player";
	}


	//
	public void BringHeadToBackground ()
	{
		idle1HeadSprite.renderer.sortingLayerName = "Player";
		idle2HeadSprite.renderer.sortingLayerName = "Player";
		jump1HeadSprite.renderer.sortingLayerName = "Player";
		jump2HeadSprite.renderer.sortingLayerName = "Player";
		jump3HeadSprite.renderer.sortingLayerName = "Player";
		jump4HeadSprite.renderer.sortingLayerName = "Player";
		jump5HeadSprite.renderer.sortingLayerName = "Player";
		jump6HeadSprite.renderer.sortingLayerName = "Player";
		landHeadSprite.renderer.sortingLayerName = "Player";
		run1HeadSprite.renderer.sortingLayerName = "Player";
		run2HeadSprite.renderer.sortingLayerName = "Player";
	}

	#endregion
	
	
	#region Initialization
	
	// Used for initialization
	// Called automatically at beginning
	void Start ()
	{
		// Assign the initial private/script/reference variables
		AssignVariables ();
	}
	
	
	// Assigns the initial private/script/reference variables
	// Called from Start ()
	private void AssignVariables ()
	{
		// Head sprites
		idle1HeadSprite = GameObject.Find ("slothIdle1_head").GetComponent <tk2dSprite> ();
		idle2HeadSprite = GameObject.Find ("slothIdle2_head").GetComponent <tk2dSprite> ();
		jump1HeadSprite = GameObject.Find ("slothJump1_head").GetComponent <tk2dSprite> ();
		jump2HeadSprite = GameObject.Find ("slothJump2_head").GetComponent <tk2dSprite> ();
		jump3HeadSprite = GameObject.Find ("slothJump3_head").GetComponent <tk2dSprite> ();
		jump4HeadSprite = GameObject.Find ("slothJump4_head").GetComponent <tk2dSprite> ();
		jump5HeadSprite = GameObject.Find ("slothJump5_head").GetComponent <tk2dSprite> ();
		jump6HeadSprite = GameObject.Find ("slothJump6_head").GetComponent <tk2dSprite> ();
		landHeadSprite = GameObject.Find ("slothLand_head").GetComponent <tk2dSprite> ();
		run1HeadSprite = GameObject.Find ("slothRun1_head").GetComponent <tk2dSprite> ();
		run2HeadSprite = GameObject.Find ("slothRun2_head").GetComponent <tk2dSprite> ();

		// Santa head sprites
		idle1HeadSpriteSH = GameObject.Find ("SantaLand_head_slothIdle1").GetComponent <tk2dSprite> ();
		idle2HeadSpriteSH = GameObject.Find ("SantaLand_head_slothIdle2").GetComponent <tk2dSprite> ();
		jump1HeadSpriteSH = GameObject.Find ("SantaLand_head_slothjump1").GetComponent <tk2dSprite> ();
		jump2HeadSpriteSH = GameObject.Find ("SantaLand_head_slothjump2").GetComponent <tk2dSprite> ();
		jump3HeadSpriteSH = GameObject.Find ("SantaLand_head_slothjump3").GetComponent <tk2dSprite> ();
		jump4HeadSpriteSH = GameObject.Find ("SantaLand_head_slothjump4").GetComponent <tk2dSprite> ();
		jump5HeadSpriteSH = GameObject.Find ("SantaLand_head_slothjump5").GetComponent <tk2dSprite> ();
		jump6HeadSpriteSH = GameObject.Find ("SantaLand_head_slothjump6").GetComponent <tk2dSprite> ();
		landHeadSpriteSH = GameObject.Find ("SantaLand_head_slothLand").GetComponent <tk2dSprite> ();
		run1HeadSpriteSH = GameObject.Find ("SantaLand_head_slothRun1").GetComponent <tk2dSprite> ();
		run2HeadSpriteSH = GameObject.Find ("SantaLand_head_slothRun2").GetComponent <tk2dSprite> ();
	}
	
	#endregion
}
