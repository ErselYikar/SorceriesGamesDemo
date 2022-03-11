using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClosedFormText : CustomBehaviour
{
    //public UnlockData UnlockData;
    public Unlockable Unlockable;
    public TMP_Text PriceText;
    private int mRemainingMoneyToUnlock;
    private int mRequiredMoneyToUnlock;
    private IEnumerator mDecreasePlayerMoneyAndRemainingMoneyToUnlockCoroutine;

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);
        InitializeWithCustomOptions();
    }

    private void InitializeWithCustomOptions()
    {
        mRequiredMoneyToUnlock = Unlockable.UnlockData.Price;
        mRemainingMoneyToUnlock = mRequiredMoneyToUnlock;
        PriceText.SetText(mRemainingMoneyToUnlock.ToString());
        mDecreasePlayerMoneyAndRemainingMoneyToUnlockCoroutine = DecreasePlayerMoneyAndRemainingMoneyToUnlock();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TAGS.Player))
        {
            if (HasMoney())
            {
               StartCoroutine(mDecreasePlayerMoneyAndRemainingMoneyToUnlockCoroutine);
            }
        }
    }
    public bool HasMoney()
    {
        if (GameManager.PlayerManager.GetTotalCoinCount() > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TAGS.Player))
        {
            if (mDecreasePlayerMoneyAndRemainingMoneyToUnlockCoroutine != null)
                StopCoroutine(mDecreasePlayerMoneyAndRemainingMoneyToUnlockCoroutine);
        }
    }
    private IEnumerator DecreasePlayerMoneyAndRemainingMoneyToUnlock()
    {
        yield return new WaitForSeconds(0.2f);

        for (int coin = mRemainingMoneyToUnlock; coin > 0; coin--)
        {
            
            if ((HasMoney()) && mRemainingMoneyToUnlock > 0)
            {
                
                mRemainingMoneyToUnlock -= 1;
                PriceText.SetText(mRemainingMoneyToUnlock.ToString());
                Unlockable.SaveData();
            }

            yield return new WaitForSeconds(0.1f);
            
        }
    }
}
