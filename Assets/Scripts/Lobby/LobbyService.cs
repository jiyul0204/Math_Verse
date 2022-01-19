using UnityEngine;
using UnityEngine.UI;

using UniRx;
using System.Collections;
 

public class LobbyService : MonoBehaviour
{
    public GameObject SettingWindow;
    public GameObject QuestWindow;

    [SerializeField]
    private Button topUICollectionButton;

    [SerializeField]
    private Button storeCollectionButton;

    [SerializeField]
    private Button SettingButton;

    [SerializeField]
    private Button QuestButton;

    [SerializeField]
    private Button gameStartButton;
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

        SettingButton.OnClickAsObservable()
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

    }

    void EntranceStore()
    {
        gameStartButton.OnClickAsObservable()
             .Subscribe(_ =>
            {
                SceneService.Instance.LoadScene("Store");
            })
            .AddTo(gameObject);
    }

}
