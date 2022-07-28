using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RpgDemo
{
    public static class UIBehaviourUtil
    {
        //屏幕空间转UI对象空间
        public static Vector2 ScreenPointToLocalPoint(GameObject obj, Vector2 screenPoint)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle((obj.transform as RectTransform), screenPoint, UIManager.Instance.UICamera, out pos);
            return pos;
        }

        //设置UI坐标
        public static void SetRectTransformAnchoredPosition(GameObject obj, Vector2 localPoint)
        {
            (obj.transform as RectTransform).anchoredPosition = localPoint;
        }

        //获取UI坐标
        public static Vector2 GetRectTransformAnchoredPosition(GameObject obj)
        {
            return (obj.transform as RectTransform).anchoredPosition;
        }

        //获取UI对象在屏幕空间的坐标
        public static Vector2 GetScreenPoint(GameObject obj)
        {
            return RectTransformUtility.WorldToScreenPoint(UIManager.Instance.UICamera, obj.transform.position);
        }

        //世界空间到UI空间
        public static Vector2 WorldToLocalPointInRectangle(Vector3 worldPos, RectTransform rect)
        {
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(CameraManager.Instance.MainCamera, worldPos);
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, screenPoint, UIManager.Instance.UICamera, out localPoint);
            return localPoint;
        }
        
        //世界空间到UI空间
        public static Vector2 WorldToLocalPointInRectangleCustCamera(Vector3 worldPos, RectTransform rect, Camera camera = null)
        {
            camera = null == camera ? CameraManager.Instance.MainCamera : camera;
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(camera, worldPos);
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, screenPoint, UIManager.Instance.UICamera, out localPoint);
            return localPoint;
        }

        //设置颜色
        public static void SetHexColor(Graphic img, string hexColor)
        {
            Color color;
            ColorUtility.TryParseHtmlString(hexColor, out color);
            img.color = color;
        }

        //设置字体边框颜色
        public static void SetOutlineColor(Outline text, string hexColor)
        {
            Color color;
            ColorUtility.TryParseHtmlString(hexColor, out color);
            text.effectColor = color;
        }

        //16进制字符串转rgb
        public static Color GetRGBColor(string hexColor)
        {
            Color color;
            ColorUtility.TryParseHtmlString(hexColor, out color);
            return color;
        }

        //设置颜色
        public static void SetAlpha(Graphic img, float alpha)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);
        }

        //构造一个PointerEventData对象
        public static PointerEventData CreatePointerEventData(GameObject go)
        {
            PointerEventData ed = new PointerEventData(EventSystem.current);
            ed.pointerPress = go;
            return ed;
        }

        public static int GetCurrentResolutionWidth()
        {
            return Screen.currentResolution.width;
        }

        public static int GetCurrentResolutionHeight()
        {
            return Screen.currentResolution.height;
        }

        public static int GetSystemWidth()
        {            
            return Display.main.systemWidth;
        }

        public static int GetSystemHeight()
        {
            return Display.main.systemHeight;
        }

        public static Toggle GetToggleByToggleGroup(ToggleGroup toggleGroup)
        {
            return toggleGroup.ActiveToggles().First();
        }

        public static void ForceRebuildLayoutImmediate(Graphic graphic)
        {
            UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(graphic.rectTransform);
        }

        public static void ForceRebuildLayoutImmediate(GameObject go)
        {
            UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(go.transform as RectTransform);
        }

        public static Vector3 ScreenToWorldPoint(Vector2 screenPos)
        {
           return UIManager.Instance.UICamera.ScreenToWorldPoint(screenPos);
        }

        public static void SetSpacing(HorizontalLayoutGroup group, float x)
        {
            group.spacing = x;
        }

        //设置描边
        public static void SetOutline(Outline outline, bool enable)
        {
            outline.enabled = enable;
        }

        public static void World2UIBounds(GameObject go, GameObject uiRect, out Vector2 min, out Vector2 max)
        {
            RectTransform rt = uiRect.transform as RectTransform;
            BoxCollider bc = go.GetComponent<BoxCollider>();
            Bounds b = bc.bounds;
            min = new Vector2(float.MaxValue, float.MaxValue);
            max = new Vector2(float.MinValue, float.MinValue);
            Vector3 v1 = b.min;
            Vector3 v2 = v1 + Vector3.right * b.size.x;
            Vector3 v3 = v2 + Vector3.forward * b.size.z;
            Vector3 v4 = v3 + Vector3.left * b.size.x;
            Vector3 v5 = v1 + Vector3.up * b.size.y;
            Vector3 v6 = v2 + Vector3.up * b.size.y;
            Vector3 v7 = v3 + Vector3.up * b.size.y;
            Vector3 v8 = v4 + Vector3.up * b.size.y;
            List<Vector3> vectors = new List<Vector3>();
            vectors.Add(v1);
            vectors.Add(v2);
            vectors.Add(v3);
            vectors.Add(v4);
            vectors.Add(v5);
            vectors.Add(v6);
            vectors.Add(v7);
            vectors.Add(v8);

            for(int i = 0;i < vectors.Count; i++)
            {
                Vector3 v = vectors[i];
                Vector2 uiv = WorldToLocalPointInRectangle(v, rt);
                if(uiv.x < min.x)
                {
                    min.x = uiv.x;
                }
                else if(uiv.x > max.x)
                {
                    max.x = uiv.x;
                }
                if (uiv.y < min.y)
                {
                    min.y = uiv.y;
                }
                else if (uiv.y > max.y)
                {
                    max.y = uiv.y;
                }
            }
        }
    }
}
