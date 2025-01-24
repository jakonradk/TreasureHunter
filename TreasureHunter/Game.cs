using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

class Game : Visualizer {
    Postac player = new Postac(50, 50);
    Mapa map = new Mapa();

    bool left;
    bool right;
    bool up;
    bool down;
    bool action;
    bool win = false;
    bool tutorial = false;
    bool nitro = false;

    public Game() : base(new Pair(515, 800), "Treasure Hunter") { }

    public override void LoadBase() {
        new Shape(new Pair(20, 20), new Pair(15, 460), "border", true);
        new Shape(new Pair(465, 20), new Pair(15, 460), "border", true);
        new Shape(new Pair(20, 20), new Pair(460, 15), "border", true);
        new Shape(new Pair(20, 465), new Pair(460, 15), "border", true);

        new StringGraphic(new Pair(20, 500), new Font("Arial", 16), new SolidBrush(Color.White), "X: " + player.pozycja_x.ToString(), false);
        new StringGraphic(new Pair(20, 525), new Font("Arial", 16), new SolidBrush(Color.White), "Y: " + player.pozycja_y.ToString(), false);

        new StringGraphic(new Pair(20, 550), new Font("Arial", 14), new SolidBrush(Color.White), "Ekwipunek:", true);
        new StringGraphic(new Pair(360, 720), new Font("Arial", 16), new SolidBrush(Color.White), "Złoto: " + (player.equipment.zloto).ToString(), false);

        map.shape_map(player);
    }
    public override void Update() {
        if (typeof(Wir).IsInstanceOfType(map.mapa[player.pozycja_x, player.pozycja_y]) && player.wirflag == false) {
            Pair copi = ((Wir)map.mapa[player.pozycja_x, player.pozycja_y]).get_random_wir();
            while (copi.x == player.pozycja_x && copi.y == player.pozycja_y) {
                copi = ((Wir)map.mapa[player.pozycja_x, player.pozycja_y]).get_random_wir();
            }

            player.wirflag = true;
            player.pozycja_x = (int)copi.x;
            player.pozycja_y = (int)copi.y;

            updateshapes.Clear();
            sprites.Clear();
            updatestrings.Clear();
        }
        if (up) {
            if (nitro) {
                player.plyn_polnoc(map);
            }
            player.plyn_polnoc(map);
            player.wirflag = false;
            updateshapes.Clear();
            sprites.Clear();
            updatestrings.Clear();
        }
        if (down) {
            if (nitro) {
                player.plyn_poludnie(map);
            }
            player.plyn_poludnie(map);
            player.wirflag = false;
            updateshapes.Clear();
            sprites.Clear();
            updatestrings.Clear();
        }
        if (left) {
            if (nitro) {
                player.plyn_zachod(map);
            }
            player.plyn_zachod(map);
            player.wirflag = false;
            updateshapes.Clear();
            sprites.Clear();
            updatestrings.Clear();
        }
        if (right) {
            if (nitro) {
                player.plyn_wschod(map);
            }
            player.plyn_wschod(map);
            player.wirflag = false;
            updateshapes.Clear();
            sprites.Clear();
            updatestrings.Clear();
        }
        if (nitro) {
            updatestrings.Clear();
            new StringGraphic(new Pair(300, 525), new Font("Arial", 16), new SolidBrush(Color.White), "N - nitro włączone", false);
        }
        else if (!nitro && player.search_in_equipment("nitro")) {
            updatestrings.Clear();
            new StringGraphic(new Pair(300, 525), new Font("Arial", 16), new SolidBrush(Color.White), "N - nitro wyłączone", false);
        }
        if (action) {
            if (typeof(Skala).IsInstanceOfType(map.mapa[player.pozycja_x - 1, player.pozycja_y]) && player.search_in_equipment("wiertlo")) {
                map.mapa[player.pozycja_x - 1, player.pozycja_y] = new Odlamki();
            }
            if (typeof(Skala).IsInstanceOfType(map.mapa[player.pozycja_x + 1, player.pozycja_y]) && player.search_in_equipment("wiertlo")) {
                map.mapa[player.pozycja_x + 1, player.pozycja_y] = new Odlamki();
            }
            if (typeof(Skala).IsInstanceOfType(map.mapa[player.pozycja_x, player.pozycja_y - 1]) && player.search_in_equipment("wiertlo")) {
                map.mapa[player.pozycja_x, player.pozycja_y - 1] = new Odlamki();
            }
            if (typeof(Skala).IsInstanceOfType(map.mapa[player.pozycja_x, player.pozycja_y + 1]) && player.search_in_equipment("wiertlo")) {
                map.mapa[player.pozycja_x, player.pozycja_y + 1] = new Odlamki();
            }

            if (typeof(Skrzynka).IsInstanceOfType(map.mapa[player.pozycja_x, player.pozycja_y])) {
                if (((Skrzynka)map.mapa[player.pozycja_x, player.pozycja_y]).przedmiot != null) {
                    player.add_to_equipment(((Skrzynka)map.mapa[player.pozycja_x, player.pozycja_y]).przedmiot);
                }
                if (player.search_in_equipment("wyciagarka")) {
                    player.equipment.zloto += 2 * ((Skrzynka)map.mapa[player.pozycja_x, player.pozycja_y]).zloto;
                }
                else {
                    player.equipment.zloto += ((Skrzynka)map.mapa[player.pozycja_x, player.pozycja_y]).zloto;
                }
                map.mapa[player.pozycja_x, player.pozycja_y] = new Woda();
            }
            if (typeof(Odlamki).IsInstanceOfType(map.mapa[player.pozycja_x, player.pozycja_y])) {
                if (player.search_in_equipment("wyciagarka")) {
                    player.equipment.zloto += 2 * ((Odlamki)map.mapa[player.pozycja_x, player.pozycja_y]).zloto;
                }
                else {
                    player.equipment.zloto += ((Odlamki)map.mapa[player.pozycja_x, player.pozycja_y]).zloto;
                }
                map.mapa[player.pozycja_x, player.pozycja_y] = new Woda();
            }

            if (player.search_in_equipment("klucz1") &&
                player.search_in_equipment("klucz2") &&
                player.search_in_equipment("klucz3") &&
                (player.equipment.zloto >= 1700) &&
                (typeof(Specjalne).IsInstanceOfType(map.mapa[player.pozycja_x + 1, player.pozycja_y]) ||
                typeof(Specjalne).IsInstanceOfType(map.mapa[player.pozycja_x - 1, player.pozycja_y]) ||
                typeof(Specjalne).IsInstanceOfType(map.mapa[player.pozycja_x, player.pozycja_y + 1]) ||
                typeof(Specjalne).IsInstanceOfType(map.mapa[player.pozycja_x, player.pozycja_y - 1]))) {

                win = true;
                constshapes.Clear();
                conststrings.Clear();
                updateshapes.Clear();
                sprites.Clear();
                updatestrings.Clear();

                new Shape(new Pair(20, 20), new Pair(460, 460), "announcement", true);
                new StringGraphic(new Pair(165, 50), new Font("Arial", 16), new SolidBrush(Color.Black), "GRATULACJE", true);
                new StringGraphic(new Pair(165, 75), new Font("Arial", 16), new SolidBrush(Color.Black), "KONIEC GRY", true);
                new Sprite(new Pair(180, 125), new Pair(200, 200), "thumbsup");
                return;
            }

            if ((!player.search_in_equipment("klucz1") ||
                !player.search_in_equipment("klucz2") ||
                !player.search_in_equipment("klucz3") || 
                (player.equipment.zloto < 1700)) &&
                (typeof(Specjalne).IsInstanceOfType(map.mapa[player.pozycja_x + 1, player.pozycja_y]) ||
                typeof(Specjalne).IsInstanceOfType(map.mapa[player.pozycja_x - 1, player.pozycja_y]) ||
                typeof(Specjalne).IsInstanceOfType(map.mapa[player.pozycja_x, player.pozycja_y + 1]) ||
                typeof(Specjalne).IsInstanceOfType(map.mapa[player.pozycja_x, player.pozycja_y - 1]))) {

                if (!tutorial) {
                    tutorial = true;

                    updateshapes.Clear();
                    sprites.Clear();
                    updatestrings.Clear();
                    new Shape(new Pair(20, 20), new Pair(460, 460), "announcement", false);
                    new StringGraphic(new Pair(110, 50), new Font("Arial", 16), new SolidBrush(Color.Black), "Zbierz klucze do skarbu trzy", false);
                    new StringGraphic(new Pair(110, 75), new Font("Arial", 16), new SolidBrush(Color.Black), "I złota tyle ile ważą 42 psy", false);
                    return;
                }
                else {
                    tutorial = false;
                }

                updateshapes.Clear();
                sprites.Clear();
                updatestrings.Clear();

            }
            updateshapes.Clear();
            sprites.Clear();
            updatestrings.Clear();

            if (nitro) {
                updatestrings.Clear();
                new StringGraphic(new Pair(300, 525), new Font("Arial", 16), new SolidBrush(Color.White), "N - nitro włączone", false);
            }
            else if (!nitro && player.search_in_equipment("nitro")) {
                updatestrings.Clear();
                new StringGraphic(new Pair(300, 525), new Font("Arial", 16), new SolidBrush(Color.White), "N - nitro wyłączone", false);
            }
        }
        if (typeof(Specjalne).IsInstanceOfType(map.mapa[player.pozycja_x - 1, player.pozycja_y]) ||
            typeof(Specjalne).IsInstanceOfType(map.mapa[player.pozycja_x + 1, player.pozycja_y]) ||
            typeof(Specjalne).IsInstanceOfType(map.mapa[player.pozycja_x, player.pozycja_y - 1]) ||
            typeof(Specjalne).IsInstanceOfType(map.mapa[player.pozycja_x, player.pozycja_y + 1]) ||
            typeof(Skrzynka).IsInstanceOfType(map.mapa[player.pozycja_x, player.pozycja_y]) ||
            typeof(Odlamki).IsInstanceOfType(map.mapa[player.pozycja_x, player.pozycja_y]) ||
            (typeof(Skala).IsInstanceOfType(map.mapa[player.pozycja_x - 1, player.pozycja_y]) && player.search_in_equipment("wiertlo")) ||
            (typeof(Skala).IsInstanceOfType(map.mapa[player.pozycja_x + 1, player.pozycja_y]) && player.search_in_equipment("wiertlo")) ||
            (typeof(Skala).IsInstanceOfType(map.mapa[player.pozycja_x, player.pozycja_y + 1]) && player.search_in_equipment("wiertlo")) ||
            (typeof(Skala).IsInstanceOfType(map.mapa[player.pozycja_x, player.pozycja_y - 1]) && player.search_in_equipment("wiertlo"))) {

            new StringGraphic(new Pair(300, 500), new Font("Arial", 16), new SolidBrush(Color.White), "E - możliwa akcja", false);
        }

        if (player.search_in_equipment("wiertlo")) {
            new StringGraphic(new Pair(20, 575), new Font("Arial", 14), new SolidBrush(Color.White), "Wiertło - pozwala na rozkruszanie skał", false);
        }
        if (player.search_in_equipment("wyciagarka")) {
            new StringGraphic(new Pair(20, 600), new Font("Arial", 14), new SolidBrush(Color.White), "Wyciągarka - podwaja wydobycie złota", false);
        }
        if (player.search_in_equipment("nitro")) {
            new StringGraphic(new Pair(20, 625), new Font("Arial", 14), new SolidBrush(Color.White), "Nitro - przebywa dwa pola naraz", false);
        }
        if (player.search_in_equipment("klucz1")) {
            new StringGraphic(new Pair(20, 650), new Font("Arial", 14), new SolidBrush(Color.White), "Klucz Odkrywcy", false);
        }
        if (player.search_in_equipment("klucz2")) {
            new StringGraphic(new Pair(20, 675), new Font("Arial", 14), new SolidBrush(Color.White), "Klucz Poszukiwacza", false);
        }
        if (player.search_in_equipment("klucz3")) {
            new StringGraphic(new Pair(20, 700), new Font("Arial", 14), new SolidBrush(Color.White), "Klucz Kartografa", false);
        }

        new StringGraphic(new Pair(20, 500), new Font("Arial", 16), new SolidBrush(Color.White), "X: " + player.pozycja_x.ToString(), false);
        new StringGraphic(new Pair(20, 525), new Font("Arial", 16), new SolidBrush(Color.White), "Y: " + player.pozycja_y.ToString(), false);
        new StringGraphic(new Pair(360, 720), new Font("Arial", 16), new SolidBrush(Color.White), "Złoto: " + (player.equipment.zloto).ToString(), false);

        map.shape_map(player);
    }

