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

    private void OnPurchaseConsumable(PurchaseEventArgs args)
    {
        switch (args.purchasedProduct.definition.id)
        {
            case "diamond":
                _coins += 10;
                text.text = _coins.ToString();
                break;
        }
    }
    
    private void OnPurchaseNonConsumable(PurchaseEventArgs args)
    {
        switch (args.purchasedProduct.definition.id)
        {
            case "disabling_ads":
                _nonConsumableButton.interactable = false;
                _buttonText.text = "Куплено";
                break;
        }
    }

    
}
