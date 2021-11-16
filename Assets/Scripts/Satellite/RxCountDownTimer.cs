/// <summary>
/// 카운트 구성 요소
/// </summary>
using System;
using UnityEngine;

using UniRx;

public class RxCountDownTimer : MonoBehaviour
{
    private IConnectableObservable<int> _countDownObservable;

    [SerializeField]
    private int startTime = 60;

    /// <summary>
    /// 카운트 다운 스트림
    /// 이 Observable을 각 클래스가 Subscribe 한다.
    /// </summary>
    public IObservable<int> CountDownObservable
    {
        get
        {
            return _countDownObservable.AsObservable();
        }
    }

    // 60초 카운트 스트림을 생성, Publish로 Hot 변환
    private void Awake()
    {
        _countDownObservable = CreateCountDownObservable(startTime).Publish();
    }   

    // 카운트 시작
    private void Start()
    {
        _countDownObservable.Connect();
    }

    /// <summary>
    /// countTime 만큼 카운트 다운하는 스트림
    /// </summary>
    /// <param name="countTime"></param>
    /// <returns></returns>
    private IObservable<int> CreateCountDownObservable(int countTime)
    {
        return Observable
            .Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1)) // 0초 이후 1초 간격으로 실행
            .Select(x => (int)(countTime - x)) // x는 시작하고 나서의 시간(초)
            .TakeWhile(x => x > 0); // 0초 초과 동안 OnNext 0이 되면 OnComplete;
    }
}