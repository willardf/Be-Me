using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TextBox : MonoBehaviour
{
    private Controller myController = new Controller();

    public StringBuilder text = new StringBuilder(32);
    public TextController controller;
    public Collider2D myCollider;

    public bool HasFocus;

    public void Start()
    {
        text.Append(">");
    }

    public void Update()
    {
        myController.Update();
        if (HasFocus)
        {
            string input = Input.inputString;
            if (input.Length > 0)
            {
                if (text.Length == 0)
                {
                    text.Append('>');
                }

                for (int i = 0; i < input.Length; ++i)
                {
                    char c = input[i];
                    switch (c)
                    {
                        case '\b':
                            if (text.Length > 1)
                            {
                                text.Length--;
                            }
                            break;
                        case '\r':
                        case '\n':
                            GameManager.Instance.AcceptInput(text.ToString().Substring(1).Trim().ToLowerInvariant());
                            text.Length = 1;
                            break;
                        default:
                            if (text.Length < 32
                                && (c == ' '
                                    || c == '.'
                                    || (c >= 'A' && c <= 'Z') 
                                    || (c >= 'a' && c <= 'z')))
                            {
                                text.Append(c);
                            }
                            break;
                    }
                }

                this.controller.displayTxt = this.text.ToString();
            }
        }
        else if (myController.TouchUp)
        {
            if (this.myCollider.OverlapPoint(Controller.Position))
            {
                this.HasFocus = true;
            }
        }
    }
}
