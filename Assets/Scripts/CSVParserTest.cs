using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

// 참고 : https://www.overflowarchives.com/unity/how-to-convert-excel-to-json-in-unity-c-sharpe-parse-excel-file-to-json/
public class CSVParserTest : MonoBehaviour
{
    private void Start()
    {
        string initDataListCSV = File.ReadAllText($"{Application.streamingAssetsPath}/DBInitDataList.csv");
        DBInitData[] initDatas = CSVParser.Deserialize<DBInitData>(initDataListCSV);
        var selectQuery = from initData in initDatas
                          where string.Compare(initData.lrn_prgs_sts_cd, "LPSC04") == 0
                          select (initData.mbr_id, initData.sid);

        foreach (var selectData in selectQuery)
        {
            Debug.Log($"[KHW] selectData : {selectData.mbr_id} / {selectData.sid}");
        }
    }
}