using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsPlatformer
{
    public class Weapon : Controller
    {
        private AimController _aim;
        private BarrelController _barrel;

        public Weapon(AimController aim, BarrelController barrel)
        {
            _aim = aim;
            _barrel = barrel;
        }

        public void Fire()
        {
            if(_aim.InDistance()) _barrel?.Fire(_aim.Aim());
        }

        public override void Update()
        {
            _aim.Update();
            _barrel.Update();
            Fire();
        }

        public override void Dispose()
        {
            base.Dispose();
            _aim = null;
            _barrel = null;
        }
    }

    public class WeaponFactory
    {
        public enum AimType { direct, ballistic}
        private BasicView _player;
        public WeaponFactory(BasicView player) { _player = player; }

        public Weapon GetWeapon(AimType type, GameObject weapon, GameObject bulletPrefab)
        {
            BasicView aim = weapon.transform.Find("Aim").GetComponent<BasicView>();
            return type == AimType.ballistic ?
                new Weapon(new BallisticAimController(aim, _player), new BarrelController(aim, new Pool(bulletPrefab, 10))) :
                new Weapon(new AimController(aim, _player, 5), new BarrelController(aim, new Pool(bulletPrefab, 10)));
        }
    }
}
