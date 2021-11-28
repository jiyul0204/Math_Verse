using UnityEngine;
using UnityEngine.UI;

using UniRx;

public class AssembleRobotService : MonoBehaviour
{
    [SerializeField]
    private Button homeButton;

    // Start is called before the first frame update
    private void Start()
    {
        BindView();
    }

    private void BindView()
    {
        homeButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                SceneService.Instance.LoadScene("Lobby");
            })
            .AddTo(gameObject);
    }
}
