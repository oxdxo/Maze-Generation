// GENERATED AUTOMATICALLY FROM 'Assets/Input/Input.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Input : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Input()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input"",
    ""maps"": [
        {
            ""name"": ""Maze"",
            ""id"": ""c315997b-5640-4806-ae24-b0672dc2ec0f"",
            ""actions"": [
                {
                    ""name"": ""Quit"",
                    ""type"": ""Button"",
                    ""id"": ""f10a9df0-4c23-4655-bebe-7f5629cf3b3d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraSwitch"",
                    ""type"": ""Button"",
                    ""id"": ""5c0ea79d-e9ac-4dfa-9a1b-e5914cbac686"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""079286d8-1e18-4c3a-9eab-2022c84831be"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Quit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fe865847-69e2-4485-8fb3-4b0647843088"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraSwitch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Maze
        m_Maze = asset.FindActionMap("Maze", throwIfNotFound: true);
        m_Maze_Quit = m_Maze.FindAction("Quit", throwIfNotFound: true);
        m_Maze_CameraSwitch = m_Maze.FindAction("CameraSwitch", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Maze
    private readonly InputActionMap m_Maze;
    private IMazeActions m_MazeActionsCallbackInterface;
    private readonly InputAction m_Maze_Quit;
    private readonly InputAction m_Maze_CameraSwitch;
    public struct MazeActions
    {
        private @Input m_Wrapper;
        public MazeActions(@Input wrapper) { m_Wrapper = wrapper; }
        public InputAction @Quit => m_Wrapper.m_Maze_Quit;
        public InputAction @CameraSwitch => m_Wrapper.m_Maze_CameraSwitch;
        public InputActionMap Get() { return m_Wrapper.m_Maze; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MazeActions set) { return set.Get(); }
        public void SetCallbacks(IMazeActions instance)
        {
            if (m_Wrapper.m_MazeActionsCallbackInterface != null)
            {
                @Quit.started -= m_Wrapper.m_MazeActionsCallbackInterface.OnQuit;
                @Quit.performed -= m_Wrapper.m_MazeActionsCallbackInterface.OnQuit;
                @Quit.canceled -= m_Wrapper.m_MazeActionsCallbackInterface.OnQuit;
                @CameraSwitch.started -= m_Wrapper.m_MazeActionsCallbackInterface.OnCameraSwitch;
                @CameraSwitch.performed -= m_Wrapper.m_MazeActionsCallbackInterface.OnCameraSwitch;
                @CameraSwitch.canceled -= m_Wrapper.m_MazeActionsCallbackInterface.OnCameraSwitch;
            }
            m_Wrapper.m_MazeActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Quit.started += instance.OnQuit;
                @Quit.performed += instance.OnQuit;
                @Quit.canceled += instance.OnQuit;
                @CameraSwitch.started += instance.OnCameraSwitch;
                @CameraSwitch.performed += instance.OnCameraSwitch;
                @CameraSwitch.canceled += instance.OnCameraSwitch;
            }
        }
    }
    public MazeActions @Maze => new MazeActions(this);
    public interface IMazeActions
    {
        void OnQuit(InputAction.CallbackContext context);
        void OnCameraSwitch(InputAction.CallbackContext context);
    }
}
