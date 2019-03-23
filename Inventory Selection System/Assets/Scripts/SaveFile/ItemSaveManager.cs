using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemSaveManager : MonoBehaviour
{
    [SerializeField] ItemDatabase itemDatabase;

    private const string InventoryFileName = "Inventory";
    private const string EquipmentFileName = "Equipment";

    public void LoadInventory(Character character)
    {
        ItemContainerSaveData savedSlots = ItemSaveIO.LoadItems(InventoryFileName);
        if (savedSlots == null) return;

        character.Inventory.Clear();

        for(int i = 0; i < savedSlots.SavedSlots.Length; i++)
        {
            ItemSlot itemSlot = character.Inventory.ItemSlots[i];
            ItemSlotSaveData savedSlot = savedSlots.SavedSlots[i];

            if(savedSlot == null)
            { 
                itemSlot.Item = null;
                
            }
            else
            {
                itemSlot.Item = itemDatabase.GetItemCopy(savedSlot.ItemID);
                
            }
        }
    }

    public void LoadEquipment(Character character)
    {
        ItemContainerSaveData savedSlots = ItemSaveIO.LoadItems(EquipmentFileName);
        if (savedSlots == null) return;

        foreach (ItemSlotSaveData savedSlot in savedSlots.SavedSlots)
        {
            if(savedSlots == null)
            {
                continue;
            }


            Item item = itemDatabase.GetItemCopy(savedSlot.ItemID);
            character.Inventory.AddItem(item);
            character.Equip((EquippableItem)item);
        }

    }

    public void SaveInventory(Character character)
    {
        SaveItems(character.Inventory.ItemSlots, InventoryFileName);
    }

    public void SaveEquipment(Character character)
    {
        SaveItems(character.EquipmentPanel.EquipmentSlots, EquipmentFileName);
    }

    private void SaveItems(IList<ItemSlot> itemSlots, string fileName)
    {
        var saveData = new ItemContainerSaveData(itemSlots.Count);

        for(int i = 0; i < saveData.SavedSlots.Length; i++)
        {
            ItemSlot itemSlot = itemSlots[i];

            if(itemSlot.Item == null)
            {
                saveData.SavedSlots[i] = null;
            }
            else
            {
                saveData.SavedSlots[i] = new ItemSlotSaveData(itemSlot.Item.ID);
            }
        }
        ItemSaveIO.SaveItems(saveData, fileName);
    }

}
