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
            yield return null;
        }


        image.texture = videoPlayer.texture;

        videoPlayer.Play();

        audioSource.Play();

        while (videoPlayer.isPlaying)
        {

            if(Input.GetKeyDown(KeyCode.Space) && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Intro"))
            {
                SceneManager.LoadScene("Level1");

            }
            yield return null;
        }

        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Intro"))
        {
            image.color = (new Color(0,0,0,1));
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene("Level1");
        }
        else
        {
            image.color = (new Color(0, 0, 0, 1));
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene("Title");
        }

    }
}
