using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

public class Pole {
    public bool collision;
    protected Sprite block;
    public virtual void shape(Pair position) { }
}

public class Skala : Pole {
    public Skala() {
        collision = true;
    }

    public override void shape(Pair position) {
        this.block = new Sprite(position, new Pair(20, 20), "rock");
    }
}

public class Woda : Pole {

    public Woda() {
        collision = false;
    }

    public override void shape(Pair position) {
        this.block = new Sprite(position, new Pair(20, 20), "water");
    }
}

public class Brzeg : Pole {
    public Brzeg() {
        collision = true;
    }

    public override void shape(Pair position) {
        this.block = new Sprite(position, new Pair(20, 20), "coast");
    }
}

public class Skrzynka : Pole {
    public int zloto;
    public Przedmiot przedmiot = null;

    public Skrzynka() {
        collision = false;
        zloto = 100;
    }
    public Skrzynka(Przedmiot przedmiot) {
        collision = false;
        zloto = 100;
        this.przedmiot = przedmiot;
    }

    public override void shape(Pair position) {
        this.block = new Sprite(position, new Pair(20, 20), "chest");
    }
}

public class Wir : Pole {
    static Random rnd = new Random();
    public static List<Pair> whirls = new List<Pair>();
    public Wir() {
        collision = false;
    }

    public override void shape(Pair position) {
        this.block = new Sprite(position, new Pair(20, 20), "whirlpool");
    }

    public void add_wir(int i, int j) {
        whirls.Add(new Pair(i, j));
    }

    public Pair get_random_wir() {
        if (whirls.Count == 0) {
            return new Pair(0, 0);
        }
        else {
            int r = rnd.Next(whirls.Count);
            return whirls[r];
        }
    }
}

public class Odlamki : Pole {
    public int zloto;
    public Odlamki() {
        collision = false;
        zloto = 100;
    }

    public override void shape(Pair position) {
        this.block = new Sprite(position, new Pair(20, 20), "remains");
    }
}

public class Specjalne : Pole {
    public Specjalne() {
        collision = true;
    }

    public override void shape(Pair position) {
        this.block = new Sprite(position, new Pair(20, 20), "special");
    }
}