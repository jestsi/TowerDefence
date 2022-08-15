using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using Assets.Scripts;
using Units;
using UnityEditor;

[EditorTool("AnchorToCell", typeof(Unit))]
public class ToolAnchorToCell : EditorTool
{
    [SerializeField] Texture2D _toolIcon;
    public override GUIContent toolbarIcon
    {
        get => new GUIContent
        {
            image = _toolIcon,
            text = "Anchor To Cell"
        };
    }

    public override void OnToolGUI(EditorWindow window)
    {
        if (target is not Unit targetCell) return;


        EditorGUI.BeginChangeCheck();
        var newPosition = Handles.PositionHandle(targetCell.transform.position, Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            if (Physics.Raycast(newPosition, Vector3.down, out RaycastHit hit, 1.0f))
            {
                if (hit.transform.gameObject.TryGetComponent<Cell>(out Cell cell))
                    targetCell.transform.position = cell.transform.position;
            }
        }
        
    }
}
