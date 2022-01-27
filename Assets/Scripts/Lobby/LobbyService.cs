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
    private Button DelSoundBar;

    [SerializeField]
    private Button DelQuestWindow;

    [SerializeField]
    private Button educationResultButton;

    private void Start()
    {
        BindView();
        Invoke("EntranceStore", 1f);
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

        educationResultButton.OnClickAsObservable()
           .Subscribe(_ =>
           {
               SceneService.Instance.LoadScene("Education_Result");
           })
           .AddTo(gameObject);

        SettingButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                SettingWindow.SetActive(!SettingWindow.activeSelf);
            })
            .AddTo(gameObject);

        DelSoundBar.OnClickAsObservable()
        .Subscribe(_ =>
        {
            SettingWindow.SetActive(!SettingWindow.activeSelf);
        })
        .AddTo(gameObject);

        QuestButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                QuestWindow.SetActive(!QuestWindow.activeSelf);
            })
            .AddTo(gameObject);

        DelQuestWindow.OnClickAsObservable()
            .Subscribe(_ =>
            {
                QuestWindow.SetActive(!QuestWindow.activeSelf);
            })
            .AddTo(gameObject);

        HowToPlayButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                HowToPlayWindow.SetActive(!HowToPlayWindow.activeSelf);
            })
            .AddTo(gameObject);

        DelHowToPlayButton.OnClickAsObservable()
        .Subscribe(_ =>
        {
            HowToPlayWindow.SetActive(!HowToPlayWindow.activeSelf);
         })
        .AddTo(gameObject);
    }

    void EntranceStore()
    {
        aseembleRobotStartButton.OnClickAsObservable()
             .Subscribe(_ =>
            {
                LocalDBDataService.Instance.PlayGameType = GameType.Assemble_Robot;
                SceneService.Instance.LoadScene("Store");
            })
            .AddTo(gameObject);

        satelliteStartButton.OnClickAsObservable()
             .Subscribe(_ =>
             {
                 LocalDBDataService.Instance.PlayGameType = GameType.Satellite;
                 SceneService.Instance.LoadScene("Store");
             })
            .AddTo(gameObject);
    }
}
