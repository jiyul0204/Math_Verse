using UnityEngine;
using UnityEngine.UI;

using UniRx;

public class AssembleRobotService : MonoBehaviour
{
    private int questionCount = 1;
    private const int maxQuestionCount = 7;

    [SerializeField]
    private GameObject pausePanel;

    [SerializeField]
    private Button pauseButton;

    [SerializeField]
    private Button homeButton;

    [SerializeField]
    private GameObject restartPopupPanel;

    [SerializeField]
    private GameObject successPopupPanel;

    public static AssembleRobotService Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        BindView();
    }

    private void BindView()
    {
        pauseButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                pausePanel.SetActive(true);
            })
            .AddTo(gameObject);

        homeButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                SceneService.Instance.LoadScene(SceneName.Lobby);
            })
            .AddTo(gameObject);
    }

    public void ShowGameResult(bool isClear)
    {
        if (isClear)
        {
            if (questionCount++ == maxQuestionCount)
            {
                successPopupPanel.SetActive(true);
            }
            else
            {
                CCalculate.Instance.GenerateQuiz();
            }
        }
        else
        {
            restartPopupPanel.SetActive(true);
        }
    }
}
