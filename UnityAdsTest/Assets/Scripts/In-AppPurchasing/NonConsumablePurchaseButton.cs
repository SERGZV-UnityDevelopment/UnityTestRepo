using UnityEngine;

public class NonConsumablePurchaseButton : MonoBehaviour
{
    [SerializeField] private PurchaseService purchaseService;
    [SerializeField] private ENonConsumableGoods purchasedItem;

    public void OnBuyNonConsumable() => purchaseService.BuyProduct(purchasedItem);
}
