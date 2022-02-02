using UnityEngine;

public class CollectionCard : MonoBehaviour
{
    [SerializeField]
    private GameType gameType;

    public void SetCardDisplayStatus(GameType gameType, bool active)
    {
        if (this.gameType == gameType)
        {
            gameObject.SetActive(active);
        }
    }
}
