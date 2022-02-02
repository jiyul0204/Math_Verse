using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SatelliteGame
{
    public class Planet : MonoBehaviour
    {
        [SerializeField]
        private SatelliteService satelliteService;

        private RectTransform rectTransform;

        #region Number
        [Header("[Planet Center Number]")]

        [SerializeField]
        private Text centerNumberText;
        private int centerNumberValue;

        private List<int> aliquots;
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
        private IEnumerator rotateCoroutine;

        [SerializeField]
        private List<Sprite> satelliteSprites;
        #endregion

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            satellites = new List<Satellite>(satellitesTransforms.Count);
        }

        private void OnDestroy()
        {
            StopCoroutine(rotateCoroutine);
        }

        public void SetPlanetScore(int questionNumber, int answer)
        {
            ResetSatellites();

            centerNumberValue = answer;
            centerNumberText.text = "?";

            aliquots = GetAliquotList(questionNumber);

            int satellitesCount = aliquots.Count > 6 ? 6 : aliquots.Count;
            bool isAnswer = false;

            for (int i = 0; i < satellitesCount; i++)
            {
                CreateSatellite(i);

                if (aliquots[i] == answer)
                {
                    isAnswer = true;
                }
            }

            if (!isAnswer)
            {
                satellites[Random.Range(0, satellitesCount)].SetSatelliteScore(answer);
            }

            rotateCoroutine = RotateAroundMainPlanet();
            StartCoroutine(rotateCoroutine);
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
            StartCoroutine(ChangeAnswerSprite(collider.GetComponent<Satellite>()));
        }

        private IEnumerator ChangeAnswerSprite(Satellite targetSatellite)
        {
            targetSatellite.StartRevolve();

            foreach (Satellite satellite in satellites)
            {
                if (!satellite.CompareNumber(centerNumberValue))
                {
                    satellite.GetComponent<Image>().sprite = satelliteSprites[1];
                }
            }

            bool isClear = targetSatellite.CompareNumber(centerNumberValue);

            if (isClear)
            {
                centerNumberText.text = "성공";
                AudioManager.Inst.PlaySFX(SoundType.division_correct.ToString());
            }
            else
            {
                centerNumberText.text = "실패";
                AudioManager.Inst.PlaySFX(SoundType.division_incorrect.ToString());
            }

            yield return new WaitForSeconds(2.0f);

            satelliteService.ShowGameResult(isClear);
        }

        private void CreateSatellite(int transformIndex)
        {
            GameObject satelliteObject = Instantiate(satellitePrefab, satellitesTransforms[transformIndex].position, Quaternion.identity, transform);

            var newSatellite = satelliteObject.GetComponent<Satellite>();

            newSatellite.Init(satellitesTransforms[transformIndex]);
            satellites.Add(newSatellite);

            newSatellite.SetSatelliteScore(aliquots[transformIndex]);
        }

        private void ResetSatellites()
        {
            if (satellites.Count > 0)
            {
                if (rotateCoroutine != null)
                {
                    StopCoroutine(rotateCoroutine);
                }

                foreach (var satellite in satellites)
                {
                    if (satellite.gameObject != null)
                    {
                        Destroy(satellite.gameObject);
                    }
                }

                satellites.Clear();
            }
        }

        private List<int> GetAliquotList(int score)
        {
            int loopEndNum = (int)Mathf.Sqrt(score);
            var newAliquots = new List<int>();

            for (int i = 1; i <= loopEndNum; i++)
            {
                if (score % i == 0)
                {
                    newAliquots.Add(i);

                    if (i != score / i)
                    {
                        newAliquots.Add(score / i);
                    }
                }
            }

            return newAliquots;
        }
    }
}