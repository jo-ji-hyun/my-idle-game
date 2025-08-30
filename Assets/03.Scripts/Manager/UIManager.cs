using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    // === �κ��丮 â�� ���׷��̵� ��ư �۵� �غ� ===
    [SerializeField]
    private InventoryUi inventory;
    public InventoryUi Inventory { get { return inventory; } }

    public GameObject InventoryWindow;
    public GameObject EnhanceWindow;

    // === UI�� ���� ===
    [SerializeField]
    private MoneyUi money;
    public MoneyUi Money { get { return money; } }

    [SerializeField]
    private EnemyHP enemyhp;
    public EnemyHP EnemyHP { get { return enemyhp; } }

    [SerializeField]
    private StageUi stage;
    public StageUi Stage { get { return stage; } }



    protected override bool IsDestroy => false;

    protected override void Awake()
    {
        base.Awake();
    }

}
