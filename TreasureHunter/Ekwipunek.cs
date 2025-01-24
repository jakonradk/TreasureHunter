using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

public class Ekwipunek {
    List<Przedmiot> przedmioty = new List<Przedmiot>();
    public int zloto = 0;

    public Ekwipunek() { }

    public void add_equipment(Przedmiot przedmiot) {
        przedmioty.Add(przedmiot);
    }

    public bool search_equipment(string type) {
        for (int i = 0; i < przedmioty.Count; i++) {
            if (przedmioty[i].type == type) {
                return true;
            }
        }
        return false;
    }
}