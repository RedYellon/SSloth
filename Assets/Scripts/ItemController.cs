


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
		
		#endregion
		
		#region Private
		
		//
		private bool headSpriteIsOn = false;
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
	public void SetHeadSpriteActive (bool b)
	{
		headSpriteIsOn = b;
		if (!b)
			DisableHeadSprites ();
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
	}
	
	#endregion
}
