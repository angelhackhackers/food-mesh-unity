using UnityEngine;

public enum DragDirection
{
    NONE,
    RIGHT,
    LEFT,
    UP,
    DOWN
}

public class StartDragSignal { };

public class DraggingSignal { public Vector2 vector2; }
public class EndDragSignal { public DragDirection DragDirection; };
