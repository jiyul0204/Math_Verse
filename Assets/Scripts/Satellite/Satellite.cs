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
        private RectTransform originRectTransform;
        private Quaternion scoreOriginRotation;
        #endregion

        private float moveSpeed;

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

        public void Init(Planet planet, RectTransform originRectTransform, float moveSpeed)
        {
            mainPlanet = planet;

            this.originRectTransform = originRectTransform;
            this.moveSpeed = moveSpeed;
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

            objectRectTransform.localPosition = originRectTransform.localPosition;
            revolutionCoroutine = StartCoroutine(RotateAroundMainPlanet());
        }

        private void StopRevolve()
        {
            if (revolutionCoroutine != null)
            {
                StopCoroutine(revolutionCoroutine);
            }
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
    }
}
