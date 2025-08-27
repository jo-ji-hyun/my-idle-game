using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    // === 인벤토리 창과 업그레이드 버튼 작동 준비 ===
    [SerializeField]
    private InventoryUi inventory;
    public InventoryUi Inventory { get { return inventory; } }

    public GameObject inventoryWindow;

    [SerializeField]
    private UpgradeUi upgrade;
    public UpgradeUi Upgrade { get { return upgrade; } }

    public GameObject upgradeWindow;

    protected override bool IsDestroy => false;

    protected override void Awake()
    {
        base.Awake();
    }
}
