using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace WizardsPlatformer
{
    public class Gap : LevelElement
    {
        public Joint _joint;
        public Gap(int length)
        {
            _length = length;
        }

        public override int DrawIntoGrid(ref bool[,] squareGrid, int position)
        {
            return position + Length;
        }

        public void SetJoint(Vector2Int start, Vector2Int end)
        {
            float deltaHeight = Mathf.Abs(end.y - start.y);
            if (deltaHeight < 2)
            {
                if (Length >= 3)
                {
                    _joint = new Bridge(start, end);
                }
            }
            else if (deltaHeight < 3)
            {
                _joint = new JumpPlatform(start, end);
            }
            else
            {
                _joint = new Lift(start, end);
            }
        }

        public override void DrawObjects(Vector2 screenOffset)
        {
            _joint?.Draw(screenOffset);
        }

        public override void Update()
        {
            _joint?.Update();
        }
    }
}
