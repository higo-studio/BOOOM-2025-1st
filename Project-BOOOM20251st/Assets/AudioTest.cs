using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System.Threading.Tasks;

public class AudioTest : MonoBehaviour
{
    public List<AudioClip> audioList = new List<AudioClip>();
    private AudioSource audioSource;

    async void Start()
    {
        // 获取 AudioSource 组件
        audioSource = GetComponent<AudioSource>();

        // 收集 Assets/Audio/Music 文件夹下所有的 MP3 音频文件
        string audioFolderPath = Application.dataPath + "/Audio/Music";
        // 修改文件过滤条件为 *.mp3
        string[] audioFiles = Directory.GetFiles(audioFolderPath, "*.mp3");
        foreach (string filePath in audioFiles)
        {
            AudioClip audioClip = await LoadAudioClip(filePath);
            if (audioClip != null)
            {
                audioList.Add(audioClip);
            }
        }

        // 随机播放一个 AudioClip
        if (audioList.Count > 0)
        {
            int randomIndex = Random.Range(0, audioList.Count);
            AudioClip randomClip = audioList[randomIndex];
            audioSource.clip = randomClip;
            audioSource.Play();
            Debug.Log("正在播放的音频文件: " + randomClip.name);
        }
        else
        {
            Debug.Log("未找到可用的音频文件。");
        }
    }

    private async Task<AudioClip> LoadAudioClip(string filePath)
    {
        // 修改 AudioType 为 MPEG 以支持 MP3 格式
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip($"file://{filePath}", AudioType.MPEG))
        {
            var asyncOperation = www.SendWebRequest();
            while (!asyncOperation.isDone)
            {
                await Task.Yield();
            }

            if (www.result == UnityWebRequest.Result.Success)
            {
                return DownloadHandlerAudioClip.GetContent(www);
            }
            else
            {
                Debug.LogError($"加载音频文件失败: {filePath}, 错误信息: {www.error}");
                return null;
            }
        }
    }
}
