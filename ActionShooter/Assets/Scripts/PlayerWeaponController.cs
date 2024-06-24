using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    private Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();

        _player.Controlls.Character.Fire.performed += context => Shoot();
    }

    private void Shoot()
    {
        GetComponentInChildren<Animator>().SetTrigger("Fire");
    }
}
