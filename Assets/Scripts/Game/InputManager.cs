using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /**
    Custom input manager class for rebinding settings during play.
    Maintains keybindings through reboot.
    */
public class InputManager : MonoBehaviour {
    public enum KeyInput {
        MOVE_FORWARD,
        MOVE_BACKWARD,
        MOVE_LEFT,
        MOVE_RIGHT,
        JUMP,
        ATTACK,
        BLOCK,
        SWITCH_SWORD,
        SWITCH_CROSSBOW,
        SWITCH_CANNON
    }
    Dictionary<KeyInput, string> controlNames = new Dictionary<KeyInput, string>{
        [ KeyInput.MOVE_FORWARD ] = "Move Forward",
        [ KeyInput.MOVE_BACKWARD ] = "Move Backward",
        [ KeyInput.MOVE_LEFT ] = "Move Left",
        [ KeyInput.MOVE_RIGHT ] = "Move Right",
        [ KeyInput.JUMP ] = "Jump",
        [ KeyInput.ATTACK ] = "Attack",
        [ KeyInput.BLOCK ] = "Block",
        [ KeyInput.SWITCH_SWORD ] = "Sword",
        [ KeyInput.SWITCH_CROSSBOW ] = "Crossbow",
        [ KeyInput.SWITCH_CANNON ] = "Cannon"
    };
    Dictionary<KeyInput, KeyCode> boundControls;
    Dictionary<KeyInput, KeyCode> defaultControls = new Dictionary<KeyInput, KeyCode> {
        [ KeyInput.MOVE_FORWARD ] = KeyCode.W,
        [ KeyInput.MOVE_BACKWARD ] = KeyCode.S,
        [ KeyInput.MOVE_LEFT ] = KeyCode.A,
        [ KeyInput.MOVE_RIGHT ] = KeyCode.D,
        [ KeyInput.JUMP ] = KeyCode.Space,
        [ KeyInput.ATTACK ] = KeyCode.Mouse0,
        [ KeyInput.BLOCK ] = KeyCode.Mouse1,
        [ KeyInput.SWITCH_SWORD ] = KeyCode.Q,
        [ KeyInput.SWITCH_CROSSBOW ] = KeyCode.E,
        [ KeyInput.SWITCH_CANNON ] = KeyCode.F
    };

    // Start is called before the first frame update
    void Awake() {
        boundControls = new Dictionary<KeyInput, KeyCode>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
