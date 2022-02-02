using UnityEngine;
using UnityEngine.UI;

using UniRx;

public class EducationCheckService : MonoBehaviour
{
    [SerializeField]
    private GameObject multiplicationPanel;

    [SerializeField]
    private GameObject divisionPanel;

    [SerializeField]
    private Button backButton;

    [SerializeField]
    private Button multiplicationButton;

    [SerializeField]
    private Button divisionButton;

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

        multiplicationButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                multiplicationPanel.SetActive(true);
                divisionPanel.SetActive(false);
            })
            .AddTo(gameObject);

        divisionButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                multiplicationPanel.SetActive(false);
                divisionPanel.SetActive(true);
            })
            .AddTo(gameObject);
    }
}