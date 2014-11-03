


using UnityEngine;
using System.Collections;


public class NetworkManager : MonoBehaviour 
{
	#region Variables

	private const string typeName = "UniqueGameName";
	private const string gameName = "RoomName";
	private HostData [] hostList;

	#endregion


	#region Networking

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


	//
	//
	private void RefreshHostList ()
	{
		MasterServer.RequestHostList (typeName);
	}


	//
	//
	void OnMasterServerEvent (MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList ();
	}


	//
	//
	private void JoinServer (HostData hostData)
	{
		Network.Connect (hostData);
	}


	//
	//
	void OnConnectedToServer ()
	{
		Debug.Log ("Server Joined");
	}

	#endregion


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
