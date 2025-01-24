using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

public class Sprite {
    public Pair position = null;
    public Pair scale = null;
    public string directory = "";
    public Bitmap sprite = null;

    public Sprite(Pair position, Pair scale, string directory) {
        this.position = position;
        this.scale = scale;
        this.directory = directory;

        Image temp = Image.FromFile($"Sprites/{directory}.png");
        sprite = new Bitmap(temp, (int)scale.x, (int)scale.y);

        Visualizer.sprites.Add(this);
    }
}