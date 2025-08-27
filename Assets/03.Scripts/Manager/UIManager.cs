using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    // === �κ��丮 â�� ���׷��̵� ��ư �۵� �غ� ===
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
