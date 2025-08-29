using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    // === 인벤토리 창과 업그레이드 버튼 작동 준비 ===
    [SerializeField]
    private InventoryUi inventory;
    public InventoryUi Inventory { get { return inventory; } }

    public GameObject Window;

    // === UI들 연결 ===
    [SerializeField]
    private MoneyUi money;
    public MoneyUi Money { get { return money; } }

    [SerializeField]
    private EnemyHP enemyhp;
    public EnemyHP EnemyHP { get { return enemyhp; } }

    [SerializeField]
    private StageUi stage;
    public StageUi Stage { get { return stage; } }

    [Header("Windows")]
    public GameObject descriptionPanel;
    public TextMeshProUGUI descriptionTxt;

    protected override bool IsDestroy => false;

    protected override void Awake()
    {
        base.Awake();

        if (descriptionPanel != null)
        {
            descriptionPanel.SetActive(false);
        }
    }

    public void DescriptionWindow(bool x, string y)
    {
        descriptionPanel.SetActive(x);

        descriptionTxt.text = y;
    }
}
