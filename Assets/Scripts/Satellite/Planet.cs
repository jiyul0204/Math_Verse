using UnityEngine;

namespace SatelliteGame
{
    public class Planet : MonoBehaviour
    {
        [SerializeField]
        private int number;

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