using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UniRx;

public class CardCollectionService : MonoBehaviour
{
    [SerializeField]
    private Button backButton;

    [SerializeField]
    private Button assembleCheckBox;

    [SerializeField]
    private GameObject assembleCheckImage;

    [SerializeField]
    private Button satelliteCheckBox;

    [SerializeField]
    private GameObject satelliteCheckImage;

    [SerializeField]
    private List<CollectionCard> collectionFrontCards;

    [SerializeField]
    private List<CollectionCard> collectionBackCards;

    private void Start()
    {
        SetCardDisplay();

        BindView();
    }

    private void BindView()
    {
        backButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                AudioManager.Inst.PlaySFX(SoundType.main_button_touch.ToString());
                SceneService.Instance.LoadScene(SceneName.Lobby);
            })
            .AddTo(gameObject);

        assembleCheckBox.OnClickAsObservable()
            .Subscribe(_ =>
            {
                AudioManager.Inst.PlaySFX(SoundType.collection_check_box.ToString());
                assembleCheckImage.SetActive(!assembleCheckImage.activeSelf);

                if (assembleCheckImage.activeSelf)
                {
                    SetCardDisplay();
                }
                else
                {
                    foreach (var card in collectionFrontCards)
                    {
                        card.SetCardDisplayStatus(GameType.Assemble_Robot, assembleCheckImage.activeSelf);
                    }

                    foreach (var card in collectionBackCards)
                    {
                        card.SetCardDisplayStatus(GameType.Assemble_Robot, assembleCheckImage.activeSelf);
                    }
                }
            })
            .AddTo(gameObject);

        satelliteCheckBox.OnClickAsObservable()
            .Subscribe(_ =>
            {
                AudioManager.Inst.PlaySFX(SoundType.collection_check_box.ToString());
                satelliteCheckImage.SetActive(!satelliteCheckImage.activeSelf);

                if (satelliteCheckImage.activeSelf)
                {
                    SetCardDisplay();
                }
                else
                {
                    foreach (var card in collectionFrontCards)
                    {
                        card.SetCardDisplayStatus(GameType.Satellite, satelliteCheckImage.activeSelf);
                    }

                    foreach (var card in collectionBackCards)
                    {
                        card.SetCardDisplayStatus(GameType.Satellite, satelliteCheckImage.activeSelf);
                    }
                }
            })
            .AddTo(gameObject);
    }

    private void SetCardDisplay()
    {

        if (PlayerInfoService.Instance.LoadData(CardCollectionCard.CardRobotA))
        {
            collectionBackCards[(int)CardCollectionCard.CardRobotA].gameObject.SetActive(false);
            collectionFrontCards[(int)CardCollectionCard.CardRobotA].gameObject.SetActive(true);
        }
        else
        {
            collectionBackCards[(int)CardCollectionCard.CardRobotA].gameObject.SetActive(true);
        }

        if (PlayerInfoService.Instance.LoadData(CardCollectionCard.CardRobotB))
        {
            collectionBackCards[(int)CardCollectionCard.CardRobotB].gameObject.SetActive(false);
            collectionFrontCards[(int)CardCollectionCard.CardRobotB].gameObject.SetActive(true);
        }
        else
        {
            collectionBackCards[(int)CardCollectionCard.CardRobotB].gameObject.SetActive(true);
        }

        if (PlayerInfoService.Instance.LoadData(CardCollectionCard.CardRobotC))
        {
            collectionBackCards[(int)CardCollectionCard.CardRobotC].gameObject.SetActive(false);
            collectionFrontCards[(int)CardCollectionCard.CardRobotC].gameObject.SetActive(true);
        }
        else
        {
            collectionBackCards[(int)CardCollectionCard.CardRobotC].gameObject.SetActive(true);
        }

        if (PlayerInfoService.Instance.LoadData(CardCollectionCard.CardRobotD))
        {
            collectionBackCards[(int)CardCollectionCard.CardRobotD].gameObject.SetActive(false);
            collectionFrontCards[(int)CardCollectionCard.CardRobotD].gameObject.SetActive(true);
        }
        else
        {
            collectionBackCards[(int)CardCollectionCard.CardRobotD].gameObject.SetActive(true);
        }

        if (PlayerInfoService.Instance.LoadData(CardCollectionCard.CardRobotE))
        {
            collectionBackCards[(int)CardCollectionCard.CardRobotE].gameObject.SetActive(false);
            collectionFrontCards[(int)CardCollectionCard.CardRobotE].gameObject.SetActive(true);
        }
        else
        {
            collectionBackCards[(int)CardCollectionCard.CardRobotE].gameObject.SetActive(true);
        }

        if (PlayerInfoService.Instance.LoadData(CardCollectionCard.CardPlanetA))
        {
            collectionBackCards[(int)CardCollectionCard.CardPlanetA].gameObject.SetActive(false);
            collectionFrontCards[(int)CardCollectionCard.CardPlanetA].gameObject.SetActive(true);
        }
        else
        {
            collectionBackCards[(int)CardCollectionCard.CardPlanetA].gameObject.SetActive(true);
        }

        if (PlayerInfoService.Instance.LoadData(CardCollectionCard.CardPlanetB))
        {
            collectionBackCards[(int)CardCollectionCard.CardPlanetB].gameObject.SetActive(false);
            collectionFrontCards[(int)CardCollectionCard.CardPlanetB].gameObject.SetActive(true);
        }
        else
        {
            collectionBackCards[(int)CardCollectionCard.CardPlanetB].gameObject.SetActive(true);
        }

        if (PlayerInfoService.Instance.LoadData(CardCollectionCard.CardPlanetC))
        {
            collectionBackCards[(int)CardCollectionCard.CardPlanetC].gameObject.SetActive(false);
            collectionFrontCards[(int)CardCollectionCard.CardPlanetC].gameObject.SetActive(true);
        }
        else
        {
            collectionBackCards[(int)CardCollectionCard.CardPlanetC].gameObject.SetActive(true);
        }
    }
}
