using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using UniRx;

public class PanelGameRestart : MonoBehaviour
{
    [SerializeField]
    private Button restartButton;

    [SerializeField]
    private Button goToLobbyButton;

    private void Start()
    {
        BindView();
    }

    private void BindView()
    {
        if (restartButton != null)
        {
            restartButton.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    SceneService.Instance.ReloadScene();
                })
                .AddTo(gameObject);
        }

        goToLobbyButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                SceneService.Instance.LoadScene(SceneName.Lobby);
            })
            .AddTo(gameObject);
    }
}
