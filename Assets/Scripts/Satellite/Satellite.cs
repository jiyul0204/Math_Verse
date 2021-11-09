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
        Multiply,
        /*Divide,
        Modular,*/
        Max
    }

    public class Satellite : MonoBehaviour
    {
        #region GameObject
        private RectTransform objectRectTransform;
        private Button objectButton;
        #endregion

        #region Score
        [SerializeField]
        private GameObject scoreObject;
        private Text scoreText;
        private int scoreValue;
        private SatelliteSign scoreSign;
        #endregion

        #region GameObject Position
        private RectTransform originRectTransform;
        private Quaternion scoreOriginRotation;
        #endregion

        #region Coroutine
        private Coroutine revolutionCoroutine;
        #endregion

        private void Awake()
        {
            objectRectTransform = GetComponent<RectTransform>();
            objectButton = GetComponent<Button>();

            scoreText = GetComponentInChildren<Text>();

            scoreOriginRotation = scoreObject.transform.rotation;
        }

        private void Start()
        {
            SetSatelliteScore();
            BindView();
            StartRevolve();
        }

        private void OnDestroy()
        {
            StopRevolve();
        }

        public void Init(RectTransform originRectTransform)
        {
            this.originRectTransform = originRectTransform;
        }

        private void SetSatelliteScore()
        {
            scoreValue = Random.Range(0, 11);
            scoreSign = (SatelliteSign)Random.Range(0, (int)SatelliteSign.Max);

            char scoreSignText;

            switch (scoreSign)
            {
                case SatelliteSign.Minus:
                    scoreSignText = '-';
                    break;
                case SatelliteSign.Plus:
                    scoreSignText = '+';
                    break;
                case SatelliteSign.Multiply:
                    scoreSignText = '*';
                    break;
                /*case SatelliteSign.Divide:
                    scoreSignText = '/';
                    break;
                case SatelliteSign.Modular:
                    scoreSignText = '%';
                    break;*/
                default:
                    scoreSignText = '-';
                    break;
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
            if (gameObject == null)
            {
                return;
            }

            objectRectTransform.localPosition = originRectTransform.localPosition;
            revolutionCoroutine = StartCoroutine(RotateAroundMainPlanet());
        }

        private void StopRevolve()
        {
            if (revolutionCoroutine == null || gameObject == null)
            {
                return;
            }

            StopCoroutine(revolutionCoroutine);
        }

        private IEnumerator RotateAroundMainPlanet()
        {
            while (true)
            {
                objectRectTransform.localPosition = originRectTransform.localPosition;
                scoreObject.transform.rotation = scoreOriginRotation;

                yield return null;
            }
        }

        public int CalculateScore(int planetScore)
        {
            switch (scoreSign)
            {
                case SatelliteSign.Minus:
                    return planetScore - scoreValue;
                case SatelliteSign.Plus:
                    return planetScore + scoreValue;
                case SatelliteSign.Multiply:
                    return planetScore * scoreValue;
                /*case SatelliteSign.Divide:
                    return planetScore / scoreValue;
                case SatelliteSign.Modular:
                    return planetScore % scoreValue;*/
                default:
                    return planetScore - scoreValue;
            }
        }
    }
}
