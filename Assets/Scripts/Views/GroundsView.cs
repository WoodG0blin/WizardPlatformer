using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace WizardsPlatformer
{
    public class GroundsView : View
    {
        [SerializeField] private Tile[] _groundTiles;
        [SerializeField] private Tilemap _groundTilemap;

        public Tile[] groundTiles { get => _groundTiles; }
        public Tilemap groundTilemap { get => _groundTilemap; }
    }
}
