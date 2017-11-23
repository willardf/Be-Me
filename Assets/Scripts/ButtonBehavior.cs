using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehavior : MonoBehaviour
{
    public bool OnUp = true;
    public bool OnDown = false;
    public bool Repeat = false;

    public Collider2D myCollider;

    public Button.ButtonClickedEvent OnClick = new Button.ButtonClickedEvent();

    private Controller myController = new Controller();

    private float downTime = 0;

    public void OnEnable()
    {
        myController.Reset();
    }

    public void FixedUpdate()
    {
        myController.Update();
        if (Repeat)
        {
            if (Controller.HasTouch && CheckTouch())
            {
                downTime += Time.fixedDeltaTime;
                if (downTime >= .3f)
                {
                    downTime = 0;
                    this.OnClick.Invoke();
                }
            }
            else
            {
                downTime = 0;
            }
        }

        if (OnDown && myController.TouchDown)
        {
            if (CheckTouch())
            {
                this.OnClick.Invoke();
            }
        }

        if (OnUp && myController.TouchUp)
        {
            if (CheckTouch())
            {
                this.OnClick.Invoke();
            }
        }
    }

    private bool CheckTouch()
    {
        return this.myCollider.OverlapPoint(Controller.Position);
    }
}
