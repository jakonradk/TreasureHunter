using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

public class GUI : Form {
    public GUI() {
        this.DoubleBuffered = true;
    }
}

public abstract class Visualizer {
    private Pair size = null;
    private string title = null;
    public GUI window = null;
    public Color backgroundcolor = Color.Black;

    public static List<Shape> constshapes = new List<Shape>();
    public static List<Shape> updateshapes = new List<Shape>();
    public static List<Sprite> sprites = new List<Sprite>();
    public static List<StringGraphic> conststrings = new List<StringGraphic>();
    public static List<StringGraphic> updatestrings = new List<StringGraphic>();

    public Visualizer(Pair size, string title) {
        this.size = size;
        this.title = title;

        window = new GUI();
        window.Size = new Size((int)this.size.x, (int)this.size.y);
        window.Text = this.title;
        window.Paint += Renderer;
        window.KeyDown += KeyDown;
        window.KeyUp += KeyUp;

        LoadBase();

        Application.Run(window);
    }
    public void KeyDown(object sender, KeyEventArgs e) {
        GetKeyDown(e);
    }

    public void KeyUp(object sender, KeyEventArgs e) {
        GetKeyUp(e);
    }

    public static void LoadShapeConst(Shape shape) {
        constshapes.Add(shape);
    }

    public static void LoadSprite(Sprite sprite) {
        sprites.Add(sprite);
    }

    public static void LoadTextConst(StringGraphic str) {
        conststrings.Add(str);
    }

    public static void LoadShapeUpdate(Shape shape) {
        updateshapes.Add(shape);
    }

    public static void LoadTextUpdate(StringGraphic str) {
        updatestrings.Add(str);
    }

    public void Renderer(object sender, PaintEventArgs e) {
        Graphics g = e.Graphics;
        g.Clear(Color.Black);

        foreach (Shape shape in constshapes) {
            if (shape.name == "border") {
                g.FillRectangle(new SolidBrush(Color.Gray), shape.position.x, shape.position.y, shape.scale.x, shape.scale.y);
            }
            else if (shape.name == "announcement") {
                g.FillRectangle(new SolidBrush(Color.White), shape.position.x, shape.position.y, shape.scale.x, shape.scale.y);
            }
        }

        foreach (Shape shape in updateshapes) {
            if (shape.name == "border") {
                g.FillRectangle(new SolidBrush(Color.Gray), shape.position.x, shape.position.y, shape.scale.x, shape.scale.y);
            }
            else if (shape.name == "announcement") {
                g.FillRectangle(new SolidBrush(Color.White), shape.position.x, shape.position.y, shape.scale.x, shape.scale.y);
            }
        }

        foreach (Sprite spr in sprites) {
            g.DrawImage(spr.sprite, spr.position.x, spr.position.y, spr.scale.x, spr.scale.y);
        }

        foreach (StringGraphic str in conststrings) {
            g.DrawString(str.text, str.font, str.color, new PointF(str.position.x, str.position.y));
        }

        foreach (StringGraphic str in updatestrings) {
            g.DrawString(str.text, str.font, str.color, new PointF(str.position.x, str.position.y));
        }
    }

    public abstract void LoadBase();
    public abstract void Update();
    public abstract void GetKeyDown(KeyEventArgs e);
    public abstract void GetKeyUp(KeyEventArgs e);
}