using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ChangeImg : MonoBehaviour
{
    public Image SprOriginImage; //기존에 존재하는 이미지
    public Sprite SprOriginSprite; //기존에 존재하는 이미지
    public Sprite SprChangeSprite; //바뀌어질 이미지

    private void Start()
    {
        if(GetHour()>=23 || GetHour()<=6)
        {
            ChangeImage(SprChangeSprite);
        }
        else
        {
            ChangeImage(SprOriginSprite);
        }
    }
    public void ChangeImage(Sprite img)
    {
        SprOriginImage.sprite = img;
    }
 
    int GetHour()
    {
        return Int32.Parse(DateTime.Now.ToString("HH"));
    }
}
