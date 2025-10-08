using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContext 
{
    public ItemConfig config;
    public int baseAttack; // Sát thương cơ bản
    public int durability; // Số lần sử dụng còn lại
    public float currentDurability; // Độ bền hiện tại
    public int upgradeLevel; // Cấp độ nâng cấp
    public int maxDurability; // Độ bền tối đa
    public int hpRecovery; // Lượng hồi máu (dành cho vật phẩm tiêu hao)

    public ItemContext(ItemConfig config)
    {
        this.config = config;
        this.durability = 100; // Mặc định độ bền là 100
        this.upgradeLevel = 0; // Mặc định cấp độ nâng cấp là 0
        this.maxDurability = 100; // Mặc định độ bền tối đa là 100
        this.hpRecovery = 50; // Mặc định hồi máu là 50

    }
    
    public void UseItem() // Sử dụng vật phẩm
    {
        if (config.Type == ItemType.MeleeWeapon || config.Type == ItemType.RangedWeapon)
        {
            durability--;
            if (durability < 0)
            {
                durability = 0;
                Debug.Log(config.itemName + " is broken.");
            }
        }
        else if (config.Type == ItemType.Consumable)
        {
            Debug.Log("Used " + config.itemName + ", recovered " + hpRecovery + " HP.");
            // Xử lý hồi máu cho người chơi ở đây
        }
    }

    public void AddDamge(int amount) // Thêm sát thương
    {
        baseAttack += amount;
        Debug.Log("Increased " + config.itemName + " damage by " + amount + ". New Damage: " + baseAttack);
    }

}
