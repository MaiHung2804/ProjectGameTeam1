//using System.Collections;
//using System.Collections.Generic;
//using System.Net;
//using UnityEngine;

//public class TestUnitConfig
//{
//    string name;
//    int health;
//    int mana;
//    int level;

//    public Đặt thông số (string name, int health, int mana, int level)
//    {
//        this.name = name;
//        this.health = health;
//        this.mana = mana;
//        this.level = level;
//    }
    
//    public virtual Lên level () // mặc định lên level. kế thừa thích sửa thì tuỳ
//    {
//        level++;
//        health += 20;
//        mana += 5;
//        Debug.Log($"{name} đã lên level {level}! Health: {health}, Mana: {mana}");
//    }

//    public hàm trả về thông số



//    Nếu cần thiết:
//        TestPlayerConfig kế thừa TestUnitConfig
//        - ...

//        TestEnemyConfig kế thừa TestUnitConfig
//        - có thể sửa config lúc lên lêl


//    UnitData:
//        - hàm lấy data từ config :  hàm trả về thông số
        



//        - trừ máu, nạp mana, hồi máu, hồi mana ...
//        - trả về kinh nghiệm khi chết .....

//        => EnemyData kế thừa UnitData: EnemyData.cs
//            + có thể có thêm hàm đặc biệt riêng EnemyData
//        =>


//    DataManager:
//        + player ... => lưu và quản lý Data của Player
//         Cookie, 


//    + quản lý Data của Enemy:



//        list enemy data




//}
