using HarmonyLib;

namespace VolumeBooster.MainPatch {
    [HarmonyPatch(typeof(SettingsMenu), "UpdateSetting")]

    internal static class VolumeBooster {
        private static void Prefix(PauseSettingButton setting) {
            if (setting.name == "volume") setting.maxInt = 20;
        }
    }
}