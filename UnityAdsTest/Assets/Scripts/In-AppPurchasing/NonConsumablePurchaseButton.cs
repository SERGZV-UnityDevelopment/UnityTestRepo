using System;
using UnityEngine;

public enum ENonConsumableGoods { DisablingAds }
public class NonConsumablePurchaseButton : MonoBehaviour
{
    [SerializeField] private PurchaseService purchaseService;
    [SerializeField] private ENonConsumableGoods nonConsumableGoods; 
    
    public void OnBuyNonConsumable()
    {
        var id = Array.IndexOf(Enum.GetValues(nonConsumableGoods.GetType()), nonConsumableGoods);
        purchaseService.BuyNonConsumable(id);
    }
}
