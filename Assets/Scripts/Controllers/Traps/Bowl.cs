using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsPlatformer
{
    public class Bowl : LevelObject
    {
        private Weapon mainWeapon;
        public Bowl(Vector2Int gridPosition) : base("Bowl", gridPosition)
        {
            BasicView aim = _view.transform.Find("Aim").GetComponent<BasicView>();
            GameObject bulletPrefab = _view.transform.Find("Ammo").gameObject;
            BasicView _player = GameObject.FindGameObjectWithTag("Player").GetComponent<BasicView>();
            mainWeapon = new Weapon(new AimController(aim, _player, 5), new BarrelController(aim, new Pool(bulletPrefab, 4)));
        }

        public override void Update()
        {
            mainWeapon?.Update();
        }
    }
}
