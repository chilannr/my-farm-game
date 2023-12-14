using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [ItemCodeDescription] // �Զ������ԣ�������ʾ��Ʒ���������
    [SerializeField]
    private int _itemCode; // ��Ʒ����

    private SpriteRenderer spriteRenderer; // Sprite��Ⱦ��

    public int ItemCode { get { return _itemCode; } set { _itemCode = value; } } // ��Ʒ���������

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(); // ��ȡ�������е�Sprite��Ⱦ�����
    }

    private void Start()
    {
        if (ItemCode != 0) // �����Ʒ���벻Ϊ0
        {
            Init(ItemCode); // ��ʼ����Ʒ
        }
    }

    private void Init(int itemCodeParam)
    {
        if (itemCodeParam != 0) // ����������Ʒ���벻Ϊ0
        {
            ItemCode = itemCodeParam; // ������Ʒ����
            ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails(ItemCode); // ��ȡ��Ʒ��ϸ��Ϣ
            spriteRenderer.sprite = itemDetails.itemSprite; // ����Sprite��Ⱦ���ľ���Ϊ��Ʒ�ľ���
            if (itemDetails.itemType == ItemType.Reapable_scenary) // �����Ʒ�����ǿ��ջ�ĳ�����Ʒ
            {
                gameObject.AddComponent<ItemNudge>(); // ���ItemNudge��������ڴ�����Ʒ����ת��������ײ�¼�
            }
        }
    }
}