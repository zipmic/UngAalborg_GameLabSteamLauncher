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
        Application.targetFrameRate = 60;
	}

	private void Start()
	{
     
        // ../ for at gå en mappe op og så ind i builds.
       path =  Application.dataPath + "/../Builds/";


        // Check alle directory under builds
        // Hvis der ikke er exe og billede i png så ignorer
        // string[] folders = System.IO.Directory.GetDirectories(@"C:\My Sample Path\","*", System.IO.SearchOption.AllDirectories);
        // tjek EXE navn
        //string[] allfiles = Directory.GetFiles("path/to/dir", "*.exe*", SearchOption.AllDirectories);
        // Hent png
        // logo.png
        // https://gyanendushekhar.com/2017/07/08/load-image-runtime-unity/
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
