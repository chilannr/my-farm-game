using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : SingletonMonobehaviour<InventoryManager>
{
    private Dictionary<int, ItemDetails> itemDetilsDictionary; // ��Ʒ�����ֵ䣬���ڴ洢��Ʒ����ϸ��Ϣ
    public List<InventoryItem>[] inventoryLists; // ��Ʒ�嵥�б����飬���ڴ洢��ͬλ�õ���Ʒ�嵥

    private int[] selectedInventoryItem; // the index of the array is the inventory list, and the value is the item code
    [HideInInspector] public int[] inventoryListCapacityIntArray; // ��Ʒ�嵥�������飬���ڴ洢��ͬλ�õ���Ʒ�嵥����

    [SerializeField] private SO_ItemList itemList = null; // ��Ʒ�б��ScriptableObject

    protected override void Awake()
    {
        base.Awake();
        CreateInventoryLists();
        CreateItemDetailsDictionary();
    }

    private void CreateInventoryLists()
    {
        // ������ͬλ�õ���Ʒ�嵥�б�
        inventoryLists = new List<InventoryItem>[(int)InventoryLocation.count];
        for (int i = 0; i < (int)InventoryLocation.count; i++)
        {
            inventoryLists[i] = new List<InventoryItem>();
        }
        inventoryListCapacityIntArray = new int[(int)InventoryLocation.count];
        inventoryListCapacityIntArray[(int)InventoryLocation.player] = Settings.playerInitialInventoryCapacity; // ������ҳ�ʼ��Ʒ�嵥����
    }

    /// <summary>
    /// ����Ʒ��ӵ�ָ��λ�õ���Ʒ�嵥�������ٴ�ɾ������Ϸ����
    /// </summary>
    public void AddItem(InventoryLocation inventoryLocation, Item item, GameObject gameObjectToDelete)
    {
        AddItem(inventoryLocation, item);

        Destroy(gameObjectToDelete);
    }

    /// <summary>
    /// ����Ʒ��ӵ�ָ��λ�õ���Ʒ�嵥
    /// </summary>
    public void AddItem(InventoryLocation inventoryLocation, Item item)
    {
        int itemCode = item.ItemCode;
        List<InventoryItem> inventoryList = inventoryLists[(int)inventoryLocation];

        // �����Ʒ�嵥���Ƿ��Ѿ���������Ʒ
        int itemPosition = FindItemInInventory(inventoryLocation, itemCode);

        if (itemPosition != -1)
        {
            AddItemAtPosition(inventoryList, itemCode, itemPosition);
        }
        else
        {
            AddItemAtPosition(inventoryList, itemCode);
        }

        // ������Ʒ�����¼�
        EventHandler.CallInventoryUpdatedEvent(inventoryLocation, inventoryLists[(int)inventoryLocation]);
    }

    private void CreateItemDetailsDictionary()
    {
        // ������Ʒ�����ֵ�
        itemDetilsDictionary = new Dictionary<int, ItemDetails>();
        foreach (ItemDetails itemDetails in itemList.itemDetails)
        {
            itemDetilsDictionary.Add(itemDetails.itemCode, itemDetails);
        }
    }

    /// <summary>
    /// ��ȡָ����Ʒ�������Ʒ����
    /// </summary>
    public ItemDetails GetItemDetails(int itemCode)
    {
        ItemDetails itemDetails;
        if (itemDetilsDictionary.TryGetValue(itemCode, out itemDetails))
        {
            return itemDetails;
        }
        else
        {
            return null;
        }
    }

    ///<summary>
    /// ����Ʒ�嵥�У��� fromItem ����������Ʒ�� toItem ����������Ʒ���н���
    ///</summary>

    public void SwapInventoryItems(InventoryLocation inventoryLocation, int fromItem, int toItem)
    {
        // ��� fromItem ������ toItem �������б�Χ�ڣ��Ҳ���ͬ�Ҵ��ڵ�����
        if (fromItem < inventoryLists[(int)inventoryLocation].Count && toItem < inventoryLists[(int)inventoryLocation].Count
             && fromItem != toItem && fromItem >= 0 && toItem >= 0)
        {
            InventoryItem fromInventoryItem = inventoryLists[(int)inventoryLocation][fromItem];
            InventoryItem toInventoryItem = inventoryLists[(int)inventoryLocation][toItem];

            inventoryLists[(int)inventoryLocation][toItem] = fromInventoryItem;
            inventoryLists[(int)inventoryLocation][fromItem] = toInventoryItem;

            // ������Ʒ�嵥�Ѹ��µ��¼�
            EventHandler.CallInventoryUpdatedEvent(inventoryLocation, inventoryLists[(int)inventoryLocation]);
        }
    }
    /// <summary>
    /// ��ָ��λ�õ���Ʒ�嵥�в�����Ʒ��������Ʒ��λ�������������Ʒ�����嵥���򷵻�-1
    /// </summary>
    public int FindItemInInventory(InventoryLocation inventoryLocation, int itemCode)
    {
        List<InventoryItem> inventoryList = inventoryLists[(int)inventoryLocation];

        for (int i = 0; i < inventoryList.Count; i++)
        {
            if (inventoryList[i].itemCode == itemCode)
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// ��ָ��λ�õ���Ʒ�嵥�е�ָ��λ�������Ʒ
    /// </summary>
    private void AddItemAtPosition(List<InventoryItem> inventoryList, int itemCode, int position)
    {
        InventoryItem inventoryItem = new InventoryItem();

        int quantity = inventoryList[position].itemQuantity + 1;
        inventoryItem.itemQuantity = quantity;
        inventoryItem.itemCode = itemCode;
        inventoryList[position] = inventoryItem;

        DebugPrintInventoryList(inventoryList);
    }

    /// <summary>
    /// ��ָ��λ�õ���Ʒ�嵥ĩβ�����Ʒ
    /// </summary>
    private void AddItemAtPosition(List<InventoryItem> inventoryList, int itemCode)
    {
        InventoryItem inventoryItem = new InventoryItem();

        inventoryItem.itemCode = itemCode;
        inventoryItem.itemQuantity = 1;
        inventoryList.Add(inventoryItem);

        DebugPrintInventoryList(inventoryList);
    }
    /// <summary>
    /// ���ָ����Ʒ�嵥λ�õ�ѡ����Ʒ
    /// </summary>
    public void ClearSelectedInventoryItem(InventoryLocation inventoryLocation)
    {
        selectedInventoryItem[(int)inventoryLocation] = -1;
    }

    /// <summary>
    /// ��ָ����Ʒ�嵥λ�õ�ѡ����Ʒ����Ϊָ������Ʒ����
    /// </summary>
    public void SetSelectedInventoryItem(InventoryLocation inventoryLocation, int itemCode)
    {
        selectedInventoryItem[(int)inventoryLocation] = itemCode;
    }

    /// <summary>
    /// ����Ʒ�嵥���Ƴ���Ʒ�������䱻������λ�ô���һ����Ϸ����
    /// </summary>
    public void RemoveItem(InventoryLocation inventoryLocation, int itemCode)
    {
        List<InventoryItem> inventoryList = inventoryLists[(int)inventoryLocation];

        // �����Ʒ�嵥���Ƿ��Ѿ���������Ʒ
        int itemPosition = FindItemInInventory(inventoryLocation, itemCode);

        if (itemPosition != -1)
        {
            RemoveItemAtPosition(inventoryList, itemCode, itemPosition);
        }

        // ������Ʒ�嵥�Ѹ��µ��¼�
        EventHandler.CallInventoryUpdatedEvent(inventoryLocation, inventoryLists[(int)inventoryLocation]);
    }

    private void RemoveItemAtPosition(List<InventoryItem> inventoryList, int itemCode, int position)
    {
        InventoryItem inventoryItem = new InventoryItem();

        int quantity = inventoryList[position].itemQuantity - 1;

        if (quantity > 0)
        {
            inventoryItem.itemQuantity = quantity;
            inventoryItem.itemCode = itemCode;
            inventoryList[position] = inventoryItem;
        }
        else
        {
            inventoryList.RemoveAt(position);
        }
    }
    private void DebugPrintInventoryList(List<InventoryItem> inventoryList)
    {
        foreach (InventoryItem inventoryItem in inventoryList)
        {
            Debug.Log("��Ʒ������" + InventoryManager.Instance.GetItemDetails(inventoryItem.itemCode).itemDescription + "    ��Ʒ������" + inventoryItem.itemQuantity);
        }
        Debug.Log("******************************************************************************");
    }
}