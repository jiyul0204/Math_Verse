using UnityEngine;
using UnityEngine.UI;

using UniRx;

public class PanelSound : MonoBehaviour
{
    [SerializeField]
    private Sprite turnOnSprite;

    [SerializeField]
    private Sprite turnOffSprite;

    [SerializeField]
    private Button bgmButton;

    [SerializeField]
    private Text bgmText;

    [SerializeField]
    private Button sfxButton;

    [SerializeField]
    private Text sfxText;

    [SerializeField]
    private Button exitButton;

    private void Start()
    {
        if (AudioManager.Inst.IsMusicOn)
        {
            bgmButton.image.sprite = turnOnSprite;
            bgmText.text = "켬";
        }
        else
        {
            bgmButton.image.sprite = turnOffSprite;
            bgmText.text = "끔";
        }

        if (AudioManager.Inst.IsSoundOn)
        {
            sfxButton.image.sprite = turnOnSprite;
            sfxText.text = "켬";
        }
        else
        {
            sfxButton.image.sprite = turnOffSprite;
            sfxText.text = "끔";
        }

        BindView();
    }

    private void BindView()
    {
        bgmButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                SetBGMStatus();
            })
            .AddTo(gameObject);

        sfxButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                SetSFXStatus();
            })
            .AddTo(gameObject);

        exitButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                gameObject.SetActive(false);
            })
            .AddTo(gameObject);
    }

    private void SetBGMStatus()
    {
        if (AudioManager.Inst.IsMusicOn)
        {
            AudioManager.Inst.IsMusicOn = false;
            AudioManager.Inst.SaveBGMPreferences();

            bgmButton.image.sprite = turnOffSprite;
            bgmText.text = "끔";
        }
        else
        {
            AudioManager.Inst.IsMusicOn = true;
            AudioManager.Inst.SaveBGMPreferences();

            bgmButton.image.sprite = turnOnSprite;
            bgmText.text = "켬";
        }
    }

    private void SetSFXStatus()
    {
        if (AudioManager.Inst.IsSoundOn)
        {
            AudioManager.Inst.IsSoundOn = false;
            AudioManager.Inst.SaveSFXPreferences();

            sfxButton.image.sprite = turnOffSprite;
            sfxText.text = "끔";
        }
        else
        {
            AudioManager.Inst.IsSoundOn = true;
            AudioManager.Inst.SaveSFXPreferences();

            sfxButton.image.sprite = turnOnSprite;
            sfxText.text = "켬";
        }
    }
}
