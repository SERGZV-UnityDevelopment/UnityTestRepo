using UnityEngine;

public class ConsumablePurchaseButton : MonoBehaviour
{
    [SerializeField] private PurchaseService purchaseService;
    [SerializeField] private EConsumableGoods purchasedItem;
    
    public void OnBuyConsumable() => purchaseService.BuyProduct(purchasedItem);
}
