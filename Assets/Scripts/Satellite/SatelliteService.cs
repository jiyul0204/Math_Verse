using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SatelliteGame
{
    public class SatelliteService : Singleton<SatelliteService>
    {
        private int questionCount = 1;
        private const int maxQuestionCount = 5;

        [SerializeField]
        private Text missionCountText;

        [SerializeField]
        private Text missionContextText;

        [SerializeField]
        private GameObject resultPopupPanel;

        [SerializeField]
        private Planet mainPlanet;
        private Image mainPlanetImage;

        [SerializeField]
        private List<Sprite> planetSprites;

        private void Start()
        {
            mainPlanetImage = mainPlanet.GetComponentInChildren<Image>();

            GenerateQuestion();
        }

        private void GenerateQuestion()
        {
            missionCountText.text = $"미션 ({questionCount} / {maxQuestionCount})";

            DBQuestDivisionData data = LocalDBDataService.Instance.GetRandomQuestDivisionData();

            switch (data.stg_cd)
            {
                case "F02_0_1":
                    missionContextText.text = $"{data.qst_cn1_1} {data.math_smb1_1} {data.qst_cn1_2} {data.math_smb1_2} {data.qst_cn1_3} ↔ {data.qst_cn2_1} {data.math_smb2_1} {data.qst_cn2_2} {data.math_smb2_2} {data.qst_cn2_3}";
                    mainPlanetImage.sprite = planetSprites[0];
                    break;
                case "F03_0_1":
                    missionContextText.text = $"{data.qst_cn1_1} {data.math_smb1_1} {data.qst_cn2_3} {data.math_smb1_2} {data.qst_cn1_3} ↔ {data.qst_cn2_1} {data.math_smb2_1} {data.qst_cn2_2} {data.math_smb2_2} {data.qst_cn2_3}";
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
                    resultPopupPanel.SetActive(true);
                }
                else
                {
                    GenerateQuestion();
                }
            }
            else
            {
                resultPopupPanel.SetActive(true);
            }
        }
    }
}