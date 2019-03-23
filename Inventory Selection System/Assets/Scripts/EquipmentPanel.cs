using System;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
    public EquipmentSlot[] EquipmentSlots;
    [SerializeField] Transform equipmentSlotsParent;
    
    public event Action<ItemSlot> OnRightClickEvent;
    public event Action<ItemSlot> OnPointerEnterEvent;
    public event Action<ItemSlot> OnPointerExitEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;


    public void Start()
    {
        for (int i = 0; i < EquipmentSlots.Length; i++)
        {
            EquipmentSlots[i].OnRightClickEvent += OnRightClickEvent;
            EquipmentSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            EquipmentSlots[i].OnPointerExitEvent += OnPointerExitEvent;
            EquipmentSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            EquipmentSlots[i].OnEndDragEvent += OnEndDragEvent;
            EquipmentSlots[i].OnDragEvent += OnDragEvent;
            EquipmentSlots[i].OnDropEvent += OnDropEvent;
        }
    }
    private void OnValidate()
    {
        EquipmentSlots = equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
    }


    public bool AddItem(EquippableItem item, out EquippableItem previousItem)
    {
        int lastFoundSlotIndex = -1;
        int i = 0;
        for (; i < EquipmentSlots.Length; i++)
        {
            if (EquipmentSlots[i].EquipmentType == item.EquipmentType)
            {
                lastFoundSlotIndex = i;

                if (EquipmentSlots[i].Item == null)
                    break;
            }
        }

        if (lastFoundSlotIndex >= 0)
        {
            previousItem = (EquippableItem)EquipmentSlots[lastFoundSlotIndex].Item;
            EquipmentSlots[lastFoundSlotIndex].Item = item;
            return true;
        }

        previousItem = null;
        return false;
    }
    



    public bool RemoveItem(EquippableItem item)
    {
        for (int i = 0; i < EquipmentSlots.Length; i++)
        {
            if (EquipmentSlots[i].Item == item)
            {
                EquipmentSlots[i].Item = null;
                return true;
            }
        }
        return false;
    }
}
