using UnityEngine;

public class PlayerControllerTest : MonoBehaviour
{
    public PlayerData playerData;

    void Start()
    {
        playerData = DataManager.Instance.player;

        //ApplyDataToPlayer();
    }

    void Update()
    {
      
    }
    //public void ApplyDataToPlayer()
    //{
    //    transform.position = playerData.userPosition;
    //}
    public void UppdateDataFromPlayer()
    {
        playerData.userPosition = transform.position;
    }
    public void Save()
    {
        UppdateDataFromPlayer();
        DataManager.SaveData(playerData);
    }
}
