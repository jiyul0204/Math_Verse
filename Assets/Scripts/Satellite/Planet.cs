using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SatelliteGame
{
    /*enum Difficulty
    {
        EASY,
        NORMAL,
        HARD
    }*/

    public class Planet : MonoBehaviour
    {
        private RectTransform rectTransform;

        /*#region Game Difficulty
        [Header("[Game Difficulty]")]

        [SerializeField]
        private Difficulty gameDifficulty;
        #endregion*/

        /*#region Count Down Timer
        [Header("[Count Down Timer]")]

        [SerializeField]
        private RxCountDownTimer rxCountDownTimer;
        #endregion*/

        #region NU Number
        [Header("[Planet Center Number]")]

        [SerializeField]
        private Text centerNumberText;
        private int centerNumberValue;
        #endregion

        #region Satellite
        [Header("[Satellite]")]

        [SerializeField]
        private GameObject satellitePrefab;

        [SerializeField]
        private float satelliteMoveSpeed = 30f;

        [SerializeField]
        private List<RectTransform> satellitesTransforms;

        private List<Satellite> satellites;
        private int satelliteGenerateTerm;
        #endregion

        /*#region Artificial Satellite
        [Header("[Artificial Satellite]")]

        [SerializeField]
        private GameObject artificialSatellitePrefab;

        [SerializeField]
        private List<RectTransform> artificialSatellitesTransforms;

        public List<ArtificialSatellite> artificialSatellites { get; private set; }
        private int artificialSatelliteGenerateTerm;
        #endregion*/

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();

            satellites = new List<Satellite>(satellitesTransforms.Count);
            /*artificialSatellites = new List<ArtificialSatellite>(artificialSatellitesTransforms.Count);*/
        }

        private void Start()
        {
            SetPlanetScore(100);

            int satellitesCount = 4;

            /*switch (gameDifficulty)
            {
                case Difficulty.EASY:
                    satellitesCount = 0;
                    satelliteGenerateTerm = 20;
                    *//*artificialSatelliteGenerateTerm = 1;*//*
                    break;
                case Difficulty.NORMAL:
                    satellitesCount = 1;
                    satelliteGenerateTerm = 10;
                    *//*artificialSatelliteGenerateTerm = 1;*//*
                    break;
                case Difficulty.HARD:
                    satellitesCount = 2;
                    satelliteGenerateTerm = 5;
                    *//*artificialSatelliteGenerateTerm = 1;*//*
                    break;
                default:
                    satellitesCount = 0;
                    break;
            }*/

            for (int i = 0; i < satellitesCount; i++)
            {
                CreateSatellite(i);
            }

            if (satellites.Count > 0)
            {
                StartCoroutine(RotateAroundMainPlanet());
            }

            // 일정 주기마다 위성 생성
            /*rxCountDownTimer.CountDownObservable
                .Skip(1)
                .Where(time => time % satelliteGenerateTerm == 0)
                .Subscribe(_ =>
                {
                    if (satellites.Count >= 4)
                    {
                        SatelliteService.Instance.ShowGameOverResult(false);
                        return;
                    }

                    CreateSatellite(satellites.Count);
                })
                .AddTo(gameObject);*/

            // 일정 주기마다 인공 위성 생성
            /*rxCountDownTimer.CountDownObservable
                .Where(time => time % artificialSatelliteGenerateTerm == 0)
                .Subscribe(_ =>
                {
                    if (artificialSatellites.Count >= artificialSatellitesTransforms.Count)
                    {
                        return;
                    }

                    CreateArtificialSatellite(Random.Range(0, artificialSatellitesTransforms.Count));
                })
                .AddTo(gameObject);*/
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        public void SetPlanetScore(int score)
        {
            centerNumberValue = score;
            centerNumberText.text = $"{centerNumberValue}";
        }

        private IEnumerator RotateAroundMainPlanet()
        {
            while (true)
            {
                foreach (RectTransform satellitesTransform in satellitesTransforms)
                {
                    satellitesTransform.RotateAround(rectTransform.position, -transform.forward, satelliteMoveSpeed * Time.deltaTime);
                }

                yield return null;
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            /*if (satellites.Count > 0)
            {
                return;
            }

            var targetArtificialSatellite = collider.GetComponent<ArtificialSatellite>();

            if (RemoveArtificialSatellite(targetArtificialSatellite))
            {
                centerNumberValue = targetArtificialSatellite.CalculateScore(centerNumberValue);
                centerNumberText.text = $"{centerNumberValue}";

                if (centerNumberValue <= 0)
                {
                    SatelliteService.Instance.ShowGameOverResult(true);
                    Destroy(gameObject);
                }
            }*/

            var targetSatellite = collider.GetComponent<Satellite>();

            if (RemoveSatellite(targetSatellite))
            {
                centerNumberValue = targetSatellite.CalculateScore(centerNumberValue);
                centerNumberText.text = $"{centerNumberValue}";

                if (centerNumberValue <= 0)
                {
                    SatelliteService.Instance.ShowGameOverResult(true);
                    Destroy(gameObject);
                }
            }
        }

        public bool RemoveSatellite(Satellite targetSatellite)
        {
            foreach (Satellite satellite in satellites)
            {
                if (targetSatellite == null || satellite == null)
                {
                    return false;
                }

                if (targetSatellite.Equals(satellite))
                {
                    satellites.Remove(satellite);
                    Destroy(satellite.gameObject);

                    return true;
                }
            }

            return false;
        }

        /*public bool RemoveArtificialSatellite(ArtificialSatellite targetArtificialSatellite)
        {
            foreach (ArtificialSatellite artificialSatellite in artificialSatellites)
            {
                if (targetArtificialSatellite == null || artificialSatellite == null)
                {
                    return false;
                }

                if (targetArtificialSatellite.Equals(artificialSatellite))
                {
                    artificialSatellites.Remove(artificialSatellite);
                    Destroy(artificialSatellite.gameObject);

                    return true;
                }
            }

            return false;
        }*/

        private void CreateSatellite(int transformIndex)
        {
            GameObject satelliteObject = Instantiate(satellitePrefab, satellitesTransforms[transformIndex].position, Quaternion.identity, transform);

            var newSatellite = satelliteObject.GetComponent<Satellite>();

            newSatellite.Init(this, satellitesTransforms[transformIndex]);
            satellites.Add(newSatellite);
        }

        /*private void CreateArtificialSatellite(int transformIndex)
        {
            GameObject artificialSatelliteObject = Instantiate(artificialSatellitePrefab, artificialSatellitesTransforms[transformIndex].position, Quaternion.identity, transform);

            var newArtificailSatellite = artificialSatelliteObject.GetComponent<ArtificialSatellite>();

            artificialSatellites.Add(newArtificailSatellite);
        }*/
    }
}