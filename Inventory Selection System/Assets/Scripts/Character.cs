using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using CharacterStats;

public class Character : MonoBehaviour
{
    public CharacterStat Strength;
    public CharacterStat Agility;
    public CharacterStat Intelligence;
    public CharacterStat Defence;
    public CharacterStat Damage;
    public CharacterStat HP;
    public CharacterStat Mana;
    public CharacterStat DodgeChance;
    public CharacterStat CritChance;

    public Inventory Inventory;
    public EquipmentPanel EquipmentPanel;

    [SerializeField] StatPanel statPanel;
    [SerializeField] Image draggableItem;
    [SerializeField] ItemTooltip itemTooltip;
    [SerializeField] ItemSaveManager itemSaveManager;

    private ItemSlot draggedSlot;

    private void OnValidate()
    {
        if (itemTooltip == null)
            itemTooltip = FindObjectOfType<ItemTooltip>();
    } 

    
    private void Awake()
    {
        statPanel.SetStats(Strength, Agility, Intelligence, Defence, Damage,HP,Mana,DodgeChance,CritChance);
        statPanel.UpdateStatValues();


        Inventory.OnRightClickEvent += InventoryRightClick;
        EquipmentPanel.OnRightClickEvent += EquipmentPanelRightClick;

        Inventory.OnPointerEnterEvent += ShowTooltip;
        EquipmentPanel.OnPointerEnterEvent += ShowTooltip;

        Inventory.OnPointerExitEvent += HideTooltip;
        EquipmentPanel.OnPointerExitEvent += HideTooltip;

        Inventory.OnBeginDragEvent += BeginDrag;
        EquipmentPanel.OnBeginDragEvent += BeginDrag;

        Inventory.OnEndDragEvent += EndDrag;
        EquipmentPanel.OnEndDragEvent += EndDrag;

        Inventory.OnDragEvent += Drag;
        EquipmentPanel.OnDragEvent += Drag;

        Inventory.OnDropEvent += Drop;
        EquipmentPanel.OnDropEvent += Drop;

        itemSaveManager.LoadEquipment(this);
        itemSaveManager.LoadInventory(this);

    }

    private void OnDestroy()
    {
        itemSaveManager.SaveEquipment(this);
        itemSaveManager.SaveInventory(this);
    }

    private void InventoryRightClick(ItemSlot itemSlot)
    {
        EquippableItem equippableItem = itemSlot.Item as EquippableItem;
        if(equippableItem != null)
        {
            Equip((EquippableItem)itemSlot.Item);
        }
    }

    private void EquipmentPanelRightClick(ItemSlot itemSlot)
    {
        EquippableItem equippableItem = itemSlot.Item as EquippableItem;
        if (equippableItem != null)
        {
            Unequip((EquippableItem)itemSlot.Item);
        }
    } 



    private void ShowTooltip(ItemSlot itemSlot)
     {
           EquippableItem equippableItem = itemSlot.Item as EquippableItem;
           if (equippableItem != null)
         {
             itemTooltip.ShowTooltip(equippableItem);
         }
     }
     private void HideTooltip(ItemSlot itemSlot)
     {
           itemTooltip.HideTooltip();
     }
 

    private void BeginDrag(ItemSlot itemSlot)
    {
        if (itemSlot.Item != null)
        {
            draggedSlot = itemSlot;
            draggableItem.sprite = itemSlot.Item.Icon;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.enabled = true; 

        }
    }

    private void EndDrag(ItemSlot itemSlot)
    {
        draggedSlot = null;
        draggableItem.enabled = false;
    }

    private void Drag(ItemSlot itemSlot)
    {
        if (draggableItem.enabled)
        {
            draggableItem.transform.position = Input.mousePosition;
        }
    }

    private void Drop(ItemSlot dropItemSlot)
    {
        if (draggedSlot == null) return;

        if(dropItemSlot.CanRecieveItem(draggedSlot.Item) && draggedSlot.CanRecieveItem(dropItemSlot.Item))
        {
            EquippableItem dragItem = draggedSlot.Item as EquippableItem;
            EquippableItem dropItem = dropItemSlot.Item as EquippableItem;

            if(draggedSlot is EquipmentSlot)
            {
                if (dragItem != null) dragItem.Unequip(this);
                if (dropItem != null) dropItem.Equip(this);
            }
            if (dropItemSlot is EquipmentSlot)
            {
                if (dragItem != null) dragItem.Equip(this);
                if (dropItem != null) dropItem.Unequip(this);
            }
            statPanel.UpdateStatValues();

            Item draggedItem = draggedSlot.Item;
            draggedSlot.Item = dropItemSlot.Item;
            dropItemSlot.Item = draggedItem;
        }
     
    }


    public void Equip(EquippableItem item)
    {
        if (Inventory.RemoveItem(item))
        {
            EquippableItem previousItem;
            if(EquipmentPanel.AddItem(item, out previousItem))
            {
                if(previousItem != null)
                {
                    Inventory.AddItem(previousItem);
                    previousItem.Unequip(this);
                    statPanel.UpdateStatValues();
                }
                item.Equip(this);
                statPanel.UpdateStatValues();

            }
            else
            {
                Inventory.AddItem(item);
            }
        }
    }
    public void Unequip(EquippableItem item)
    {
        if(!Inventory.IsFull() && EquipmentPanel.RemoveItem(item))
        {
            item.Unequip(this);
            statPanel.UpdateStatValues();
            Inventory.AddItem(item);
        }
    }
}
