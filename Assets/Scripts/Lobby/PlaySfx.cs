using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySfx : MonoBehaviour
{
    GameObject SfxSound;
    AudioSource SfxMusic;
    public void BackGroundMusicOffButton() //배경음악 키고 끄는 버튼
    {
        SfxSound = GameObject.Find("BackgroundMusic");
        SfxMusic = SfxSound.GetComponent<AudioSource>(); //배경음악 저장해둠
        if (SfxMusic.isPlaying) SfxMusic.Pause();
        else SfxMusic.Play();
    }
}
