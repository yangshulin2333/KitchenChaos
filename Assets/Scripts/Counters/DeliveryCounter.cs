using UnityEngine;
using System.Collections.Generic;

public class DeliveryCounter : BaseCounter
{


    public static DeliveryCounter Instance { get; private set; }
    public void Awake()
    {
        Instance = this;
    }

    public override void Interact(Player player)
    {
        if(player.HasKitchenObject())
        {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                //当玩家拿着盘子时，尝试将盘子交付
                DeliveryManager.Instance.DeliveryRecipe(plateKitchenObject);





                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}
