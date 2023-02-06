using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace WizardsPlatformer
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private BasicView _playerView;
        [SerializeField] private AllAnimationsConfig _animationsConfig;
        [SerializeField] private Transform[] _backgrounds;
        [SerializeField] private List<GameObject> _weaponObjects;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private GameObject _bulletHeavyPrefab;
        [SerializeField] private BasicView _scareCrowView;
        [SerializeField] private BasicView _enemyView;
        [SerializeField] private Tile[] _groundTiles;
        [SerializeField] private Tilemap _groundTilemap;

        private Level _level;

        private PlayerController _player;
        private CameraController _camera;
        private Scarecrow _scareCrow;
        private WeaponFactory _weaponFactory;
        private List<Weapon> _weapons = new List<Weapon>();
        private Enemy _enemy1;

        void Awake()
        {
            _level = new Level(100, _groundTiles, _groundTilemap);

            _player = new PlayerController(_playerView);
            _player.SetPlayer(_level.GetStartPosition());
            _camera = new CameraController(_playerView, _backgrounds);

            //_enemy1 = new Enemy(_enemyView, _playerView);

            //_scareCrow = new Scarecrow(_scareCrowView);

            //_weaponFactory = new WeaponFactory(_playerView);
            //_weapons.Add(_weaponFactory.GetWeapon(WeaponFactory.AimType.ballistic, _weaponObjects[0], _bulletHeavyPrefab));
            //_weapons.Add(_weaponFactory.GetWeapon(WeaponFactory.AimType.direct, _weaponObjects[1], _bulletPrefab));
        }

        void Update()
        {
            _level.Update();

            _player.Update();
            _camera.Update();

            //_enemy1.Update();

            //_scareCrow.Update();

            //foreach(Weapon w in _weapons) w.Update();
        }
    }
}
