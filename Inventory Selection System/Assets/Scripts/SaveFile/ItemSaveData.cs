using System;


[Serializable]
public class ItemSlotSaveData
{
    public string ItemID;

    public ItemSlotSaveData(string itemID)
    {
        ItemID = itemID;
    }
}

[Serializable]
public class ItemContainerSaveData
{
    public ItemSlotSaveData[] SavedSlots;

    public ItemContainerSaveData(int numItems)
    {
        SavedSlots = new ItemSlotSaveData[numItems];
    }
}