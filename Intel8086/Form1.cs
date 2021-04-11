﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Text.RegularExpressions;

namespace Intel8086
{

    public partial class Form1 : Form
    {
        string ax, bx, cx, dx;
        string si, di, bp, disp;
        string XCHG;
        string[] TABLICA = new string[65536];
        string starszyAdres, mlodszyAdres;
        int siDec, diDec, dispDec, sumDec;
        int bxDec, bpDec;
        string sumHex, pobierz, odczyt;

        public Form1()
        {
            InitializeComponent();
            ax = "0000";
            axView.Text = ax;
            axText.Text = ax;
            bx = "0000";
            bxView.Text = bx;
            bxText.Text = bx;
            cx = "0000";
            cxView.Text = cx;
            cxText.Text = cx;
            dx = "0000";
            dxView.Text = dx;
            dxText.Text = dx;
            si = "0000";
            siView.Text = si;
            siText.Text = si;
            di = "0000";
            diView.Text = di;
            diText.Text = di;
            bp = "0000";
            bpView.Text = bp;
            bpText.Text = bp;
            disp = "0000";
            dispView.Text = disp;
            dispText.Text = disp;

            comboBoxBazowy.Visible = false;
            comboBoxIndeksowy.Visible = false;
            comboBoxIndeksowoBazowy.Visible = false;
            comboBoxPOLACZENIE.Visible = false;

            comboBoxKierunek.SelectedIndex = 0;
            comboBoxPOLACZENIE.SelectedIndex = 0;

            for (int i = 0; i < TABLICA.Length; i++)
            {
                TABLICA[i] = "00";
            }

            axView.ReadOnly = true;
            bxView.ReadOnly = true;
            cxView.ReadOnly = true;
            dxView.ReadOnly = true;
        }

        private void labelTryb_Click(object sender, EventArgs e)
        {

        }


