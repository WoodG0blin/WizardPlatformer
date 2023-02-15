using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace WizardsPlatformer
{
    public class Bowl : LevelObject
    {
        private Weapon mainWeapon;
        public Bowl(Vector2Int gridPosition) : base("Bowl", gridPosition)
        {
            BasicView aim = _view.transform.Find("Aim").GetComponent<BasicView>();
            GameObject bulletPrefab = _view.transform.Find("Ammo").gameObject;
            mainWeapon = new Weapon(new AimController(aim, 5), new BarrelController(aim, new Pool(bulletPrefab, 4)));
        }

        protected override void OnUpdate()
        {
            mainWeapon?.Update();
        }
    }
}
