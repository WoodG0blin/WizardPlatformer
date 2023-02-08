using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsPlatformer
{
    public class Fireplace : LevelObject
    {
        private Weapon mainWeapon;
        public Fireplace(Vector2Int gridPosition) : base("Fireplace", gridPosition)
        {
            BasicView aim = _view.transform.Find("Aim").GetComponent<BasicView>();
            GameObject bulletPrefab = _view.transform.Find("Ammo").gameObject;
            BasicView _player = GameObject.FindGameObjectWithTag("Player").GetComponent<BasicView>();
            mainWeapon = new Weapon(new AimController(aim, _player, 5), new BarrelController(aim, new Pool(bulletPrefab, 4)));
        }

        protected override void OnUpdate()
        {
            mainWeapon?.Update();
        }
    }
}
