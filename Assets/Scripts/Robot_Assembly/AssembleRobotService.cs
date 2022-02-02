using UnityEngine;
using UnityEngine.UI;

using UniRx;

public class AssembleRobotService : MonoBehaviour
{
    private int questionCount = 1;
    private const int maxQuestionCount = 7;
    public int QuestionCodeNumber { get; set; }

    [SerializeField]
    private GameObject pausePanel;

    [SerializeField]
    private Button pauseButton;

    [SerializeField]
    private Button homeButton;

    [SerializeField]
    private Button DelButton;

    [SerializeField]
    private Button HowToPlayButton;

    [SerializeField]
    private GameObject HowToPlayPopupPanel;

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

        HowToPlayButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                HowToPlayPopupPanel.SetActive(!HowToPlayPopupPanel.activeSelf);
            })
            .AddTo(gameObject);

        DelButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                HowToPlayPopupPanel.SetActive(!HowToPlayPopupPanel.activeSelf);
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
                AudioManager.Inst.PlaySFX(SoundType.multiplication_doll_complete.ToString());

                successPopupPanel.SetActive(true);

                switch (QuestionCodeNumber)
                {
                    case 'A':
                        PlayerInfoService.Instance.SaveData(CardCollectionCard.CardRobotA, true);
                        break;
                    case 'B':
                        PlayerInfoService.Instance.SaveData(CardCollectionCard.CardRobotB, true);
                        break;
                    case 'C':
                        PlayerInfoService.Instance.SaveData(CardCollectionCard.CardRobotC, true);
                        break;
                    case 'D':
                        PlayerInfoService.Instance.SaveData(CardCollectionCard.CardRobotD, true);
                        break;
                    case 'E':
                        PlayerInfoService.Instance.SaveData(CardCollectionCard.CardRobotE, true);
                        break;
                }
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
