using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using UniRx;
using UniRx.Triggers;

namespace SatelliteGame
{
    enum SatelliteSign
    {
        Divide,
        Max
    }

    public class Satellite : MonoBehaviour
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

        public void SetSatelliteScore(int score)
        {
            centerNumberValue = score;
            centerNumberSign = (SatelliteSign)Random.Range(0, (int)SatelliteSign.Max);

            centerNumberText.text = $"{centerNumberValue}";
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

        public void StartRevolve()
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

        public bool CompareNumber(int sourceCenterNumber)
        {
            return sourceCenterNumber == centerNumberValue;
        }
    }
}