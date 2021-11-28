using UnityEngine;
using UnityEngine.UI;

using UniRx;

public class LoginAndJoinService : MonoBehaviour
{
    [SerializeField]
    private Button mainBackButton;

    [SerializeField]
    private Button joinButton;

    [SerializeField]
    private GameObject joinSuccessPanel;

    [SerializeField]
    private Button joinBackButton;

    private void Start()
    {
        BindView();
    }

    private void BindView()
    {
        mainBackButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                SceneService.Instance.LoadScene("Logo");
            })
            .AddTo(gameObject);

        joinButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                joinSuccessPanel.SetActive(true);
            })
            .AddTo(gameObject);

        joinBackButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                SceneService.Instance.LoadScene("Logo");
            })
            .AddTo(gameObject);
    }
}
