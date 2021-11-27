using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPopup : MonoBehaviour
{
    bool IsPause;
    // Start is called before the first frame update
    void Start()
    {
        IsPause = false;
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = (IsPause == true) ? (1) : (0);
        IsPause = !IsPause;

        return;
    }

    public void GameOver()
    {
        //dragndrop.PopUp.gameObject.GetComponent<Text>().text = "게임 오버";
    }

    void Game_pause()
    {
        //dragndrop.PopUp.gameObject.GetComponent<Text>().text = "일시 정지";
    }



}
