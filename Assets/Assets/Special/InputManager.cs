using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SecondMonoBehaviour
{
    private static InputManager _instance;
    public static InputManager Instance => _instance;

    private Vector2 _touchStartPos;
    private bool _isSwipeActive;

    public float swipeThreshold = 25f;

    public bool isMoveUp = false;
    public bool isMoveDown = false;
    public bool isMoveLeft = false;
    public bool isMoveRight = false;

    protected override void Awake()
    {
        base.Awake();
        if (InputManager._instance != null) Debug.LogError("Only 1 InputManager allow to exist");
        InputManager._instance = this;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this._touchStartPos = Input.mousePosition;
            this._isSwipeActive = true;
        }

        if (Input.GetMouseButton(0) && this._isSwipeActive)
        {
            Vector2 direction = (Vector2)Input.mousePosition - this._touchStartPos;
            if (direction.magnitude > this.swipeThreshold)
            {
                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                {
                    if (direction.x > 0) this.isMoveRight = true;
                    else this.isMoveLeft = true;
                }
                else
                {
                    if (direction.y > 0)  this.isMoveUp = true;
                    else this.isMoveDown = true;
                }
                this._isSwipeActive = false;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            this._isSwipeActive = false;
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) this.isMoveUp = true;    
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) this.isMoveDown = true;    
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) this.isMoveLeft = true;    
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) this.isMoveRight = true;    
    }
}

