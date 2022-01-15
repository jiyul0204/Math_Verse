using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CSVParserTest : MonoBehaviour
{
    private void Start()
    {
        string textData = File.ReadAllText(Application.streamingAssetsPath + "/DBInitDataList.csv");

        var sample = new DBInitDataList();

        sample.initDatas = new List<DBInitData>(CSVParser.Deserialize<DBInitData>(textData));

        var selectQuery = from initData in sample.initDatas
                          where string.Compare(initData.lrn_prgs_sts_cd, "LPSC04") == 0
                          select (initData.mbr_id, initData.sid);

        foreach (var selectData in selectQuery)
        {
            Debug.Log($"[KHW] selectData : {selectData.mbr_id} / {selectData.sid}");
        }
    }
}
