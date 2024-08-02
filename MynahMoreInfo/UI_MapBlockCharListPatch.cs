using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using FrameWork;
using HarmonyLib;
using UnityEngine;

namespace MynahMoreInfo;

[HarmonyPatch]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "UnusedMember.Local")]
public class UI_MapBlockCharListPatch
{
    [HarmonyPatch(typeof(MapBlockCharNormal), "Refresh")]
    [HarmonyPostfix]
    static void MapBlockCharNormalRefreshPostfix(
        // bool canInteract,
        // CharacterDisplayData characterDisplayData,
        MapBlockCharNormal __instance)
    {
        if (ModEntry.MTC_MapBlockCharList == false) return;

        var charId = __instance.CharId;
        Transform transform = __instance.transform;

        EnableMouseTipChar(charId, transform);
    }

    [HarmonyPatch(typeof(MapBlockCharGrave), "Refresh")]
    [HarmonyPostfix]
    static void MapBlockCharGraveRefreshPostfix(
        // bool canInteract,
        // CharacterDisplayData characterDisplayData,
        MapBlockCharGrave __instance)
    {
        if (ModEntry.MTC_MapBlockCharList == false) return;

        var charId = __instance._graveDisplayData.Id;
        Transform transform = __instance.transform;

        EnableMouseTipChar(charId, transform);
    }

    private static void EnableMouseTipChar(int charId, Transform transform)
    {
        var trigger = charId > -1;
        var cbutton = transform.Find("Button");
        var obj = cbutton.gameObject;
        var mouseTipDisplayer = Util.EnsureMouseTipDisplayer(obj);

        if (!trigger)
        {
            // Debug.Log($"{key} {index} not trigger!");
            mouseTipDisplayer.enabled = false;
            return;
        }

        try
        {
            var characterId = charId;

            // Debug.Log($"charId: {characterId}, disp: {(charDisplayData?.FullName ?? ____graveDataDict[charIndex].NameData.FullName).GetName(charDisplayData?.Gender ?? ____graveDataList[charIndex].NameData.Gender, new Dictionary<int, string>())}");

            if (ModEntry.MTC_MapBlockCharList)
            {
                Util.EnableMouseTipCharacter(mouseTipDisplayer, characterId);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
    
}