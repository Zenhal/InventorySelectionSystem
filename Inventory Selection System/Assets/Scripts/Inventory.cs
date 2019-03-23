using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.Serialization;

public class Inventory : MonoBehaviour
{
    [SerializeField] Item[] startingItems;
    [SerializeField] Transform itemsParent;
    public ItemSlot[] ItemSlots;

    public event Action<ItemSlot> OnPointerEnterEvent;
    public event Action<ItemSlot> OnPointerExitEvent;
    public event Action<ItemSlot> OnRightClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;



    private void OnValidate()
    {
        if (itemsParent != null)
            itemsParent.GetComponentsInChildren<ItemSlot>();

        SetStartingItems();

    }

    private void Start()
    {
        for (int i = 0; i < ItemSlots.Length; i++)
        {
            ItemSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            ItemSlots[i].OnPointerExitEvent += OnPointerExitEvent;
            ItemSlots[i].OnRightClickEvent += OnRightClickEvent;
            ItemSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            ItemSlots[i].OnEndDragEvent += OnEndDragEvent;
            ItemSlots[i].OnDragEvent += OnDragEvent;
            ItemSlots[i].OnDropEvent += OnDropEvent;

        }
        

        SetStartingItems();
    }

    private void SetStartingItems()
    {
        Clear();
        foreach (Item item in startingItems)
        {
            AddItem(item.GetCopy());
        }

    }
    public bool AddItem(Item item)
    {
        for (int i = 0; i < ItemSlots.Length; i++)
        {
            if (ItemSlots[i].Item == null)
            {
                ItemSlots[i].Item = item;
                return true;
            }
        }
        return false;
    }
    public bool RemoveItem(Item item)
    {
        for (int i = 0; i < ItemSlots.Length; i++)
        {
            if (ItemSlots[i].Item == item)
            {
                ItemSlots[i].Item = null;
                return true;
            }
        }
        return false;
    }

    public Item RemoveItem(string itemID)
    {
        for (int i = 0; i < ItemSlots.Length; i++)
        {
            Item item = ItemSlots[i].Item;
            if (item != null && item.ID == itemID)
            {
                ItemSlots[i].Item = null;
                return item;
            }
        }
        return null;
    }

    public bool IsFull()
    {
        for (int i = 0; i < ItemSlots.Length; i++)
        {
            if (ItemSlots[i].Item == null)
            {
                return false;
            }
        }
        return false;

    }

    public int ItemCount(string itemID)
    {
        int number = 0;

        for (int i = 0; i < ItemSlots.Length; i++)
        {
            if (ItemSlots[i].Item.ID == itemID)
            {
                number++;
            }
        }
        return number;
    }
    public void Clear()
    {
        for (int i = 0; i < ItemSlots.Length; i++)
        {
            if (ItemSlots[i].Item != null && Application.isPlaying)
            {
                ItemSlots[i].Item.Destroy();
            }
            ItemSlots[i].Item = null;
        }
    }
}