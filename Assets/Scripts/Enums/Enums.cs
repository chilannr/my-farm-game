public enum InventoryLocation
{
    player,
    chest,
    count
}




public enum ToolEffect
{
    none,       // ��Ч��
    watering,   // ��ˮ
}

public enum Direction
{
    up,     // ��
    down,   // ��
    left,   // ��
    right,  // ��
    none    // �޷���
}

public enum ItemType
{
    Seed,               // ����
    Commodity,          // ��Ʒ
    Watering_tool,      // ��ˮ����
    Hoeing_tool,        // ���ع���
    Chopping_tool,      // ��������
    Breaking_tool,      // �ƻ�����
    Reaping_tool,       // �ո��
    Collecting_tool,    // �ռ�����
    Reapable_scenary,   // ���ջ�ĳ�����Ʒ
    none,               // ������
    Count               // ����
}