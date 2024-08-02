using HarmonyLib;

namespace MynahMoreInfo;

[HarmonyPatch]
public class CharacterNameClickLinkHandlerPatch
{
    /**
     * 详细经历界面的人物链接 增加人物浮窗
     */
    [HarmonyPatch(
        typeof(CharacterNameClickLinkHandler),
        nameof(CharacterNameClickLinkHandler.SetBtnInteractionAndClickListener))
    ]
    [HarmonyPostfix]
    static void SetBtnInteractionAndClickListenerPostfix(CButton btn, bool interactable, int charId)
    {
        if (!ModEntry.MTC_CharacterNameClickLink) return;
        if (btn == null) return;
        var dp = btn.GetComponent<MouseTipDisplayer>();
        if (dp == null) return;

        Util.EnableMouseTipCharacter(dp, charId, true);
    }
}