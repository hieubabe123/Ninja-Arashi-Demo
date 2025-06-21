using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopSkinManager : MonoBehaviour
{

    [Header("---------------Skin Data---------------")]
    public SkinScriptableObject treeTrunkSkin;
    public SkinScriptableObject wineUrnSkin;
    public SkinScriptableObject samuraiSkin;
    public SkinScriptableObject knightSkin;
    public SkinScriptableObject thinkerSkin;
    public SkinScriptableObject assasinSkin;
    public SkinScriptableObject goldenSexySkin;

    [Header("---------------Tree Trunk---------------")]
    public TMP_Text treeTrunkText;
    public TMP_Text treeTrunkPriceText;
    public Sprite treeTrunkSprite;
    public TMP_Text treeTrunkButtonText;

    [Header("---------------Wine Urn---------------")]
    public TMP_Text wineUrnText;
    public TMP_Text wineUrnPriceText;
    public Sprite wineUrnSprite;
    public TMP_Text wineUrnButtonText;
    [Header("---------------Samurai---------------")]
    public TMP_Text samuraiText;
    public TMP_Text samuraiPriceText;
    public Sprite samuraiSprite;
    public TMP_Text samuraiButtonText;

    [Header("---------------Knight---------------")]
    public TMP_Text knightText;
    public TMP_Text knightPriceText;
    public Sprite knightSprite;
    public TMP_Text knightButtonText;

    [Header("---------------Thinker---------------")]
    public TMP_Text thinkerText;
    public TMP_Text thinkerPriceText;
    public Sprite thinkerSprite;
    public TMP_Text thinkerButtonText;

    [Header("---------------Assasin---------------")]
    public TMP_Text assasinText;
    public TMP_Text assasinPriceText;
    public Sprite assasinSprite;
    public TMP_Text assasinButtonText;

    [Header("---------------Golden Sexy---------------")]
    public TMP_Text goldenSexyText;
    public TMP_Text goldenSexyPriceText;
    public Sprite goldenSexySprite;
    public TMP_Text goldenSexyButtonText;

    void Start()
    {
        treeTrunkText.text = treeTrunkSkin.SkinName;
        wineUrnText.text = wineUrnSkin.SkinName;
        samuraiText.text = samuraiSkin.SkinName;
        knightText.text = knightSkin.SkinName;
        thinkerText.text = thinkerSkin.SkinName;
        assasinText.text = assasinSkin.SkinName;
        goldenSexyText.text = goldenSexySkin.SkinName;

        CheckAllButtonText();
    }

    #region Check Button Text
    public void CheckTreeTrunkButtonText()
    {
        if (DataManager.instance != null)
        {
            treeTrunkPriceText.text = "Purchased";
            if (DataManager.instance.isTreeTrunkSkinInUse)
            {
                DataManager.instance.currentPlayerSkinInUse = DataManager.PlayerSkinInUse.TreeTrunk;
                treeTrunkButtonText.text = "In Use";
            }
            else
            {
                treeTrunkButtonText.text = "Use";
            }
        }
    }
    public void CheckWineUrnButtonText()
    {
        if (DataManager.instance != null)
        {
            wineUrnButtonText.text = "Purchased";
            if (DataManager.instance.isWineUrnSkinPurchased)
            {
                wineUrnPriceText.text = "Purchased";
                if (DataManager.instance.isWineUrnSkinInUse)
                {
                    DataManager.instance.currentPlayerSkinInUse = DataManager.PlayerSkinInUse.WineUrn;
                    wineUrnButtonText.text = "In Use";
                }
                else
                {
                    wineUrnButtonText.text = "Use";
                }
            }
            else
            {
                wineUrnButtonText.text = wineUrnSkin.Cost.ToString();
                wineUrnPriceText.text = wineUrnSkin.Cost.ToString();
            }
        }
    }

    public void CheckSamuraiButtonText()
    {
        samuraiPriceText.text = "Purchased";
        if (DataManager.instance.isSamuraiSkinPurchased)
        {
            if (DataManager.instance.isSamuraiSkinInUse)
            {
                DataManager.instance.currentPlayerSkinInUse = DataManager.PlayerSkinInUse.Samurai;
                samuraiButtonText.text = "In Use";
            }
            else
            {
                samuraiButtonText.text = "Use";
            }
        }
        else
        {
            samuraiButtonText.text = samuraiSkin.Cost.ToString();
            samuraiPriceText.text = samuraiSkin.Cost.ToString();
        }
    }

    public void CheckKnightButtonText()
    {
        if (DataManager.instance != null)
        {
            knightPriceText.text = "Purchased";
            if (DataManager.instance.isKnightSkinPurchased)
            {
                if (DataManager.instance.isKnightSkinInUse)
                {
                    DataManager.instance.currentPlayerSkinInUse = DataManager.PlayerSkinInUse.Knight;
                    knightButtonText.text = "In Use";
                }
                else
                {
                    knightButtonText.text = "Use";
                }
            }
            else
            {
                knightButtonText.text = knightSkin.Cost.ToString();
                knightPriceText.text = knightSkin.Cost.ToString();
            }
        }
    }

    public void CheckThinkerButtonText()
    {
        if (DataManager.instance != null)
        {
            thinkerPriceText.text = "Purchased";
            if (DataManager.instance.isThinkerSkinPurchased)
            {
                if (DataManager.instance.isThinkerSkinInUse)
                {
                    DataManager.instance.currentPlayerSkinInUse = DataManager.PlayerSkinInUse.Thinker;
                    thinkerButtonText.text = "In Use";
                }
                else
                {
                    thinkerButtonText.text = "Use";
                }
            }
            else
            {
                thinkerButtonText.text = thinkerSkin.Cost.ToString();
                thinkerPriceText.text = thinkerSkin.Cost.ToString();
            }
        }
    }

    public void CheckAssasinButtonText()
    {
        if (DataManager.instance != null)
        {
            assasinPriceText.text = "Purchased";
            if (DataManager.instance.isAssasinSkinPurchased)
            {
                if (DataManager.instance.isAssasinSkinInUse)
                {
                    DataManager.instance.currentPlayerSkinInUse = DataManager.PlayerSkinInUse.Assasin;
                    assasinButtonText.text = "In Use";
                }
                else
                {
                    assasinButtonText.text = "Use";
                }
            }
            else
            {
                assasinButtonText.text = assasinSkin.Cost.ToString();
                assasinPriceText.text = assasinSkin.Cost.ToString();
            }
        }
    }

    public void CheckGoldenSexyButtonText()
    {
        if (DataManager.instance != null)
        {
            goldenSexyPriceText.text = "Purchased";
            if (DataManager.instance.isGoldenSexySkinPurchased)
            {
                if (DataManager.instance.isGoldenSexySkinInUse)
                {
                    DataManager.instance.currentPlayerSkinInUse = DataManager.PlayerSkinInUse.GolderSexy;
                    goldenSexyButtonText.text = "In Use";
                }
                else
                {
                    goldenSexyButtonText.text = "Use";
                }
            }
            else
            {
                goldenSexyButtonText.text = goldenSexySkin.Cost.ToString();
                goldenSexyPriceText.text = goldenSexySkin.Cost.ToString();
            }
        }
    }

    private void CheckAllButtonText()
    {
        CheckTreeTrunkButtonText();
        CheckWineUrnButtonText();
        CheckSamuraiButtonText();
        CheckKnightButtonText();
        CheckThinkerButtonText();
        CheckAssasinButtonText();
        CheckGoldenSexyButtonText();
    }
    #endregion

    #region Button Press Methods
    public void OnTreeTrunkButtonPress()
    {
        if (DataManager.instance != null)
        {
            if (!DataManager.instance.isTreeTrunkSkinInUse)
            {
                DataManager.instance.isTreeTrunkSkinInUse = true;
                DataManager.instance.isWineUrnSkinInUse = false;
                DataManager.instance.isSamuraiSkinInUse = false;
                DataManager.instance.isKnightSkinInUse = false;
                DataManager.instance.isThinkerSkinInUse = false;
                DataManager.instance.isAssasinSkinInUse = false;
                DataManager.instance.isGoldenSexySkinInUse = false;

                CheckAllButtonText();
            }
        }
    }

    public void OnWineUrnButtonPress()
    {
        if (DataManager.instance != null)
        {
            if (DataManager.instance.isWineUrnSkinPurchased)
            {
                if (!DataManager.instance.isWineUrnSkinInUse)
                {
                    DataManager.instance.isTreeTrunkSkinInUse = false;
                    DataManager.instance.isWineUrnSkinInUse = true;
                    DataManager.instance.isSamuraiSkinInUse = false;
                    DataManager.instance.isKnightSkinInUse = false;
                    DataManager.instance.isThinkerSkinInUse = false;
                    DataManager.instance.isAssasinSkinInUse = false;
                    DataManager.instance.isGoldenSexySkinInUse = false;

                    CheckAllButtonText();
                }
            }
            else
            {
                if (DataManager.instance.CurrentMoney >= wineUrnSkin.Cost)
                {
                    DataManager.instance.CurrentMoney -= wineUrnSkin.Cost;
                    DataManager.instance.isWineUrnSkinPurchased = true;
                    CheckWineUrnButtonText();
                }
            }
            SaveAndLoadManager.SaveGame();
        }
    }

    public void OnSamuraiButtonPress()
    {
        if (DataManager.instance != null)
        {
            if (DataManager.instance.isSamuraiSkinPurchased)
            {
                if (!DataManager.instance.isSamuraiSkinInUse)
                {
                    DataManager.instance.isTreeTrunkSkinInUse = false;
                    DataManager.instance.isWineUrnSkinInUse = false;
                    DataManager.instance.isSamuraiSkinInUse = true;
                    DataManager.instance.isKnightSkinInUse = false;
                    DataManager.instance.isThinkerSkinInUse = false;
                    DataManager.instance.isAssasinSkinInUse = false;
                    DataManager.instance.isGoldenSexySkinInUse = false;

                    CheckAllButtonText();
                }
            }
            else
            {
                if (DataManager.instance.CurrentMoney >= samuraiSkin.Cost)
                {
                    DataManager.instance.CurrentMoney -= samuraiSkin.Cost;
                    DataManager.instance.isSamuraiSkinPurchased = true;
                    CheckSamuraiButtonText();
                }
            }
            SaveAndLoadManager.SaveGame();
        }
    }

    public void OnKnightButtonPress()
    {
        if (DataManager.instance != null)
        {
            if (DataManager.instance.isKnightSkinPurchased)
            {
                if (!DataManager.instance.isKnightSkinInUse)
                {
                    DataManager.instance.isTreeTrunkSkinInUse = false;
                    DataManager.instance.isWineUrnSkinInUse = false;
                    DataManager.instance.isSamuraiSkinInUse = false;
                    DataManager.instance.isKnightSkinInUse = true;
                    DataManager.instance.isThinkerSkinInUse = false;
                    DataManager.instance.isAssasinSkinInUse = false;
                    DataManager.instance.isGoldenSexySkinInUse = false;

                    CheckAllButtonText();
                }
            }
            else
            {
                if (DataManager.instance.CurrentMoney >= knightSkin.Cost)
                {
                    DataManager.instance.CurrentMoney -= knightSkin.Cost;
                    DataManager.instance.isKnightSkinPurchased = true;
                    CheckKnightButtonText();
                }
            }
            SaveAndLoadManager.SaveGame();
        }
    }

    public void OnThinkerButtonPress()
    {
        if (DataManager.instance != null)
        {
            if (DataManager.instance.isThinkerSkinPurchased)
            {
                if (!DataManager.instance.isThinkerSkinInUse)
                {
                    DataManager.instance.isTreeTrunkSkinInUse = false;
                    DataManager.instance.isWineUrnSkinInUse = false;
                    DataManager.instance.isSamuraiSkinInUse = false;
                    DataManager.instance.isKnightSkinInUse = false;
                    DataManager.instance.isThinkerSkinInUse = true;
                    DataManager.instance.isAssasinSkinInUse = false;
                    DataManager.instance.isGoldenSexySkinInUse = false;

                    CheckAllButtonText();
                }
            }
            else
            {
                if (DataManager.instance.CurrentMoney >= thinkerSkin.Cost)
                {
                    DataManager.instance.CurrentMoney -= thinkerSkin.Cost;
                    DataManager.instance.isThinkerSkinPurchased = true;
                    CheckThinkerButtonText();
                }
            }
            SaveAndLoadManager.SaveGame();
        }
    }

    public void OnAssasinButtonPress()
    {
        if (DataManager.instance != null)
        {
            if (DataManager.instance.isAssasinSkinPurchased)
            {
                if (!DataManager.instance.isAssasinSkinInUse)
                {
                    DataManager.instance.isTreeTrunkSkinInUse = false;
                    DataManager.instance.isWineUrnSkinInUse = false;
                    DataManager.instance.isSamuraiSkinInUse = false;
                    DataManager.instance.isKnightSkinInUse = false;
                    DataManager.instance.isThinkerSkinInUse = false;
                    DataManager.instance.isAssasinSkinInUse = true;
                    DataManager.instance.isGoldenSexySkinInUse = false;

                    CheckAllButtonText();
                }
            }
            else
            {
                if (DataManager.instance.CurrentMoney >= assasinSkin.Cost)
                {
                    DataManager.instance.CurrentMoney -= assasinSkin.Cost;
                    DataManager.instance.isAssasinSkinPurchased = true;
                    CheckAssasinButtonText();
                }
            }
            SaveAndLoadManager.SaveGame();
        }
    }

    public void OnGoldenSexyButtonPress()
    {
        if (DataManager.instance != null)
        {
            if (DataManager.instance.isGoldenSexySkinPurchased)
            {
                if (!DataManager.instance.isGoldenSexySkinInUse)
                {
                    DataManager.instance.isTreeTrunkSkinInUse = false;
                    DataManager.instance.isWineUrnSkinInUse = false;
                    DataManager.instance.isSamuraiSkinInUse = false;
                    DataManager.instance.isKnightSkinInUse = false;
                    DataManager.instance.isThinkerSkinInUse = false;
                    DataManager.instance.isAssasinSkinInUse = false;
                    DataManager.instance.isGoldenSexySkinInUse = true;

                    CheckAllButtonText();
                }
            }
            else
            {
                if (DataManager.instance.CurrentMoney >= goldenSexySkin.Cost)
                {
                    DataManager.instance.CurrentMoney -= goldenSexySkin.Cost;
                    DataManager.instance.isGoldenSexySkinPurchased = true;
                    CheckGoldenSexyButtonText();
                }
            }
            SaveAndLoadManager.SaveGame();
        }
    }

    #endregion
}
