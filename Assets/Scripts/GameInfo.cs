using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
	public string folderName, EXEname;
	private LauncherScript _launcherScript;

	private void Start()
	{
		_launcherScript = GameObject.Find("Launcher").GetComponent<LauncherScript>();
	}

	public void StartGame()
	{
		_launcherScript.StartProcess(folderName, EXEname);
	}
}
