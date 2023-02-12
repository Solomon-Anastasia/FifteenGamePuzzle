using System;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;

namespace FifteenGamePuzzle
{
    public partial class FifteenGamePuzzleForm : Form
    {
        // Variabilele ce vor pastra nr de clicuri efectuate
        // intr-o joaca
        int nrOfClicks;
        int nrMinOfClicks = Int32.MaxValue;

        // Variabile ce vor duce evidenta timer-ului
        System.Timers.Timer t;
        int sec;
        int min;
        int h;

        // Crearea unui array pentru a amesteca cifrele
        int[] nrButoane = new int[16]; 

        // Metoda ce initializeaza componentele aplicatiei
        public FifteenGamePuzzleForm()
        {
            InitializeComponent();
        }

        // Metoda ce va permite peremutarea textului dintr-un buton,
        // pe ce cel liber
        private void Peremutare(Button b1, Button b2)
        {
            if (b2.Text == "")
            {
                b2.Text = b1.Text;
                b1.Text = "";
                nrOfClicks++; // Incrementam nr de clicuri
            }
        }

        // Metoda ce va verifica daca s-a gasit solutia jocului
        private void VerificareSolutie()
        {
            CuloareCorecta();

            if (button1.Text == "1" && button2.Text == "2" && button3.Text == "3" &&
                button4.Text == "4" && button5.Text == "5" && button6.Text == "6" &&
                button7.Text == "7" && button8.Text == "8" && button9.Text == "9" &&
                button10.Text == "10" && button11.Text == "11" && button12.Text == "12" &&
                button13.Text == "13" && button14.Text == "14" && button15.Text == "15")
            {
                // Resetarea timer-ului
                t.Stop();
                timerLabel.Text = "00:00:00";

                ResetareCuloareButton();

                // In cazul castigului, se va afisa un mesaj sugestiv
                MessageBox.Show("Bravo, ai castigat!", "15 puzzle", MessageBoxButtons.OK,
                                 MessageBoxIcon.Information);

                // Determinarea celui mai bun scor
                if (nrOfClicks < nrMinOfClicks && nrOfClicks != 0)
                {
                    nrMinOfClicks = nrOfClicks;
                    celMaiBunScorLabel.Text = "Cel mai bun scor: " + (nrMinOfClicks - 1);
                }

                EnableButton(0); // Deactivarea butoanelor

                nrClicksLabel.Text = "Numarul de clicuri: 0";
                nrOfClicks = 0; // Resetam contorul
            }

            // Afisarea nr de clicuri efectuate la moment
            nrClicksLabel.Text = "Numarul de clicuri: " + nrOfClicks;
        }

        // Metoda ce va amesteca cifrele din joc
        private void Amestecare()
        {
            
            int i = 1;
            int randChecker;
            bool flag = false;

            // Generam cifrele aleatoriu
            do 
            {
                Random rand = new Random();
                randChecker = Convert.ToInt32(rand.Next(0, 15) + 1);

                for (int j = 1; j <= i; j++)
                {
                    if (nrButoane[j] == randChecker)
                    {
                        flag = true;
                        break;
                    }
                }

                if (flag)
                {
                    flag = false;
                } else {
                    nrButoane[i] = randChecker;
                    i++;
                }
            } while (i < 16);

            AtribuireValoare();
        }

