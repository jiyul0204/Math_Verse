using UnityEngine;
using UniRx;

namespace SatilliteGame
{
    public class Satellite : MonoBehaviour
    {
        [SerializeField]
        private GameObject mainPlanet;

        [SerializeField]
        private GameObject sateliteScore;

        private Quaternion sateliteScoreOriginRotation;

        private void Start()
        {
            sateliteScoreOriginRotation = sateliteScore.transform.rotation;
        }

        // Update is called once per frame
        private void Update()
        {
            GetComponent<RectTransform>().RotateAround(mainPlanet.GetComponent<RectTransform>().position, -transform.forward, 20 * Time.deltaTime);
            sateliteScore.transform.rotation = sateliteScoreOriginRotation;
        }
    }
}
