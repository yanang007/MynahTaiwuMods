﻿using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
// ReSharper disable RedundantAssignment

namespace MynahMoreInfo;

public class MouseTipManagerPatch
{
    /// <summary>
    /// 旧版MouseTipManager#UpdateMouseOverObj
    /// </summary>
    public static IEnumerator UpdateMouseOverObj()
    {
        var __instance = SingletonObject.getInstance<MouseTipManager>();
        var pointerEventData = typeof(MouseTipManager).GetField("_pointerEventData", (BindingFlags)(-1))!;
        var _currMouseOverObj = typeof(MouseTipManager).GetField("_currMouseOverObj", (BindingFlags)(-1))!;
        var _raycastResults = typeof(MouseTipManager).GetField("_raycastResults", (BindingFlags)(-1))!;
        var _currMouseTipDisplayerActive = typeof(MouseTipManager).GetField("_currMouseTipDisplayerActive", (BindingFlags)(-1))!;

        Debug.Log("1");
        while (true)
        {
            var ____pointerEventData = (PointerEventData)pointerEventData.GetValue(__instance);
            var ____currMouseOverObj = (GameObject)_currMouseOverObj.GetValue(__instance);
            var ____raycastResults = (List<RaycastResult>)_raycastResults.GetValue(__instance);
            var ____currMouseTipDisplayerActive = (bool)_currMouseTipDisplayerActive.GetValue(__instance);

            var screenMousePos = (Vector2) UIManager.Instance.UiCamera.ScreenToViewportPoint(Input.mousePosition);
            var hitObj = (GameObject) null;
            if (screenMousePos.x >= 0.0 && screenMousePos.x <= 1.0 && screenMousePos.y >= 0.0 && screenMousePos.y <= 1.0)
            {
                ____pointerEventData.position = Input.mousePosition;
                EventSystem.current.RaycastAll(____pointerEventData, ____raycastResults);
                if (____raycastResults.Count > 0)
                    hitObj = ____raycastResults[0].gameObject;
            }
            var mouseTipDisplayerActive = hitObj != null && hitObj.GetComponent<MouseTipDisplayer>() != null && hitObj.GetComponent<MouseTipDisplayer>().enabled;
            if (hitObj != ____currMouseOverObj || mouseTipDisplayerActive != ____currMouseTipDisplayerActive)
            {
                if (____currMouseTipDisplayerActive)
                    __instance.HideTips();
                _currMouseOverObj.SetValue(__instance, hitObj);
                _currMouseTipDisplayerActive.SetValue(__instance, mouseTipDisplayerActive && hitObj.GetComponent<MouseTipDisplayer>().ShowTips());
                
                // this._currMouseOverObj = hitObj;
                // this._currMouseTipDisplayerActive = mouseTipDisplayerActive && hitObj.GetComponent<MouseTipDisplayer>().ShowTips();
            }
            yield return null;
            screenMousePos = new Vector2();
            hitObj = null;
        }

        // ReSharper disable once IteratorNeverReturns
    }

}