        // 1. PRZYCISK PRZYPISZ
        private void buttonPrzypisz_Click(object sender, EventArgs e)
        {
            if (axText.Text.IsHexString() != true)
                return;
            if (bxText.Text.IsHexString() != true)
                return;
            if (cxText.Text.IsHexString() != true)
                return;
            if (dxText.Text.IsHexString() != true)
                return;

            if (axText.Text.Length == 4 && axText.Text.ToUpper() != axView.Text)
            {
                axView.Text = axText.Text.ToUpper();
                listBoxRejestrOperacji.Items.Insert(0, $"MOV AX, {axText.Text.ToUpper()}                     PRZYPISZ");
            }
            if (bxText.Text.Length == 4 && bxText.Text.ToUpper() != bxView.Text)
            {
                bxView.Text = bxText.Text.ToUpper();
                listBoxRejestrOperacji.Items.Insert(0, $"MOV BX, {bxText.Text.ToUpper()}                     PRZYPISZ");
            }
            if (cxText.Text.Length == 4 && cxText.Text.ToUpper() != cxView.Text)
            {
                cxView.Text = cxText.Text.ToUpper();
                listBoxRejestrOperacji.Items.Insert(0, $"MOV CX, {cxText.Text.ToUpper()}                     PRZYPISZ");
            }
            if (dxText.Text.Length == 4 && dxText.Text.ToUpper() != dxView.Text)
            {
                dxView.Text = dxText.Text.ToUpper();
                listBoxRejestrOperacji.Items.Insert(0, $"MOV DX, {dxText.Text.ToUpper()}                     PRZYPISZ");
            }
        }
        // 2. PRZYCISK WYCZYŚĆ
        private void buttonWyczysc_Click(object sender, EventArgs e)
        {
            axText.Text = "";
            bxText.Text = "";
            cxText.Text = "";
            dxText.Text = "";
        }
        // 3. PRZYCISK RANDOM
        private void buttonRandom_Click(object sender, EventArgs e)
        {
            string[] tablicaLiczbLosowych = new string[16];
            int y = 0;
            for (int i = 48; i <= 57; i++)
            {
                tablicaLiczbLosowych[y] += Convert.ToChar(i).ToString();
                y++;
            }

            for (int i = 65; i <= 70; i++)
            {
                tablicaLiczbLosowych[y] = Convert.ToChar(i).ToString();
                y++;
            }

            Random losowanie = new Random();
            char[] ilosc = new char[16];
            string los;
            for (int i = 0; i < tablicaLiczbLosowych.Length; i++)
            {
                los = tablicaLiczbLosowych[losowanie.Next(0, tablicaLiczbLosowych.Length)];
                ilosc[i] = Convert.ToChar(los);
            }
            axView.Text = ilosc[0].ToString() + ilosc[1].ToString() + ilosc[2].ToString() + ilosc[3].ToString();
            bxView.Text = ilosc[4].ToString() + ilosc[5].ToString() + ilosc[6].ToString() + ilosc[7].ToString();
            cxView.Text = ilosc[8].ToString() + ilosc[9].ToString() + ilosc[10].ToString() + ilosc[11].ToString();
            dxView.Text = ilosc[12].ToString() + ilosc[13].ToString() + ilosc[14].ToString() + ilosc[15].ToString();

            listBoxRejestrOperacji.Items.Insert(0, $"MOV AX, {axView.Text}                     RANDOM");
            listBoxRejestrOperacji.Items.Insert(0, $"MOV BX, {bxView.Text}                     RANDOM");
            listBoxRejestrOperacji.Items.Insert(0, $"MOV CX, {cxView.Text}                     RANDOM");
            listBoxRejestrOperacji.Items.Insert(0, $"MOV DX, {dxView.Text}                     RANDOM");
        }
        // 4. PRZYCISK RESET
        private void buttonReset_Click(object sender, EventArgs e)
        {
            if (axView.Text != "0000")
                listBoxRejestrOperacji.Items.Insert(0, $"MOV AX, 0000                            RESET");
            if (bxView.Text != "0000")
                listBoxRejestrOperacji.Items.Insert(0, $"MOV BX, 0000                            RESET");
            if (cxView.Text != "0000")
                listBoxRejestrOperacji.Items.Insert(0, $"MOV CX, 0000                            RESET");
            if (dxView.Text != "0000")
                listBoxRejestrOperacji.Items.Insert(0, $"MOV DX, 0000                            RESET");

            axView.Text = "0000";
            bxView.Text = "0000";
            cxView.Text = "0000";
            dxView.Text = "0000";
        }
        // 5. PRZYCISK MOV
        private void buttonMOV_Click(object sender, EventArgs e)
        {
            switch (comboBoxFROM.Text)
            {
                case "AX":
                    switch (comboBoxTO.Text)
                    {
                        case "AX":
                            break;
                        case "BX":
                            if (bxView.Text != axView.Text)
                            {
                                bxView.Text = axView.Text;
                                listBoxRejestrOperacji.Items.Insert(0, $"MOV BX, AX                               MOV");
                            }
                            break;
                        case "CX":
                            if (cxView.Text != axView.Text)
                            {
                                cxView.Text = axView.Text;
                                listBoxRejestrOperacji.Items.Insert(0, $"MOV CX, AX                               MOV");
                            }
                            break;
                        case "DX":
                            if (dxView.Text != axView.Text)
                            {
                                dxView.Text = axView.Text;
                                listBoxRejestrOperacji.Items.Insert(0, $"MOV DX, AX                               MOV");
                            }
                            break;
                    }
                    break;
                case "BX":
                    switch (comboBoxTO.Text)
                    {
                        case "AX":
                            if (axView.Text != bxView.Text)
                            {
                                axView.Text = bxView.Text;
                                listBoxRejestrOperacji.Items.Insert(0, $"MOV AX, BX                               MOV");
                            }
                            break;
                        case "BX":
                            break;
                        case "CX":
                            if (cxView.Text != bxView.Text)
                            {
                                cxView.Text = bxView.Text;
                                listBoxRejestrOperacji.Items.Insert(0, $"MOV CX, BX                               MOV");
                            }
                            break;
                        case "DX":
                            if (dxView.Text != bxView.Text)
                            {
                                dxView.Text = bxView.Text;
                                listBoxRejestrOperacji.Items.Insert(0, $"MOV DX, BX                               MOV");
                            }
                            break;
                    }
                    break;
                case "CX":
                    switch (comboBoxTO.Text)
                    {
                        case "AX":
                            if (axView.Text != cxView.Text)
                            {
                                axView.Text = cxView.Text;
                                listBoxRejestrOperacji.Items.Insert(0, $"MOV AX, CX                               MOV");
                            }
                            break;
                        case "BX":
                            if (bxView.Text != cxView.Text)
                            {
                                bxView.Text = cxView.Text;
                                listBoxRejestrOperacji.Items.Insert(0, $"MOV BX, CX                               MOV");
                            }
                            break;
                        case "CX":
                            break;
                        case "DX":
                            if (dxView.Text != cxView.Text)
                            {
                                dxView.Text = cxView.Text;
                                listBoxRejestrOperacji.Items.Insert(0, $"MOV DX, CX                               MOV");
                            }
                            break;
                    }
                    break;
                case "DX":
                    switch (comboBoxTO.Text)
                    {
                        case "AX":
                            if (axView.Text != dxView.Text)
                            {
                                axView.Text = dxView.Text;
                                listBoxRejestrOperacji.Items.Insert(0, $"MOV AX, DX                               MOV");
                            }
                            break;
                        case "BX":
                            if (bxView.Text != dxView.Text)
                            {
                                bxView.Text = dxView.Text;
                                listBoxRejestrOperacji.Items.Insert(0, $"MOV BX, DX                               MOV");
                            }
                            break;
                        case "CX":
                            if (cxView.Text != dxView.Text)
                            {
                                cxView.Text = dxView.Text;
                                listBoxRejestrOperacji.Items.Insert(0, $"MOV CX, DX                               MOV");
                            }
                            break;
                        case "DX":
                            break;
                    }
                    break;
            }
        }
        // 6. PRZYCISK XCHG
        private void buttonXCHG_Click(object sender, EventArgs e)
        {
            switch (comboBoxFROM.Text)
            {
                case "AX":
                    {
                        switch (comboBoxTO.Text)
                        {
                            case "AX":
                                break;
                            case "BX":
                                {
                                    XCHG = axView.Text;
                                    axView.Text = bxView.Text;
                                    bxView.Text = XCHG;
                                    listBoxRejestrOperacji.Items.Insert(0, $"XCHG BX, AX                              XCHG");
                                    break;
                                }
                            case "CX":
                                {
                                    XCHG = axView.Text;
                                    axView.Text = cxView.Text;
                                    cxView.Text = XCHG;
                                    listBoxRejestrOperacji.Items.Insert(0, $"XCHG CX, AX                              XCHG");
                                    break;
                                }
                            case "DX":
                                {
                                    XCHG = axView.Text;
                                    axView.Text = dxView.Text;
                                    dxView.Text = XCHG;
                                    listBoxRejestrOperacji.Items.Insert(0, $"XCHG DX, AX                              XCHG");
                                    break;
                                }
                        }
                        break;
                    }
                case "BX":
                    {
                        switch (comboBoxTO.Text)
                        {
                            case "AX":
                                {
                                    XCHG = bxView.Text;
                                    bxView.Text = axView.Text;
                                    axView.Text = XCHG;
                                    listBoxRejestrOperacji.Items.Insert(0, $"XCHG AX, BX                              XCHG");
                                    break;
                                }
                            case "BX":
                                break;
                            case "CX":
                                {
                                    XCHG = bxView.Text;
                                    bxView.Text = cxView.Text;
                                    cxView.Text = XCHG;
                                    listBoxRejestrOperacji.Items.Insert(0, $"XCHG CX, BX                              XCHG");
                                    break;
                                }
                            case "DX":
                                {
                                    XCHG = bxView.Text;
                                    bxView.Text = dxView.Text;
                                    dxView.Text = XCHG;
                                    listBoxRejestrOperacji.Items.Insert(0, $"XCHG DX, BX                              XCHG");
                                    break;
                                }
                        }
                        break;
                    }
                case "CX":
                    {
                        switch (comboBoxTO.Text)
                        {
                            case "AX":
                                {
                                    XCHG = cxView.Text;
                                    cxView.Text = axView.Text;
                                    axView.Text = XCHG;
                                    listBoxRejestrOperacji.Items.Insert(0, $"XCHG AX, CX                              XCHG");
                                    break;
                                }
                            case "BX":
                                {
                                    XCHG = cxView.Text;
                                    cxView.Text = bxView.Text;
                                    bxView.Text = XCHG;
                                    listBoxRejestrOperacji.Items.Insert(0, $"XCHG BX, CX                              XCHG");
                                    break;
                                }
                            case "CX":
                                break;
                            case "DX":
                                {
                                    XCHG = cxView.Text;
                                    cxView.Text = dxView.Text;
                                    dxView.Text = XCHG;
                                    listBoxRejestrOperacji.Items.Insert(0, $"XCHG DX, CX                              XCHG");
                                    break;
                                }
                        }
                        break;
                    }
                case "DX":
                    {
                        switch (comboBoxTO.Text)
                        {
                            case "AX":
                                {
                                    XCHG = dxView.Text;
                                    dxView.Text = axView.Text;
                                    axView.Text = XCHG;
                                    listBoxRejestrOperacji.Items.Insert(0, $"XCHG AX, DX                              XCHG");
                                    break;
                                }
                            case "BX":
                                {
                                    XCHG = dxView.Text;
                                    dxView.Text = bxView.Text;
                                    bxView.Text = XCHG;
                                    listBoxRejestrOperacji.Items.Insert(0, $"XCHG BX, DX                              XCHG");
                                    break;
                                }
                            case "CX":
                                {
                                    XCHG = dxView.Text;
                                    dxView.Text = cxView.Text;
                                    cxView.Text = XCHG;
                                    listBoxRejestrOperacji.Items.Insert(0, $"XCHG CX, DX                              XCHG");
                                    break;
                                }
                            case "DX":
                                break;
                        }
                        break;
                    }
            }
        }


