using UnityEngine;
using UnityEngine.UI;

using UniRx;

public class StoreService : MonoBehaviour
{
    [SerializeField]
    private Button prevQuestButton;

    [SerializeField]
    private Button nextQuestButton;

    [SerializeField]
    private Text questTypeText;

    [SerializeField]
    private Button okButton;

    // Start is called before the first frame update
    private void Start()
    {
        BindView();

        questTypeText.text = LocalDBDataService.Instance.GetCurrentQuestName();
    }

    private void BindView()
    {
        prevQuestButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                questTypeText.text = LocalDBDataService.Instance.GetPreviousQuestName();
            })
            .AddTo(gameObject);

        nextQuestButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                questTypeText.text = LocalDBDataService.Instance.GetNextQuestName();
            })
            .AddTo(gameObject);

        okButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                SceneService.Instance.LoadScene(LocalDBDataService.Instance.PlayGameType);
            })
            .AddTo(gameObject);
    }
}
