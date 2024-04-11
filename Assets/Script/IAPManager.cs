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
 //   public ShopController shopController;
    public ConsumableItem[] listItems;
    const string COIN_5 = "coin_5";
    const string COIN_11 = "coin_11";
    const string COIN_17 = "coin_17";
    const string COIN_23 = "coin_23";
    const string COIN_29 = "coin_29";
    const string COIN_35 = "coin_35";
    const string COIN_41 = "coin_41";
    const string COIN_47 = "coin_47";

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
        m_StoreController.InitiatePurchase(COIN_5);
    }
    public void Consumable_BtnCoin11_Pressed()
    {
        m_StoreController.InitiatePurchase(COIN_11);
    }
    public void Consumable_BtnCoin17_Pressed()
    {
        m_StoreController.InitiatePurchase(COIN_17);
    }
    public void Consumable_BtnCoin23_Pressed()
    {
        m_StoreController.InitiatePurchase(COIN_23);
    }
    public void Consumable_BtnCoin29_Pressed()
    {
        m_StoreController.InitiatePurchase(COIN_29);
    }
    public void Consumable_BtnCoin35_Pressed()
    {
        m_StoreController.InitiatePurchase(COIN_35);
    }
    public void Consumable_BtnCoin41_Pressed()
    {
        m_StoreController.InitiatePurchase(COIN_41);
    }
    public void Consumable_BtnCoin47_Pressed()
    {
        m_StoreController.InitiatePurchase(COIN_47);
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
        if (product.definition.id == COIN_5)
        {
            //Add Coins
            Debug.Log("Plus 5 coins");
         //   shopController.AddCoin(5);
        }
        else if (product.definition.id == COIN_11)
        {
            Debug.Log("Plus 11 coins");
      //      shopController.AddCoin(11);
        }
        else if (product.definition.id == COIN_17)
        {
            Debug.Log("Plus 17 coins");
       //     shopController.AddCoin(17);
        }
        else if (product.definition.id == COIN_23)
        {
            Debug.Log("Plus 23 coins");
        //    shopController.AddCoin(23);
        }
        else if (product.definition.id == COIN_29)
        {
            Debug.Log("Plus 29 coins");
     //       shopController.AddCoin(29);
        }
        else if (product.definition.id == COIN_35)
        {
            Debug.Log("Plus 35 coins");
     //      shopController.AddCoin(35);
        }
        else if (product.definition.id == COIN_41)
        {
            Debug.Log("Plus 41 coins");
     //       shopController.AddCoin(41);
        }
        else if (product.definition.id == COIN_47)
        {
            Debug.Log("Plus 47 coins");
      //      shopController.AddCoin(47);
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
