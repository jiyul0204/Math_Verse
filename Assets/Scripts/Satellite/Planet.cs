using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SatelliteGame
{
    public class Planet : MonoBehaviour
    {
        private RectTransform rectTransform;

        [SerializeField]
        private GameObject satellitePrefab;

        [SerializeField]
        private List<RectTransform> satellitesTrasforms;

        [SerializeField]
        private float satelliteMoveSpeed = 30f;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            foreach (var satellitesTransform in satellitesTrasforms)
            {
                var satelliteObject = Instantiate(satellitePrefab, satellitesTransform.position, Quaternion.identity, transform);

                satelliteObject.GetComponent<Satellite>().Init(this, satellitesTransform, satelliteMoveSpeed);
            }

            StartCoroutine(RotateAroundMainPlanet());
        }

        private IEnumerator RotateAroundMainPlanet()
        {
            while (true)
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
            Debug.Log($"[KHW] collider.name : {collider.name}");
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            
        }

        private void GenerateSatellite()
        {

        }

        public bool CalculateScore()
        {
            return true;
        }
    }
}