    public override void GetKeyDown(KeyEventArgs e) {
        if (!win) {
            if (!tutorial) {
                if (e.KeyCode == Keys.W) {
                    up = true;
                    window.BeginInvoke((MethodInvoker)delegate { window.Refresh(); });
                    Update();
                }
                if (e.KeyCode == Keys.S) {
                    down = true;
                    window.BeginInvoke((MethodInvoker)delegate { window.Refresh(); });
                    Update();
                }
                if (e.KeyCode == Keys.A) {
                    left = true;
                    window.BeginInvoke((MethodInvoker)delegate { window.Refresh(); });
                    Update();
                }
                if (e.KeyCode == Keys.D) {
                    right = true;
                    window.BeginInvoke((MethodInvoker)delegate { window.Refresh(); });
                    Update();
                }
            }
            if (e.KeyCode == Keys.E) {
                action = true;
                window.BeginInvoke((MethodInvoker)delegate { window.Refresh(); });
                Update();
            }
            if (e.KeyCode == Keys.N) {
                if (player.search_in_equipment("nitro")) {
                    if (!nitro) {
                        nitro = true;
                    }
                    else {
                        nitro = false;
                    }
                }
                window.BeginInvoke((MethodInvoker)delegate { window.Refresh(); });
                Update();
            }
        }
        if (e.KeyCode == Keys.Escape) {
            window.Close();
        }
    }

    public override void GetKeyUp(KeyEventArgs e) {
        if (!win) {
            if (e.KeyCode == Keys.W) {
                up = false;
            }
            if (e.KeyCode == Keys.S) {
                down = false;
            }
            if (e.KeyCode == Keys.A) {
                left = false;
            }
            if (e.KeyCode == Keys.D) {
                right = false;
            }
            if (e.KeyCode == Keys.E) {
                action = false;
            }
        }
    }
}