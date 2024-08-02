using GameData.Domains.CombatSkill;
using HarmonyLib;
using TMPro;
using UnityEngine;

namespace MynahMoreInfo;

[HarmonyPatch]
public class EventWindowCharacterPatch
{
    [HarmonyPostfix]
    [HarmonyPatch(typeof(EventWindowCharacter), "Refresh")]
    public static void Postfix(EventWindowCharacter __instance)
    {
        if (!ModEntry.ShowEventUICharacterMouseTip) return;
        if (__instance == null) return;
        
        var transform = __instance.transform.Find("CanvasChanger/AvatarArea/ShowCharacterMenu");
        if (transform == null) return;
        
        var mou = Util.EnsureMouseTipDisplayer(transform.gameObject);
        mou.enabled = __instance.GetHasCharacter();

        if (__instance.Data == null) return;
        if (!__instance.GetHasCharacter()) return;

        var character = __instance.IsLeftCharacter
            ? __instance.Data.MainCharacter
            : __instance.Data.TargetCharacter;

        if(character == null) return;

        Util.EnableMouseTipCharacter(mou, character.CharacterId, character.AliveState != 0);
    }
}