using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LauncherScript : MonoBehaviour
{
    private Process _process;
    private string path;

	private void Awake()
	{
        Application.runInBackground = false;
        Application.targetFrameRate = 100;
	}

	private void Start()
	{
       path =  Application.dataPath + "/../Builds/";
    }

	public void StartProcess(string FolderName, string ExeName)
    {
        print(path + FolderName + "/" + ExeName + ".exe");
        _process = Process.Start(path + FolderName + "/" + ExeName + ".exe");

    }

	private void Update()
	{
        if (_process != null)
        {
            if (_process.HasExited)
            {
                _process = null;
            }
        }
	}
}
