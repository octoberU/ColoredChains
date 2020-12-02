using Harmony;
using MelonLoader;
using Modding;
using System;
using UnityEngine;

[assembly: MelonGame("Harmonix Music Systems, Inc.", "Audica")]
[assembly: MelonInfo(typeof(ColoredChains), "Colored Chains", "0.1.0", "octo & Alternity", "https://github.com/octoberU/ModSettings")]

namespace Modding
{
    public class ColoredChains : MelonMod
    {
        public override void OnApplicationStart()
        {
            Config.RegisterConfig();
        }


        public override void OnModSettingsApplied()
        {
            Config.OnModSettingsApplied();
        }


        [HarmonyPatch(typeof(Target), "OnCreated", new Type[] { typeof(Target.TargetBehavior), typeof(Target.TargetHandType) })]
        private static class OnCreated
        {
            private static void Postfix(Target __instance, Target.TargetBehavior behavior, Target.TargetHandType handType)
            {
                if (!Config.Enabled) return;
                if (behavior == Target.TargetBehavior.Chain)
                {
                    Target target = __instance;

                    Color rightColor;
                    Color leftColor;

                    if (Config.UseHandColors)
                    {
                        rightColor = PlayerPreferences.I.GunColorRight.Get() / 2;
                        leftColor = PlayerPreferences.I.GunColorLeft.Get() / 2;
                    }
                    else
                    {
                        rightColor = new Color(Config.RightB, Config.RightG, Config.RightB);
                        leftColor = new Color(Config.LeftR, Config.LeftG, Config.LeftB);
                    }

                    if (handType == Target.TargetHandType.Right)
                    {
                        target.chainLine.startColor = rightColor;
                        target.chainLine.endColor = rightColor;
                    }
                    else if (handType == Target.TargetHandType.Left)
                    {
                        target.chainLine.startColor = leftColor;
                        target.chainLine.endColor = leftColor;
                    }
                    else
                    {
                        target.chainLine.startColor = KataConfig.I.eitherHandColor;
                        target.chainLine.endColor = KataConfig.I.eitherHandColor;
                    }
                }
            }
        }
    }
}