using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Draw_10 : MonoBehaviour
{
    [Header("Draw")]
    public Button GoInventoryBtn;
    public Button CloseBtn;
    public List<CardSlot> CardSlots = new();

    private void OnEnable()
    {
        StartCoroutine(StartDraw());
    }

    private IEnumerator StartDraw()
    {
        List<ItemData> result = InventoryManager.Instance.Draw10items();

        for (int i = 0; i < result.Count; i++)
        {
            ItemData item = result[i];

            CardSlots[i].gameObject.SetActive(true);

            CardSlots[i].SetInfo(item);

            InventoryManager.Instance.InventoryItems.Add(item);

            SoundManager.Instance.SpecialEffectSound(Consts.SpecialItem.CardDraw);

            yield return new WaitForSeconds(0.2f);
        }

        InventoryManager.Instance.SaveItems(SaveManager.Instance.UserData.PlayerInventory);

        InventoryManager.Instance.ChangeInventory();

        GoInventoryBtn.gameObject.SetActive(true);
        CloseBtn.gameObject.SetActive(true);

        GoInventoryBtn.onClick.AddListener(GoToInventory);
    }

    private void GoToInventory()
    {
        UiManager.Instance.CardWindow.SetActive(false);

        UiManager.Instance.StoreWindow.SetActive(false);

        UiManager.Instance.Inventory.ShowInventory();
    }

    private void OnDisable()
    {
        GoInventoryBtn.onClick?.RemoveAllListeners();

        GoInventoryBtn.gameObject.SetActive(false);
        CloseBtn.gameObject.SetActive(false);

        for (int i = 0; i < CardSlots.Count; i++)
        {
            CardSlots[i].gameObject.SetActive(false);
        }
    }
}
