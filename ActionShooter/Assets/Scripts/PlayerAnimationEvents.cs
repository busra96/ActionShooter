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
        
    }
}
