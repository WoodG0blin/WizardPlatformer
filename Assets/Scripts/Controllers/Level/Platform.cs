using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsPlatformer
{
    public class Platform : LevelElement
    {
        private int _height;
        public int Height { get => _height; }
        private List<LevelObject> _objects;

        public Platform(int length, int height)
        {
            _length = length;
            _height = height;
            _objects = new List<LevelObject>();
        }

        public override int DrawIntoGrid(ref bool[,] squareGrid, int position)
        {
            for(int x = position; x < position + Length; x++)
            {
                squareGrid[x, Height] = true;
                squareGrid[x, Height - 1] = (x > position && x < position + Length - 1);
                squareGrid[x, Height - 2] = (x > position + 1 && x < position + Length - 2) && Random.Range(0, 100) < 40;
            }

            return position + Length;
        }

        public void Fill(int startX, int endX)
        {
            int intervals = Length / 3;
            int choice;
            for(int i = 0; i < intervals; i++)
            {
                choice = Random.Range(i, 6);
                switch(choice)
                {
                    case 0: _objects.Add(new Scarecrow(new Vector2Int(Random.Range(startX + 1, endX), Height))); break;
                    case 1: _objects.Add(new Bowl(new Vector2Int(Random.Range(startX + 1, endX), Height))); break;
                    case 2: _objects.Add(new Fireplace(new Vector2Int(Random.Range(startX + 1, endX), Height))); break;
                    case 3: _objects.Add(new Spikes(new Vector2Int(Random.Range(startX + 1, endX), Height))); break;
                    case 4: _objects.Add(new Rock(new Vector2Int(Random.Range(startX + 1, endX), Height))); break;
                    default: break;
                }
            }
            if (Length > 5) _objects.Add(new Enemy(new Vector2Int(Random.Range(startX + 1, endX), Height), 3));
        }

        public void AddObject(LevelObject obj) { _objects.Add(obj); }

        public override void DrawObjects(Vector2 screenOffset)
        {
            foreach(LevelObject obj in _objects) obj.Draw(screenOffset);
        }

        protected override void OnUpdate()
        {
            foreach (LevelObject obj in _objects) obj.Update();
        }

        protected override void OnDispose()
        {
            if(_objects != null) foreach (LevelObject obj in _objects) obj.Dispose();
        }
    }
}
