using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Программа для решения волнового уравнения
/// </summary>
namespace WaveEqu
{
    class Program
    {
        static void Main(string[] args)
        {
            double t = 1; //t
            double x = 0; //x
            string stroka; //переменная для хранения данных строк
            double g2 = 0; //g*
            double f = 0; //f(x,t)
            double σ1 = 0; //сигма1
            double σ2 = 0; //сигма2
            double p0 = 1; //p0
            double p1 = 0; //p1
            double s0 = 1; //s0
            double s1 = 0; //s1
            double coefA; //коэф A
            double TAU = 0; //T
            double Bt = 0; //B(t)
            double h; //h
            int NSetka; //сетка
            float maxValue1 = 0; //максимальное в явном мкр
            float maxValue2 = 0; //максимальное в неявном мкр
            float del; //ошибка
            Console.WriteLine("Волновое уравнение: Utt = g^2(x,t)* Uxx + f(x,t), x = (0,1), t = (0,T)");
            Console.WriteLine("U(x,0) = σ1(x), U1(x,0) = σ2(x)");
            Console.WriteLine("p0*U(0,t)+p1*Ux(0,t) = A(t)");
            Console.WriteLine("s0*U(1,t) + s1*Ux(1,t) = B(t)");
        gg:
            Console.Write("Введите шаг по пространству: ");//запрос на ввод данных
            stroka = Console.ReadLine();                    //считывание с консоли
            h = Convert.ToDouble(stroka);                   //конвертация
            Console.Write("Введите шаг по времени: ");//запрос на ввод данных
            stroka = Console.ReadLine();               //считывание с консоли
            TAU = Convert.ToDouble(stroka);            //конвертация

            Console.Write("Введите x: ");//запрос на ввод данных
            stroka = Console.ReadLine();  //считывание с консоли
            x = Convert.ToDouble(stroka); //конвертация

            Console.Write("Введите t: ");//запрос на ввод данных
            stroka = Console.ReadLine();  //считывание с консоли
            t = Convert.ToDouble(stroka); //конвертация

            Console.Write("Введите размер сетки: ");//запрос на ввод данных
            stroka = Console.ReadLine();             //считывание с консоли
            NSetka = Convert.ToInt32(stroka);        //конвертация
            if ((x >= 0 && x <= 1) && (t >= 0 && t <= TAU))//условие на границы t
            {

            }
            else goto gg;

            float[,] massivWave = new float[NSetka + 5, NSetka + 5]; //переменная для массива
            float[,] massivWave2 = new float[NSetka + 5, NSetka + 5]; //переменная для массив
            for (int i = 0; i < NSetka; i++)
            {
                massivWave[i, 0] = 0;
                massivWave2[i, 0] = 0;
            }
            g2 = 4 * (x + 0.1);
            f = t * (0.01 + t);
            coefA = 0.001 * Math.Sin(t);

            for (int n = 1; n < NSetka; n++)
            {
                for (int i = 1; i < NSetka; i++)
                {
                    //решение метода
                    massivWave[i, n] = (float)(((massivWave[i, n + 1] + massivWave[i, n - 1]) / 2) - coefA * ((massivWave[i - 1, n - 1] - 2 * massivWave[i, n - 1] + massivWave[i + 1, n - 1]) / (Math.Pow(h, 2) * Math.Pow(TAU, 2))) - f / Math.Pow(TAU, 2));
                    maxValue1 = massivWave[i, n];
                    if (massivWave[i, n] > maxValue1) maxValue1 = massivWave[i, n];
                }
            }

            for (int n = 1; n < NSetka; n++)
            {
                for (int i = 1; i < NSetka; i++)
                {
                    //решение метода
                    massivWave2[i, n] = (float)(((massivWave2[i, n + 1] + massivWave2[i, n - 1]) / 2) - coefA * ((massivWave2[i - 1, n + 1] - 2 * massivWave2[i, n + 1] + massivWave2[i + 1, n + 1]) / (Math.Pow(h, 2) * Math.Pow(TAU, 2))) - f / Math.Pow(TAU, 2));
                    maxValue2 = massivWave2[i, n];
                    if (massivWave2[i, n] > maxValue2) maxValue2 = massivWave2[i, n];
                }
            }
            Console.WriteLine("Явный МКР: ");
            for (int j = NSetka - 1; j != 0; j--)
            {
                Console.Write("|" + " ");

                for (int i = 1; i < NSetka; i++)
                {

                    Console.Write(massivWave[i, j] + "\t" + "|");
                    //workSheet.Cells[i+1, j+1] = massivResult[i,j];
                }
                Console.WriteLine();
                Console.Write(new string('-', NSetka * 5));
                Console.WriteLine();
            }
            Console.Write("|" + " ");

            for (int i = 0; i < NSetka; i++)
            {

                Console.Write("{0}", massivWave[i, 0] + "\t" + "|");
            }
            Console.WriteLine();
            Console.Write(new string('-', NSetka * 5));

            Console.WriteLine("Неявный МКР: ");
            for (int j = NSetka - 1; j != 0; j--)
            {
                Console.Write("|" + " ");

                for (int i = 1; i < NSetka; i++)
                {

                    Console.Write(massivWave2[i, j] + "\t" + "|");
                    //workSheet.Cells[i+1, j+1] = massivResult[i,j];
                }
                Console.WriteLine();
                Console.Write(new string('-', NSetka * 5));
                Console.WriteLine();
            }
            Console.Write("|" + " ");

            for (int i = 0; i < NSetka; i++)
            {

                Console.Write("{0}", massivWave2[i, 0] + "\t" + "|");
            }
            Console.WriteLine();
            Console.Write(new string('-', NSetka * 5));




            del = maxValue1 - maxValue2;
            Console.WriteLine();
            Console.WriteLine("Погрешность: {0}", -del);


            
            Console.ReadKey(true);
        }
    }
}
