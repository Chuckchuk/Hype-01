using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    PlayerInput _playerInput;

    // Start is called before the first frame update
    void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.CharacterControls.Quit.performed += context => { QuitGame(); };
        _playerInput.CharacterControls.Jump.performed += context => { StartGamePressed(); };

        
    }
    void OnEnable() {
        // Enable Character Controls Action Map
        _playerInput.CharacterControls.Enable();
    }
    void OnDisable() {
        // Disable Character Controls Action Map
        _playerInput.CharacterControls.Disable();
    }

    private void QuitGame(){
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
    private void StartGamePressed() {
        SceneManager.LoadScene("SampleScene");
        Debug.Log("START GAME");
    }

    // Update is called once per frame
    void Update(){ }
}
