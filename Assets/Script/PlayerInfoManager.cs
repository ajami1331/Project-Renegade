using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PlayerInfoManager : MonoBehaviour {

	public PlayerInfo playerInfo;
	public static PlayerInfoManager infoManager;



	// Use this for initialization
	void Start () {
		if (infoManager == null) {
			DontDestroyOnLoad (gameObject);
			infoManager = this;
		}
		else if (infoManager != this) {
			Destroy ( gameObject );
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Save() {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Open ( Application.persistentDataPath + "/playerInfo.sav", FileMode.Open );
		bf.Serialize ( file, playerInfo);
		file.Close ();
	}

	public void Load() {
		if (File.Exists (Application.persistentDataPath + "/playerInfo.sav")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open ( Application.persistentDataPath + "/playerInfo.sav", FileMode.Open );
			playerInfo = (PlayerInfo)bf.Deserialize ( file );
		}
	}
}