        // 
        private void buttonPrzypisz1_Click(object sender, EventArgs e)
        {
            if (siText.Text.IsHexString() != true)
            {
                return;
            }
            if (diText.Text.IsHexString() != true)
            {
                return;
            }
            if (bpText.Text.IsHexString() != true)
            {
                return;
            }
            if (dispText.Text.IsHexString() != true)
            {
                return;
            }

            if (siText.Text.Length == 4)
                siView.Text = siText.Text.ToUpper();
            if (diText.Text.Length == 4)
                diView.Text = diText.Text.ToUpper();
            if (bpText.Text.Length == 4)
                bpView.Text = bpText.Text.ToUpper();
            if (dispText.Text.Length == 4)
                dispView.Text = dispText.Text.ToUpper();
        }
        private void buttonCLEAR2_Click(object sender, EventArgs e)
        {
            siText.Text = "";
            diText.Text = "";
            bpText.Text = "";
            dispText.Text = "";
            Array.Clear(TABLICA, 0, TABLICA.Length - 1);
        }

        private void buttonMOV2_Click(object sender, EventArgs e)
        {
            switch (comboBoxPOLACZENIE.Text)
            {
                case "AX":
                    pobierz = axView.Text;
                    break;
                case "BX":
                    pobierz = bxView.Text;
                    break;
                case "CX":
                    pobierz = cxView.Text;
                    break;
                case "DX":
                    pobierz = dxView.Text;
                    break;
            }

            if (comboBoxKierunek.SelectedIndex == 0) // z rejestru do pamięci
            {
                if (radioButtonIndeksowy.Checked == true)
                {
                    if (comboBoxIndeksowy.SelectedIndex == 0) // SI
                    {
                        starszyAdres = pobierz.Substring(0, 2);
                        mlodszyAdres = pobierz.Substring(2, 2);

                        siDec = int.Parse(siView.Text, System.Globalization.NumberStyles.HexNumber); // SI decymalne
                        dispDec = int.Parse(dispView.Text, System.Globalization.NumberStyles.HexNumber); // DISP decymalne
                        sumDec = (siDec + dispDec);

                        TABLICA[sumDec] = mlodszyAdres;
                        TABLICA[sumDec + 1] = starszyAdres;

                    }
                    else if (comboBoxIndeksowy.SelectedIndex == 1) // DI
                    {
                        starszyAdres = pobierz.Substring(0, 2);
                        mlodszyAdres = pobierz.Substring(2, 2);

                        diDec = int.Parse(diView.Text, System.Globalization.NumberStyles.HexNumber);
                        dispDec = int.Parse(dispView.Text, System.Globalization.NumberStyles.HexNumber);
                        sumDec = (diDec + dispDec);

                        TABLICA[sumDec] = mlodszyAdres;
                        TABLICA[sumDec + 1] = starszyAdres;
                    }
                    else
                    {
                        return;
                    }
                }
                else if (radioButtonBAZOWY.Checked == true)
                {
                    if (comboBoxBazowy.SelectedIndex == 0) // BX
                    {
                        starszyAdres = pobierz.Substring(0, 2);
                        mlodszyAdres = pobierz.Substring(2, 2);

                        bxDec = int.Parse(bxView.Text, System.Globalization.NumberStyles.HexNumber); // BX decymalne
                        dispDec = int.Parse(dispView.Text, System.Globalization.NumberStyles.HexNumber); // DISP decymalne
                        sumDec = (bxDec + dispDec);

                        TABLICA[sumDec] = mlodszyAdres;
                        TABLICA[sumDec + 1] = starszyAdres;
                    }
                    else if (comboBoxBazowy.SelectedIndex == 1) // BP
                    {
                        starszyAdres = pobierz.Substring(0, 2);
                        mlodszyAdres = pobierz.Substring(2, 2);

                        bpDec = int.Parse(bpView.Text, System.Globalization.NumberStyles.HexNumber); // BP decymalne
                        dispDec = int.Parse(dispView.Text, System.Globalization.NumberStyles.HexNumber); // DISP decymalne
                        sumDec = (bpDec + dispDec);

                        TABLICA[sumDec] = mlodszyAdres;
                        TABLICA[sumDec + 1] = starszyAdres;
                    }
                    else
                    {
                        return;
                    }
                }
                else if (radioButtonIndeksowoBazowy.Checked == true)
                {
                    if (comboBoxIndeksowoBazowy.SelectedIndex == 0) // SI + BX
                    {
                        starszyAdres = pobierz.Substring(0, 2);
                        mlodszyAdres = pobierz.Substring(2, 2);

                        siDec = int.Parse(siView.Text, System.Globalization.NumberStyles.HexNumber); // SI decymalne
                        bxDec = int.Parse(bxView.Text, System.Globalization.NumberStyles.HexNumber); // BX decymalne
                        dispDec = int.Parse(dispView.Text, System.Globalization.NumberStyles.HexNumber); // DISP decymalne
                        sumDec = (siDec + bxDec + dispDec);

                        TABLICA[sumDec] = mlodszyAdres;
                        TABLICA[sumDec + 1] = starszyAdres;
                    }
                    else if (comboBoxIndeksowoBazowy.SelectedIndex == 1) // SI + BP
                    {
                        starszyAdres = pobierz.Substring(0, 2);
                        mlodszyAdres = pobierz.Substring(2, 2);

                        siDec = int.Parse(siView.Text, System.Globalization.NumberStyles.HexNumber); // SI decymalne
                        bpDec = int.Parse(bpView.Text, System.Globalization.NumberStyles.HexNumber); // BX decymalne
                        dispDec = int.Parse(dispView.Text, System.Globalization.NumberStyles.HexNumber); // DISP decymalne
                        sumDec = (siDec + bpDec + dispDec);

                        TABLICA[sumDec] = mlodszyAdres;
                        TABLICA[sumDec + 1] = starszyAdres;
                    }
                    else if (comboBoxIndeksowoBazowy.SelectedIndex == 2) // DI + BX
                    {
                        starszyAdres = pobierz.Substring(0, 2);
                        mlodszyAdres = pobierz.Substring(2, 2);

                        diDec = int.Parse(diView.Text, System.Globalization.NumberStyles.HexNumber); // SI decymalne
                        bxDec = int.Parse(bxView.Text, System.Globalization.NumberStyles.HexNumber); // BX decymalne
                        dispDec = int.Parse(dispView.Text, System.Globalization.NumberStyles.HexNumber); // DISP decymalne
                        sumDec = (diDec + bxDec + dispDec);

                        TABLICA[sumDec] = mlodszyAdres;
                        TABLICA[sumDec + 1] = starszyAdres;
                    }
                    else if (comboBoxIndeksowoBazowy.SelectedIndex == 3) // DI + BP
                    {
                        starszyAdres = pobierz.Substring(0, 2);
                        mlodszyAdres = pobierz.Substring(2, 2);

                        diDec = int.Parse(diView.Text, System.Globalization.NumberStyles.HexNumber); // SI decymalne
                        bpDec = int.Parse(bpView.Text, System.Globalization.NumberStyles.HexNumber); // BX decymalne
                        dispDec = int.Parse(dispView.Text, System.Globalization.NumberStyles.HexNumber); // DISP decymalne
                        sumDec = (diDec + bpDec + dispDec);

                        TABLICA[sumDec] = mlodszyAdres;
                        TABLICA[sumDec + 1] = starszyAdres;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
            else // z pamięci do rejestru
            {
                if (radioButtonIndeksowy.Checked == true)
                {
                    if (comboBoxIndeksowy.SelectedIndex == 0) // SI
                    {

                        siDec = int.Parse(siView.Text, System.Globalization.NumberStyles.HexNumber);
                        dispDec = int.Parse(dispView.Text, System.Globalization.NumberStyles.HexNumber);
                        sumDec = (siDec + dispDec);

                        odczyt = TABLICA[sumDec + 1] + TABLICA[sumDec];
                        sumHex = sumDec.ToString("X");
                        if (odczyt.Length == 4)
                            switch (comboBoxPOLACZENIE.Text)
                            {
                                case "AX":
                                    axView.Text = odczyt;
                                    break;
                                case "BX":
                                    bxView.Text = odczyt;
                                    break;
                                case "CX":
                                    cxView.Text = odczyt;
                                    break;
                                case "DX":
                                    dxView.Text = odczyt;
                                    break;
                            }
                    }
                    else // DI
                    {
                        diDec = int.Parse(siView.Text, System.Globalization.NumberStyles.HexNumber);
                        dispDec = int.Parse(dispView.Text, System.Globalization.NumberStyles.HexNumber);
                        sumDec = (diDec + dispDec);

                        odczyt = TABLICA[sumDec + 1] + TABLICA[sumDec];
                        sumHex = sumDec.ToString("X");
                        if (odczyt.Length == 4)
                            switch (comboBoxPOLACZENIE.Text)
                            {
                                case "AX":
                                    axView.Text = odczyt;
                                    break;
                                case "BX":
                                    bxView.Text = odczyt;
                                    break;
                                case "CX":
                                    cxView.Text = odczyt;
                                    break;
                                case "DX":
                                    dxView.Text = odczyt;
                                    break;
                            }
                    }
                }
                else if (radioButtonBAZOWY.Checked == true)
                {
                    if (comboBoxBazowy.SelectedIndex == 0) // BX
                    {

                    }
                    else // BP
                    {

                    }
                }
                else if (radioButtonIndeksowoBazowy.Checked == true)
                {
                    if (comboBoxIndeksowoBazowy.SelectedIndex == 0) // SI + BX
                    {

                    }
                    else if (comboBoxIndeksowoBazowy.SelectedIndex == 1) // SI + BP
                    {

                    }
                    else if (comboBoxIndeksowoBazowy.SelectedIndex == 2) // DI + BX
                    {

                    }
                    else // DI + BP
                    {

                    }
                }
                else
                {
                    return;
                }
            }
        }
        private void radioButtonIndeksowy_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonIndeksowy.Checked)
            {
                comboBoxBazowy.Visible = false;
                comboBoxIndeksowy.Visible = true;
                comboBoxIndeksowoBazowy.Visible = false;
                comboBoxPOLACZENIE.Visible = true;
                labelAdresowanie.Text = "Adresowanie indeksowe";
            }

        }
        private void radioButtonBAZOWY_CheckedChanged(object sender, EventArgs e)
                {
                    if (radioButtonBAZOWY.Checked)
                    {
                        comboBoxBazowy.Visible = true;
                        comboBoxIndeksowy.Visible = false;
                        comboBoxIndeksowoBazowy.Visible = false;
                        comboBoxPOLACZENIE.Visible = true;
                    }

                }
        private void radioButtonIndeksowoBazowy_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonIndeksowoBazowy.Checked)
            {
                comboBoxBazowy.Visible = false;
                comboBoxIndeksowy.Visible = false;
                comboBoxIndeksowoBazowy.Visible = true;
                comboBoxPOLACZENIE.Visible = true;
                labelAdresowanie.Text = "Adresowanie indeksowo\n                          bazowe";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            listBox2.Items.Clear();
            for (int i = sumDec; i < sumDec + 10; i++)
            {

                listBox2.Items.Add($"{i.ToString("X")}: {TABLICA[i]}");
            }
        }

        


    }
    public static class StringExtensions
    {
        public static bool IsHexString(this string str)
        {
            foreach (var c in str)
            {
                var isHex = ((c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F'));

                if (!isHex)
                {
                    return false;
                }
            }

            return true;
        }
        //bonus, verify whether a string can be parsed as byte[]
        public static bool IsParseableToByteArray(this string str)
        {
            return IsHexString(str) && str.Length % 2 == 0;
        }
    }
}
