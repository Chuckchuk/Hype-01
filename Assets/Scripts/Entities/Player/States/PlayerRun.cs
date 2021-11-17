using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : BaseState {
    private PlayerMovementSM _context;
    public PlayerRun(PlayerMovementSM context, int StateLayer) : base("Run", context) {
        _context = context;
        this.StateLayer = StateLayer;
    }
    public override void Enter(){

    }
    public override void Exit(){

    }
    public override void Update(){

    }
}
