using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UniRx;

namespace SatelliteGame
{
    public class SatelliteService : MonoBehaviour
    {
        [SerializeField]
        private GameObject pausePanel;

        [SerializeField]
        private Button pauseButton;

        [SerializeField]
        private Button homeButton;

        private int questionCount = 1;
        private const int maxQuestionCount = 5;
        private string stg_cd;

        [SerializeField]
        private Text missionCountText;

        [SerializeField]
        private Text missionContextText;

        [SerializeField]
        private GameObject restartPopupPanel;

        [SerializeField]
        private GameObject successPopupPanel;

        [SerializeField]
        private GameObject HowToPlayPopupPanel;

        [SerializeField]
        private Button DelHowToPlayPopupPanel;

        [SerializeField]
        private Button HowToPlay;

        [SerializeField]
        private Button HowToPlay2;

        [SerializeField]
        private Planet mainPlanet;
        private Image mainPlanetImage;

        [SerializeField]
        private List<Sprite> planetSprites;

        private void Start()
        {
            AudioManager.Inst.PlayBGM(SoundType.division_bgm.ToString());
            AudioManager.Inst.PlaySFX(SoundType.division_enter.ToString());

            mainPlanetImage = mainPlanet.GetComponentInChildren<Image>();

            BindView();

            GenerateQuestion();
        }

        private void BindView()
        {
            pauseButton.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    pausePanel.SetActive(true);
                })
                .AddTo(gameObject);

            homeButton.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    SceneService.Instance.LoadScene(SceneName.Lobby);
                })
                .AddTo(gameObject);

            DelHowToPlayPopupPanel.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    HowToPlayPopupPanel.SetActive(!HowToPlayPopupPanel.activeSelf);
                })
                .AddTo(gameObject);

            HowToPlay.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    HowToPlayPopupPanel.SetActive(!HowToPlayPopupPanel.activeSelf);
                })
                .AddTo(gameObject);

            HowToPlay2.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    HowToPlayPopupPanel.SetActive(!HowToPlayPopupPanel.activeSelf);
                })
                .AddTo(gameObject);
        }

        private void GenerateQuestion()
        {
            missionCountText.text = $"?????? ({questionCount} / {maxQuestionCount})";

            DBQuestDivisionData data = LocalDBDataService.Instance.GetRandomQuestDivisionData();

            stg_cd = data.stg_cd;

            switch (stg_cd)
            {
                case "F02_0_1":
                    missionContextText.text = $"{data.qst_cn1_1} {data.math_smb1_1} {data.qst_cn1_2} {data.math_smb1_2} {data.qst_cn1_3} ??? {data.qst_cn2_1} {data.math_smb2_1} {data.qst_cn2_2} {data.math_smb2_2} {data.qst_cn2_3}";
                    mainPlanetImage.sprite = planetSprites[0];
                    break;
                case "F03_0_1":
                    missionContextText.text = $"{data.qst_cn1_1} {data.math_smb1_1} {data.qst_cn2_3} {data.math_smb1_2} {data.qst_cn1_3} ??? {data.qst_cn2_1} {data.math_smb2_1} {data.qst_cn2_2} {data.math_smb2_2} {data.qst_cn2_3}";
                    mainPlanetImage.sprite = planetSprites[1];
                    break;
                case "F03_0_2":
                    missionContextText.text = $"{data.qst_cn2_1} {data.math_smb2_1} {data.qst_cn2_2} {data.math_smb2_2} {data.qst_cn2_3}";
                    mainPlanetImage.sprite = planetSprites[2];
                    break;
            }

            mainPlanet.SetPlanetScore(data.qst_cn2_1, data.boxed_cransr);
        }

        public void ShowGameResult(bool isClear)
        {
            if (isClear)
            {
                if (questionCount++ == maxQuestionCount)
                {
                    switch (stg_cd)
                    {
                        case "F02_0_1":
                            PlayerInfoService.Instance.SaveData(CardCollectionCard.CardPlanetA, true);
                            break;
                        case "F03_0_1":
                            PlayerInfoService.Instance.SaveData(CardCollectionCard.CardPlanetB, true);
                            break;
                        case "F03_0_2":
                            PlayerInfoService.Instance.SaveData(CardCollectionCard.CardPlanetC, true);
                            break;
                    }

                    successPopupPanel.SetActive(true);
                }
                else
                {
                    GenerateQuestion();
                }
            }
            else
            {
                restartPopupPanel.SetActive(true);
            }
        }
    }
}