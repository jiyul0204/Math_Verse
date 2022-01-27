using UnityEngine;
using UnityEngine.UI;

using UniRx;

public class EducationCheckService : MonoBehaviour
{
    [SerializeField]
    private Button backButton;

    private void Awake()
    {
        BindView();
    }

    private void BindView()
    {
        backButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                SceneService.Instance.LoadScene("Lobby");
            })
            .AddTo(gameObject);
    }
}
