using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintTrapController : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        EventManager.Broadcast(GameEventsHandler.PlayerPaintTrapEvent);
    }
}
