using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsPlatformer
{
    public class Platform : LevelElement
    {
        private int _height;
        public int Height { get => _height; }

        public Platform(int length, int height)
        {
            _length = length;
            _height = height;
        }
    }
}
