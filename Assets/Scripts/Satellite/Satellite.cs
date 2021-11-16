using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/*using UniRx;
using UniRx.Triggers;*/

namespace SatelliteGame
{
    enum SatelliteSign
    {
        Minus,
        /*Plus,        
        Multiply,
        Divide,
        Modular,*/
        Max
    }

    public class Satellite : MonoBehaviour
    {
        #region GameObject
        private Planet mainPlanet;

        private RectTransform objectRectTransform;
        private Button objectButton;
        #endregion

        #region CenterNumber
        [SerializeField]
        private GameObject centerNumberObject;
        private Text centerNumberText;
        private int centerNumberValue;
        /*private SatelliteSign centerNumberSign;*/
        #endregion

        #region GameObject Position
        private RectTransform originRectTransform;
        private Quaternion centerNumberOriginRotation;
        #endregion

        #region Coroutine
        private Coroutine revolutionCoroutine;
        #endregion

        private void Awake()
        {
            objectRectTransform = GetComponent<RectTransform>();
            objectButton = GetComponent<Button>();

            centerNumberText = GetComponentInChildren<Text>();
            centerNumberOriginRotation = centerNumberObject.transform.rotation;
        }

        private void Start()
        {
            SetSatelliteScore();
            /*BindView();*/
            StartRevolve();
        }

        private void OnDestroy()
        {
            StopRevolve();
        }

        public void Init(Planet mainPlanet, RectTransform originRectTransform)
        {
            this.mainPlanet = mainPlanet;
            this.originRectTransform = originRectTransform;
        }

        private void SetSatelliteScore()
        {
            centerNumberValue = Random.Range(1, 31);
            /*centerNumberSign = (SatelliteSign)Random.Range(0, (int)SatelliteSign.Max);

            char centerNumberSignText;

            switch (centerNumberSign)
            {
                case SatelliteSign.Minus:
                    centerNumberSignText = '-';
                    break;
                case SatelliteSign.Plus:
                    centerNumberSignText = '+';
                    break;
                case SatelliteSign.Multiply:
                    centerNumberSignText = '*';
                    break;
                *//*case SatelliteSign.Divide:
                    scoreSignText = '/';
                    break;
                case SatelliteSign.Modular:
                    scoreSignText = '%';
                    break;*//*
                default:
                    centerNumberSignText = '-';
                    break;
            }*/

            centerNumberText.text = $"{centerNumberValue}";
        }

        /*private void BindView()
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
        }*/

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
                objectRectTransform.SetPositionAndRotation(originRectTransform.position, originRectTransform.rotation);
                centerNumberObject.transform.rotation = centerNumberOriginRotation;

                yield return null;
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            var targetArtificialSatellite = collider.GetComponent<ArtificialSatellite>();

            if (mainPlanet.RemoveArtificialSatellite(targetArtificialSatellite))
            {
                centerNumberValue = targetArtificialSatellite.CalculateScore(centerNumberValue);
                centerNumberText.text = $"{centerNumberValue}";

                if (centerNumberValue <= 0)
                {
                    mainPlanet.RemoveSatellite(this);
                }
            }
        }
    }
}
