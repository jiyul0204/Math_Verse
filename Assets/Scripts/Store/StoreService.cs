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
        AudioManager.Inst.PlayBGM(SoundType.store_bgm.ToString());

        BindView();

        questTypeText.text = LocalDBDataService.Instance.GetCurrentQuestName();
    }

    private void BindView()
    {
        prevQuestButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                AudioManager.Inst.PlaySFX(SoundType.main_button_touch.ToString());
                questTypeText.text = LocalDBDataService.Instance.GetPreviousQuestName();
            })
            .AddTo(gameObject);

        nextQuestButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                AudioManager.Inst.PlaySFX(SoundType.main_button_touch.ToString());
                questTypeText.text = LocalDBDataService.Instance.GetNextQuestName();
            })
            .AddTo(gameObject);

        okButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                AudioManager.Inst.PlaySFX(SoundType.store_enter_guest.ToString());
                SceneService.Instance.LoadScene(LocalDBDataService.Instance.PlayGameType);
            })
            .AddTo(gameObject);
    }
}
