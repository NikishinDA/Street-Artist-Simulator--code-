// The Game Events used across the Game.
// Anytime there is a need for a new event, it should be added here.

using System;
using System.Collections.Generic;
using UnityEngine;

public static class GameEventsHandler
{
    public static readonly GameStartEvent GameStartEvent = new GameStartEvent();
    public static readonly GameOverEvent GameOverEvent = new GameOverEvent();
    public static readonly MoneyCollectEvent MoneyCollectEvent = new MoneyCollectEvent();
    public static readonly FinisherStartEvent FinisherStartEvent = new FinisherStartEvent();
    public static readonly PlayerFinishLevelEvent PlayerFinishLevelEvent = new PlayerFinishLevelEvent();
    public static readonly TutorialShowEvent TutorialShowEvent = new TutorialShowEvent();
    public static readonly TutorialToggleEvent TutorialToggleEvent = new TutorialToggleEvent();
    public static readonly AmbianceChangeEvent AmbianceChangeEvent = new AmbianceChangeEvent();
    public static readonly PlayerModelChangeEvent PlayerModelChangeEvent = new PlayerModelChangeEvent();
    public static readonly DebugCallEvent DebugCallEvent = new DebugCallEvent();
    public static readonly UseButtonToggleEvent UseButtonToggleEvent = new UseButtonToggleEvent();
    public static readonly UseButtonClickEvent UseButtonClickEvent = new UseButtonClickEvent();
    public static readonly TutorialGuardEvent TutorialGuardEvent = new TutorialGuardEvent();
    public static readonly DrawingToggleEvent DrawingToggleEvent = new DrawingToggleEvent();
    public static readonly DrawingButtonClickEvent DrawingButtonClickEvent = new DrawingButtonClickEvent();
    public static readonly InteractableObjectActivateEvent InteractableObjectActivateEvent = new InteractableObjectActivateEvent();
    public static readonly PlayerPaintTrapEvent PlayerPaintTrapEvent = new PlayerPaintTrapEvent();
    public static readonly LevelGraffitiCompleteEvent LevelGraffitiCompleteEvent = new LevelGraffitiCompleteEvent();
    public static readonly LevelObjectivesCompleteEvent LevelObjectivesCompleteEvent = new LevelObjectivesCompleteEvent();
    public static readonly NewGraffitiShowEvent NewGraffitiShowEvent = new NewGraffitiShowEvent();
}

public class GameEvent {}

public class GameStartEvent : GameEvent
{
}

public class GameOverEvent : GameEvent
{
    public bool IsWin;
}

public class MoneyCollectEvent : GameEvent
{
    
}

public class UseButtonToggleEvent : GameEvent
{
    public bool Toggle;
    //public InteractableObject Object;
    public InteractType Type;
}
public class FinisherStartEvent : GameEvent
{
    
}

public class  PlayerFinishLevelEvent : GameEvent{}

public class TutorialShowEvent : GameEvent
{
}

public class TutorialToggleEvent : GameEvent
{
    public bool Toggle;
}


public class AmbianceChangeEvent : GameEvent
{
    public int Number;
}
public class PlayerModelChangeEvent : GameEvent
{
    public bool Bin;
}
public class DebugCallEvent : GameEvent
{
    public float Speed;
    public float Strafe;
}

public class UseButtonClickEvent : GameEvent
{
    
}

public class TutorialGuardEvent : GameEvent
{
    public bool Turn;
}

public class DrawingToggleEvent : GameEvent
{
    public bool Toggle;
    public bool IsFloor;
}

public class DrawingButtonClickEvent : GameEvent
{
    
}

public class InteractableObjectActivateEvent : GameEvent
{
    public bool IsActive;
    public InteractableObject Object;
}

public class PlayerPaintTrapEvent : GameEvent
{
    
}

public class LevelGraffitiCompleteEvent : GameEvent
{
    
}

public class LevelObjectivesCompleteEvent : GameEvent
{
    
}

public class NewGraffitiShowEvent : GameEvent
{
    public bool Toggle;
    public bool IsFirst;
}


