using FrameWork;
using UnityEngine;

namespace MynahMoreInfo;

public class Util
{
    public static string GetSpriteStr(string spriteName)
    {
        // return TMPTextSpriteHelper.GetStringWithTextSpriteTag(spriteName);
        return $"<sprite=\"mmiSprites\" name=\"{spriteName}\">";
    }
    
    public static MouseTipDisplayer EnsureMouseTipDisplayer(GameObject obj)
    {
        var mouseTipDisplayer = obj.GetComponent<MouseTipDisplayer>();
        if (mouseTipDisplayer != null) return mouseTipDisplayer;
        obj.AddComponent<MouseTipDisplayer>();
        mouseTipDisplayer = obj.GetComponent<MouseTipDisplayer>();

        return mouseTipDisplayer;
    }

    public static void EnableMouseTipCharacter(MouseTipDisplayer mouseTipDisplayer, int characterId, bool forceMod = false)
    {
        var type = ModEntry.MouseTipCharStyle;
        if (type != 0 || forceMod)
        {
            mouseTipDisplayer.Type = TipType.SimpleWide;
            if (mouseTipDisplayer.RuntimeParam == null)
            {
                mouseTipDisplayer.RuntimeParam = EasyPool.Get<ArgumentBox>();
                mouseTipDisplayer.RuntimeParam.Clear();
            }

            mouseTipDisplayer.RuntimeParam = new ArgumentBox();
            mouseTipDisplayer.RuntimeParam.Set("arg0", "人物浮窗加载中");
            mouseTipDisplayer.RuntimeParam.Set("arg1", "人物浮窗加载中");

            mouseTipDisplayer.RuntimeParam.Set("_mmi_charId", characterId);
            mouseTipDisplayer.enabled = true;
        }
        else
        {
            mouseTipDisplayer.Type = TipType.CharacterComplete;
            if (mouseTipDisplayer.RuntimeParam == null)
            {
                mouseTipDisplayer.RuntimeParam = EasyPool.Get<ArgumentBox>();
                mouseTipDisplayer.RuntimeParam.Clear();
            }
            // var item = Character.Instance.GetItem(Character.Instance[charDisplayData.TemplateId].TemplateId);

            mouseTipDisplayer.RuntimeParam.Set("CharId", characterId);
            mouseTipDisplayer.RuntimeParam.Set("_mmi_no_replace", true);
            mouseTipDisplayer.enabled = true;
        }
    }
}