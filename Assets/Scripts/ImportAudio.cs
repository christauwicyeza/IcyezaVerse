using System.Collections;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class ImportAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public TMP_Dropdown fileDropdown;
    private string audioFolder = "/sdcard/Music/";
    private Dictionary<string, string> audioFilePaths = new Dictionary<string, string>();

    void Start()
    {
        LoadAudioFiles();
        fileDropdown.onValueChanged.AddListener(delegate { PlaySelectedFile(); });
    }

    void LoadAudioFiles()
    {
        if (Directory.Exists(audioFolder))
        {
            string[] audioFiles = Directory.GetFiles(audioFolder, "*.mp3").ToArray();

            if (audioFiles.Length > 0)
            {
                fileDropdown.ClearOptions();
                List<string> fileNames = audioFiles.Select(Path.GetFileName).ToList();
                fileDropdown.AddOptions(fileNames);

                for (int i = 0; i < fileNames.Count; i++)
                {
                    audioFilePaths[fileNames[i]] = audioFiles[i];
                }
            }
            else
            {
                fileDropdown.ClearOptions();
                fileDropdown.AddOptions(new List<string> { "No audio found" });
            }
        }
    }

    void PlaySelectedFile()
    {
        string selectedFile = fileDropdown.options[fileDropdown.value].text;
        
        if (audioFilePaths.ContainsKey(selectedFile))
        {
            StartCoroutine(PlayAudio(audioFilePaths[selectedFile]));
        }
    }

    private IEnumerator PlayAudio(string path)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + path, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                audioSource.clip = DownloadHandlerAudioClip.GetContent(www);
                audioSource.Play();
            }
        }
    }
}
