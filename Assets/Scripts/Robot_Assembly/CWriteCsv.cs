using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CWriteCsv : MonoBehaviour
{
    CCalculate m_CCal;

    Text Txt_UserID;
    // Start is called before the first frame update
    void Start()
    {
        // 절대 좌표 말고 상대 좌표로 써주기! - Hyeonwoo, 2021.11.26.
        /*using (var writer = new CsvFileWriter("C:\\Users\\KOREA\\Documents\\GitHub\\Math_Verse\\Assets\\AI_Edu_Data\\EducationData.csv"))
        {
            List<string> columns = new List<string>() { "UserID", "Level", "Correctw", "Quiz" };
            writer.WriteRow(columns);
            columns.Clear();

            //columns.Add(Txt_UserID.ToString());
            //columns.Add(문제레벨);
            //columns.Add(정오답);

            columns.Add("jiyul");
            columns.Add("Easy");
            columns.Add("1");
            columns.Add("2*3=6");
            writer.WriteRow(columns);
            columns.Clear();

            columns.Add("jiyul");
            columns.Add("Hardest");
            columns.Add("0");
            columns.Add("2*8=16");
            writer.WriteRow(columns);
            columns.Clear();
        }*/
    }
}
