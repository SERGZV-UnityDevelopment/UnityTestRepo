using System;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    
    [SerializeField] private int _coins;
    [SerializeField] private Button _nonConsumableButton;
    [SerializeField] private TextMeshProUGUI _buttonText;
    
    private void Start()
    {
        PurchaseService.PurchaseConsumable += OnPurchaseConsumable;
        PurchaseService.PurchaseNonConsumable += OnPurchaseNonConsumable;
        text.text = _coins.ToString();
    }

    public void OnCloseApp()
    {
        Application.Quit();
    }
    
    private void OnPurchaseConsumable(PurchaseEventArgs args)
    {
        Enum.TryParse(args.purchasedProduct.definition.id, out PurchaseService.EConsumableGoods productName);
        
        switch (productName)
        {
            case PurchaseService.EConsumableGoods.diamond:
                _coins += 10;
                text.text = _coins.ToString();
                break;
        }
    }
    
    private void OnPurchaseNonConsumable(PurchaseEventArgs args)
    {
        Enum.TryParse(args.purchasedProduct.definition.id, out PurchaseService.ENonConsumableGoods productName);
        
        switch (productName)
        {
            case PurchaseService.ENonConsumableGoods.disabling_ad:
                _nonConsumableButton.interactable = false;
                _buttonText.text = "Куплено";
                break;
        }
    }
}
