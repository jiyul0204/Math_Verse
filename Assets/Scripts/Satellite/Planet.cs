using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SatelliteGame
{
    enum Difficulty
    {
        EASY,
        NORMAL,
        HARD
    }

    public class Planet : MonoBehaviour
    {
        private RectTransform rectTransform;

        [Header("Game Difficulty")]
        [SerializeField]
        private Difficulty gameDifficulty;

        #region Score
        [Header("Score")]
        [SerializeField]
        private Text scoreText;
        private int scoreValue;
        #endregion

        [Header("Satellite")]
        [SerializeField]
        private GameObject satellitePrefab;
        [SerializeField]
        private List<RectTransform> satellitesTrasforms;
        private List<Satellite> satellites;
        [SerializeField]
        private float satelliteMoveSpeed = 30f;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            satellites = new List<Satellite>();
        }

        private void Start()
        {
            SetPlanetScore();

            int satellitesCount;

            switch (gameDifficulty)
            {
                case Difficulty.EASY:
                    satellitesCount = 0;
                    break;
                case Difficulty.NORMAL:
                    satellitesCount = 1;
                    break;
                case Difficulty.HARD:
                    satellitesCount = 2;
                    break;
                default:
                    satellitesCount = 0;
                    break;
            }

            for (int i = 0; i < satellitesCount; i++)
            {
                CreateSatellite(i);
            }

            if (satellites.Count > 0)
            {
                StartCoroutine(RotateAroundMainPlanet());
            }
        }

        private void SetPlanetScore()
        {
            scoreValue = Random.Range(20, 201);
            scoreText.text = $"{scoreValue}";
        }

        private IEnumerator RotateAroundMainPlanet()
        {
            while (satellites.Count > 0)
            {
                foreach (var satellitesTransform in satellitesTrasforms)
                {
                    satellitesTransform.RotateAround(rectTransform.position, -transform.forward, satelliteMoveSpeed * Time.deltaTime);
                }

                yield return null;
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            var targetSatellite = collider.GetComponent<Satellite>();

            foreach (var satellite in satellites)
            {
                if (targetSatellite.Equals(satellite))
                {
                    scoreValue = targetSatellite.CalculateScore(scoreValue);
                    scoreText.text = $"{scoreValue}";

                    satellites.Remove(satellite);
                    Destroy(satellite.gameObject);
                    return;
                }
            }
        }

        private void CreateSatellite(int transformIndex)
        {
            var satelliteObject = Instantiate(satellitePrefab, satellitesTrasforms[transformIndex].position, Quaternion.identity, transform);

            var newSatellite = satelliteObject.GetComponent<Satellite>();

            newSatellite.Init(satellitesTrasforms[transformIndex]);
            satellites.Add(newSatellite);
        }
    }
}