using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CurrencyAdder))]
public class CurrencyAdderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Collect 100 Currency"))
        {
            CurrencyAdder.AddCurrency();
        }
    }
}