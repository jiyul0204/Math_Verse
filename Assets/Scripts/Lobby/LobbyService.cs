using UnityEngine;
using UnityEngine.UI;

using UniRx;
using System.Collections;
 

public class LobbyService : MonoBehaviour
{
    ChangeImage m_ChangeImg;

    [SerializeField]
    private Button topUICollectionButton;

    [SerializeField]
    private Button storeCollectionButton;

    [SerializeField]
    private Button gameStartButton;
    private void Awake()
    {
        m_ChangeImg = GetComponent<ChangeImage>();
    }
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
