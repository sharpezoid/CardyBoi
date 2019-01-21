using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckOrbGrid : MonoBehaviour
{
    public List<OrbSlot> orbs = new List<OrbSlot>();
    public List<Connection> connections = new List<Connection>();
    public GameObject connectionPrefab;
    public GameObject orbPrefab;
    public Transform connectionParent;

    Grid grid;

    private void Start()
    {
        grid = GetComponent<Grid>();
    }

    public void AddConnection(OrbSlot A, OrbSlot B)
    {
        Debug.Log("add conn : " + A + " - " + B);
        GameObject newConnection = GameObject.Instantiate(connectionPrefab, connectionParent);
        Connection c = newConnection.GetComponent<Connection>();
        c.SetEnds(A, B);
        A.connectedOrbs.Add(B);
        B.connectedOrbs.Add(A);
        connections.Add(c);
    }

    public void RemoveConnection(Connection C)
    {
        C.A.connectedOrbs.Remove(C.B);
        C.B.connectedOrbs.Remove(C.A);
        List<Connection> removeMe = new List<Connection>();
        for (int i = 0; i < connections.Count; i++)
        {
            if( (connections[i].A == C.A && connections[i].B == C.B) || (connections[i].A == C.A && connections[i].B == C.B))
            {
                removeMe.Add(connections[i]);
            }
        }
        for (int i = 0; i < connections.Count; i++)
        {
            connections.Remove(removeMe[i]);
        }
        removeMe.Clear();
    }

    void OnSceneGUI()
    {
        GUILayout.Label("EFJD");
    }
}

public class ConnectionKey
{
    OrbSlot A;
    OrbSlot B;

    public ConnectionKey(OrbSlot A, OrbSlot B)
    {
        
    }
}