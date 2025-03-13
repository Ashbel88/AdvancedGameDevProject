/* Evan Stark - March 13th 2025 - ITCS 4231
This script sets up a waypoint path for a moving platform object to follow. 

SOURCE USED: https://www.youtube.com/watch?v=ly9mK0TGJJo*/

using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    // Get waypoint given index.
    public Transform GetWaypoint(int index)
    {
        return transform.GetChild(index);
    }

    // Set next waypoint in the path.
    public int GetNextIndex(int currentIndex)
    {
        int index = currentIndex + 1;

        // Loop back if the last element.
        if (index == transform.childCount)
        {
            index = 0;
        }

        return index;
    }

}
