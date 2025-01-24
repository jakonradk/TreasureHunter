using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

public class Shape {
    public Pair position = null;
    public Pair scale = null;
    public string name = "";
    public bool constant = false;

    public Shape(Pair position, Pair scale, string name, bool constant) {
        this.position = position;
        this.scale = scale;
        this.name = name;
        this.constant = constant;

        if (constant) {
            Visualizer.LoadShapeConst(this);
        }
        else {
            Visualizer.LoadShapeUpdate(this);
        }
    }
}