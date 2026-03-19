using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{

    public event EventHandler OnPlayerGrabbedObject;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;


    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {


            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            //Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
            //kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
        OnPlayerGrabbedObject?.Invoke(this,EventArgs.Empty);
        }
      
       
    }

   
}
