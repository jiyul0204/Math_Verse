using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using UniRx;

public class PanelGameRestart : MonoBehaviour
{
    [SerializeField]
    private Button yesButton;

    [SerializeField]
    private Button noButton;

    private void Start()
    {
        BindView();
    }

    private void BindView()
    {
        yesButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
            })
            .AddTo(gameObject);

        noButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                SceneManager.LoadScene("Lobby");
            })
            .AddTo(gameObject);
    }
}
