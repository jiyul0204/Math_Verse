using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using UniRx;
using UniRx.Triggers;

namespace SatelliteGame
{
    public class Satellite : MonoBehaviour
    {
        [SerializeField]
        private GameObject mainPlanet;
        private RectTransform mainPlanetRectTransform;

        [SerializeField]
        private GameObject satelliteScore;

        private Quaternion satelliteScoreOriginRotation;

        private RectTransform satelliteRectTransform;
        private Button satelliteButton;

        private Coroutine revolutionCoroutine;
        private Coroutine satelliteMoveCoroutine;

        private ObservableLongPointerDownTrigger longPointerDownTrigger;

        private void Awake()
        {
            mainPlanetRectTransform = mainPlanet.GetComponent<RectTransform>();
            satelliteRectTransform = GetComponent<RectTransform>();
            satelliteButton = GetComponent<Button>();
            longPointerDownTrigger = GetComponent<ObservableLongPointerDownTrigger>();

            satelliteScoreOriginRotation = satelliteScore.transform.rotation;
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

        private void BindView()
        {
            satelliteButton.OnPointerDownAsObservable()
                .Subscribe(_ =>
                {
                    Debug.Log($"[KHW] Pointer Down");

                    StopRevolve();
                })
                .AddTo(gameObject);

            satelliteButton.OnPointerUpAsObservable()
                .Subscribe(_ =>
                {
                    Debug.Log($"[KHW] Pointer Up");

                    StartRevolve();
                })
                .AddTo(gameObject);

            longPointerDownTrigger.OnLongPointerDownAsObservable()
                .Subscribe(_ =>
                {
                    Debug.Log($"[KHW] Long Pointer Down");
                })
                .AddTo(gameObject);
        }

        private void StartRevolve()
        {
            StopRevolve();

            revolutionCoroutine = StartCoroutine(RotateAroundMainPlanet());
            /*satelliteMoveCoroutine = StartCoroutine()*/
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
                satelliteRectTransform.RotateAround(mainPlanetRectTransform.position, -transform.forward, 30f * Time.deltaTime);
                satelliteScore.transform.rotation = satelliteScoreOriginRotation;

                yield return null;
            }
        }
    }
}
