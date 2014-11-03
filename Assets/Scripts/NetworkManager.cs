


using UnityEngine;
using System.Collections;


public class NetworkManager : MonoBehaviour 
{
	#region Variables

	private const string typeName = "UniqueGameName";
	private const string gameName = "RoomName";

	#endregion


	//
	//
	private void StartServer ()
	{
		Network.InitializeServer (4, 25000, !Network.HavePublicAddress ());
		MasterServer.RegisterHost (typeName, gameName);
	}


	//
	//
	void OnServerInitialized ()
	{
		Debug.Log ("Server Initializied");
	}


	#region Button Response

	//
	//
	public void StartMultiplayerButtonPressed ()
	{
		//
		if (!Network.isClient && !Network.isServer)
		{
			StartServer ();
		}
	}

	#endregion
}
