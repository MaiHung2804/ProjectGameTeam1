using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class UnitData
{
    protected string configId;
    protected int currentHp;
    protected int attackRange;

    protected UnitConfig Config => ConfigManager.Instance.GetUnitConfig(configId);

    public UnitData(UnitConfig config)
    {
        this.configId = config.Id;
        this.currentHp = config.BaseHp;
        this.attackRange = config.BaseAttackRange;
    }

    public string Id => Config.Id; 

    public int Hp { get => currentHp; set => currentHp = value; }
    public virtual int MaxHp { get => Config.BaseHp; set { } }
    public virtual Team Team { get => Config.Team; set { } } // Phan loai dong minh hay ke thu
    public virtual Skill UnitType { get => Config.UnitType; set { } } // Phan loai Unit theo ky nang. Thay cho Enemy Type truoc day
    public virtual string Name { get => Config.Name; set { } } // Ten don vi
    public virtual string Description { get => Config.Description; set { } } // Mo ta neu co
    public virtual Sprite Photo { get => Config.Photo; set { } } // Hinh anh dai dien don vi
    public virtual int Level { get => Config.BaseLevel; set { } }
    public virtual int Damage { get => Config.BaseDamage; set { } }
    public virtual int Defense { get => Config.BaseDefense; set { } }
    public virtual int MaxMana { get => Config.BaseMana; set { } }
    public virtual int Mana { get => Config.BaseMana; set { } }
    public virtual float MaxSpeed { get => Config.BaseMaxSpeed; set { } }
    public virtual int ExpReward { get => Config.ExpReward; set { } }
    public virtual int MaxGoldReward { get => Config.MaxGoldReward; set { } }
    public virtual int Gold { get => Config.BaseGold; set { } }
    public virtual int AttackRange { get => attackRange; set { } }

    //public virtual void TakeDamage(int damage)
    //{
    //    UnitConfig config = ConfigManager.Instance.GetUnitConfig(configId);
    //    currentHp -= damage - config.BaseDefense;
    //    if (currentHp < 0)
    //    {
    //        currentHp = 0;
    //    }
    //}


}
