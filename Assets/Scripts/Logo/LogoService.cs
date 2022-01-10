using UnityEngine;
using UnityEngine.UI;

using UniRx;

public class LogoService : MonoBehaviour
{
    /*[SerializeField]
    private Button loginAndJoinButton;*/

    [SerializeField]
    private Button startButton;

    private void Start()
    {
        BindView();
    }

    private void BindView()
    {
        /*loginAndJoinButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                SceneService.Instance.LoadScene("LoginAndJoin");
            })
            .AddTo(gameObject);*/

        startButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                SceneService.Instance.LoadScene("Lobby");
            })
            .AddTo(gameObject);
    }
}
