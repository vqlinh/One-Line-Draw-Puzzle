using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public IAPManager iAPManager;

    private void Start(){
        iAPManager.SetupBuilder();
    }

    public void BuyHint(int pos)
    {
        AudioManager.Instance.AudioBought();
        iAPManager.HandleInitiatePurchase(pos);
    }
}
