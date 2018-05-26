using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TextBox : MonoBehaviour
{
    public StringBuilder text = new StringBuilder(32);
    public TextController controller;

    public bool AnyKey;

    private string lastFrame = null;

    public void Start()
    {
        text.Append(">");
    }

    private bool bDown;

    public void Update()
    {
        if (this.AnyKey)
        {
            this.controller.displayTxt = "(any key)";

            if (Input.inputString.Length > 0)
            {
                GameManager.Instance.AcceptInput(Input.inputString.ToLowerInvariant());
            }

            return;
        }
        else if (this.controller.displayTxt.Equals("(any key)"))
        {
            this.controller.displayTxt = ">";
        }

        string input = Input.inputString;
        if (input.Length > 0 || !input.Equals(this.lastFrame))
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
                        if (bDown)
                        {
                            bDown = false;
                            if (text.Length > 1)
                            {
                                text.Length--;
                            }
                        }
                        else { bDown = true; }

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

        this.lastFrame = input;
    }
}
