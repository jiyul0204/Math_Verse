using UnityEngine;
using UnityEngine.UI;

using UniRx;

public class LogoService : MonoBehaviour
{
    [SerializeField]
    private Button startButton;

    private void Start()
    {
        BindView();
    }

    private void BindView()
    {
        startButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                SceneService.Instance.LoadScene("Lobby");
            })
            .AddTo(gameObject);
    }
}