        // Metoda ce va afisa o fereastra care va intreba daca chiar dorim 
        // sa inchidem aplicatia, atunci cand apasam butonul de inchidere
        private void FifteenGamePuzzleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult iExit = MessageBox.Show("Parasiti joaca?", "15 puzzle",
                                 MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (iExit == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        // Metoda ce va afisa o fereastra care va intreba daca chiar dorim 
        // sa inchidem aplicatia, atunci cand apasam 'Exit', din bara de meniu
        private void exitToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            DialogResult iExit = MessageBox.Show("Parasiti joaca?", "15 puzzle",
                                 MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (iExit == DialogResult.Yes)
            {
                Application.ExitThread();
            }
        }

        // Metoda ce va afisa solutia corecta a jocului,
        // atunci cand apasam 'Show solution', din bara de meniu
        private void showToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            button1.Text = "1";
            button2.Text = "2";
            button3.Text = "3";
            button4.Text = "4";
            button5.Text = "5";
            button6.Text = "6";
            button7.Text = "7";
            button8.Text = "8";
            button9.Text = "9";
            button10.Text = "10";
            button11.Text = "11";
            button12.Text = "12";
            button13.Text = "13";
            button14.Text = "14";
            button15.Text = "15";
            button16.Text = "";

            EnableButton(0); // Activarea butoanelor

            // Resetarea timer-ului
            t.Stop();
            timerLabel.Text = "00:00:00";

            ResetareCuloareButton();
        }

        // Metoda ce crea o joaca noua,
        // atunci cand apasam 'New', din bara de meniu
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Amestecare();
            nrClicksLabel.Text = "Numarul de clicuri: 0";
            nrOfClicks = 0; // Resetam contorul

            EnableButton(1);

            // Resetarea timer-ului
            t.Stop();
            timerLabel.Text = "00:00:00";
            sec = 0;
            min = 0;
            h = 0;
            t.Start();

            ResetareCuloareButton();
            CuloareCorecta();
        }

        // Metoda ce va reseta jocul,
        // atunci cand apasam 'Reset', din bara de meniu
        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AtribuireValoare();
            nrClicksLabel.Text = "Numarul de clicuri: 0";
            nrOfClicks = 0; // Resetam contorul

            // Resetarea timer-ului
            t.Stop();
            timerLabel.Text = "00:00:00";
            sec = 0;
            min = 0;
            h = 0;
            t.Start();

            ResetareCuloareButton();
            CuloareCorecta();
        }

        // Metoda ce va amesteca cifrele, atunci cand se va deschide aplicatia
        private void FifteenGamePuzzleForm_Load(object sender, System.EventArgs e)
        {
            Amestecare();

            // Aplicarea timer-ului
            t = new System.Timers.Timer();
            t.Interval = 1000; // 1s
            t.Elapsed += OnTimeEvent;

            // Inceperea timer-ului
            t.Start();

            CuloareCorecta();
        }

        // Metoda ce va converti sec in min, min in h
        private void OnTimeEvent(object sender, ElapsedEventArgs e)
        {
            Invoke(new Action(() => {
                sec++;

                if (sec == 60) {
                    sec = 0;
                    min += 1;
                }
                if (min == 60) {
                    min = 0;
                    h += 1;
                }

                // Afisarea timpului in aplicatie
                timerLabel.Text = string.Format("{0}:{1}:{2}", 
                                  h.ToString().PadLeft(2, '0'), 
                                  min.ToString().PadLeft(2, '0'), 
                                  sec.ToString().PadLeft(2, '0'));
            }));
        }

        // Metoda ce va muta cifra de pe butonul 1, pe butonul liber (dupa caz),
        // atunci cand va fi apasat
        private void button1_Click(object sender, EventArgs e)
        {
            Peremutare(button1, button2);
            Peremutare(button1, button5);
            VerificareSolutie();
        }

        // Metoda ce va muta cifra de pe butonul 2, pe butonul liber (dupa caz),
        // atunci cand va fi apasat
        private void button2_Click(object sender, EventArgs e)
        {
            Peremutare(button2, button1);
            Peremutare(button2, button3);
            Peremutare(button2, button6);
            VerificareSolutie();
        }

        // Metoda ce va muta cifra de pe butonul 3, pe butonul liber (dupa caz),
        // atunci cand va fi apasat
        private void button3_Click(object sender, EventArgs e)
        {
            Peremutare(button3, button2);
            Peremutare(button3, button4);
            Peremutare(button3, button7);
            VerificareSolutie();
        }

