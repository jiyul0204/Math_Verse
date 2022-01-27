using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayMusic : MonoBehaviour
{
    public AudioSource backmusic;
    public AudioClip BackgroundMusic;

    public Sprite newsprite;
    public Sprite oidsprite;
    Button BgmBtn;
    public Text BgmText;

    void Awake()
    {
        // backmusic = GetComponent<AudioSource>(); //배경음악 저장해둠
        if (backmusic.isPlaying) return; //배경음악이 재생되고 있다면 패스
        else
        {
            backmusic.Play();
            DontDestroyOnLoad(BackgroundMusic); //배경음악 계속 재생하게(이후 버튼매니저에서 조작)
        }
    }

    public static void PlaySound(AudioClip Clip, AudioSource AudioSource)
    {
        AudioSource.Stop();
        AudioSource.clip = Clip;
        AudioSource.loop = true;
        AudioSource.time = 0;
        AudioSource.Play();
    }
    void Start()
    {
        BgmBtn = GetComponent<Button>();
        PlaySound(BackgroundMusic, backmusic);
    }

    public void BackGroundMusicOffButton() //배경음악 키고 끄는 버튼
    {
        // backmusic = GetComponent<AudioSource>(); //배경음악 저장해둠
        if (backmusic.isPlaying)
        {
            backmusic.Pause();
            BgmBtn.image.overrideSprite = newsprite;
            BgmText.GetComponent<Text>().text = "끔";
        }
        else
        {
            BgmBtn.image.overrideSprite = oidsprite;
            BgmText.GetComponent<Text>().text = "켬";
            backmusic.Play();
        }
    }
}
