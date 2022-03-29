using System;
using UnityEngine;

public enum EConsumableGoods { Diamond }

public class ConsumablePurchaseButton : MonoBehaviour
{
    [SerializeField] private PurchaseService purchaseService;
    [SerializeField] private EConsumableGoods consumableGoods; 
    
    public void OnBuyConsumable()
    {
        var id = Array.IndexOf(Enum.GetValues(consumableGoods.GetType()), consumableGoods);
        purchaseService.BuyConsumable(id);
    }
}
