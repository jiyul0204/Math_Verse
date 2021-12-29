using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImg : MonoBehaviour
{
    public Image SprOriginImage; //기존에 존제하는 이미지
    public Sprite SprChangeImage; //바뀌어질 이미지

    public void ChangeImage()
    {
        SprOriginImage.sprite = SprChangeImage;
    }
 
}
