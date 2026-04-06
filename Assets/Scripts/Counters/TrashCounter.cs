using System;
using UnityEngine;

public class TrashCounter : BaseCounter
{

    public static event EventHandler OnAnyObjectTrashedHere;
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();

            OnAnyObjectTrashedHere?.Invoke(this, EventArgs.Empty);
        }
    }
}
