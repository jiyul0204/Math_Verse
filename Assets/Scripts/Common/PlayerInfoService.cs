using UnityEngine;

public enum CardCollectionCard
{
    CardRobotA,
    CardRobotB,
    CardRobotC,
    CardRobotD,
    CardRobotE,
    CardPlanetA,
    CardPlanetB,
    CardPlanetC
}

public class PlayerInfoService : Singleton<PlayerInfoService>
{
    public void DataReset()
    {
        PlayerPrefs.DeleteKey(CardCollectionCard.CardRobotA.ToString());
        PlayerPrefs.DeleteKey(CardCollectionCard.CardRobotB.ToString());
        PlayerPrefs.DeleteKey(CardCollectionCard.CardRobotC.ToString());
        PlayerPrefs.DeleteKey(CardCollectionCard.CardRobotD.ToString());
        PlayerPrefs.DeleteKey(CardCollectionCard.CardRobotE.ToString());
        PlayerPrefs.DeleteKey(CardCollectionCard.CardPlanetA.ToString());
        PlayerPrefs.DeleteKey(CardCollectionCard.CardPlanetB.ToString());
        PlayerPrefs.DeleteKey(CardCollectionCard.CardPlanetC.ToString());
        PlayerPrefs.Save();
    }

    public void SaveData(CardCollectionCard card, bool isGet)
    {
        PlayerPrefs.SetInt(card.ToString(), isGet ? 1 : 0);
        PlayerPrefs.Save();
    }

    public bool LoadData(CardCollectionCard card)
    {
        return ToBool(PlayerPrefs.GetInt(card.ToString(), 0));
    }

    private bool ToBool(int integer)
    {
        return integer == 0 ? false : true;
    }
}