        // Metoda ce va muta cifra de pe butonul 4, pe butonul liber (dupa caz),
        // atunci cand va fi apasat
        private void button4_Click(object sender, EventArgs e)
        {
            Peremutare(button4, button3);
            Peremutare(button4, button8);
            VerificareSolutie();
        }

        // Metoda ce va muta cifra de pe butonul 5, pe butonul liber (dupa caz),
        // atunci cand va fi apasat
        private void button5_Click(object sender, EventArgs e)
        {
            Peremutare(button5, button1);
            Peremutare(button5, button6);
            Peremutare(button5, button9);
            VerificareSolutie();
        }

        // Metoda ce va muta cifra de pe butonul 6, pe butonul liber (dupa caz),
        // atunci cand va fi apasat
        private void button6_Click(object sender, EventArgs e)
        {
            Peremutare(button6, button2);
            Peremutare(button6, button5);
            Peremutare(button6, button7);
            Peremutare(button6, button10);
            VerificareSolutie();
        }

        // Metoda ce va muta cifra de pe butonul 7, pe butonul liber (dupa caz),
        // atunci cand va fi apasat
        private void button7_Click(object sender, EventArgs e)
        {
            Peremutare(button7, button3);
            Peremutare(button7, button6);
            Peremutare(button7, button8);
            Peremutare(button7, button11);
            VerificareSolutie();
        }

        // Metoda ce va muta cifra de pe butonul 8, pe butonul liber (dupa caz),
        // atunci cand va fi apasat
        private void button8_Click(object sender, EventArgs e)
        {
            Peremutare(button8, button4);
            Peremutare(button8, button7);
            Peremutare(button8, button12);
            VerificareSolutie();
        }

        // Metoda ce va muta cifra de pe butonul 9, pe butonul liber (dupa caz),
        // atunci cand va fi apasat
        private void button9_Click(object sender, EventArgs e)
        {
            Peremutare(button9, button5);
            Peremutare(button9, button10);
            Peremutare(button9, button13);
            VerificareSolutie();
        }

        // Metoda ce va muta cifra de pe butonul 10, pe butonul liber (dupa caz),
        // atunci cand va fi apasat
        private void button10_Click(object sender, EventArgs e)
        {
            Peremutare(button10, button6);
            Peremutare(button10, button9);
            Peremutare(button10, button11);
            Peremutare(button10, button14);
            VerificareSolutie();
        }

        // Metoda ce va muta cifra de pe butonul 11, pe butonul liber (dupa caz),
        // atunci cand va fi apasat
        private void button11_Click(object sender, EventArgs e)
        {
            Peremutare(button11, button7);
            Peremutare(button11, button10);
            Peremutare(button11, button12);
            Peremutare(button11, button15);
            VerificareSolutie();
        }

        // Metoda ce va muta cifra de pe butonul 12, pe butonul liber (dupa caz),
        // atunci cand va fi apasat
        private void button12_Click(object sender, EventArgs e)
        {
            Peremutare(button12, button8);
            Peremutare(button12, button11);
            Peremutare(button12, button16);
            VerificareSolutie();
        }

        // Metoda ce va muta cifra de pe butonul 13, pe butonul liber (dupa caz),
        // atunci cand va fi apasat
        private void button13_Click(object sender, EventArgs e)
        {
            Peremutare(button13, button9);
            Peremutare(button13, button14);
            VerificareSolutie();
        }

        // Metoda ce va muta cifra de pe butonul 14, pe butonul liber (dupa caz),
        // atunci cand va fi apasat
        private void button14_Click(object sender, EventArgs e)
        {
            Peremutare(button14, button10);
            Peremutare(button14, button13);
            Peremutare(button14, button15);
            VerificareSolutie();
        }

