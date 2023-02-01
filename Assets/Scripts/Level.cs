using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsPlatformer
{
    public class Level
    {
        private int _maxLength;
        private int _lenthCounter;
        private int _minPlatformLength = 3;
        private int _maxPlatformLength = 10;
        private int _minPlatformHeight = 3;
        private int _maxPlatformHeight = 10;
        private int _minGapLength = 0;
        private int _maxGapLength = 5;

        private List<LevelElement> _elements;

        public Level(int maxLength)
        {
            _maxLength = maxLength;
            _lenthCounter = 0;
            _elements = new List<LevelElement>();
            Generate();
            SetDrawingGrid();
            SetJoints();
        }

        private void Generate()
        {
            while(_lenthCounter < _maxLength)
            {
                _elements.Add(new Platform(Random.Range(_minPlatformLength, _maxPlatformLength+1), Random.Range(_minPlatformHeight, _maxPlatformHeight)));
                _elements.Add(new Gap(Random.Range(_minGapLength, _maxGapLength)));
                _lenthCounter += _elements[_elements.Count-1].Length + _elements[_elements.Count - 2].Length;
            }
            _elements.RemoveAt(_elements.Count-1);
        }

        private void SetDrawingGrid()
        {
            //set marching squares with nodes. nodes count active squares around it (except the node they're in)
        }

        private void SetJoints()
        {

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
    }
}
