using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using System;
using TMPro;


[System.Serializable]
public class ConsumableItem {
    public string Name;
    public string Id;
    public string desc;
    public float price;
}
[System.Serializable]
public class IAPManager : MonoBehaviour, IStoreListener
{
    public ConsumableItem[] listItems;
    public GameObject[] listTextPrice;
    public GameObject MessageSuccess;
    const string PACK_1 = "com.onelinedrawpuzzle.pack1";
    const string PACK_2 = "com.onelinedrawpuzzle.pack2";
    const string PACK_3 = "com.onelinedrawpuzzle.pack3";
    const string PACK_4 = "com.onelinedrawpuzzle.pack4";
    const string PACK_5 = "com.onelinedrawpuzzle.pack5";
    const string PACK_6 = "com.onelinedrawpuzzle.pack6";
    const string PACK_7 = "com.onelinedrawpuzzle.pack7";
    const string PACK_8 = "com.onelinedrawpuzzle.pack8";

    IStoreController m_StoreController;
    private String ConsumableId;
    public Data data;
    public Payload payload;
    public PayloadData payloadData;
    int numberHint;

    //SETUP BUILDER
    public void SetupBuilder()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        //Add All Product Items to Builder
        for (int i = 0; i < listItems.Length; i++)
        {
            builder.AddProduct(listItems[i].Id, ProductType.Consumable);
        }
        Debug.Log("SetupBuilder");
        UnityPurchasing.Initialize(this, builder);
    }

    /** UI BUTTON EVENTs for PURCHASE **/
    public void HandleInitiatePurchase(int posPack)
    {
        String productId = PACK_1;
      switch (posPack)
        {
            case 2:
                productId = PACK_2;
                break;
            case 3:  productId = PACK_3;
                break;
            case 4:  productId = PACK_4;
                break;
            case 5:  productId = PACK_5;
                break;
            case 6:  productId = PACK_6;
                break;
            case 7:  productId = PACK_7;
                break;
            case 8: productId = PACK_8;
                break;
        }
      SetHandlePurchase(productId);
    }

    private void SetHandlePurchase(String pack)
    {
        ConsumableId = pack;
        m_StoreController.InitiatePurchase(pack);
    }
    
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        throw new System.NotImplementedException();
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        var product = purchaseEvent.purchasedProduct;
            var hint = 1;
            switch (product.definition.id)
            {
                case PACK_2: hint = 6;
                    break;
                case PACK_3:  hint = 13;
                    break;
                case PACK_4:  hint = 25;
                    break;
                case PACK_5:  hint = 40;
                    break;
                case PACK_6:  hint = 65;
                    break;
                case PACK_7:  hint = 90;
                    break;
                case PACK_8: hint =150 ;
                    break;
            }
            SaveGold(hint);
        return PurchaseProcessingResult.Complete;
    }
    
    private void SaveGold(int hint)
    {
        numberHint = PlayerPrefs.GetInt("NumberHint", 5);
        numberHint += hint;
        PlayerPrefs.SetInt("NumberHint", numberHint);
        PlayerPrefs.Save();
        MessageSuccess.SetActive(true);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        throw new System.NotImplementedException();
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("Init purchase success!");
        m_StoreController = controller;
        for (int i = 0; i < listItems.Length; i++)
        {
            var item = listItems[i];
            Product product = m_StoreController.products.WithID(item.Id);
            if (product != null && product.metadata != null)
            {
                Debug.Log($"Product ID: {item.Id} - Price: {product.metadata.localizedPriceString}");
                var textComponent = listTextPrice[i].GetComponent<TMP_Text>();
                textComponent.text = product.metadata.localizedPriceString;
            }
        }
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new NotImplementedException();
    }
    
    [Serializable]
    public class SkuDetails
    {
        public string productId;
        public string type;
        public string title;
        public string name;
        public string iconUrl;
        public string description;
        public string price;
        public long price_amount_micros;
        public string price_currency_code;
        public string skuDetailsToken;
    }
    
    [Serializable]
    public class Payload
    {
        public string json;
        public string signature;
        public List<SkuDetails> skuDetails;
        public PayloadData payloadData;
    }
    
    [Serializable]
    public class PayloadData
    {
        public string orderId;
        public string packageName;
        public string productId;
        public long purchaseTime;
        public int purchaseState;
        public string purchaseToken;
        public int quantity;
        public bool acknowledged;
    }

    [Serializable]
    public class Data
    {
        public string Payload;
        public string Store;
        public string TransactionID;
    }
}