        // Metoda ce va muta cifra de pe butonul 15, pe butonul liber (dupa caz),
        // atunci cand va fi apasat
        private void button15_Click(object sender, EventArgs e)
        {
            Peremutare(button15, button11);
            Peremutare(button15, button14);
            Peremutare(button15, button16);
            VerificareSolutie();
        }

        // Metoda ce va muta cifra de pe butonul 16, pe butonul liber (dupa caz),
        // atunci cand va fi apasat
        private void button16_Click(object sender, EventArgs e)
        {
            Peremutare(button16, button12);
            Peremutare(button16, button15);
            VerificareSolutie();
        }

        // Metoda ce va atribui valorile generate
        private void AtribuireValoare()
        {
            button1.Text = nrButoane[1].ToString();
            button2.Text = nrButoane[2].ToString();
            button3.Text = nrButoane[3].ToString();
            button4.Text = nrButoane[4].ToString();
            button5.Text = nrButoane[5].ToString();
            button6.Text = nrButoane[6].ToString();
            button7.Text = nrButoane[7].ToString();
            button8.Text = nrButoane[8].ToString();
            button9.Text = nrButoane[9].ToString();
            button10.Text = nrButoane[10].ToString();
            button11.Text = nrButoane[11].ToString();
            button12.Text = nrButoane[12].ToString();
            button13.Text = nrButoane[13].ToString();
            button14.Text = nrButoane[14].ToString();
            button15.Text = nrButoane[15].ToString();
            button16.Text = "";
        }

        // Metoda ce va schimba culoare butonului,
        // atunci cand cifra corespunde pozitiei sale corecte
        private void Culoare(Button b, string i)
        {
            if (b.Text == i) {
                b.BackColor = Color.LightGreen;
            } else {
                b.BackColor = Color.White;
            }
        }

        // Metoda ce va reseta culoarea butoanelor
        private void ResetareCuloareButton()
        {
            Culoare(button1, "-1");
            Culoare(button2, "-1");
            Culoare(button3, "-1");
            Culoare(button4, "-1");
            Culoare(button5, "-1");
            Culoare(button6, "-1");
            Culoare(button7, "-1");
            Culoare(button8, "-1");
            Culoare(button9, "-1");
            Culoare(button10, "-1");
            Culoare(button11, "-1");
            Culoare(button12, "-1");
            Culoare(button13, "-1");
            Culoare(button14, "-1");
            Culoare(button15, "-1");
        }

        // Metoda ce va colora butoanele,
        // in cazul in care se afla in pozitia sa
        private void CuloareCorecta()
        {
            Culoare(button1, "1");
            Culoare(button2, "2");
            Culoare(button3, "3");
            Culoare(button4, "4");
            Culoare(button5, "5");
            Culoare(button6, "6");
            Culoare(button7, "7");
            Culoare(button8, "8");
            Culoare(button9, "9");
            Culoare(button10, "10");
            Culoare(button11, "11");
            Culoare(button12, "12");
            Culoare(button13, "13");
            Culoare(button14, "14");
            Culoare(button15, "15");
        }

        // Metoda ce blocheaza/deblocheaza butoanele
        private void EnableButton(int i)
        {
            if (i == 1)
            {
                // Deblocam butoanele, pentru a permite efectuarea schimbarilor
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = true;
                button7.Enabled = true;
                button8.Enabled = true;
                button9.Enabled = true;
                button10.Enabled = true;
                button11.Enabled = true;
                button12.Enabled = true;
                button13.Enabled = true;
                button14.Enabled = true;
                button15.Enabled = true;
                button16.Enabled = true;
            } else {
                // Blocam butoanele, pentru a nu permite efectuarea schimbarilor
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
                button8.Enabled = false;
                button9.Enabled = false;
                button10.Enabled = false;
                button11.Enabled = false;
                button12.Enabled = false;
                button13.Enabled = false;
                button14.Enabled = false;
                button15.Enabled = false;
                button16.Enabled = false;
            }
        }
    }
}
