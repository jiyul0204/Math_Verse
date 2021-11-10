using System;
using UnityEngine;
using UnityEngine.UI;

using UniRx;

namespace SatelliteGame
{
    public class GameManager : Singleton<GameManager>
    {
        private RxCountDownTimer rxCountDownTimer;

        [SerializeField]
        private Text remainTimeText;

        [SerializeField]
        private GameObject resultPopupPanel;

        [SerializeField]
        private Text gameResultText;

        private IDisposable countDownDisposable;
        private IDisposable changeCountDownTextColorDisposable;

        private void Awake()
        {
            rxCountDownTimer = GetComponent<RxCountDownTimer>();
        }

        private void Start()
        {
            // 타이머의 남은 시간을 표시한다.
            countDownDisposable = rxCountDownTimer.CountDownObservable
                .Subscribe
                (
                    time =>
                    {
                        // onNext에서 시간을 표시한다.
                        remainTimeText.text = $"남은 시간 : {time}초";
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
                .Subscribe(_ => remainTimeText.color = Color.red);
        }

        public void ShowGameOverResult(bool isClear)
        {
            countDownDisposable.Dispose();
            changeCountDownTextColorDisposable.Dispose();

            remainTimeText.text = $"종료!";

            gameResultText.text = isClear ? $"게임 성공!" : $"게임 실패...";
            resultPopupPanel.SetActive(true);
        }
    }
}