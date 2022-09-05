using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bank2
{
    class Program
    {
        static void Kiiras()
        {
            string a = "*";
            string b = "***************************";
            string c = " Válasszon opciót!";
            string d = " 1. Egyenleg lekérdezés";
            string e = " 2. Pénz betétel";
            string f = " 3. Pénz felvétel";
            string g = " 4. Kilépés";
            for (int i = 0; i <= 8; i++)
            {
                if (i == 0 || i == 8)
                {
                    Console.WriteLine(b);
                }
                if (i == 1 || i == 7)
                {
                    Console.WriteLine(a);
                }
                if (i == 2)
                {
                    Console.WriteLine(a + c);
                }
                if (i == 3)
                {
                    Console.WriteLine(a + d);
                }
                if (i == 4)
                {
                    Console.WriteLine(a + e);
                }
                if (i == 5)
                {
                    Console.WriteLine(a + f);
                }
                if (i == 6)
                {
                    Console.WriteLine(a + g);
                }
            }
        }

        static void Ujugyfel(int n)
        {
            if (n == 1)
            {
                Console.Write("Üdvözöljük bankunknál!\r\nKérem írja be a kért adatokat!\r\nNév? ");
                string nev = Console.ReadLine();
                Console.Write("Ügyfélazonosító: ");
                string uk = Console.ReadLine();
                Console.Write("PIN kód: ");
                string pin = Console.ReadLine();
                Console.Write("Pénz befizetés? 1-Igen ; 2-Nem\r\n");
                int money = int.Parse(Console.ReadLine());
                Console.Write("Összeg: ");
                string osszeg = Console.ReadLine();
                StreamWriter s = new StreamWriter("ugyfeladat.txt", true);
                s.WriteLine(nev + ";" + uk + ";" + pin + ";" + osszeg + ";\r\n");
                s.Close();
                Console.WriteLine("Viszontlátásra!");
                Console.ReadKey();
                System.Environment.Exit(1);
            }
        }

        static int Sorokszama()
        {
            int sorok = 0;
            StreamReader s = new StreamReader("ugyfeladat.txt");            
            while (!s.EndOfStream)
            {
                string sor = s.ReadLine();
                sorok++;
            }
            s.Close();
            return sorok;
        }

        static string[,] Tomb(int n)
        {
            string[,] tomb = new string[n, 4];
            int a = 0;
            StreamReader s = new StreamReader("ugyfeladat.txt");
            while (!s.EndOfStream)
            {
                string sor = s.ReadLine();
                for (int i = 0; i < 4; i++)
                {
                    int index = sor.IndexOf(";");
                    tomb[a, i] = sor.Substring(0, index);
                    sor = sor.Remove(0, index + 1);
                }
                a++;
            }
            s.Close();
            return tomb;
        }

        static bool Beengedes(int n, int p, string[,] m, int t)
        {
            bool jo = false;
            int a = 0;           
            int i = 0;
            while (i < t)
            {
                int b = int.Parse(m[a, 1]);
                int c = int.Parse(m[a, 2]);
                if (n == b && p == c)
                {
                    jo = true;
                }
                else
                {
                    a++;
                }
                i++;
            }
            return jo;
        }

        static string Nev(int n, string[,] m, int t)
        {
            string nev = null;
            int a = 0;            
            int i = 0;
            while (i < t)
            {
                int b = int.Parse(m[a, 1]);
                if (n == b)
                {
                    nev = m[a, 0];
                }
                else
                {
                    a++;
                }
                i++;
            }           
            return nev;
        }

        static int Uszam(int n, string[,] m, int t)
        {
            string uszam = null;
            int a = 0;
            int i = 0;
            while (i < t)
            {
                int b = int.Parse(m[a, 1]);
                if (n == b)
                {
                    uszam = m[a, 0];
                }
                else
                {
                    a++;
                }
                i++;
            }
            return a;
        }

        static void Egyenleg(string[,] m, int u)
        {
            Console.Write("Az ön egyenlege: " + m[u, 3] + " Ft");
        }

        static int Betet(string [,] m, int u)
        {
            Console.Write("Behelyezendő összeg: ");
            int be = int.Parse(Console.ReadLine());
            int volt = int.Parse(m[u, 3]);
            volt = volt + be;
            Console.Write("Az Ön új egyenlege: " + volt + " Ft\r\n");
            return volt;
        }

        static int Kivet(string[,] m, int u)
        {
            Console.Write("Kivétel összege: ");
            int ki = int.Parse(Console.ReadLine());
            int volt = int.Parse(m[u, 3]);
            volt = volt - ki;
            Console.Write("Az Ön új egyenlege: " + volt + " Ft\r\n");
            return volt;
        }

        static void Ujadatok(int n, string[,] m, int v, int t)
        {
            int a = 0;
            int i = 0;
            while (i < t)
            {
                int b = int.Parse(m[a, 1]);
                if (n == b)
                {
                     m[a, 3] = v.ToString();
                }
                else
                {
                    a++;
                }
                i++;
            }
            StreamWriter s = new StreamWriter("ugyfeladat.txt");
            for (int j = 0; j < m.GetLength(0); j++)
            {
                for (int k = 0; k < m.GetLength(1); k++)
                {
                    s.Write(m[j,k] + ";");
                }
                Console.WriteLine("\r\n");
            }

            s.Close();
        }



        static void Main(string[] args)
        {
            //1. lépés --- Új/nemúj
            Console.Write("Új ügyfél?\r\nAmennyiben igen, kérem nyomja meg az 1-es gombot, ha bankfiókkal rendelkezik, a 2-est: ");
            int ell = int.Parse(Console.ReadLine());
            Ujugyfel(ell);
            
            //2. lépés --- Belépés
             
             Console.Write("Tisztelt Ügyfél!\r\nKérjük adja meg az ügyfélazonosítóját: ");
             int beuk1 = int.Parse(Console.ReadLine());
            Console.Write("PIN kód: ");
            int pin = int.Parse(Console.ReadLine());

            //3. lépés --- Beazonosítás
             var s = Sorokszama();
             var t = Tomb(Sorokszama());
             var u = Uszam(beuk1, t, Sorokszama());
             bool kezdes = Beengedes(beuk1, pin, t, Sorokszama());
             if (kezdes == true)
             {
                 Console.WriteLine("\r\nÜdvözöljük Tisztelt " + Nev(beuk1, t, Sorokszama()));
                 Kiiras();
             }
             else
             {
                 Console.WriteLine("Hibás ügyfélazonosító vagy PIN kód!");
                 System.Environment.Exit(1);
             }

            //4. lépés --- Feladatválasztás
             
            Console.Write("Opció: ");
            int opc1 = int.Parse(Console.ReadLine());
                         
            if (opc1 == 1) //le
            {
                Egyenleg(t, u);
                Console.WriteLine("Viszontlátásra!");
            }
            if (opc1 == 2) //be
            {
                var v = Betet(t, u);
                Console.WriteLine("Viszontlátásra!");
                Ujadatok(beuk1, t, v, s);
            }
            if (opc1 == 3) //ki
            {
                var v = Kivet(t, u);
                Console.WriteLine("Viszontlátásra!");
                Ujadatok(beuk1 ,t, v, s);
            }
            if (opc1 == 4) //off
            {
                Console.Clear();
                Console.WriteLine("Viszontlátásra!");
            }
            Console.ReadKey();
        }
    }
}
