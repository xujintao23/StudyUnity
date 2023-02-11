using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class Learn : MonoBehaviour
{
     Type t = typeof(A);

     void Start()
     {
         FieldInfo[] fi = t.GetFields();
         MethodInfo[] mi = t.GetMethods();

         foreach (var item in fi)
             Debug.Log($"Filed:{item.Name}");
         foreach (var item in mi)
             Debug.Log($"Method:{item.Name}");
     }
}

class  A
{
    public int Field;
    public int Field1;
    
    public void Method1(){}

    public int Method2()
    {
        return 1;
    }
}
