/*
 	PlatformController.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		February 13, 2014
 	Last Edited:	November 22, 2014
 	
 	Controls this specific platform.
*/


using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour
{
	#region Variables
	
	#region Public
	
	#region Sprite Names
	
	public string dirtyPlatOffSprite = "PlatformDirtyOff";
	public string dirtyPlatOnSprite = "PlatformDirtyOff";
	public string angryPlatOffSprite = "PlatformDirtyOff";
	public string angryPlatOnSprite = "PlatformDirtyOff";
	public string sadPlatOffSprite = "PlatformDirtyOff";
	public string sadPlatOnSprite = "PlatformDirtyOff";
	public string starPlatOffSprite = "PlatformDirtyOff";
	public string starPlatOnSprite = "PlatformDirtyOff";
	
	#endregion
	
	// The height of this platform
	public float platformHeight = 0.46f;
	// The width of this platform
	public float platformWidth = 1.7f;
	// How quickly this platform moves
	public float moveSpeed = 5.0f;
	// The material index for this platform
	public int matIndex = 0;
	// The face sprite for this platform
	public GameObject faceSprite;
	// The crack sprite for this platform
	public GameObject crackSprite;
	// The dirty fly for this platform
	public GameObject dirtyFly;
	// The snow sprite for this platform
	public GameObject snowSprite;
	// The icicle sprite for this platform
	public GameObject icicleSprite;
	// The animation for the icicle falling
	public Animation icicleFallingAnimation;
	// The range of local x positions the icicle sprite can appear on the platform
	public Vector2 icicleXPosRange = new Vector2 (-1.5f, 1.5f);
	
	#endregion
	
	#region Scripts
	
	// The platform manager
	PlatformManager manager;
	// The audio controller
	AudioController audioCont;
	
	#endregion
	
	#region Caches
	
	// The cached transform
	private Transform trans;
	
	#endregion
	
	#region Private
	
	// If this platform is currently in use
	private bool isCurrentlyInUse = false;
	// The default color of this platform
	private Color defaultColor;
	// The type of face this platform has (0 = no face)
	private int faceType = 0;
	// The despawn X position
	private float despawnXPos;
	// If the platform should be moving
	private bool isMoving = false;
	//
	private Vector3 stagingPos;
	// The renderer for this platform's face sprite
	private Renderer faceRend;
	// The sprite for the base 
	private tk2dSprite baseSprite;
	// The sprite for the face 
	private tk2dSprite faceImg;
	// The renderer for the crack
	private Renderer crackRend;
	// The y position this platform spawns at
	private float ySpawnPos;
	// The speed at which the sad platform falls
	private float sadFallSpeed = 0;
	// Whether or not this dirty platform has already been used
	private bool hasBeenUsedDirty = false;
	// The saved position of the icicle sprite
	private Vector3 _icicleStartPos;
	
	#endregion
	
	#endregion
	
	
	#region Update
	
	// Main update loop
	// Called automatically once per frame
	void LateUpdate ()
	{ 
		// Move this platform
		if (isMoving)
			MovePlatform ();
		
		// Check to see if this platform should despawn
		CheckForDespawn ();
		
		//
		if (icicleSprite.activeInHierarchy == true)
			icicleSprite.transform.localPosition = new Vector3 (_icicleStartPos.x, icicleSprite.transform.localPosition.y, _icicleStartPos.z);
	}
	
	
	// Moves this platform across the screen
	// Called every frame from Update ()
	void MovePlatform ()
	{
		trans.Translate (Vector3.left * manager.GetPlatformMoveSpeed () * Time.deltaTime);
	}
	
	
	// Checks to see if this platform is far enough to the left to despawn
	// Called every frame from Update ()
	void CheckForDespawn ()
	{
		if (trans.position.x <= despawnXPos) 
			Despawn ();
	}
	
	#endregion
	
	
	// Despawns this platform
	// Called from CheckForDespawn ()
	void Despawn ()
	{
		DeactivatePlatform ();
	}
	
	
	#region Getters
	
	public bool GetIsCurrentlyInUse () { return isCurrentlyInUse; }
	public bool GetIsLauncher ()
	{
		if (faceType == 2)
			return true;
		
		return false;
	}
	
	#endregion
	
	
	#region Setters
	
	public void SetIsMoving (bool b) { isMoving = b; }
	public void SetDefaultColor (Color c) { defaultColor = c; }
	public void SetFaceType (int type)
	{
		faceType = type;
		faceRend.enabled = true;
		
		// Set the proper sprite
		switch (type)
		{
			// Happy face (star)
		case 1: faceImg.SetSprite (starPlatOffSprite); break;
			// Angry face
		case 2: faceImg.SetSprite (angryPlatOffSprite); break;
			// Dirty face
		case 3:
			faceImg.SetSprite (dirtyPlatOffSprite);
			dirtyFly.SetActive (true);
			break;
			// Sad face
		case 4: faceImg.SetSprite (sadPlatOffSprite); break;
		}
	}
	
	
	//
	//
	public void PlayerDidLand (bool isHardLand)
	{
		// If the player landed hard, we should play the hard land sound effect
		if (isHardLand)
			audioCont.PlaySound ("Land Hard");
		else
			audioCont.PlayLandSound ();
		
		// The reaction will depend on the face type for this platform
		switch (faceType)
		{
			// No face
		case 0:
			baseSprite.color = Color.white;
			StartCoroutine (ReturnColor ());
			break;
			// Happy (star) face
		case 1:
			faceImg.SetSprite (starPlatOnSprite);
			audioCont.PlayLandSound ();
			audioCont.PlayStarLandSound ();
			Invoke ("ReturnToNormalcy", 0.4f);
			break;
			// Angry face
		case 2:
			faceImg.SetSprite (angryPlatOnSprite);
			audioCont.PlayLandSound ();
			audioCont.PlayAngryLandSound ();
			Invoke ("ReturnToNormalcy", 0.4f);
			break;
			// Dirty face
		case 3:
			faceImg.SetSprite (dirtyPlatOnSprite);
			audioCont.PlayLandSound ();
			audioCont.PlayDirtyLandSound ();
			
			// Only cut the move speed if this platform hasn't been used to cut speed yet
			if (!hasBeenUsedDirty)
			{
				manager.CutMoveSpeed ();
				hasBeenUsedDirty = true;
			}
			manager.ActivateDirtySlowdown ();
			Invoke ("ReturnToNormalcy", 0.4f);
			break;
			// Sad face
		case 4:
			faceImg.SetSprite (sadPlatOnSprite);
			audioCont.PlayLandSound ();
			audioCont.PlaySadLandSound ();
			Invoke ("BeginSadFall", 0.0f);
			break;
		}
	}
	
	
	//
	//
	void ReturnToNormalcy ()
	{
		SetFaceType (faceType);
	}
	
	#endregion
	
	
	#region Launch Player
	
	//
	//
	public void PlayerLaunched ()
	{
		StartCoroutine ("MoveUpLaunch");
	}
	
	
	//
	//
	IEnumerator MoveUpLaunch ()
	{
		while (isCurrentlyInUse)
		{
			trans.Translate (Vector3.up * Time.deltaTime * 8.0f);
			if (trans.localPosition.y >= ySpawnPos + 0.65f)
			{
				StartCoroutine (MoveDownLaunch ());
				StopCoroutine ("MoveUpLaunch");
			}
			
			yield return null;
		}
	}
	
	
	//
	//
	IEnumerator MoveDownLaunch ()
	{
		while (isCurrentlyInUse)
		{
			trans.Translate (Vector3.down * Time.deltaTime * 10.0f);
			if (trans.localPosition.y <= ySpawnPos)
			{
				trans.localPosition = new Vector3 (trans.localPosition.x, ySpawnPos, trans.localPosition.z);
				StopCoroutine ("MoveDownLaunch");
			}
			
			yield return null;
		}
	}
	
	#endregion
	
	
	#region Sad Fall
	
	//
	//
	void BeginSadFall () { StartCoroutine ("SadFaceFall"); }
	IEnumerator SadFaceFall ()
	{
		while (isCurrentlyInUse && trans.localPosition.x > -10)
		{
			sadFallSpeed += Time.deltaTime * 0.135f;
			trans.Translate (Vector3.down * sadFallSpeed);
			yield return null;
		}
	}
	
	#endregion
	
	
	#region Utility
	
	IEnumerator ReturnColor ()
	{
		while (isCurrentlyInUse)
		{
			baseSprite.color = Color.Lerp (baseSprite.color, defaultColor, Time.deltaTime * 7.0f);
			yield return null;
		}
	}
	
	
	// Called when the player lands hard on this platform
	// Called from HardLand (PlatformController p) in PlayerController.cs
	public void SetDidHitHard ()
	{
		crackRend.enabled = true;
		if (icicleSprite.activeInHierarchy == true)
			icicleFallingAnimation.Play ();
	}
	
	
	//
	//
	public void ActivatePlatform ()
	{
		isCurrentlyInUse = true;
		ySpawnPos = trans.position.y;
	}
	
	
	//
	//
	public void StartMovingPlatform ()
	{
		SetIsMoving (true);
	}
	
	
	//
	//
	public int GetFaceType ()
	{
		return faceType;
	}
	
	
	//
	//
	public void DeactivatePlatform ()
	{
		sadFallSpeed = 0;
		SetIsMoving (false);
		crackRend.enabled = false;
		icicleSprite.SetActive (false);
		isCurrentlyInUse = false;
		hasBeenUsedDirty = false;
		trans.position = stagingPos;
		faceType = 0;
		faceRend.enabled = false;
		dirtyFly.SetActive (false);
		icicleFallingAnimation.Stop ();
		icicleSprite.transform.localPosition = _icicleStartPos;
		CancelInvoke ("ReturnToNormalcy");
		StopAllCoroutines ();
	}
	
	#endregion
	
	
	#region Initialization
	
	// Used for initialization
	// Called automatically at start
	void Start ()
	{
		// Assign the initial variables
		AssignVariables ();
		
		// Place the speciality sprites
		PlaceSpecialSprites ();
	}
	
	
	// Positions special sprites (like icicles)
	// Called from Start ()
	void PlaceSpecialSprites ()
	{
		// Icicles
		// Get random icicle sprite
		int r = Random.Range (0, 10);
		string icicleSpriteName = "icicle_1";
		if (r <= 5)
			icicleSpriteName = "icicle_2";
		icicleSprite.GetComponent <tk2dSprite> ().SetSprite (icicleSpriteName);
		
		// Set icicle sprite at random local position
		float randomPos = Random.Range (icicleXPosRange.x, icicleXPosRange.y);
		icicleSprite.transform.localPosition = new Vector3 (randomPos, icicleSprite.transform.localPosition.y, icicleSprite.transform.localPosition.z);
		_icicleStartPos = icicleSprite.transform.localPosition;
		
		// Randomize snow sprite
		int rs = Random.Range (0, 30);
		string snowSpriteName = "snow_1";
		if (rs <= 9)
			snowSpriteName = "snow_2";
		else if (rs <= 19)
			snowSpriteName = "snow_3";
		snowSprite.GetComponent <tk2dSprite> ().SetSprite (snowSpriteName);
	}
	
	
	// Assigns initial variables
	// Called initially from Start ()
	private void AssignVariables ()
	{
		manager = GameObject.Find ("&MainController").GetComponent <PlatformManager> ();
		audioCont = GameObject.Find ("&MainController").GetComponent <AudioController> ();
		despawnXPos = manager.platformDespawnXPos;
		trans = transform;
		stagingPos = new Vector3 (manager.spawnXPos, 0f, 0f);
		faceRend = faceSprite.GetComponent<Renderer> ();
		faceImg = faceSprite.GetComponent <tk2dSprite> ();
		crackRend = crackSprite.GetComponent<Renderer>();
		baseSprite = GetComponent <tk2dSprite> ();
	}
	
	#endregion
}
