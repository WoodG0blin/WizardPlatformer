using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsPlatformer
{
    public abstract class LevelElement : Controller
    {
        protected int _length;
        public int Length { get => _length; }

        public abstract int DrawIntoGrid(ref bool[,] squareGrid, int position);
        public abstract void DrawObjects(Vector2 screenOffset);
    }
}
