using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUI : MonoBehaviour
{
    [HideInInspector]public QuestManager am;
    [HideInInspector]public GameObject achCardPrefab;
    public RectTransform listContent;
    public List<AchievementCard> achievementsCards;
    [SerializeField] private List<Quest> achievements;
    public int achsN;
    private RectTransform lastCard;
    public int cardHeightSpace;

    public void CreateCard()
    {
        Quest thisAch;
        AchievementCard thisCard = new AchievementCard();

        thisAch = achievements[achsN];

        listContent.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, listContent.sizeDelta.y + 180);
        thisCard.achCard = Instantiate(achCardPrefab, listContent);

        thisAch.visibleInt = achsN;
        thisCard.achN = achsN;
        thisCard.achName = thisCard.achCard.GetComponent<Transform>().GetChild(0).GetComponent<TextMeshProUGUI>();
        thisCard.achDesc = thisCard.achCard.GetComponent<Transform>().GetChild(1).GetComponent<TextMeshProUGUI>();
        thisCard.achImage = thisCard.achCard.GetComponent<Transform>().GetChild(2).GetComponentInChildren<Image>();

        thisCard.achName.text = thisAch.questName;
        thisCard.achDesc.text = thisAch.questDescription;
        thisCard.achImage.sprite = thisAch.incompleteImage;

        achievementsCards.Add(thisCard);
        if (lastCard != null)
            thisCard.achCard.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, lastCard.anchoredPosition.y - 180);
        lastCard = thisCard.achCard.GetComponent<RectTransform>();

        achsN++;
        //falta añadir las cosas a las listas, adaptarlo para que se pueda llamar con un for constantemente para que quede una funcion mas ordenada y se pueda 
        //usar multiples veces en vez de crear varias funciones distintas
    }

    public void CreateList()
    {
        if (achievements.Count == am.quests.Length)
        {
            for (int i = 0; i < achievements.Count; i++)
                CreateCard();
            Debug.Log("Lista de logros creada");
        }
        else
            Debug.Log("ERROR: \n Los logros percibidos y los logros que hay en el Manager NO son iguales, chequea los logros antes de volver a intentar");
    }

    public void CheckAchievements()
    {
        if (achievements.Count != am.quests.Length)
        {
            achievements.Clear();
            achievements.AddRange(am.quests);
            Debug.Log("Logros percibidos igualados a totales y logros reiniciados.");
        }
        else
        {
            Debug.Log("Logros percibidos y totales ya estan igualados. Logros reiniciados.");
        }
        for (int i = 0; i < achievements.Count; i++)
            achievements[i].Reset();
    }


    public void ResetList()
    {
        for(int i = 0; i < achsN; i++)
            DestroyImmediate(achievementsCards[i].achCard);
        listContent.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 50);
        achievements.Clear();
        achievementsCards.Clear();
        achsN = 0;
        lastCard = null;
        Debug.Log("Lista reseteada, recordá volver a chequear los achievements antes de intentar agregarlos");
        //CheckAchievements();
    }

    public void CompleteCard(Quest achCompleted)
    {
        AchievementPopUp achPopUp = GetComponent<AchievementPopUp>();
        string achName = achCompleted.questName;
        for(int i = 0; i < achievementsCards.Count; i++)
        {
            AchievementCard thisCard = achievementsCards[i];
            if(thisCard.achN == achCompleted.visibleInt)
            {
                if (achCompleted.alternativeName != "")
                {
                    thisCard.achName.text = achCompleted.alternativeName;
                    achName = achCompleted.alternativeName;
                }
                if (achCompleted.alternativeDescription != "")
                    thisCard.achDesc.text = achCompleted.alternativeDescription;
                thisCard.achImage.sprite = achCompleted.completeImage;
            }
        }
        if (!achPopUp.timer)
            achPopUp.TogglePopUp(achName, achCompleted.completeImage);
        else
            Debug.Log("se agrega a la cola");
    }
}
