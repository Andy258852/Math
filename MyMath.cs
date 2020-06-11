using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;

namespace MyMath
{
    public static class NodAndNok
    {
        private static void Swap(ref BigInteger a, ref BigInteger b)
        {
            BigInteger tmp = a;
            a = b;
            b = tmp;
        }

        public static BigInteger GetNod(string a, string b)
        {
            BigInteger aa = BigInteger.Parse(a);
            BigInteger bb = BigInteger.Parse(b);
            return BigInteger.GreatestCommonDivisor(aa, bb);
        }

        public static BigInteger GetNok(string a, string b)
        {
            BigInteger aa = BigInteger.Parse(a);
            BigInteger bb = BigInteger.Parse(b);
            return BigInteger.Divide(BigInteger.Multiply(aa, bb), GetNod(a, b));
        }
    }

    public static class Div
    {
        public static BigInteger GetDivision(string a, string b)
        {
            BigInteger aa = BigInteger.Parse(a);
            BigInteger bb = BigInteger.Parse(b);
            return BigInteger.Divide(aa, bb);
        }

        public static BigInteger GetRemainder(string a, string b)
        {
            BigInteger aa = BigInteger.Parse(a);
            BigInteger bb = BigInteger.Parse(b);
            BigInteger.DivRem(aa, bb, out BigInteger remainder);
            return remainder;
        }
    }

    public static class InputController
    {
        public static bool CheckNumber(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (!Char.IsDigit(input[i]))
                    return false;
            }
            return true;
        }

