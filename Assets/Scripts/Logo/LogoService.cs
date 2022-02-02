using UnityEngine;
using UnityEngine.UI;

using UniRx;

public class LogoService : MonoBehaviour
{
    [SerializeField]
    private Button startButton;

    [SerializeField]
    private Button resetButton;

    private void Start()
    {
        AudioManager.Inst.PlayBGM(SoundType.main_bgm.ToString());

        BindView();
    }

    private void BindView()
    {
        startButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                AudioManager.Inst.PlaySFX(SoundType.main_button_touch.ToString());

                SceneService.Instance.LoadScene(SceneName.Lobby);
            })
            .AddTo(gameObject);

        resetButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                PlayerInfoService.Instance.DataReset();

                Debug.Log($"[KHW] Data Reset Success!");
            })
            .AddTo(gameObject);
    }
}
