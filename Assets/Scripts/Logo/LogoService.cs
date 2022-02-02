using UnityEngine;
using UnityEngine.UI;

using UniRx;

public class LogoService : MonoBehaviour
{
    [SerializeField]
    private Button startButton;

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
                SceneService.Instance.LoadScene(SceneName.Lobby);
            })
            .AddTo(gameObject);
    }
}
