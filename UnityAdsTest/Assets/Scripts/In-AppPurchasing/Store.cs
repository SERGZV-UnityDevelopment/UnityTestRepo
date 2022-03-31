using System;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI diamondsCount;
    [SerializeField] private int coins;
    
    [SerializeField] private Button nonConsumableButton;
    [SerializeField] private TextMeshProUGUI ncButtonText;

    [SerializeField] private Button subscriptionButton;
    [SerializeField] private TextMeshProUGUI sButtonText;
    
    private void Start()
    {
        PurchaseService.PurchasingsInitialized += OnPurchasingsInitialized;
        PurchaseService.PurchaseConsumable += OnPurchaseConsumable;
        PurchaseService.PurchaseNonConsumable += OnPurchaseNonConsumable;
        PurchaseService.PurchaseSubscription += OnPurchaseSubscription;
        diamondsCount.text = coins.ToString();
    }

    public void OnCloseApp()
    {
        Application.Quit();
    }

    private void OnPurchasingsInitialized()
    {
        if (PurchaseService.ProductPurchased(ENonConsumableGoods.disabling_ad)) 
            DeactivateButton(nonConsumableButton, ncButtonText);   
        
        if (PurchaseService.ProductPurchased(ESubscriptionGoods.disabling_ad_month))
            DeactivateButton(subscriptionButton, sButtonText);
    }
    
    private void OnPurchaseConsumable(PurchaseEventArgs args)
    {
        Enum.TryParse(args.purchasedProduct.definition.id, out EConsumableGoods productName);
        
        switch (productName)
        {
            case EConsumableGoods.diamond:
                coins += 10;
                diamondsCount.text = coins.ToString();
                break;
        }
    }
    
    private void OnPurchaseNonConsumable(PurchaseEventArgs args)
    {
        Enum.TryParse(args.purchasedProduct.definition.id, out ENonConsumableGoods productName);
        
        switch (productName)
        {
            case ENonConsumableGoods.disabling_ad:
                DeactivateButton(nonConsumableButton, ncButtonText);
                break;
        }
    }

    private void OnPurchaseSubscription(PurchaseEventArgs args)
    {
        Enum.TryParse(args.purchasedProduct.definition.id, out ESubscriptionGoods productName);

        switch (productName)
        {
            case ESubscriptionGoods.disabling_ad_month:
                DeactivateButton(subscriptionButton, sButtonText);
                break;
        }
    }

    private void DeactivateButton(Button buttonRoot, TextMeshProUGUI buttonText)
    {
        buttonRoot.interactable = false;
        buttonText.text = "Куплено";
    }
}
