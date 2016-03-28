using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Utility
{
    public static class Extensions
    {
        // Borrowed from a Unity forum thread discussing the need for a negative relative rotation value
        // Posted by user Tinus, April 6 2009
        // http://forum.unity3d.com/threads/need-vector3-angle-to-return-a-negtive-or-relative-value.51092/
        public static float SignedAngleBetween(this Vector3 v1, Vector3 v2, Vector3 rotationAxis)
        {
            return Mathf.Atan2(Vector3.Dot(rotationAxis, Vector3.Cross(v1, v2)), Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
        }

        public static Vector3 Clone(this Vector3 vec) 
        {
            return new Vector3(vec.x, vec.y, vec.z);
        }
    }
}
