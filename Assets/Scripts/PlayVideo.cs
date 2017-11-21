using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PlayVideo : MonoBehaviour
{

    // Use this for initialization
    public RawImage image;
    //Video To Play [Assign from the Editor]
    [SerializeField]
    public VideoClip videoToPlay;

    private VideoPlayer videoPlayer;
    private VideoSource videoSource;

    //Audio
    private AudioSource audioSource;
    void Start()
    {
        Application.runInBackground = true;
        StartCoroutine(playVideo());
    }

    IEnumerator playVideo()
    {
        videoPlayer = gameObject.AddComponent<VideoPlayer>();

        audioSource = gameObject.AddComponent<AudioSource>();

        videoPlayer.playOnAwake = false;
        audioSource.playOnAwake = false;

        videoPlayer.source = VideoSource.VideoClip;

        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);

        videoPlayer.clip = videoToPlay;
        videoPlayer.Prepare();

        while (!videoPlayer.isPrepared)
        {
            Debug.Log("Preparing Video");
            yield return null;
        }

        Debug.Log("Done Preparing Video");

        image.texture = videoPlayer.texture;

        videoPlayer.Play();

        audioSource.Play();

        Debug.Log("Playing Video");
        while (videoPlayer.isPlaying)
        {
            Debug.LogWarning("Video Time: " + Mathf.FloorToInt((float)videoPlayer.time));
            yield return null;
        }

        SceneManager.LoadScene("Title");

        Debug.Log("Done Playing Video");
    }
}
