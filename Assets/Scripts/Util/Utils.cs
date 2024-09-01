using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Utils
{
   // T랑 이름 같은 오브젝트에 스크립트 추가
   public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
   {
      T component = go.GetComponent<T>();
      
      if (component == null)
         component = go.AddComponent<T>();
      
      return component;
   }

   public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
   {
      if (go == null)
         return null;

      if (recursive == false)
      {
         Transform transform = go.transform.Find(name);

         if (transform != null)
            return transform.GetComponent<T>();
      }
      else
      {
         foreach (T component in go.GetComponentsInChildren<T>())
         {
            if (string.IsNullOrEmpty(name) || component.name == name)
               return component;
         }
      }
      
      return null;
   }

   public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
   {
      Transform transform = FindChild<Transform>(go, name, recursive);

      if (transform != null)
         return transform.gameObject;

      return null;
   }

   /**
    * location 위치
    * sprtieName 찾을 이름
    */
   public static Sprite FindSprite(string location, string sprtieName)
   {
      Sprite[] sprites = Resources.LoadAll<Sprite>($"Sprites/{location}");

      foreach (Sprite sprite in sprites)
      {
         if (sprite.name == sprtieName)
         {
            return sprite;
         }
      }
      
      Debug.Log($"no {location} name {sprtieName}");
      return null;
   }
}
