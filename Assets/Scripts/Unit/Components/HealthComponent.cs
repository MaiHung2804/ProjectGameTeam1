using UnityEngine;

/// <summary>
/// Quan ly mau, nhan sat thuong va kiem tra trang thai song/chet cua unit.
/// </summary>
public class HealthComponent : MonoBehaviour
{
    private UnitData unitData;

    public void InitComponent()
    {
        UnitBase unitBase = GetComponent<UnitBase>();
        if (unitBase == null)
        {
            Debug.LogError("HealthComponent: UnitBase component is missing on " + gameObject.name);
            return;
        }
        unitData = unitBase.GetUnitData();
    }

    public bool IsDead
    {
        get 
        { 
            return (unitData !=null) && (unitData.Hp <= 0f); 
        }
    }

    /// <param name="damage"> nhan sat thuong damage va giam.</param>
    public void TakeDamage(int damage)
    {
        if ((unitData == null) || (IsDead)) return;
        unitData.Hp = (int)(unitData.Hp - damage / unitData.Defense);

        //Debug.Log($"{gameObject.name} took {damage} damage. Remaining health: {unitData.Hp}");

        if (unitData.Hp < 0)
        {
            unitData.Hp = 0;
        }

    }
    public void Cure(int amount)
    {
        if (IsDead)
        {
            return;
        }
        unitData.Hp += amount;
        if (unitData.Hp > unitData.MaxHp)
        {
            unitData.Hp = unitData.MaxHp;
        }
    }


}