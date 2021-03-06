using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extentions
{
    public static Vector3 x(this Vector3 vec, float value)
    {
        return new Vector3(value, vec.y, vec.z);
    }
    public static Vector3 y(this Vector3 vec, float value)
    {
        return new Vector3(vec.x, value, vec.z);
    }
    public static Vector3 z(this Vector3 vec, float value)
    {
        return new Vector3(vec.x,vec.y,value);
    }


    /// <summary>
    /// Scales a UI element depending on a target world element.
    /// Target needs to have a collider. 
    /// </summary>
    /// <param name="self">Element to be scaled</param>
    /// <param name="target">Target element</param>
    /// <param name="scaleFactor">Amount of scaling to be done</param>
    


    /// <returns></returns>
    public static RectTransform ScaleWithTarget(this RectTransform self, Transform target, 
        float scaleFactor = 1f, bool lockRatio = true)
    {
        Vector3 center = target.position;

        Vector3 top = target.position.y(
            target.position.y +
            target.gameObject.GetComponent<Collider>().bounds.extents.y);


        Vector3 screenCenter = Camera.main.WorldToScreenPoint(center);
        Vector3 screenTop = Camera.main.WorldToScreenPoint(top);

        float x = (screenTop.y - screenCenter.y) * scaleFactor;

        float y = x;
        if (lockRatio)
        {
            float ratio = self.sizeDelta.y / self.sizeDelta.x;
            y = x * ratio;
        }

        self.sizeDelta = new Vector2(x, y);

        return self;
    }


    public static string CapFirst(this string String)
    {
        return String[0].ToString().ToUpper() + 
                String.Substring(1, String.Length - 1);
    }

    public static Sprite ToSprite(this Texture2D texture)
    {
        return Sprite.Create(texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f));
    }

    //Whyyyyy
    public static string elements<T>(this IEnumerable<T> self)
    {
        string returnString = "empty" ;

        bool token = false;
        foreach(T t in self)
        {
            if (!token)
            {
                returnString = "";
                token = true;
            }
            returnString += t.ToString() + "\n";
        }


        return returnString;
    }


}
