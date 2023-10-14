using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq; // Needed for the Select method

[CustomEditor(typeof(SandboxTargetUpgradeSelector))] // Replace 'YourScript' with the name of your script that needs the WeaponUpgrade instance
public class SandboxTargetUpgradeSelectorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        // Access the weaponUpgradePool list
        List<WeaponUpgrade> weaponUpgrades = UpgradeManager.DefaultWeaponUpgradePool;

        // Create the dropdown menu
        string[] options = weaponUpgrades.Select(upgrade => upgrade.Name).ToArray();

        // Assign the selected WeaponUpgrade instance to your script
        SandboxTargetUpgradeSelector yourScript = (SandboxTargetUpgradeSelector) target;

        // Use serializedObject to store the selectedIndex
        serializedObject.Update();
        SerializedProperty selectedIndexProperty = serializedObject.FindProperty("selectedIndex");
        selectedIndexProperty.intValue = EditorGUILayout.Popup("Weapon Upgrade", selectedIndexProperty.intValue, options);
        serializedObject.ApplyModifiedProperties();

        WeaponUpgrade selectedUpgrade = weaponUpgrades[selectedIndexProperty.intValue];

        // Assign the selected WeaponUpgrade instance to your script
        yourScript.weaponUpgrade = selectedUpgrade;
    }
}