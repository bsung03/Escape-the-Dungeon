using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsStats : MonoBehaviour
{
    public string roomName;

    public bool isTopDoorOpen;
    public bool isBottomDoorOpen;
    public bool isRightDoorOpen;
    public bool isLeftDoorOpen;

    public List<Vector3> unopenedChests;

    public List<Vector3> nonpickedKeys;
    public List<Vector3> nonpickedGold;
    public List<Vector3> nonpickedPowerups;

    //TODO: save enemies status (wave level, remaining non killed enemies locations)

    public RoomsStats()
    {
        roomName = "invalid";
    }

    public RoomsStats(string roomName, bool isTopDoorOpen, bool isBottomDoorOpen, bool isRightDoorOpen, bool isLeftDoorOpen) 
    {
        this.roomName = roomName;

        this.isTopDoorOpen = isTopDoorOpen;
        this.isBottomDoorOpen = isBottomDoorOpen;
        this.isRightDoorOpen = isRightDoorOpen;
        this.isLeftDoorOpen = isLeftDoorOpen;

        this.unopenedChests = new List<Vector3>();

        this.nonpickedKeys = new List<Vector3>();
        this.nonpickedGold = new List<Vector3>();
        this.nonpickedPowerups = new List<Vector3>();
    }

}
