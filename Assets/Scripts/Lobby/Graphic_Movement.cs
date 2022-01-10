using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graphic_Movement : MonoBehaviour
{
    public Vector3[] waypoints;      //이동포인트 배열
    private Vector3 currPosition;    //현재위치
    private int waypointIndex = 0;   //이동포인트 인덱스
    public float speed = 3f;        //속도

    void Start()
    {
        waypoints = new Vector3[3];

        //이동포인트 배열에 값 할당
        waypoints.SetValue(new Vector3(30,1, 0), 0);
        waypoints.SetValue(new Vector3(20, 2, 0), 1);
        waypoints.SetValue(new Vector3(18, 3, 0), 2);
    }
    //매 프레임마다 호출됨.
    void Update()
    {
        //이동중인 현재위치 currPosition 변수에 담기
        currPosition = transform.position;

        //이동지점 배열의 인덱스 0 부터 배열크기-1 까지
        if (waypointIndex < waypoints.Length)
        {
            float step = speed * Time.deltaTime;
            //현재위치를 frame 처리시간비율로 계산한 속도만큼 옮겨줌. 즉, 1개의 프레임단위로 움직임처리.
            transform.position = Vector3.MoveTowards(currPosition, waypoints[waypointIndex], step);

            //현재위치가 이동지점의 위치라면 배열 인덱스 +1하여 다음 포인트로 이동하도록.
            if (Vector3.Distance(waypoints[waypointIndex], currPosition) == 0f)
                waypointIndex++;
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (waypoints == null)
            return;

        for (int i = 0; i < waypoints.Length; i++)
        {
            Gizmos.color = Color.green;
            //처음시작이면 오브젝트 현재위치에서 0번째 waypoint위치까지 선긋기
            if (i == 0)
            {
                Gizmos.DrawLine(transform.position, waypoints[i]);
                //그 외에는 이전 waypoint에서 현재 waypoint까지 선긋기
            }
            else
            {
                Debug.Log(waypoints[i - 1].ToString() + " / to : " + waypoints[i].ToString());
                Gizmos.DrawLine(waypoints[i - 1], waypoints[i]);
            }
            //포인트마다 Gizmo 아이콘 그려주기(아래 그림의 별모양 아이콘)
            Gizmos.DrawIcon(waypoints[i], "Jiyul.png");
        }
    }
}
