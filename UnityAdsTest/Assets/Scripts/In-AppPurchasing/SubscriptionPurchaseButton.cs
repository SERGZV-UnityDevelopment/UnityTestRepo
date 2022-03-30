using UnityEngine;

public class SubscriptionPurchaseButton : MonoBehaviour
{
    [SerializeField] private PurchaseService purchaseService;
    [SerializeField] private ESubscriptionGoods purchasedItem;
    
    public void OnBuySubscription() => purchaseService.BuyProduct(purchasedItem);
}
