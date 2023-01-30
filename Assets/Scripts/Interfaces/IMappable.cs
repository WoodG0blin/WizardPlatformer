using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsPlatformer
{
    public interface IMappable
    {
        public MapDetail MappingDetails { get; }

        public void Draw();

    }
}
