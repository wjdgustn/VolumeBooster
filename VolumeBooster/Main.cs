using System.Reflection;
using HarmonyLib;
using VolumeBooster.MainPatch;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityModManagerNet;

namespace VolumeBooster {
    #if DEBUG
    [EnableReloading]
    #endif

    internal static class Main {
        // public static Text text;
        internal static UnityModManager.ModEntry Mod;
        private static Harmony _harmony;
        internal static bool IsEnabled { get; private set; }

        private static void Load(UnityModManager.ModEntry modEntry) {
            Mod = modEntry;
            Mod.OnToggle = OnToggle;

            #if DEBUG
            Mod.OnUnload = Stop;
            #endif
            
            ADOStartup.ModWasAdded(Mod.Info.Id);
        }

        private static bool OnToggle(UnityModManager.ModEntry modEntry, bool value) {
            IsEnabled = value;

            if (value) Start();
            else Stop(modEntry);

            return true;
        }

        private static void Start() {
            _harmony = new Harmony(Mod.Info.Id);
            _harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        private static bool Stop(UnityModManager.ModEntry modEntry) {
            _harmony.UnpatchAll(Mod.Info.Id);
            #if RELEASE
            _harmony = null;
            #endif

            return true;
        }
    }
}