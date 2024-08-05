using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private WeaponVisualController _visualController;

    private void Start()
    {
        _visualController = GetComponentInParent<WeaponVisualController>();
    }

    public void ReloadIsOver()
    {
        _visualController.ReturnRigWeighthToOne();
        
        //refill - bullets
    }

    public void ReturnRig()
    {
        _visualController.ReturnRigWeighthToOne();
        _visualController.ReturnWeighthToLeftHandIK();
    }
    
    public void WeaponGrabIsOver()
    {
        _visualController.SetBusyGrabbingWeaponTo(false);
    }
}
