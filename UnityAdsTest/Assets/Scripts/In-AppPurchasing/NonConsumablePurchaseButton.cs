using UnityEngine;

public class NonConsumablePurchaseButton : MonoBehaviour
{
    [SerializeField] private PurchaseService purchaseService;
    [SerializeField] private PurchaseService.ENonConsumableGoods purchasedItem;

    public void OnBuyNonConsumable() => purchaseService.BuyProduct(purchasedItem);
}
