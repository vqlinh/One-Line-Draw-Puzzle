using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using System;


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
    public ShopController shopController;
    public ConsumableItem[] listItems;
    const string PACK_1 = "com.onelinedrawpuzzle.pack1";
    const string PACK_2 = "com.onelinedrawpuzzle.pack2";
    const string PACK_3 = "com.onelinedrawpuzzle.pack3";
    const string PACK_4 = "com.onelinedrawpuzzle.pack4";
    const string PACK_5 = "com.onelinedrawpuzzle.pack5";
    const string PACK_6 = "com.onelinedrawpuzzle.pack6";
    const string PACK_7 = "com.onelinedrawpuzzle.pack7";
    const string PACK_8 = "com.onelinedrawpuzzle.pack8";

    IStoreController m_StoreController;
    // void Start()
    // {
    //     SetupBuilder();
    // }

    //SETUP BUILDER
    public void SetupBuilder()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        //Add All Product Items to Builder
        for (int i = 0; i < listItems.Length; i++)
        {
            builder.AddProduct(listItems[i].Id, ProductType.Consumable);
        }
        // builder.AddProduct(cItem.Id, ProductType.Consumable);
        Debug.Log("SetupBuilder");
        UnityPurchasing.Initialize(this, builder);
    }

    /** UI BUTTON EVENTs for PURCHASE **/
    public void Consumable_BtnCoin5_Pressed()
    {
        m_StoreController.InitiatePurchase(PACK_1);
    }
    public void Consumable_BtnCoin11_Pressed()
    {
        m_StoreController.InitiatePurchase(PACK_2);
    }
    public void Consumable_BtnCoin17_Pressed()
    {
        m_StoreController.InitiatePurchase(PACK_3);
    }
    public void Consumable_BtnCoin23_Pressed()
    {
        m_StoreController.InitiatePurchase(PACK_4);
    }
    public void Consumable_BtnCoin29_Pressed()
    {
        m_StoreController.InitiatePurchase(PACK_5);
    }
    public void Consumable_BtnCoin35_Pressed()
    {
        m_StoreController.InitiatePurchase(PACK_6);
    }
    public void Consumable_BtnCoin41_Pressed()
    {
        m_StoreController.InitiatePurchase(PACK_7);
    }
    public void Consumable_BtnCoin47_Pressed()
    {
        m_StoreController.InitiatePurchase(PACK_8);
    }
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        throw new System.NotImplementedException();
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        //Restrive the purchased product
        var product = purchaseEvent.purchasedProduct;
        Debug.Log("Purchase completed: " + product.definition.id);
        if (product.definition.id == PACK_1)
        {
            //Add Coins
            Debug.Log("Plus 5 coins");
            shopController.AddCoin(5);
        }
        else if (product.definition.id == PACK_2)
        {
            Debug.Log("Plus 11 coins");
            shopController.AddCoin(11);
        }
        else if (product.definition.id == PACK_3)
        {
            Debug.Log("Plus 17 coins");
            shopController.AddCoin(17);
        }
        else if (product.definition.id == PACK_4)
        {
            Debug.Log("Plus 23 coins");
            shopController.AddCoin(23);
        }
        else if (product.definition.id == PACK_5)
        {
            Debug.Log("Plus 29 coins");
            shopController.AddCoin(29);
        }
        else if (product.definition.id == PACK_6)
        {
            Debug.Log("Plus 35 coins");
            shopController.AddCoin(35);
        }
        else if (product.definition.id == PACK_7)
        {
            Debug.Log("Plus 41 coins");
            shopController.AddCoin(41);
        }
        else if (product.definition.id == PACK_8)
        {
            Debug.Log("Plus 47 coins");
            shopController.AddCoin(47);
        }
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        throw new System.NotImplementedException();
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("Init purchase success!");
        m_StoreController = controller;
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new NotImplementedException();
    }
}
