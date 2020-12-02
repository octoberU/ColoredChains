using MelonLoader;
using System.Reflection;

namespace Modding
{
    public static class Config
    {
        public const string Category = "ColoredChains";

        public static bool Enabled;
        public static bool UseHandColors;

        public static float RightR;
        public static float RightG;
        public static float RightB;
        public static float LeftR;
        public static float LeftG;
        public static float LeftB;

        public static void RegisterConfig()
        {
            MelonPrefs.RegisterBool(Category, nameof(Enabled), true, "Enables the mod.");
            MelonPrefs.RegisterBool(Category, nameof(UseHandColors), true, "Use hand colors for chains.");

            MelonPrefs.RegisterFloat(Category, nameof(RightR), 0.0f, "Chain color. (0 to 1)");
            MelonPrefs.RegisterFloat(Category, nameof(RightG), 0.0f, "Chain color. (0 to 1)");
            MelonPrefs.RegisterFloat(Category, nameof(RightB), 0.0f, "Chain color. (0 to 1)");

            MelonPrefs.RegisterFloat(Category, nameof(LeftR), 0.0f, "Chain color. (0 to 1)");
            MelonPrefs.RegisterFloat(Category, nameof(LeftG), 0.0f, "Chain color. (0 to 1)");
            MelonPrefs.RegisterFloat(Category, nameof(LeftB), 0.0f, "Chain color. (0 to 1)");

            OnModSettingsApplied();
        }

        public static void OnModSettingsApplied()
        {
            foreach (var fieldInfo in typeof(Config).GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                if (fieldInfo.FieldType == typeof(bool))
                    fieldInfo.SetValue(null, MelonPrefs.GetBool(Category, fieldInfo.Name));
                
                if (fieldInfo.FieldType == typeof(float))
                    fieldInfo.SetValue(null, MelonPrefs.GetFloat(Category, fieldInfo.Name));
            }
        }
    }
}
