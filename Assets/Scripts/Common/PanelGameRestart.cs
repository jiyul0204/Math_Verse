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
                    SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
                })
                .AddTo(gameObject);
        }

        goToLobbyButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                SceneManager.LoadScene("Lobby");
            })
            .AddTo(gameObject);
    }
}
