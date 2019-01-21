using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DeckOrbGrid))]
[CanEditMultipleObjects]
public class OrbGridEditor : Editor
{
    int selectedOrbIndex = 0;
    bool editMode = false;

    public void OnSceneGUI()
    {
        Handles.BeginGUI();

        DeckOrbGrid grid = (DeckOrbGrid)target;

        if (GUILayout.Button("Add Orb"))
        {
            Debug.Log("Got it to work. " + target.name);
            GenericMenu menu = new GenericMenu();

            for (int tLoop = 0; tLoop < System.Enum.GetValues(typeof(OrbSlot.SlotType)).Length; tLoop++)
            {
                menu.AddItem( new GUIContent( ((OrbSlot.SlotType)tLoop).ToString(), ""), false, AddOrbSlotType, (OrbSlot.SlotType)tLoop );
            }

            menu.ShowAsContext();
        }

        Color prevColor = GUI.color;
        if (editMode)
        {
            GUI.color = Color.red;
            if (GUILayout.Button("End Edit Mode"))
            {
                editMode = false;
                Debug.Log("Got it to work. " + target.name);
            }
            GUI.color = prevColor;
        }
        else
        {
            if (GUILayout.Button("Start Edit Mode"))
            {
                editMode = true;
            }
        }
          
        for (int i = 0; i < grid.orbs.Count; i++)
        {
            if (grid.orbs[i] == null) { return; }

            if (selectedOrbIndex == i)
            {
                // -- show the selection
                Handles.color = Color.cyan;
                Handles.DrawWireCube(grid.orbs[i].gameObject.transform.position, new Vector3(1.5f, 1.5f, 1.5f));

                // -- get screen position (weeoo)
                var view = SceneView.currentDrawingSceneView;
                if (view != null)
                {
                    Vector3 screenPos = view.camera.WorldToScreenPoint(grid.orbs[i].gameObject.transform.position);

                    GUI.Box(new Rect(screenPos.x, view.camera.pixelHeight - screenPos.y, 20, 20), new GUIContent("A THING", "OMGWOW AN ORB"));
                }
            }
            else
            {
                var view = SceneView.currentDrawingSceneView;
                if (view != null)
                {
                    Vector3 screenPos = view.camera.WorldToScreenPoint(grid.orbs[i].gameObject.transform.position);
                    if (GUI.Button(new Rect(screenPos.x, view.camera.pixelHeight - screenPos.y, 20, 20), new GUIContent("X", "sdweajk")))
                    {
                        if (editMode)
                        {
                            if (EditorUtility.DisplayDialog("Add Connection?", "Would you like to add a connection between these nodes?", "Yes", "No"))
                            {
                                grid.AddConnection(grid.orbs[selectedOrbIndex], grid.orbs[i]);
                            }
                        }
                        selectedOrbIndex = i;
                    }
                }
            }
        }

        if (editMode)
        {
            for (int i = 0; i < grid.connections.Count; i++)
            {
                if (grid == null || grid.connections[i] == null || grid.connections[i].A == null || grid.connections[i].B == null) { return; }

                var view = SceneView.currentDrawingSceneView;
                if (view != null)
                {
                    Vector3 midPoint = (grid.connections[i].A.transform.position + grid.connections[i].B.transform.position ) / 2;
                    Vector3 screenPos = view.camera.WorldToScreenPoint(midPoint);
                    if (GUI.Button(new Rect(screenPos.x, view.camera.pixelHeight - screenPos.y, 20, 20), new GUIContent("X", "sdweajk")))
                    {
                        if (EditorUtility.DisplayDialog("Remove Connection?", "Would you like to remove connection between these nodes?", "Yes", "No"))
                        {
                            grid.RemoveConnection(grid.connections[i]);
                        }
                    }
                }
            }
        }
        Handles.EndGUI();
    }

    void AddOrbSlotType(object userData)
    {
        OrbSlot.SlotType type = (OrbSlot.SlotType)userData;
        DeckOrbGrid grid = (DeckOrbGrid)target;
        GameObject newOrb = GameObject.Instantiate(grid.orbPrefab, grid.transform);

        Selection.activeObject = newOrb;
        SceneView.lastActiveSceneView.AlignViewToObject(newOrb.transform);

        newOrb.GetComponent<OrbSlot>().orbType = type;

        grid.orbs.Add(newOrb.GetComponent<OrbSlot>());
    }
}