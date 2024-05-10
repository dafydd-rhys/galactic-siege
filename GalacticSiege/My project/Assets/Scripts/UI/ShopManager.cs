using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public TMP_Text coinUI;
    public ShopItemSO[] items;
    public ShopTemplate[] panels;
    private int coins;

    private void Start() {
        coins = PlayerPrefs.GetInt("Coins", 0);
        coinUI.text = "Coins: " + coins.ToString();

        LoadImages();
        LoadPanels();
        CheckPurchasable();
    }


    public void Purchase(ShopItemSO item) {
        ShopTemplate panel = panels[item.id];

        if (item.locked) {
            if (coins >= item.cost) {
                item.locked = false;
                coins -= item.cost;
                PlayerPrefs.SetInt("Coins", coins);
                PlayerPrefs.Save();
                coinUI.text = "Coins: " + coins.ToString();
                panel.cost.text = "Purchased";
                panel.txtPurchase.text = "Equip";

                CheckPurchasable();
            }
        } else {
            if (!item.equipped) {
                
                for (int i = 0; i < items.Length; i++) {
                    if (items[i].equipped) {
                        items[i].equipped = false;
                        panels[i].txtPurchase.text = "Equip";
                    }
                }

                item.equipped = true;
                PlayerPrefs.SetInt("Player", item.id);
                PlayerPrefs.Save();
                panel.txtPurchase.text = "Unequip";
            } 
        }  
    }

    private void LoadImages()
{
    for (int i = 0; i < items.Length; i++)
    {
        Image panelImage = panels[i].sprite.GetComponent<Image>();
        
        if (panelImage != null)
        {
            panelImage.sprite = items[i].sprite;
        }
    }
}


    private void CheckPurchasable() {
        for (int i = 0; i < items.Length; i++) {
            ShopItemSO item = items[i];
            Button purchase = panels[i].btnPurchase;
            TMP_Text cost = panels[i].cost;
            TextMeshProUGUI purchased = panels[i].txtPurchase;
            purchase.interactable = coins >= item.cost;

            if (!item.locked) {
                purchase.interactable = true;
                cost.text = "Purchased";
                purchased.text = item.equipped ? "Unequip" : "Equip";
            }
        }
    }

    public void LoadPanels() {
        for (int i = 0; i < items.Length; i++) {
            panels[i].title.text = items[i].title;
            panels[i].description.text = items[i].description;
            panels[i].cost.text = items[i].cost.ToString();
        }
    }

}
