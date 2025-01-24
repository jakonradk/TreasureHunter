using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

public class StringGraphic {
    public Pair position = null;
    public Font font = null;
    public SolidBrush color = null;
    public string text = null;
    public bool constant = false;

    public StringGraphic(Pair position, Font font, SolidBrush color, string text, bool constant) {
        this.position = position;
        this.font = font;
        this.color = color;
        this.text = text;
        this.constant = constant;

        if (constant) {
            Visualizer.LoadTextConst(this);
        }
        else {
            Visualizer.LoadTextUpdate(this);
        }
    }
}