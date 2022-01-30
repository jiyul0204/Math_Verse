using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SatelliteGame
{
    public class SatelliteService : Singleton<SatelliteService>
    {
        /*private RxCountDownTimer rxCountDownTimer;*/

        [SerializeField]
        private Text remainTimeText;

        [SerializeField]
        private GameObject resultPopupPanel;

        [SerializeField]
        private Text gameResultText;

        [SerializeField]
        private Planet mainPlanet;

        /*private IDisposable countDownDisposable;
        private IDisposable changeCountDownTextColorDisposable;*/

        /*private void Awake()
        {
            rxCountDownTimer = GetComponent<RxCountDownTimer>();
        }*/

        private void Start()
        {
            /*// 타이머의 남은 시간을 표시한다.
            countDownDisposable = rxCountDownTimer.CountDownObservable
                .Subscribe
                (
                    time =>
                    {
                        // onNext에서 시간을 표시한다.
                        // remainTimeText.text = $"남은 시간 : {time}초";
                    },
                    () =>
                    {
                        // onComplete에서 문자를 지운다.
                        ShowGameOverResult(false);
                    }
                ).AddTo(gameObject);

            // 타이머가 10초 이하로 되는 타이밍에 색을 붉은 색으로 한다.
            changeCountDownTextColorDisposable = rxCountDownTimer.CountDownObservable
                .First(timer => timer <= 10)
                .Subscribe(_ => remainTimeText.color = Color.red);*/

            DBQuestDivisionData data = LocalDBDataService.Instance.GetRandomQuestDivisionData();

            Debug.Log($"[KHW] data.stg_cd : {data.stg_cd}\ndata.qst_cd : {data.qst_cd}\ndata.text_cn : {data.text_cn}\ndata.qst_cn : {data.qst_cn}\ndata.qst_cn1_1 : {data.qst_cn1_1}\ndata.math_smb1_1 : {data.math_smb1_1}\ndata.qst_cn1_2 : {data.qst_cn1_2}\ndata.math_smb1_2 : {data.math_smb1_2}\ndata.qst_cn1_3 : {data.qst_cn1_3}\ndata.qst_cn2_1 : {data.qst_cn2_1}\ndata.math_smb2_1 : {data.math_smb2_1}\ndata.qst_cn2_2 : {data.qst_cn2_2}\ndata.math_smb2_2 : {data.math_smb2_2}\ndata.qst_cn2_3 : {data.qst_cn2_3}\ndata.boxed_cransr : {data.boxed_cransr}");
        }

        public void ShowGameOverResult(bool isClear)
        {
            /*countDownDisposable.Dispose();
            changeCountDownTextColorDisposable.Dispose();*/

            remainTimeText.text = $"종료!";

            gameResultText.text = isClear ? $"게임 성공!" : $"게임 실패...";
            resultPopupPanel.SetActive(true);
        }
    }
}