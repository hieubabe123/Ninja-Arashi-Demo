using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public enum TargetPanel
    {
        Upgrade,
        Shop
    }
    public PlayerScriptableObject playerData;
    public TargetPanel activePanelOnLoad = TargetPanel.Upgrade;


    [Header("---------------Current Collectible Of Player (Coin,Gem,Scroll)---------------")]

    public int currentMoney;
    public int currentGem;
    public int currentScrollPaper;

    [Header("---------------Current Skin Purchased By Player ---------------")]
    public bool isWineUrnSkinPurchased;
    public bool isSamuraiSkinPurchased;
    public bool isKnightSkinPurchased;
    public bool isThinkerSkinPurchased;
    public bool isAssasinSkinPurchased;
    public bool isGoldenSexySkinPurchased;

    [Header("---------------Current Skin In Use---------------")]
    public bool isTreeTrunkSkinInUse;
    public bool isWineUrnSkinInUse;
    public bool isSamuraiSkinInUse;
    public bool isKnightSkinInUse;
    public bool isThinkerSkinInUse;
    public bool isAssasinSkinInUse;
    public bool isGoldenSexySkinInUse;

    public enum PlayerSkinInUse
    {
        TreeTrunk,
        WineUrn,
        Samurai,
        Knight,
        Thinker,
        Assasin,
        GolderSexy
    }
    public PlayerSkinInUse currentPlayerSkinInUse = PlayerSkinInUse.TreeTrunk;

    //Seperate private stat in PlayerStats script and child stat in another script

    public int CurrentMoney
    {
        get { return currentMoney; }
        set
        {
            if (currentMoney != value)
            {
                currentMoney = value;
                if (UIForAll.instance != null)
                {
                    UIForAll.instance.currentMoneyDisplay.text = currentMoney.ToString();
                }
            }
        }
    }
    public int CurrentGem
    {
        get { return currentGem; }
        set
        {
            if (currentGem != value)
            {
                currentGem = value;
                if (UIForAll.instance != null)
                {
                    UIForAll.instance.currentGemDisplay.text = currentGem.ToString();
                }
            }
        }
    }

    public int CurrentScrollPaper
    {
        get { return currentScrollPaper; }
        set
        {
            if (currentScrollPaper != value)
            {
                currentScrollPaper = value;
                if (UIForAll.instance != null)
                {
                    UIForAll.instance.currentScrollPaperDisplay.text = currentScrollPaper.ToString();
                }
            }
        }
    }


    [Header("---------------Current Skills Level Integer---------------")]
    public int currentDashLevel;
    public int currentThrowShurikenLevel;
    public int currentHealAndShieldLevel;
    public int currentCamouflageLevel;

    [Header("---------------Curent Skill Upgrade Data---------------")]

    public DashSkillScriptableObject currentDashData;
    public ThrowShurikenScriptableObject currentThrowShurikenData;
    public HealAndShieldScriptableObject currentHealAndShieldData;
    public CamouflageScriptableObject currentCamouflageData;


    [Header("---------------Skill Upgrade Data---------------")]
    public List<DashSkillScriptableObject> dashSkillData = new List<DashSkillScriptableObject>();
    public List<ThrowShurikenScriptableObject> throwShurikenData = new List<ThrowShurikenScriptableObject>();
    public List<HealAndShieldScriptableObject> healAndShieldData = new List<HealAndShieldScriptableObject>();
    public List<CamouflageScriptableObject> camouflageSkillData = new List<CamouflageScriptableObject>();

    [Header("---------------Skin Data---------------")]
    public SkinScriptableObject treeTrunkSkin;
    public SkinScriptableObject wineUrnSkin;
    public SkinScriptableObject samuraiSkin;
    public SkinScriptableObject knightSkin;
    public SkinScriptableObject thinkerSkin;
    public SkinScriptableObject assasinSkin;
    public SkinScriptableObject goldenSexySkin;

    [Header("---------------Sprite Camouflage---------------")]
    public Sprite camouflageSprite;



    void Awake()
    {

        SaveAndLoadManager.instance.DataManager = this;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        if (!System.IO.File.Exists(Application.persistentDataPath + "/save" + ".save"))
        {
            currentMoney = playerData.Money;
            currentGem = playerData.Gem;
            currentScrollPaper = playerData.ScrollPaper;

            currentDashLevel = 0;
            currentCamouflageLevel = 0;
            currentThrowShurikenLevel = 0;
            currentHealAndShieldLevel = 0;

            isTreeTrunkSkinInUse = true;
        }
        else
        {
            SaveAndLoadManager.LoadGame();
        }

        UpgradeCurrentSkillData();
    }

    void Start()
    {
        if (UIForAll.instance != null)
        {
            UIForAll.instance.currentMoneyDisplay.text = CurrentMoney.ToString();
            UIForAll.instance.currentGemDisplay.text = CurrentGem.ToString();
            UIForAll.instance.currentScrollPaperDisplay.text = CurrentScrollPaper.ToString();
        }

        if (isTreeTrunkSkinInUse)
        {
            currentPlayerSkinInUse = PlayerSkinInUse.TreeTrunk;
        }
        else if (isWineUrnSkinInUse)
        {
            currentPlayerSkinInUse = PlayerSkinInUse.WineUrn;
        }
        else if (isSamuraiSkinInUse)
        {
            currentPlayerSkinInUse = PlayerSkinInUse.Samurai;
        }
        else if (isKnightSkinInUse)
        {
            currentPlayerSkinInUse = PlayerSkinInUse.Knight;
        }
        else if (isThinkerSkinInUse)
        {
            currentPlayerSkinInUse = PlayerSkinInUse.Thinker;
        }
        else if (isAssasinSkinInUse)
        {
            currentPlayerSkinInUse = PlayerSkinInUse.Assasin;
        }
        else if (isGoldenSexySkinInUse)
        {
            currentPlayerSkinInUse = PlayerSkinInUse.GolderSexy;
        }


    }

    private void Update()
    {

        switch (currentPlayerSkinInUse)
        {
            case PlayerSkinInUse.TreeTrunk:
                UseTreeTrunkSkin();
                break;
            case PlayerSkinInUse.WineUrn:
                UseWineUrnSkin();
                break;
            case PlayerSkinInUse.Samurai:
                UseSamuraiSkin();
                break;
            case PlayerSkinInUse.Knight:
                UseKnightSkin();
                break;
            case PlayerSkinInUse.Thinker:
                UseThinkerSkin();
                break;
            case PlayerSkinInUse.Assasin:
                UseAssasinSkin();
                break;
            case PlayerSkinInUse.GolderSexy:
                UseGoldenSexySkin();
                break;
        }
    }

    public void UpgradeCurrentSkillData()
    {
        if (dashSkillData.Count > 0)
        {
            currentDashData = dashSkillData[Mathf.Clamp(currentDashLevel, 0, dashSkillData.Count - 1)];
            if (SkillUpgradeManager.instance != null)
            {
                SkillUpgradeManager.instance.currentDashData = currentDashData;
            }
        }
        if (throwShurikenData.Count > 0)
        {
            currentThrowShurikenData = throwShurikenData[Mathf.Clamp(currentThrowShurikenLevel, 0, throwShurikenData.Count - 1)];
            if (SkillUpgradeManager.instance != null)
            {
                SkillUpgradeManager.instance.currentThrowShurikenData = currentThrowShurikenData;

            }

        }
        if (healAndShieldData.Count > 0)
        {
            currentHealAndShieldData = healAndShieldData[Mathf.Clamp(currentHealAndShieldLevel, 0, healAndShieldData.Count - 1)];
            if (SkillUpgradeManager.instance != null)
            {
                SkillUpgradeManager.instance.currentHealAndShieldData = currentHealAndShieldData;

            }
        }
        if (camouflageSkillData.Count > 0)
        {
            currentCamouflageData = camouflageSkillData[Mathf.Clamp(currentCamouflageLevel, 0, camouflageSkillData.Count - 1)];
            if (SkillUpgradeManager.instance != null)
            {
                SkillUpgradeManager.instance.currentCamouflageData = currentCamouflageData;

            }
        }
    }

    #region Using Skin
    private void UseTreeTrunkSkin()
    {
        camouflageSprite = treeTrunkSkin.SkinSprite;
    }

    private void UseWineUrnSkin()
    {
        camouflageSprite = wineUrnSkin.SkinSprite;
    }

    private void UseSamuraiSkin()
    {
        camouflageSprite = samuraiSkin.SkinSprite;
    }

    private void UseKnightSkin()
    {
        camouflageSprite = knightSkin.SkinSprite;
    }

    private void UseThinkerSkin()
    {
        camouflageSprite = thinkerSkin.SkinSprite;
    }

    private void UseAssasinSkin()
    {
        camouflageSprite = assasinSkin.SkinSprite;
    }

    private void UseGoldenSexySkin()
    {
        camouflageSprite = goldenSexySkin.SkinSprite;
    }

    #endregion

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SaveAndLoadManager.LoadGame();
    }



    #region Save And Load Player Data

    public void Save(ref PlayerSaveData data)
    {
        data.Gem = CurrentGem;
        data.Coin = CurrentMoney;
        data.ScrollPaper = CurrentScrollPaper;

        data.DashLevel = currentDashLevel;
        data.ThrowShurikenLevel = currentThrowShurikenLevel;
        data.HealAndShieldLevel = currentHealAndShieldLevel;
        data.CamouflageLevel = currentCamouflageLevel;

        data.IsWineUrnSkinPurchased = isWineUrnSkinPurchased;
        data.IsSamuraiSkinPurchased = isSamuraiSkinPurchased;
        data.IsKnightSkinPurchased = isKnightSkinPurchased;
        data.IsThinkerSkinPurchased = isThinkerSkinPurchased;
        data.IsAssasinSkinPurchased = isAssasinSkinPurchased;
        data.IsGoldenSexySkinPurchased = isGoldenSexySkinPurchased;

        data.IsTreeTrunkSkinInUse = isTreeTrunkSkinInUse;
        data.IsWineUrnSkinInUse = isWineUrnSkinInUse;
        data.IsSamuraiSkinInUse = isSamuraiSkinInUse;
        data.IsKnightSkinInUse = isKnightSkinInUse;
        data.IsThinkerSkinInUse = isThinkerSkinInUse;
        data.IsAssasinSkinInUse = isAssasinSkinInUse;
        data.IsGoldenSexySkinInUse = isGoldenSexySkinInUse;

    }

    public void Load(PlayerSaveData data)
    {
        CurrentGem = data.Gem;
        CurrentMoney = data.Coin;
        CurrentScrollPaper = data.ScrollPaper;

        currentDashLevel = data.DashLevel;
        currentThrowShurikenLevel = data.ThrowShurikenLevel;
        currentHealAndShieldLevel = data.HealAndShieldLevel;
        currentCamouflageLevel = data.CamouflageLevel;

        isWineUrnSkinPurchased = data.IsWineUrnSkinPurchased;
        isSamuraiSkinPurchased = data.IsSamuraiSkinPurchased;
        isKnightSkinPurchased = data.IsKnightSkinPurchased;
        isThinkerSkinPurchased = data.IsThinkerSkinPurchased;
        isAssasinSkinPurchased = data.IsAssasinSkinPurchased;
        isGoldenSexySkinPurchased = data.IsGoldenSexySkinPurchased;

        isTreeTrunkSkinInUse = data.IsTreeTrunkSkinInUse;
        isWineUrnSkinInUse = data.IsWineUrnSkinInUse;
        isSamuraiSkinInUse = data.IsSamuraiSkinInUse;
        isKnightSkinInUse = data.IsKnightSkinInUse;
        isThinkerSkinInUse = data.IsThinkerSkinInUse;
        isAssasinSkinInUse = data.IsAssasinSkinInUse;
        isGoldenSexySkinInUse = data.IsGoldenSexySkinInUse;

    }

    [System.Serializable]
    public struct PlayerSaveData
    {
        public int Coin;
        public int Gem;
        public int ScrollPaper;

        public int DashLevel;
        public int ThrowShurikenLevel;
        public int HealAndShieldLevel;
        public int CamouflageLevel;

        public bool IsWineUrnSkinPurchased;
        public bool IsSamuraiSkinPurchased;
        public bool IsKnightSkinPurchased;
        public bool IsThinkerSkinPurchased;
        public bool IsAssasinSkinPurchased;
        public bool IsGoldenSexySkinPurchased;

        public bool IsTreeTrunkSkinInUse;
        public bool IsWineUrnSkinInUse;
        public bool IsSamuraiSkinInUse;
        public bool IsKnightSkinInUse;
        public bool IsThinkerSkinInUse;
        public bool IsAssasinSkinInUse;
        public bool IsGoldenSexySkinInUse;
    }

    #endregion
}
