using UnityEngine;
using UnityEngine.UI;

using UniRx; 

public class LobbyService : MonoBehaviour
{
    public GameObject SettingWindow;
    public GameObject QuestWindow;
    public GameObject HowToPlayWindow;

    [SerializeField]
    private Button topUICollectionButton;

    [SerializeField]
    private Button storeCollectionButton;

    [SerializeField]
    private Button SettingButton;

    [SerializeField]
    private Button QuestButton;

    [SerializeField]
    private Button aseembleRobotStartButton;

    [SerializeField]
    private Button satelliteStartButton;

    [SerializeField]
    private Button HowToPlayButton;

    [SerializeField]
    private Button DelHowToPlayButton;

    [SerializeField]
    private Button DelQuestWindow;

    [SerializeField]
    private Button educationResultButton;

    private void Start()
    {
        AudioManager.Inst.PlayBGM(SoundType.main_bgm.ToString());

        BindView();
    }

    private void BindView()
    {
        topUICollectionButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                AudioManager.Inst.PlaySFX(SoundType.main_button_touch.ToString());
                SceneService.Instance.LoadScene(SceneName.CardCollection);
            })
            .AddTo(gameObject);

        storeCollectionButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                AudioManager.Inst.PlaySFX(SoundType.main_button_touch.ToString());
                SceneService.Instance.LoadScene(SceneName.CardCollection);
            })
            .AddTo(gameObject);

        aseembleRobotStartButton.OnClickAsObservable()
             .Subscribe(_ =>
             {
                 AudioManager.Inst.PlaySFX(SoundType.store_door_touch.ToString());
                 LocalDBDataService.Instance.PlayGameType = GameType.Assemble_Robot;
                 SceneService.Instance.LoadScene(SceneName.Store);
             })
            .AddTo(gameObject);

        satelliteStartButton.OnClickAsObservable()
             .Subscribe(_ =>
             {
                 AudioManager.Inst.PlaySFX(SoundType.main_button_touch.ToString());
                 LocalDBDataService.Instance.PlayGameType = GameType.Satellite;
                 SceneService.Instance.LoadScene(SceneName.Store);
             })
            .AddTo(gameObject);

        educationResultButton.OnClickAsObservable()
           .Subscribe(_ =>
           {
               AudioManager.Inst.PlaySFX(SoundType.main_button_touch.ToString());
               SceneService.Instance.LoadScene(SceneName.Education_Check);
           })
           .AddTo(gameObject);

        SettingButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                AudioManager.Inst.PlaySFX(SoundType.main_button_touch.ToString());
                SettingWindow.SetActive(!SettingWindow.activeSelf);
            })
            .AddTo(gameObject);

        QuestButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                AudioManager.Inst.PlaySFX(SoundType.main_button_touch.ToString());
                QuestWindow.SetActive(!QuestWindow.activeSelf);
            })
            .AddTo(gameObject);

        DelQuestWindow.OnClickAsObservable()
            .Subscribe(_ =>
            {
                AudioManager.Inst.PlaySFX(SoundType.main_button_touch.ToString());
                QuestWindow.SetActive(!QuestWindow.activeSelf);
            })
            .AddTo(gameObject);

        HowToPlayButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                AudioManager.Inst.PlaySFX(SoundType.main_button_touch.ToString());
                HowToPlayWindow.SetActive(!HowToPlayWindow.activeSelf);
            })
            .AddTo(gameObject);

        DelHowToPlayButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                AudioManager.Inst.PlaySFX(SoundType.main_button_touch.ToString());
                HowToPlayWindow.SetActive(!HowToPlayWindow.activeSelf);
            })
            .AddTo(gameObject);
    }
}
