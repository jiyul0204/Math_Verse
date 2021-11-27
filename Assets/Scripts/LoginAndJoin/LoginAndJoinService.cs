using UnityEngine;
using UnityEngine.UI;

using UniRx;

public class LoginAndJoinService : MonoBehaviour
{
    [SerializeField]
    private Button backButton;

    private void Start()
    {
        BindView();
    }

    private void BindView()
    {
        backButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                SceneService.Instance.LoadScene("Logo");
            })
            .AddTo(gameObject);
    }
}
