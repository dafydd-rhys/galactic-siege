using UnityEngine;

[CreateAssetMenu(fileName ="ShopMenu", menuName = "Scriptable/NewShopItem", order = 1)]
public class ShopItemSO : ScriptableObject
{
    public bool equipped = false;
    public bool locked = true;
    public int id;
    public string title;
    public string description;
    public int cost;
    public GameObject player;
    public Sprite sprite;

}
