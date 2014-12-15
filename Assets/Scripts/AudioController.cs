/*
 	AudioController.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		September 18, 2013
 	Last Edited:	September 12, 2014
 	
 	Contains methods for audio playback and manipulation.
*/


using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class AudioController : MonoBehaviour 
{
	#region Variables
	
		#region Public
		
		//The max volumes for the day and night tracks
		public float dayMusicMaxVolume = 1.4f;
		public float nightMusicMaxVolume = 1.4f;
		// The rate that music tracks fade in/out
		public float musicFadeRate = 0.003f;
		// The percentage of total volume that is played when the music is ducked
		public float duckVolumePlayPercentage = 0.5f;
		// The default volume for the general sound effects
		public float defaultSESourceVolume = 0.3f;
		// The default volume for the platform landing source
		public float defaultPlatLandSourceVolume = 0.49f;
		// The default volume for the happy platform landing source
		public float defaultHappyPlatLandSourceVolume = 0.549f;
		// The default volume for the angry platform landing source
		public float defaultAngryPlatLandSourceVolume = 0.919f;
		// The default volume for the sad platform landing source
		public float defaultSadPlatLandSourceVolume = 0.919f;
		// The default volume for the dirty platform landing source
		public float defaultDirtyPlatLandSourceVolume = 0.919f;
		// The default volume for the running score source
		public float defaultRunningScoreSourceVolume = 0.919f;
		// The default volume for the button source
		public float defaultButtonSourceVolume = 0.919f;
		// The default volume for the drumroll source
		public float defaultDrumrollSourceVolume = 0.919f;
		// The image used to indicate the volume has been turned off
		public Image volumeOffImg;
			
		#endregion

		#region Music

		// The default daytime music clip
		public AudioClip defaultDayMusicClip;
		// The winter daytime mix
		public AudioClip winterDayMusicClip;
		// The holiday daytime mix
		public AudioClip holidayDayMusicClip;

		#endregion
	
		#region Sound Effects
		
		// Our sound effect clips
		public AudioClip jumpClip;
		public AudioClip doubleJumpClip;
		public AudioClip landClip;
		public AudioClip landHardClip;
		public AudioClip gameOverClip;
		public AudioClip buttonClip;
		public AudioClip symbalClip;
		public AudioClip starGoldGetClip;
		public AudioClip starSilverGetClip;
		public AudioClip starBronzeGetClip;
		public AudioClip starCollectClip;
		public AudioClip mainMenuButtonClip;
		public AudioClip beginningFallClip;
		public AudioClip colorBarChangeClip;
		public AudioClip cloudBounceClip;
		public AudioClip menuLoadedClip;
		public AudioClip multiplierUpClip;
		public AudioClip scoreMultiBlinkClip;
		public AudioClip scoreAddingLoopClip;
		public AudioClip scoreAddingLoopDoneClip;
		public AudioClip scoreAddingLoopDoneHighClip;
		public AudioClip scoreAddingStarsValueClip;
		public AudioClip dirtyLandClip;
		public AudioClip optionsPressedClip;
		public AudioClip tabPressedClip;
		public AudioClip optionsLandPressedClip;
		public AudioClip dexterWhooClip;
		public AudioClip whooshClip;
		public AudioClip applauseClip;
		public AudioClip shingClip;
		public AudioClip cheatButtonClip;
		public AudioClip cheatFailedClip;
		public AudioClip cheatSucceededClip;
		public AudioClip animalBlinkClip;
		public AudioClip animalLeaveClip;
		// The happy sounds
		public AudioClip [] happySounds;
		// The angry sounds
		public AudioClip [] angrySounds;
		// The sad sounds
		public AudioClip [] sadSounds;
		// The dirty sounds
		public AudioClip [] dirtySounds;
	
		#endregion
		
		#region Scripts
		
		// The data controller
		DataController dataCont;
		
		#endregion
		
		#region Private
	
		// The sound effect audio sources
		private AudioSource [] soundEffectSources;
		// The music audio source for the daytime
		private AudioSource dayMusicSource;
		// The music audio source for the nighttime
		private AudioSource nightMusicSource;
		// The platform landing sound source
		private AudioSource platLandSource;
		//
		private AudioSource starPlatLandSource;
		//
		private AudioSource angryPlatLandSource;
		//
		private AudioSource sadPlatLandSource;
		//
		private AudioSource dirtyPlatLandSource;
		//
		private AudioSource drumrollSource;
		//
		private AudioSource scoreRunnerSource;
		//
		private AudioSource buttonSource;
		// If it is day time (if false, it is night time)
		private bool isDayTime = true;
		// The current un-corrected volume of the sound effects
		private float uncorrectedSEVol;
		// The current un-corrected volume of the music
		private float uncorrectedDayMusicVol = 1.4f;
		private float uncorrectedNightMusicVol = 0;
		// The master volume level of the SE
		private float masterSEVolumeLevel = 1.0f;
		// The master volume level of the music
		private float masterMusicVolumeLevel = 1.0f;
		// Whether or not the music is ducked
		private bool musicIsDucked = false;
		// 1 = normal, 2 = winter, 3 = christmas
		private int _currentThemeIndex = 1;
	
		#endregion
	
	#endregion
	
	
	#region Play Sounds
	
	// Plays the specified sound from the camera source
	// Called from a bunch of places
	public void PlaySound (string soundName)
	{
		// First we need to get the next available audio source
		AudioSource useSource = GetOpenSESource ();
		useSource.clip = GetClip (soundName);
		
		// Set the volume of the sound effect source
		useSource.volume *= masterSEVolumeLevel;
		
		// Clip has been loaded, we can play the sound now
		useSource.Play ();
	}
	
	
	// Plays the landing sound
	// Called from 
	public void PlayLandSound ()
	{
		platLandSource.pitch = Random.Range (0.85f, 1.15f);
		platLandSource.Play ();
	}
	
	
	// Plays a random "happy" sound when the player lands on a happy platform
	// Called from 
	public void PlayStarLandSound ()
	{
		int randnum = Random.Range (0, happySounds.Length);
		starPlatLandSource.clip = happySounds [randnum];
		starPlatLandSource.Play ();
	}
	
	
	// Plays a random "angry" sound when the player lands on an angry platform
	//
	public void PlayAngryLandSound ()
	{
		int randnum = Random.Range (0, angrySounds.Length);
		angryPlatLandSource.clip = angrySounds [randnum];
		angryPlatLandSource.Play ();
	}
	
	
	// Plays a random "sad" sound when the player lands on an angry platform
	//
	public void PlaySadLandSound ()
	{
		int randnum = Random.Range (0, sadSounds.Length);
		sadPlatLandSource.clip = sadSounds [randnum];
		sadPlatLandSource.Play ();
	}
	
	
	// Plays a random "dirty" sound when the player lands on an angry platform
	//
	public void PlayDirtyLandSound ()
	{
		int randnum = Random.Range (0, dirtySounds.Length);
		dirtyPlatLandSource.clip = dirtySounds [randnum];
		dirtyPlatLandSource.Play ();
	}
	
	
	//
	//
	public void PlayDrumroll ()
	{
		drumrollSource.Play ();
	}
	
	
	//
	//
	public void StopDrumroll ()
	{
		drumrollSource.Stop ();
	}
	
	
	// Plays the button pressed sound
	//
	public void PlayButtonPressSound ()
	{
		buttonSource.Play ();
	}

	
	// Sets whether the running score source is playing
	// Called from 
	public void SetScoreRunnerIsPlaying (bool b)
	{
		if (b)
			scoreRunnerSource.Play ();
		else
			scoreRunnerSource.Stop ();
	}
	
	#endregion
	
	
	#region Time Adjustment
	
	// Sets the daytime bool & begins transitioning music to/from night/day
	//
	public void SetIsDayTime (bool b)
	{
		isDayTime = b;
	}
	
	
	// Hard resets the music to day full volume without lerping
	//
	public void ResetToDay ()
	{
		isDayTime = true;
		uncorrectedDayMusicVol = dayMusicMaxVolume;
		uncorrectedNightMusicVol = 0f;
	}
	
	#endregion
	
	
	#region Update
	
	// Main logic loop
	// Called automatically every frame
	void Update ()
	{
		// Reset the music volume
		ResetMusicVolume ();
		
		// Reset SE volumes
		ResetSEVolume ();
	}
	
	
	// Resets the volume of the music
	// Called every frame from Update ()
	void ResetMusicVolume ()
	{
		// If it's currently day time in-game...
		if (isDayTime)
		{
			if (uncorrectedDayMusicVol < dayMusicMaxVolume) { uncorrectedDayMusicVol += musicFadeRate; }
			else { uncorrectedDayMusicVol = dayMusicMaxVolume; }
			
			if (uncorrectedNightMusicVol > 0.0f) { uncorrectedNightMusicVol -= musicFadeRate; }
			else { uncorrectedNightMusicVol = 0f; }
		}
		// If it's currently night time in-game...
		else
		{
			if (uncorrectedNightMusicVol < nightMusicMaxVolume) { uncorrectedNightMusicVol += musicFadeRate; }
			else { uncorrectedNightMusicVol = nightMusicMaxVolume; }
			
			if (uncorrectedDayMusicVol > 0.0f) { uncorrectedDayMusicVol -= musicFadeRate; } 	
			else { uncorrectedDayMusicVol = 0f; }
		}
		
		// Update music volume with corrections from master volume 
		if (musicIsDucked)
		{
			dayMusicSource.volume = (uncorrectedDayMusicVol * duckVolumePlayPercentage) * masterMusicVolumeLevel;
			nightMusicSource.volume = (uncorrectedNightMusicVol * duckVolumePlayPercentage) * masterMusicVolumeLevel;
		}
		else
		{
			dayMusicSource.volume = uncorrectedDayMusicVol * masterMusicVolumeLevel;
			nightMusicSource.volume = uncorrectedNightMusicVol * masterMusicVolumeLevel;
		}
	}
	
	
	// Resets the volume of the SE sources
	// Called every frame from Update ()
	void ResetSEVolume ()
	{
		// Reset the default sources
		foreach (AudioSource a in soundEffectSources)
		{
			a.volume = defaultSESourceVolume * masterSEVolumeLevel;
		}
		
		// Reset the other sources
		platLandSource.volume = defaultPlatLandSourceVolume * masterSEVolumeLevel;
		starPlatLandSource.volume = defaultHappyPlatLandSourceVolume * masterSEVolumeLevel;
		angryPlatLandSource.volume = defaultAngryPlatLandSourceVolume * masterSEVolumeLevel;
		sadPlatLandSource.volume = defaultSadPlatLandSourceVolume * masterSEVolumeLevel;
		dirtyPlatLandSource.volume = defaultDirtyPlatLandSourceVolume * masterSEVolumeLevel;
		scoreRunnerSource.volume = defaultRunningScoreSourceVolume * masterSEVolumeLevel;
		buttonSource.volume = defaultButtonSourceVolume * masterSEVolumeLevel;
		drumrollSource.volume = defaultDrumrollSourceVolume * masterSEVolumeLevel;
	}
	
	#endregion
	
	
	#region Utility
	
	
	// Gets the correct audio clip based on string ID passed in
	AudioClip GetClip (string soundName)
	{
		AudioClip clipp = null;
		switch (soundName)
		{
			case "Jump":
				clipp = jumpClip;
			break;
			case "Double Jump":
				clipp = doubleJumpClip;
			break;
			case "Land":
				clipp = landClip;
			break;
			case "Land Hard":
				clipp = landHardClip;
			break;
			case "GameOver":
				clipp = gameOverClip;
			break;
			case "Button":
				clipp = buttonClip;
			break;
			case "Cymbal":
				clipp = symbalClip;
			break;
			case "StarGold":
				clipp = starGoldGetClip;
			break;
			case "StarSilver":
				clipp = starSilverGetClip;
			break;
			case "StarBronze":
				clipp = starBronzeGetClip;
			break;
			case "StarGet":
				clipp = starCollectClip;
			break;
			case "MainMenuButton":
				clipp = mainMenuButtonClip;
			break;
			case "BeginningFall":
				clipp = beginningFallClip;
			break;
			case "Color Bar":
				clipp = colorBarChangeClip;
			break;
			case "CloudBounce":
				clipp = cloudBounceClip;
			break;
			case "MenuLoaded":
				clipp = menuLoadedClip;
			break;
			case "MultiplierUp":
				clipp = multiplierUpClip;
			break;
			case "ScoreMultiBlink":
				clipp = scoreMultiBlinkClip;
			break;
			case "ScoreRunnerDone":
				clipp = scoreAddingLoopDoneClip;
			break;
			case "ScoreRunnerDoneHigh":
				clipp = scoreAddingLoopDoneHighClip;
			break;
			case "ScoreRunnerStar":
				clipp = scoreAddingStarsValueClip;
			break;
			case "DirtyLand":
				clipp = dirtyLandClip;
			break;
			case "Options":
				clipp = optionsPressedClip;
			break;
			case "Tab":
				clipp = tabPressedClip;
			break;
			case "OptionsLand":
				clipp = optionsLandPressedClip;
			break;
			case "Dexter":
				clipp = dexterWhooClip;
			break;
			case "Whoosh":
				clipp = whooshClip;
			break;
			case "Applause":
				clipp = applauseClip;
			break;
			case "Shing":
				clipp = shingClip;
			break;
			case "CheatButton":
				clipp = cheatButtonClip;
			break;
			case "CheatFailed":
				clipp = cheatFailedClip;
			break;
			case "CheatSucceeded":
				clipp = cheatSucceededClip;
			break;
			case "AnimalBlink":
				clipp = animalBlinkClip;
			break;
			case "AnimalLeave":
				clipp = animalLeaveClip;
			break;
		}
		return clipp;
	}
	
	
	// Finds the next audio source that is not currently playing a sound
	AudioSource GetOpenSESource ()
	{
		AudioSource returnSource = soundEffectSources [0];
		for (int i = 0; i < soundEffectSources.Length; i++)
		{
			// If the audio source is NOT playing, it is available
			if (!soundEffectSources [i].isPlaying) {returnSource = soundEffectSources [i]; return returnSource;}
		}
		
		// If we get this far, there is no open source :(
		print ("No available audio source, overwriting 1st source...");
		return returnSource;
	}
	
	#endregion
	
	
	#region Set Volume

	// Sets the master sound effect volume level (0 - 1)
	// Called from AdjustSEVolume () in OptionsController.cs
	public void SetSEVolume (float f)
	{
		masterSEVolumeLevel = f;
	}
	
	
	//
	//
	public void SwitchVolume ()
	{
		if (masterSEVolumeLevel > 0.1f)
		{
			masterSEVolumeLevel = 0;
			masterMusicVolumeLevel = 0;	
			volumeOffImg.enabled = true;
		}
		else
		{
			masterSEVolumeLevel = 1;
			masterMusicVolumeLevel = 1;	
			volumeOffImg.enabled = false;
		}
	}
	
	
	// Sets the master music volume level (0 - 1)
	// Called from AdjustMusicVolume () in OptionsController.cs
	public void SetMusicVolume (float f)
	{
		masterMusicVolumeLevel = f;
	}
	
	
	// Ducks or unducks the music level (for the death screen)
	//
	public void DuckMusic (bool b)
	{
		musicIsDucked = b;
	}
	void UnDuckMusic ()
	{
		musicIsDucked = false;
	}
	
	#endregion


	#region Theme
	
	//
	public void ChangeCurrentTheme (int index)
	{
		_currentThemeIndex = index;
		switch (_currentThemeIndex)
		{
			case 1:
				dayMusicSource.clip = defaultDayMusicClip;
				dayMusicMaxVolume = 1.4f;
			break;
			case 2:
				dayMusicSource.clip = winterDayMusicClip;
				dayMusicMaxVolume = 0.4f;
			break;
			case 3:
				dayMusicSource.clip = holidayDayMusicClip;
				dayMusicMaxVolume = 0.92f;
			break;
		}

		//
		dayMusicSource.Stop ();
		nightMusicSource.Stop ();
		dayMusicSource.Play ();
		nightMusicSource.Play ();
	}
	
	#endregion
	
	
	#region Initialization
	
	// Used for initialization
	// Called automaticall at beginning
	void Start () 
	{
		// Get and assign audio sources
		ConfigureAudioSources ();
		
		// Assign the initial private/script/reference variables
		AssignVariables ();
		
		// Load the saved volume data from the data controller
		GetSavedVolumeData ();
	}
	
	
	// Finds and assigns the various audio sources
	// Called from Start ()
	private void ConfigureAudioSources ()
	{
		// Assign and populate sound effect array
		Transform sourcesParent = GameObject.Find ("*AudioSourcesSE").transform;
		dayMusicSource = GameObject.Find ("DayMusicSource").GetComponent <AudioSource> ();
		nightMusicSource = GameObject.Find ("NightMusicSource").GetComponent <AudioSource> ();
		platLandSource = GameObject.Find ("PlatLandSource").GetComponent <AudioSource> ();
		starPlatLandSource = GameObject.Find ("StarPlatLandSource").GetComponent <AudioSource> ();
		angryPlatLandSource = GameObject.Find ("AngryPlatLandSource").GetComponent <AudioSource> ();
		sadPlatLandSource = GameObject.Find ("SadPlatLandSource").GetComponent <AudioSource> ();
		dirtyPlatLandSource = GameObject.Find ("DirtyPlatLandSource").GetComponent <AudioSource> ();
		drumrollSource = GameObject.Find ("DrumrollSource").GetComponent <AudioSource> ();
		scoreRunnerSource = GameObject.Find ("ScoreAdvanceLoopSource").GetComponent <AudioSource> ();
		buttonSource = GameObject.Find ("ButtonSource").GetComponent <AudioSource> ();
		soundEffectSources = new AudioSource [sourcesParent.childCount];
		int index = 0;
		
		foreach (Transform child in sourcesParent)
		{
			soundEffectSources [index] = child.GetComponent <AudioSource> ();
			index ++;
		}
		
		// Begin playing the day and night music
		//dayMusicSource.Play ();
		//nightMusicSource.Play ();
	}
	
	
	// Assigns the initial private/script/reference variables
	// Called from Start ()
	private void AssignVariables ()
	{
		dataCont = gameObject.GetComponent <DataController> ();
	}
	
	
	// Loads the saved volume data from the data controller
	// Called from Start ()
	private void GetSavedVolumeData ()
	{
		SetSEVolume (dataCont.GetSEVolume ());
		SetMusicVolume (dataCont.GetMusicVolume ());
		
		// Set the music sources volume
		dayMusicSource.volume = uncorrectedDayMusicVol * masterMusicVolumeLevel;
		nightMusicSource.volume = uncorrectedNightMusicVol * masterMusicVolumeLevel;
	}
	
	#endregion
	
	
	#region EventManager
	
	// Subscribes to events
	// Called automatically when this script is enabled
	void OnEnable ()
	{
		EventManager.OnBackToMainMenuFromGame += ResetToDay;
		EventManager.OnBackToMainMenuFromGame += UnDuckMusic;
	}
	
	
	// Unsubscribes to events
	// Called automatically when this script is disabled
	void OnDisable ()
	{
		EventManager.OnBackToMainMenuFromGame -= ResetToDay;
		EventManager.OnBackToMainMenuFromGame -= UnDuckMusic;
	}
	
	#endregion
}