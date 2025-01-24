using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

public class Postac {
    public int pozycja_x;
    public int pozycja_y;
    public bool wirflag = false;
    public Ekwipunek equipment = new Ekwipunek();
    Sprite shape;

    public Postac(int pozycja_x, int pozycja_y) {
        this.pozycja_x = pozycja_x;
        this.pozycja_y = pozycja_y;
    }

    public void add_to_equipment(Przedmiot przedmiot) {
        equipment.add_equipment(przedmiot);
    }

    public bool search_in_equipment(string type) {
        return equipment.search_equipment(type);
    }

    public void shape_player(Pair position) {
        shape = new Sprite(position, new Pair(20, 20), "boat");
    }

    public void plyn_polnoc(Mapa mapa) {
        if (!(mapa.mapa[pozycja_x, pozycja_y - 1].collision)) {
            pozycja_y -= 1;
        }
    }

    public void plyn_poludnie(Mapa mapa) {
        if (!(mapa.mapa[pozycja_x, pozycja_y + 1].collision)) {
            pozycja_y += 1;
        }
    }

    public void plyn_wschod(Mapa mapa) {
        if (!(mapa.mapa[pozycja_x + 1, pozycja_y].collision)) {
            pozycja_x += 1;
        }
    }

    public void plyn_zachod(Mapa mapa) {
        if (!(mapa.mapa[pozycja_x - 1, pozycja_y].collision)) {
            pozycja_x -= 1;
        }
    }
}