using UnityEngine;

public class CameraMovement {

    private Camera _camera;
    private PlayerMovementSM _context;
    //Mouse Variables
    Vector2 _mousePos;
    float _mouseX;
    float _mouseY;

    public CameraMovement(PlayerMovementSM context){
        _context = context;
        _camera = _context.Camera;
        _mousePos = _context.MousePos;
        _mouseX = _mousePos.x;
        _mouseY = _mousePos.y;
    }

    public void HandleIdleCamera() {
        

    }
    public void HandleWalkCamera() {

    }
    public void HandleRunCamera() {

    }
}
