using Pathfinding.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace WizardsPlatformer
{
    public class Level : Controller
    {
        private int _maxLength;
        private int _lenthCounter;
        private int _minPlatformLength = 3;
        private int _maxPlatformLength = 10;
        private int _minPlatformHeight = 3;
        private int _maxPlatformHeight = 10;
        private int _minGapLength = 1;
        private int _maxGapLength = 4;

        private List<LevelElement> _elements;

        private Tilemap _tilemapBase;
        private Dictionary<string, Tile> _tiles;

        private bool[,] _squareGrid;
        private SquaresGrid _grid;

        private Vector2 _screenOffset;

        public Level(int maxLength, Tile[] tiles, Tilemap tilemap)
        {
            _maxLength = maxLength;
            _lenthCounter = 0;
            _elements = new List<LevelElement>();

            _tiles = new Dictionary<string, Tile>();
            foreach(Tile tile in tiles) _tiles.Add(tile.name, tile);

            _tilemapBase = tilemap;

            _screenOffset = new Vector2(_tilemapBase.transform.localPosition.x + 0.5f, _tilemapBase.transform.localPosition.y + 0.5f);

            Generate();
            SetDrawingGrid();
            SetJointsAndTraps();
            Draw();
        }

        private void Generate()
        {
            while(_lenthCounter < _maxLength)
            {
                _elements.Add(new Platform(Random.Range(_minPlatformLength, _maxPlatformLength+1), Random.Range(_minPlatformHeight, _maxPlatformHeight)));
                _elements.Add(new Gap(Random.Range(_minGapLength, _maxGapLength)));
                _lenthCounter += _elements[_elements.Count-1].Length + _elements[_elements.Count - 2].Length;
            }
            _lenthCounter -= _elements[_elements.Count-1].Length;
            _elements.RemoveAt(_elements.Count-1);
        }

        private void SetDrawingGrid()
        {
            int position = 0;
            _squareGrid = new bool[_lenthCounter, _maxPlatformHeight + 3];
            foreach(LevelElement element in _elements)
            {
                position = element.DrawIntoGrid(ref _squareGrid, position);
            }
            _grid = new SquaresGrid(_squareGrid);
        }

        private void SetJointsAndTraps()
        {
            int position = 0;
            for(int i = 0; i < _elements.Count; i++)
            {
                if (_elements[i] is Gap gap) gap.SetJoint(new Vector2Int(position-1, (_elements[i-1] as Platform).Height), new Vector2Int(position + _elements[i].Length, (_elements[i+1] as Platform).Height));
                if (_elements[i] is Platform platform)
                {
                    platform.Fill(position, position + _elements[i].Length - 1);
                    if (i == _elements.Count - 1) platform.AddObject(new Portal(new Vector2Int(position + _elements[i].Length - 1, platform.Height + 2)));
                }
                position += _elements[i].Length;
            }
        }

        public void Draw()
        {
            for(int i = 0; i < _squareGrid.GetLength(0); i++)
                for(int j = 0; j < _squareGrid.GetLength(1); j++)
                {
                    if (_squareGrid[i,j]) _tilemapBase.SetTile(new Vector3Int(i, j, 0), _tiles.ContainsKey(_grid[i + 1, j + 1]) ? _tiles[_grid[i + 1, j + 1]] : _tiles["4444"]);
                }
            foreach(LevelElement element in _elements)
            {
                element.DrawObjects(_screenOffset);
            }
        }

        public LevelElement GetElement(int lengthPosition)
        {
            int count = 0;
            for(int i = 0; i < _elements.Count; i++)
            {
                count += _elements[i].Length;
                if(count >= lengthPosition) return _elements[i];
            }
            return _elements[_elements.Count - 1];
        }

        public Vector3 GetStartPosition() => new Vector3(1 + _screenOffset.x, (_elements[0] as Platform).Height + _screenOffset.y + 2f, 0);

        public override void Update()
        {
            foreach(LevelElement element in _elements)
                element.Update();
        }
    }
}
