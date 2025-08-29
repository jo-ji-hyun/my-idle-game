using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquip : MonoBehaviour
{
    public List<ItemData> EquipmentSlot;

    private void Start()
    {
        EquipmentSlot = new List<ItemData>();
    }
}
