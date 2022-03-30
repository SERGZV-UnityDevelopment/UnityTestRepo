using System;
using UnityEngine;
using UnityEngine.Purchasing;

public class PurchaseService : MonoBehaviour, IStoreListener
{
    // Reusable goods. More suitable for buying game currency, etc.
    public enum EConsumableGoods { diamond }
    // Not reusable products. More suitable for disabling ads, etc.
    public enum ENonConsumableGoods { disabling_ad }
    
    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtenshionProvider;
    private int _currentProductIndex;

    public static Action<PurchaseEventArgs> PurchaseConsumable = (args) => {};
    public static Action<PurchaseEventArgs> PurchaseNonConsumable = (args) => {};
    public static Action<Product, PurchaseFailureReason> PurchaseFailed = (product, reason) => {};

    private void Awake()
    {
        InitializePurchasing();
    }

    private void InitializePurchasing()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        
        var cArr = Enum.GetValues(typeof(EConsumableGoods)); 
        var nonCArr = Enum.GetValues(typeof(ENonConsumableGoods)); 
        
        foreach (var consumable in cArr) builder.AddProduct(consumable.ToString(), ProductType.Consumable);
        foreach (var nonConsumable in nonCArr) builder.AddProduct(nonConsumable.ToString(), ProductType.NonConsumable);
        
        UnityPurchasing.Initialize(this, builder);
    }
    
    // Check if the item has been purchased.
    public static bool CheckBuyState(string id)
    {
        var product = m_StoreController.products.WithID(id);
        return product.hasReceipt;
    }
    
    public void BuyProduct <T> (T purchasedItem) where T : Enum
    {
        _currentProductIndex = Array.IndexOf(Enum.GetValues(purchasedItem.GetType()), purchasedItem);
        BuyProductByID(purchasedItem.ToString());
    }

    private void BuyProductByID(string productId)
    {
        if (IsInitialized())
        {
            var product = m_StoreController.products.WithID(productId);

            if (product != null && product.availableToPurchase)
            {
                Debug.Log($"Purchasing product asynchronously: {product.definition.id}");
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log($"Buy (ProductID: {product.definition.id}) FAIL. Not purchasing product, either is not found or is not available for purchase");
                OnPurchaseFailed(product, PurchaseFailureReason.ProductUnavailable);
            }
        }
    }
    
    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtenshionProvider != null;
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if 
        (
            Enum.GetValues(typeof(EConsumableGoods)).Length > 0 
            && args.purchasedProduct.definition.id == Enum.GetName(typeof(EConsumableGoods), _currentProductIndex)
        )   OnSuccessC(args);
        else if 
        (
            Enum.GetValues(typeof(ENonConsumableGoods)).Length > 0 
            && args.purchasedProduct.definition.id == Enum.GetName(typeof(ENonConsumableGoods), _currentProductIndex)
        )   OnSuccessNC(args);
        else
            Debug.Log(string.Format($"ProcessPurchase: FAIL. Unrecognized product: {args.purchasedProduct.definition.id}"));
        
        return PurchaseProcessingResult.Complete;
    }

    protected virtual void OnSuccessC(PurchaseEventArgs args)
    {
        PurchaseConsumable.Invoke(args);
        Debug.Log($"Purchased consumable item {Enum.GetName(typeof(EConsumableGoods), _currentProductIndex)}");
    }

    protected virtual void OnSuccessNC(PurchaseEventArgs args)
    {
        PurchaseNonConsumable.Invoke(args);
        Debug.Log($"Purchased a one-time item {Enum.GetName(typeof(ENonConsumableGoods), _currentProductIndex)}");
    }

    protected virtual void OnFailedP(Product product, PurchaseFailureReason failureReason)
    {
        PurchaseFailed.Invoke(product, failureReason);
        Debug.Log($"OnPurchaseFailed: FAIL. Product: {product.definition.storeSpecificId}, {failureReason}");
    }
    
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");

        m_StoreController = controller;
        m_StoreExtenshionProvider = extensions;
    }
    
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log($"OnInitializeFailed InitializationFailureReason: {error}");
    }
    
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        OnFailedP(product, failureReason);
    }
}


