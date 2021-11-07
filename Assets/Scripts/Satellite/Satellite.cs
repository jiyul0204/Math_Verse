using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using UniRx;
using UniRx.Triggers;

namespace SatelliteGame
{
    enum SatelliteSign
    {
        Plus,
        Minus,
        /*Multiply,
        Divide,
        Modular,*/
        Max
    }

    public class Satellite : MonoBehaviour
    {
        #region Planet GameObject
        [SerializeField]
        private GameObject planetObject;
        private RectTransform mainPlanetRectTransform;
        private Planet mainPlanet;

        private RectTransform objectRectTransform;
        private Button objectButton;
        #endregion

        #region Satellite Score
        [SerializeField]
        private GameObject scoreObject;
        private Text scoreText;
        private int scoreValue;
        private SatelliteSign scoreSign;
        #endregion

        #region GameObject Position
        private Vector3 originPosition;
        private Quaternion scoreOriginRotation;
        #endregion

        #region Coroutine
        private Coroutine revolutionCoroutine;
        #endregion

        private void Awake()
        {
            mainPlanetRectTransform = planetObject.GetComponent<RectTransform>();
            mainPlanet = planetObject.GetComponent<Planet>();

            objectRectTransform = GetComponent<RectTransform>();
            objectButton = GetComponent<Button>();

            scoreText = GetComponentInChildren<Text>();

            originPosition = GetComponent<RectTransform>().localPosition;
            scoreOriginRotation = scoreObject.transform.rotation;
        }

        private void Start()
        {
            SetSatelliteScore();
            BindView();
            StartRevolve();
        }

        private void SetSatelliteScore()
        {
            scoreValue = Random.Range(0, 11);
            scoreSign = (SatelliteSign)Random.Range(0, (int)SatelliteSign.Max);

            char scoreSignText = '+';

            switch (scoreSign)
            {
                case SatelliteSign.Plus:
                    // '+'의 경우 이미 초기화 단계에서 설정했으므로
                    // 아무 것도 안하고 지나감. - Hyeonwoo, 2021.11.07.
                    break;
                case SatelliteSign.Minus:
                    scoreSignText = '-';
                    break;
                /*case SatelliteSign.Multiply:
                    scoreSignText = '*';
                    break;
                case SatelliteSign.Divide:
                    scoreSignText = '/';
                    break;
                case SatelliteSign.Modular:
                    scoreSignText = '%';
                    break;*/
            }

            scoreText.text = $"{scoreSignText}{scoreValue}";
        }

        private void BindView()
        {
            objectButton.OnPointerDownAsObservable()
                .Subscribe(_ =>
                {
                    StopRevolve();
                })
                .AddTo(gameObject);

            objectButton.OnPointerUpAsObservable()
                .Subscribe(_ =>
                {
                    StartRevolve();
                })
                .AddTo(gameObject);
        }

        private void StartRevolve()
        {
            /*if (mainPlanet.CalculateScore())
            {
                Destroy(gameObject);
                return;
            }*/

            objectRectTransform.localPosition = originPosition;
            revolutionCoroutine = StartCoroutine(RotateAroundMainPlanet());
        }

        private void StopRevolve()
        {
            if (revolutionCoroutine != null)
            {
                StopCoroutine(revolutionCoroutine);
                originPosition = GetComponent<RectTransform>().localPosition;
            }
        }

        private IEnumerator RotateAroundMainPlanet()
        {
            while (true)
            {
                objectRectTransform.RotateAround(mainPlanetRectTransform.position, -transform.forward, 30f * Time.deltaTime);
                scoreObject.transform.rotation = scoreOriginRotation;

                yield return null;
            }
        }
    }
}
