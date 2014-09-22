////////////////////////////////////////////////////////////////////////////////
//  
// @module IOS Native Plugin for Unity3D 
// @author Osipov Stanislav (Stan's Assets) 
// @support stans.assets@gmail.com 
//
////////////////////////////////////////////////////////////////////////////////



using UnityEngine;
using System.Collections;

public class IOSStoreKitResponse  {

	public string productIdentifier;
	public string receipt;

	public string error = string.Empty;




	public bool isSuccess  {
		get {
			if(error.Length == 0) {
				return true;
			} else {
				return false;
			}
		}
	}
	
	public bool isFailure {
		get {
			return !isSuccess;
		}
	}

}
