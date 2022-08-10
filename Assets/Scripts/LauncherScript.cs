using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LauncherScript : MonoBehaviour
{
    private Process _process;
    private string path;
    private List<string> _buildFolders = new List<string>();
    public AudioSource _music;

    public GameObject _contentParent;
    public GameObject PrefabGameButton;

    public GameObject _OverlayWhenIngame;
    

	private void Awake()
	{
        Application.runInBackground = false;
        Application.targetFrameRate = 100;
	}

	void Start()
	{
     
        // ../ for at gå en mappe op og så ind i builds.
       path =  Application.dataPath + "/../Builds/";


        // Check alle directory under builds
        // Hvis der ikke er exe og billede i png så ignorer
        foreach (string s in System.IO.Directory.GetDirectories(path, "*", System.IO.SearchOption.TopDirectoryOnly))
        {
            DirectoryInfo dir = new DirectoryInfo(s);
            string dirName = dir.Name;
            _buildFolders.Add(dirName);
         
        }

        for (int i = 0; i < _buildFolders.Count; i++)
        {
            string gamename = _buildFolders[i];
            GameObject spawn = Instantiate(PrefabGameButton) as GameObject;
            spawn.transform.SetParent(_contentParent.transform);
            spawn.name = gamename;
            byte[] byteArray = File.ReadAllBytes(path + gamename + "/logo.png");
            Texture2D logo = new Texture2D(2, 2);
            logo.LoadImage(byteArray);
            Sprite logoSprite = Sprite.Create(logo, new Rect(0, 0, logo.width, logo.height), new Vector2(0.5f, 0.5f));
            spawn.GetComponent<Image>().sprite = logoSprite;
            spawn.transform.localScale = Vector3.one;
            string[] exefile = System.IO.Directory.GetFiles(path + gamename, "*.exe", SearchOption.TopDirectoryOnly);
            DirectoryInfo DI = new DirectoryInfo(exefile[0]);
            spawn.GetComponent<Button>().onClick.AddListener(() => StartProcess(gamename, DI.Name));
        }

        // tjek EXE navn
        //string[] allfiles = Directory.GetFiles("path/to/dir", "*.exe*", SearchOption.AllDirectories);
        // Hent png
        // logo.png
        // https://gyanendushekhar.com/2017/07/08/load-image-runtime-unity/
    }

    public void StartProcess(string FolderName, string GameNameWithExe)
    {
        print(path + FolderName + "/" + GameNameWithExe);
        _process = Process.Start(path + FolderName + "/" + GameNameWithExe);

    }

	private void Update()
	{
        if (_process != null)
        {
            if (!_OverlayWhenIngame.activeSelf)
            {
                _OverlayWhenIngame.SetActive(true);
                _music.mute = true;
                Time.timeScale = 0;
            }
            if (_process.HasExited)
            {
                _OverlayWhenIngame.SetActive(false);
                _process = null;
                _music.mute = false;
                Time.timeScale = 1;
            }
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
	}
}
