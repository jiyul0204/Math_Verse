using UnityEngine;
using UnityEngine.UI;

using UniRx;

public class LobbyService : MonoBehaviour
{
    [SerializeField]
    private Button topUICollectionButton;

    [SerializeField]
    private Button storeCollectionButton;

    [SerializeField]
    private Button gameStartButton;

    private void Start()
    {
        BindView();
    }

    private void BindView()
    {
        topUICollectionButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                SceneService.Instance.LoadScene("CardCollection");
            })
            .AddTo(gameObject);

        storeCollectionButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                SceneService.Instance.LoadScene("CardCollection");
            })
            .AddTo(gameObject);

        gameStartButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                SceneService.Instance.LoadScene("Store");
            })
            .AddTo(gameObject);
    }
}
