using System;
using UnityEngine;
using UnityEngine.Purchasing;

public class PurchaseService : MonoBehaviour, IStoreListener
{
    [Tooltip("Not reusable products. More suitable for disabling ads, etc.")]
    [SerializeField] private string[] nc_products;
    
    [Tooltip("Reusable goods. More suitable for buying game currency, etc.")]
    [SerializeField] private string[] c_products;

    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtenshionProvider;
    private int _currentProductIndex;
    
    public delegate void OnSuccessConsumable(PurchaseEventArgs args);
    public delegate void OnSuccessNonConsumable(PurchaseEventArgs args);
    public delegate void OnFailedPurchase(Product product, PurchaseFailureReason failureReason);
    
    /// <summary>
    /// An event that is triggered when a reusable item is successfully purchased.
    /// </summary>
    public static Action<PurchaseEventArgs> OnPurchaseConsumable;

    /// <summary>
    ///An event that is triggered when a non-reusable item is successfully purchased.
    /// </summary>
    public static Action<PurchaseEventArgs> OnPurchaseNonConsumable;
    
    /// <summary>
    /// An event that is fired when a product purchase fails.
    /// </summary>
    public static Action<Product, PurchaseFailureReason> PurchaseFailed;

    private void Awake()
    {
        InitializePurchasing();
    }

    private void InitializePurchasing()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        foreach (var s in c_products) builder.AddProduct(s, ProductType.Consumable);
        foreach (var s in nc_products) builder.AddProduct(s, ProductType.NonConsumable);
        UnityPurchasing.Initialize(this, builder);
    }
    
    /// <summary>
    /// Check if the item has been purchased.
    /// </summary>
    /// <param name="id">Индекс товара в списке.</param>
    public static bool CheckBuyState(string id)
    {
        var product = m_StoreController.products.WithID(id);
        return product.hasReceipt;
    }

    public void BuyConsumable(int index)
    {
        _currentProductIndex = index;
        BuyProductID(c_products[index]);
    }

    public void BuyNonConsumable(int index)
    {
        _currentProductIndex = index;
        BuyProductID(nc_products[index]);
    }

    void BuyProductID(string productId)
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
        if (c_products.Length > 0 && String.Equals(args.purchasedProduct.definition.id, nc_products[_currentProductIndex],
            StringComparison.Ordinal)) OnSuccessC(args);
        else if (nc_products.Length > 0 && string.Equals(args.purchasedProduct.definition.id,
            nc_products[_currentProductIndex],
            StringComparison.Ordinal)) OnSuccessNC(args);
        else Debug.Log(string.Format($"ProcessPurchase: FAIL. Unrecognized product: {args.purchasedProduct.definition.id}"));
        return PurchaseProcessingResult.Complete;
    }

    protected virtual void OnSuccessC(PurchaseEventArgs args)
    {
        if (OnPurchaseConsumable != null) OnPurchaseConsumable.Invoke(args);
        Debug.Log($"Purchased consumable item {c_products[_currentProductIndex]}");
    }

    protected virtual void OnSuccessNC(PurchaseEventArgs args)
    {
        if (OnPurchaseNonConsumable != null) OnPurchaseNonConsumable.Invoke(args);
        Debug.Log($"Purchased a one-time item {nc_products[_currentProductIndex]}");
    }

    protected virtual void OnFailedP(Product product, PurchaseFailureReason failureReason)
    {
        if (PurchaseFailed != null) PurchaseFailed.Invoke(product, failureReason);
        Debug.Log($"OnPurchaseFailed: FAIL. Product: {product.definition.storeSpecificId}, {failureReason}");
    }
    
    public void OnInitialized(IStoreController controller, IExtensionProvider extenshions)
    {
        Debug.Log("OnInitialized: PASS");

        m_StoreController = controller;
        m_StoreExtenshionProvider = extenshions;
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


