using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

public class Mapa {
    public Pole[,] mapa;

    public Mapa() {
        mapa = new Pole[110, 110];

        for (int i = 0; i < 110; i++) {
            for (int j = 0; j < 110; j++) {
                if (i > 98 || i < 11 || j > 98 || j < 11) {
                    mapa[i, j] = new Brzeg();
                }
                else if (i == 45 && j == 40 || i == 47 && j == 28 || i == 83 && j == 45 || i == 87 && j == 62 || i == 20 && j == 75) {
                    mapa[i, j] = new Skala();
                }
                else if (i == 85 && j == 85) {
                    mapa[i, j] = new Skrzynka(new Przedmiot("wiertlo"));
                }
                else if (i == 57 && j == 62) {
                    mapa[i, j] = new Skrzynka(new Przedmiot("wyciagarka"));
                }
                else if (i == 30 && j == 54) {
                    mapa[i, j] = new Skrzynka(new Przedmiot("klucz1"));
                }
                else if (i == 15 && j == 21) {
                    mapa[i, j] = new Skrzynka(new Przedmiot("klucz2"));
                }
                else if (i == 21 && j == 15) {
                    mapa[i, j] = new Skrzynka(new Przedmiot("klucz3"));
                }
                else if (i == 65 && j == 78) {
                    mapa[i, j] = new Skrzynka(new Przedmiot("nitro"));
                }
                else if (i == 80 && j == 80 || i == 20 && j == 20 || i == 80 && j == 20 || i == 55 && j == 42) {
                    mapa[i, j] = new Wir();
                    ((Wir)mapa[i, j]).add_wir(i, j);
                }
                else if (i == 50 && j == 51) {
                    mapa[i, j] = new Specjalne();
                }
                else {
                    mapa[i, j] = new Woda();
                }
            }
        }
    }

    public void shape_map(Postac player) {
        int pos1 = 0;
        for (int i = player.pozycja_y - 10; i < player.pozycja_y + 11; i++) {
            int pos2 = 0;
            for (int j = player.pozycja_x - 10; j < player.pozycja_x + 11; j++) {
                if (i == player.pozycja_y && j == player.pozycja_x) {
                    player.shape_player(new Pair(40 + pos2 * 20, 40 + pos1 * 20));
                }
                else {
                    mapa[j, i].shape(new Pair(40 + pos2 * 20, 40 + pos1 * 20));
                }
                pos2 += 1;
            }
            pos1 += 1;
        }
    }
}