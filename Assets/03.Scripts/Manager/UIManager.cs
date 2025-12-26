using UnityEngine;

public class UiManager : Singleton<UiManager>
{
    // === 인벤토리 창과 업그레이드 버튼 작동 준비 ===
    [field:SerializeField]
    public InventoryUi Inventory { get; private set; }

    [field:SerializeField]
    public EnhancementUi Enhancement { get; private set; }

    [field: SerializeField]
    public StoreUi Store { get; private set; }

    [field: SerializeField]
    public SystemUi System { get; private set; }

    [field: SerializeField]
    public OwnFundValueUi Money { get; private set; }

    [field: SerializeField]
    public OwnFundValueUi EnhanceStone { get; private set; }

    [field: SerializeField]
    public EnemyHP EnemyHP { get; private set; }

    [field: SerializeField]
    public StageUi Stage { get; private set; }

    [field: SerializeField]
    public StatusUi Status { get; private set; }

    [Header("Object")]
    public GameObject InventoryWindow;
    public GameObject EnhanceWindow;
    public GameObject SystemWindow;
    public GameObject StoreWindow;
    public GameObject CardWindow;
    public GameObject DailyCheckWindow;

    protected override bool IsDestroy => false;

}
