using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TextController : MonoBehaviour
{
    public float Scale = 1;

    public int maxHeight = 32;
    public int maxWidth = 33;

    [Multiline]
    public string displayTxt;

    private string displaying;

    public int lineOffset;
    private int lastLineOffset;

    [HideInInspector] private Texture2D texture;
    [HideInInspector] private Texture2D colorTexture;
    [HideInInspector] private MeshRenderer rend;

    private float _scale = float.NegativeInfinity;

    public Color Color = Color.white;
    private Color lastColor;

    public Vector3 Offset;

    public void Start()
    {
        this.rend = GetComponent<MeshRenderer>();
    }

    public void FixedUpdate()
    {
        if (string.IsNullOrEmpty(displayTxt))
        {
            this.rend.enabled = false;
            return;
        }
        
        if (displaying == null
            || displaying.GetHashCode() != displayTxt.GetHashCode()
            || this.Color != this.lastColor
            || this.lastLineOffset != this.lineOffset)
        {
            int maxX, maxY;
            this.DecideRes(ref displayTxt, out maxX, out maxY);

            if (!texture || !colorTexture)
            {
                if (!texture)
                {
                    texture = new Texture2D(maxX, maxY, TextureFormat.ARGB32, false);
                    texture.filterMode = FilterMode.Point;
                    texture.wrapMode = TextureWrapMode.Clamp;
                }
                if (!colorTexture)
                {
                    colorTexture = new Texture2D(maxX, maxY, TextureFormat.ARGB32, false);
                    colorTexture.filterMode = FilterMode.Point;
                    colorTexture.wrapMode = TextureWrapMode.Clamp;
                }
            }
            else if (texture.width != maxX || texture.height != maxY)
            {
                texture.Resize(maxX, maxY, TextureFormat.ARGB32, false);
                colorTexture.Resize(maxX, maxY, TextureFormat.ARGB32, false);
            }

            var colors = new Color[maxX * maxY];
            colors.SetAll(this.Color);
            colorTexture.SetPixels(colors);
            colors.SetAll(new Color(' ' / 256f, 0, 0));
            texture.SetPixels(colors);

            int x = 0;
            int y = this.texture.height - 1;
            int trueY = 0;
            Color curColor = this.Color;
            bool coloring = false;
            int colorInt = 0;
            char[] colorHex = new char[6];
            for (int i = 0; i < displayTxt.Length; ++i)
            {
                if (trueY >= this.texture.height + lineOffset) break;
                char c = displayTxt[i];

                if (displayTxt[i] == '[')
                {
                    colorInt = 0;
                    coloring = true;
                    continue;
                }
                else if (displayTxt[i] == ']')
                {
                    byte[] bytes = StringToByteArray(colorHex);
                    curColor = new Color(bytes[0] / 256f, bytes[1] / 256f, bytes[2] / 256f);
                    coloring = false;
                    continue;
                }
                else if (coloring)
                {
                    colorHex[colorInt++] = c;
                    continue;
                }

                if (c == '\r') continue;
                if (c == '\n')
                {
                    x = 0;
                    y--;
                    trueY++;
                    continue;
                }

                if (trueY < lineOffset) continue;

                texture.SetPixel(x, y + lineOffset, new Color(c / 256f, 0, 0));
                colorTexture.SetPixel(x, y + lineOffset, curColor);
                x++;
            }
            
            texture.Apply(false, false);
            colorTexture.Apply(false, false);

            this.rend.enabled = true;
            this.rend.material.SetTexture("_InputTex", texture);
            this.rend.material.SetTexture("_ColorTex", colorTexture);
            _scale = float.NegativeInfinity;
            this.displaying = displayTxt;
            this.lastColor = this.Color;
            this.lastLineOffset = this.lineOffset;
        }

        if (_scale != this.Scale)
        {
            _scale = this.Scale;
            this.transform.localScale = new Vector3(this.texture.width, this.texture.height, 1) * Scale;
            this.transform.localPosition = this.Offset + new Vector3(
                this.texture.width * Scale / 2,
                -this.texture.height * Scale / 2, 
                0);
        }
    }

    public static byte[] StringToByteArray(char[] hex)
    {
        byte[] output = new byte[hex.Length / 2];
        for (int i = 0; i < hex.Length; i += 2)
        {
            output[i / 2] = Convert.ToByte(new string(hex, i, 2), 16);
        }

        return output;
    }

    private void DecideRes(ref string displayTxt, out int maxX, out int maxY)
    {
        int curX = 0;
        maxX = 0;
        maxY = 1;
        int lastSpace = -1;
        bool coloring = false;
        for (int i = 0; i < displayTxt.Length; ++i)
        {
            if (displayTxt[i] == '[')
            {
                coloring = true;

            }
            else if (displayTxt[i] == ']')
            {
                coloring = false;
            }

            if (coloring) continue;

            if (displayTxt[i] == '\n')
            {
                lastSpace = -1;
                curX = 0;
                maxY++;
                continue;
            }

            if (displayTxt[i] == ' ')
            {
                lastSpace = i;
            }

            if (curX + 1 > this.maxWidth)
            {
                if (lastSpace != -1)
                {
                    displayTxt = displayTxt.Substring(0, lastSpace) + '\n' + displayTxt.Substring(lastSpace + 1);
                    curX = i - lastSpace;
                }
                else
                {
                    displayTxt = displayTxt.Substring(0, i) + '\n' + displayTxt.Substring(i);
                    curX = 0;
                }

                maxY++;
            }

            curX++;
            if (curX > maxX) maxX = curX;
        }

        if (maxY > maxHeight)
        {
            lineOffset = maxY - maxHeight;
            maxY = maxHeight;
        }
        else
        {
            this.lineOffset = 0;
        }
    }
}