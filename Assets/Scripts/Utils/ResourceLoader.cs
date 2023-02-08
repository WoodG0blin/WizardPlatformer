using UnityEngine;

namespace WizardsPlatformer
{
    public static class ResourceLoader
    {
        public static Sprite LoadSprite(string path) =>
            Resources.Load<Sprite>(path);

        public static GameObject LoadPrefab(string path) =>
            Resources.Load<GameObject>(path);

        public static View LoadView(string path) =>
            Resources.Load<View>(path);
    }
}
