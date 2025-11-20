using UnityEngine;

public class UiManager : Singleton<UiManager>
{
    // === 인벤토리 창과 업그레이드 버튼 작동 준비 ===
    [field:SerializeField]
    public InventoryUi Inventory { get; private set; }

    [field:SerializeField]
    public EnhancementtUi Enhancement { get; private set; }

    [field: SerializeField]
    public SystemUi System { get; private set; }

    [field: SerializeField]
    public MoneyUi Money { get; private set; }

    [field: SerializeField]
    public EnemyHP EnemyHP { get; private set; }

    [field: SerializeField]
    public StageUi Stage { get; private set; }

    [Header("Object")]
    public GameObject InventoryWindow;
    public GameObject EnhanceWindow;
    public GameObject SystemWindow;

    protected override bool IsDestroy => false;

}
