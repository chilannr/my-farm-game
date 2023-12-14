public enum InventoryLocation
{
    player,
    chest,
    count
}




public enum ToolEffect
{
    none,       // 无效果
    watering,   // 浇水
}

public enum Direction
{
    up,     // 上
    down,   // 下
    left,   // 左
    right,  // 右
    none    // 无方向
}

public enum ItemType
{
    Seed,               // 种子
    Commodity,          // 商品
    Watering_tool,      // 浇水工具
    Hoeing_tool,        // 耕地工具
    Chopping_tool,      // 砍伐工具
    Breaking_tool,      // 破坏工具
    Reaping_tool,       // 收割工具
    Collecting_tool,    // 收集工具
    Reapable_scenary,   // 可收获的场景物品
    none,               // 无类型
    Count               // 计数
}