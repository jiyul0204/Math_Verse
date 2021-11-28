using UnityEngine;
using UnityEngine.UI;

using UniRx;

public class StoreService : MonoBehaviour
{
    [SerializeField]
    private Button okButton;

    // Start is called before the first frame update
    private void Start()
    {
        BindView();
    }

    private void BindView()
    {
        okButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                SceneService.Instance.LoadScene("Assemble_Robot");
            })
            .AddTo(gameObject);
    }
}