        public static bool CheckLong(string number, ref long value, int max)
        {
            if (long.TryParse(number, out value))
                return false;
            else
                return value <= max;
        }
    }

    public static class Sieve
    {
        public static int MaxSize { get; } = 100000000;

        public static void GetFullArray(out int[] a, int number)
        {
            a = new int[number + 1];
            for (int i = 0; i < number + 1; i++)
                a[i] = i;
            for (int p = 2; p < number + 1; p++)
            {
                if (a[p] != 0)
                {
                    for (uint j = (uint)(p * p); j < number + 1; j += (uint)p)
                        a[j] = 0;
                }
            }
            a[1] = 0;
        }

        public static void GetArray(out int[] a, int maxNumber)
        {
            GetFullArray(out int[] temp, maxNumber);
            int counter = 0;
            for (int i = 0; i < maxNumber; i++)
            {
                if (temp[i] != 0)
                {
                    counter++;
                }
            }
            a = new int[counter];
            counter = 0;
            for (int i = 0; i < maxNumber; i++)
            {
                if (temp[i] != 0)
                {
                    a[counter] = temp[i];
                    counter++;
                }
            }
        }

        public static bool CheckNumber(string number, ref int value)
        {
            if (int.TryParse(number, out value))
                return false;
            else
                return value <= MaxSize;
        }
    }

    public static class Decomposition
    {
        public static long MaxSize { get; } = 10000000000000000;

        public static void GetList(out List<long> a, long value)
        {
            a = new List<long>();
            Sieve.GetArray(out int[] temp, (int)Math.Round(Math.Sqrt(value)));
            for (int i = 0; i < temp.Length; i++)
            {
                if (value % temp[i] == 0)
                {
                    value /= temp[i];
                    a.Add(temp[i]);
                    i--;
                }
            }
            if (value != 1)
            {
                a.Add(value);
            }
        }

        public static long[] GetArr(long value)
        {
            GetList(out List<long> a, value);
            long[] result = new long[a.Count];
            for(int i = 0; i < a.Count; i++)
            {
                result[i] = a[i];
            }
            a.Clear();
            return result;
        }
    }

    public static class Encryption
    {
        private static bool CheckReplacementKey(string key)
        {
            if (key.Length > 34)
                return false;
            List<char> temp = new List<char>();
            for (int i = 0; i < key.Length; i++)
            {
                if (temp.Contains(key[i]))
                    return false;
                else
                    temp.Add(key[i]);
            }
            temp.Clear();
            return true;
        }

        public static string Replacement(string text, string key)
        {
            StringBuilder msg = new StringBuilder(text);
            for (int i = 0; i < msg.Length; i++)
            {
                if (msg[i] >= 'а' && msg[i] <= 'а' + key.Length)
                {
                    msg[i] = key[msg[i] - 'а' + 1];
                }
                else if (msg[i] == ' ')
                    msg[i] = key[0];
            }
            return msg.ToString();
        }

        public static string Cezar(string text, char letter)
        {
            char Shift(char symbol)
            {
                int shift = letter - 'а';
                for (int i = 0; i < shift; i++)
                {
                    if (symbol == 'я')
                    {
                        symbol = 'а';
                    }
                    else
                    {
                        symbol++;
                    }
                }
                return symbol;
            }

            StringBuilder encoded = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                char symbol;
                if (char.IsLetter(text[i]))
                {
                    symbol = Shift(char.ToLower(text[i]));
                }
                else
                {
                    symbol = text[i];
                }
                encoded.Append(symbol);
            }
            return encoded.ToString();
        }

        public static string VigenereEncryption(string text, string key)
        {
            StringBuilder encoded = new StringBuilder();
            for (int i = 0, j = 0; i < text.Length; i++, j++)
            {
                if (j == key.Length)
                {
                    j = 0;
                }
                encoded.Append(Cezar(text[i].ToString(), key[j]));
            }
            return encoded.ToString();
        }
    }

    public static class Decryption
    {
        private static bool CheckReplacementKey(string key)
        {
            if (key.Length > 34)
                return false;
            List<char> temp = new List<char>();
            for (int i = 0; i < key.Length; i++)
            {
                if (temp.Contains(key[i]))
                    return false;
                else
                    temp.Add(key[i]);
            }
            temp.Clear();
            return true;
        }

        public static string Replacement(string text, string key)
        {
            StringBuilder msg = new StringBuilder(text);
            for (int i = 0; i < msg.Length; i++)
            {
                int temp = key.IndexOf(msg[i]);
                if (temp > 0)
                {
                    msg[i] = (char)('а' + temp - 1);
                }
                else if (msg[i] == key[0])
                    msg[i] = ' ';

            }
            return msg.ToString();
        }

        public static string CezarDecryption(string text, char letter)
        {
            char Shift(char symbol)
            {
                int shift = letter - 'а';
                for (int i = 0; i < shift; i++)
                {
                    if (symbol == 'а')
                    {
                        symbol = 'я';
                    }
                    else
                    {
                        symbol--;
                    }
                }
                return symbol;
            }

            StringBuilder encoded = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                char symbol;
                if (char.IsLetter(text[i]))
                {
                    symbol = Shift(char.ToLower(text[i]));
                }
                else
                {
                    symbol = text[i];
                }
                encoded.Append(symbol);
            }
            return encoded.ToString();
        }

        public static string VigenereDecryption(string text, string key)
        {
            StringBuilder encoded = new StringBuilder();
            for (int i = 0, j = 0; i < text.Length; i++, j++)
            {
                if (j == key.Length)
                {
                    j = 0;
                }
                encoded.Append(Encryption.Cezar(text[i].ToString(), key[j]));
            }
            return encoded.ToString();
        }
    }

    public static class Diofant
    {
        public static void CountDiofant(int a, int b, int c, int d, out int x, out int y)
        {
            void GetSolve(int a1, int b1, out int x1, out int y1)
            {

                int s;

                if (b1 == 0)
                {
                    x1 = 1;
                    y1 = 0;
                    return;
                }

                GetSolve(b1, a1 % b1, out x1, out y1);

                s = y1;

                y1 = x1 - (a1 / b1) * y1;

                x1 = s;

            }

            if (c % d != 0)
            {
                throw new ArgumentException("Unable to solve in integers");
            }

            if (a > b)
            {
                GetSolve(a / d, b / d, out x, out y);
            }
            else
            {
                GetSolve(b / d, a / d, out y, out x);
            }

            x *= c / d;
            y *= c / d;
        }

        public static string DiofantSolution(int a, int b, int c)
        {
            int d = int.Parse(BigInteger.GreatestCommonDivisor((BigInteger)a, (BigInteger)b).ToString());
            CountDiofant(a, b, c, d, out int x, out int y);
            return "x = " + x + " + " + b / d + "k\n" + "y = " + y + " + " + (-1) * a / d + "k";
        }
    }

    public static class Kelly
    {
        public static int[,] CountTable(int residue, char operation)
        {
            if (operation != '+' && operation != '*')
            {
                throw new ArgumentException("Wrong operation on residue class");
            }
            int[,] kellyTable = new int[residue, residue];

            if (operation == '+')
            {
                for (int i = 0; i < residue; i++)
                {
                    for (int j = 0; j < residue; j++)
                    {
                        kellyTable[i, j] = (i + j) % residue;
                    }
                }
            }
            if (operation == '*')
            {
                for (int i = 0; i < residue; i++)
                {
                    for (int j = 0; j < residue; j++)
                    {
                        kellyTable[i, j] = (i * j) % residue;
                    }
                }
            }
            return kellyTable;
        }

        public static string FormatedKelly(int residue, char operation)
        {
            int[,] kellyTable = CountTable(residue, operation);
            StringBuilder formated = new StringBuilder();
            formated.Append("  ");
            for (int i = 0; i < kellyTable.GetLength(0); i++)
            {
                formated.Append("|" + i + "|");
            }
            formated.Append("\n");
            for (int i = 0; i < kellyTable.GetLength(0); i++)
            {
                formated.Append(i + "|");
                for (int j = 0; j < kellyTable.GetLength(1); j++)
                {
                    formated.Append(" " + kellyTable[i, j] + " ");
                }
                formated.Append("\n");
            }
            return formated.ToString();
        }
    }

    public static class Polynoms
    {
        internal class Polynom
        {
            public List<double> a;

            public Polynom()
            {
                a = new List<double>();
            }

            public void SetPolynom(Polynom polynom)
            {
                a.Clear();
                for (int i = 0; i < polynom.a.Count; i++)
                    a.Add(polynom.a[i]);
            }

            public void SetA(params double[] a)
            {
                this.a.Clear();
                for (int i = a.Length - 1; i >= 0; i--)
                {
                    this.a.Add(a[i]);
                }
            }

            public void SetA(List<double> a)
            {
                this.a.Clear();
                for (int i = a.Count - 1; i >= 0; i--)
                {
                    this.a.Add(a[i]);
                }
            }
        }

        internal static void Divide(Polynom a, Polynom b, out Polynom result, out Polynom residue)
        {
            Polynom sA = new Polynom();
            sA.SetPolynom(a);

            result = new Polynom();
            for (int i = 0; i < a.a.Count; i++)
            {
                result.a.Add(0);
            }

            Polynom temp = new Polynom();
            while (sA.a.Count >= b.a.Count && sA.a.Count != 0 && b.a.Count != 0)
            {
                temp.SetPolynom(b);
                double gg, hh;
                double kof = (gg = sA.a[sA.a.Count - 1]) / (hh = temp.a[temp.a.Count - 1]);
                result.a[sA.a.Count - temp.a.Count] = kof;

                for (int i = 0; i < temp.a.Count; i++)
                {
                    temp.a[i] *= -kof;
                }

                temp.a.RemoveAt(temp.a.Count - 1);

                for (int i = 0; i < sA.a.Count - b.a.Count; i++)
                {
                    temp.a.Insert(0, 0);
                }

                sA.a.RemoveAt(sA.a.Count - 1);

                Sum(ref sA, temp);
            }

            residue = sA;

            Cut(ref result);
            Cut(ref residue);
        }

        private static void Cut(ref Polynom polynom)
        {
            if (polynom.a.Count == 0)
            {
                polynom.a.Add(0);
                return;
            }

            int size = polynom.a.Count - 1;

            for (int i = size; i > 0; i--)
            {
                if (polynom.a[i] == 0)
                    polynom.a.RemoveAt(i);
                else
                    break;
            }
        }

        private static void Sum(ref Polynom a, Polynom b)
        {
            int min = a.a.Count > b.a.Count ? b.a.Count : a.a.Count;
            for (int i = 0; i < min; i++)
            {
                a.a[i] += b.a[i];
            }
        }

        public static void GetDiv(List<double> pol1, List<double> pol2, out string result, out string residue)
        {
            Polynom a = new Polynom(); ;
            Polynom b = new Polynom();
            a.SetA(pol1);
            b.SetA(pol2);

            Polynom res = new Polynom();
            Polynom resid = new Polynom();

            Divide(a, b, out res, out resid);

            result = Show(res);
            residue = Show(resid);
        }

        internal static string Show(Polynom polynom)
        {
            string result = "";
            for (int i = polynom.a.Count - 1; i >= 0; i--)
            {
                if (i != 0)
                    result += polynom.a[i] + " * x^" + i + " + ";
                else
                    result += polynom.a[i];
            }
            return result;
        }
    }

    public static class Horner
    {
        public static void GetResult(List<double> list, double kof, out string diagram, out string row)
        {
            int[] len1 = new int[list.Count + 1];
            int[] len2 = new int[list.Count + 1];
            len1[0] = 0;
            for (int i = 0; i < list.Count; i++)
                len1[i + 1] = list[i].ToString().Length;
            len2[0] = kof.ToString().Length;

            Polynoms.Polynom a = new Polynoms.Polynom();
            a.SetA(list);
            Polynoms.Polynom b = new Polynoms.Polynom();
            b.SetA(1, -kof);
            Polynoms.Divide(a, b, out Polynoms.Polynom result, out Polynoms.Polynom residue);

            List<double> res = new List<double>();

            for (int i = result.a.Count - 1; i >= 0; i--)
            {
                len2[i + 1] = result.a[result.a.Count - 1 - i].ToString().Length;
                res.Add(result.a[i]);
            }

            res.Add(residue.a[0]);
            len2[list.Count] = residue.a[0].ToString().Length;

            int[] len = new int[list.Count + 1];

            for (int i = 0; i < list.Count + 1; i++)
                len[i] = len1[i] > len2[i] ? len1[i] : len2[i];

            diagram = GetSpaces(len[0]) + "|";
            for (int i = 0; i < list.Count; i++)
                diagram += GetSpaces(len[i+1] - len1[i+1]) + list[i] + "|";
            diagram += "\n";
            for (int i = 0; i < list.Count + 1; i++)
                diagram += GetP(len[i]);
            diagram += GetP(list.Count + 1) + "\n" + GetSpaces(len[0] - len2[0]) + kof + "|";
            for(int i = 0; i < result.a.Count + 1; i++)
                diagram += GetSpaces(len[i+1] - len2[i+1]) + res[i] + "|";

            row = Polynoms.Show(a) + " = (" + Polynoms.Show(b) + ") * (" + Polynoms.Show(result) + ") + " + Polynoms.Show(residue);
        }

        private static string GetSpaces(int x)
        {
            string result = "";
            for (int i = 0; i < x; i++)
                result += " ";
            return result;
        }

        private static string GetP(int x)
        {
            string result = "";
            for (int i = 0; i < x; i++)
                result += "-";
            return result;
        }
    }

    public static class RSA
    {
        private static int MaxRage { get; set; } = 100;
        private static int MaxStartNum { get; set; } = 10000;

        public static void GenerateKeys(out long publicKey, out long privateKey, out long remainder)
        {
            var rand = new Random();
            Sieve.GetArray(out int[] a, MaxStartNum);
            long p = a[rand.Next(200, a.Length - 1)];
            long q = a[rand.Next(200, a.Length - 1)];
            long n = p * q;
            remainder = n;
            long f = (p - 1) * (q - 1);

            long[] conds = new long[MaxRage];
            int counter = 0;
            long i = 1 + f;
            while (counter < MaxRage)
            {
                if (Decomposition.GetArr(i).Length > 1)
                {
                    conds[counter] = i;
                    counter++;
                }
                i += f;
            }
            long current = conds[rand.Next(0, conds.Length)];
            Decomposition.GetList(out List<long> currentList, current);
            List<long> tempList = new List<long>();
            int tempSize = rand.Next(1, currentList.Count);
            for(int k = 0; k < tempSize; k++)
            {
                int tempIndex = rand.Next(0, currentList.Count);
                tempList.Add(currentList[tempIndex]);
                currentList.RemoveAt(tempIndex);
            }
            publicKey = GetMult(currentList);
            privateKey = GetMult(tempList);
        }

        public static long EncryptNumber(long publicKey, long remainder, long num)
        {
            long result = 1;
            for(int i = 0; i < publicKey; i++)
            {
                result = (result * num) % remainder;
            }
            return result;
        }

        public static long DecryptNumber(long privateKey, long remainder, long num)
        {
            return EncryptNumber(privateKey, remainder, num);
        }

        public static long GetMult(List<long> list)
        {
            long result = 1;
            for(int i = 0; i < list.Count; i++)
            {
                result *= list[i];
            }
            return result;
        }
    }
}
