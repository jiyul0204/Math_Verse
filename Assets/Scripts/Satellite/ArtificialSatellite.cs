using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using UniRx;
using UniRx.Triggers;

namespace SatelliteGame
{
    public class ArtificialSatellite : MonoBehaviour
    {
        #region GameObject
        private RectTransform objectRectTransform;
        private Button objectButton;
        #endregion

        #region CenterNumber
        [SerializeField]
        private GameObject centerNumberObject;
        private Text centerNumberText;
        private int centerNumberValue;
        private SatelliteSign centerNumberSign;
        #endregion

        #region GameObject Position
        private Vector3 originPosition;
        private Quaternion centerNumberOriginRotation;
        #endregion

        private void Awake()
        {
            objectRectTransform = GetComponent<RectTransform>();
            objectButton = GetComponent<Button>();

            originPosition = objectRectTransform.localPosition;

            centerNumberText = GetComponentInChildren<Text>();
            centerNumberOriginRotation = centerNumberObject.transform.rotation;
        }

        private void Start()
        {
            SetSatelliteScore();
            BindView();

            StartCoroutine(RotateItSelf());
        }

        private void SetSatelliteScore()
        {
            centerNumberValue = Random.Range(1, 11);
            centerNumberSign = (SatelliteSign)Random.Range(0, (int)SatelliteSign.Max);

            char centerNumberSignText;

            switch (centerNumberSign)
            {
                case SatelliteSign.Minus:
                    centerNumberSignText = '-';
                    break;
                /*case SatelliteSign.Plus:
                    centerNumberSignText = '+';
                    break;
                case SatelliteSign.Multiply:
                    centerNumberSignText = '*';
                    break;
                case SatelliteSign.Divide:
                    scoreSignText = '/';
                    break;
                case SatelliteSign.Modular:
                    scoreSignText = '%';
                    break;*/
                default:
                    centerNumberSignText = '-';
                    break;
            }

            centerNumberText.text = $"{centerNumberSignText}{centerNumberValue}";
        }

        private void BindView()
        {
            objectButton.OnPointerUpAsObservable()
                .Subscribe(_ =>
                {
                    ResetPosition();
                })
                .AddTo(gameObject);
        }

        private void ResetPosition()
        {
            objectRectTransform.localPosition = originPosition;
        }

        private IEnumerator RotateItSelf()
        {
            var rotateVector = new Vector3(0f, 0f, 0.3f);

            while (true)
            {
                transform.Rotate(rotateVector);
                centerNumberObject.transform.rotation = centerNumberOriginRotation;

                yield return null;
            }
        }

        public int CalculateScore(int sourceCenterNumber)
        {
            switch (centerNumberSign)
            {
                case SatelliteSign.Minus:
                    return sourceCenterNumber - centerNumberValue;
                /*case SatelliteSign.Plus:
                    return sourceCenterNumber + centerNumberValue;
                case SatelliteSign.Multiply:
                    return sourceCenterNumber * centerNumberValue;
                case SatelliteSign.Divide:
                    return planetScore / scoreValue;
                case SatelliteSign.Modular:
                    return planetScore % scoreValue;*/
                default:
                    return sourceCenterNumber - centerNumberValue;
            }
        }
    }
}