using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringAlgorithms
{
    public class Program
    {
        public static class Naive
        {
            public static List<int> NaiveStringMatcher(string text, string template)
            {
                var result = new List<int>();

                for (var s = 0; s < text.Length - template.Length + 1; s++)
                {
                    var equal = true;
                    for (var k = 0; k < template.Length; k++)
                    {
                        if (text[k] != text[s + k])
                        {
                            equal = false;
                            break;
                        }
                    }
                    if (equal)
                        result.Add(s);
                }
                return result;
            }
        }

        //Hardcore
        public static class KnutMorrisPratt
        {
            public static List<int> KmpSearch(string P, string T)
            {
                int n = T.Length;
                int m = P.Length;
                int[] pi = ComputePrefixFunction(P);
                int q = 0;
                List<int> result = new List<int>();

                for (int i = 1; i <= n; i++)
                {
                    while (q > 0 && P[q] != T[i - 1])
                    {
                        q = pi[q - 1];
                    }
                    if (P[q] == T[i - 1]) { q++; }
                    if (q == m)
                    {
                        q = pi[q - 1];
                        result.Add(q);
                    }
                }

                return result;
            }

            private static int[] ComputePrefixFunction(string P)
            {
                int m = P.Length;
                int[] pi = new int[m];
                int k = 0;
                pi[0] = 0;

                for (int q = 1; q < m; q++)
                {
                    while (k > 0 && P[k] != P[q]) { k = pi[k]; }

                    if (P[k] == P[q]) { k++; }
                    pi[q] = k;
                }
                return pi;
            }
        }

        static void Main(string[] args)
        {
            foreach (var r in Naive.NaiveStringMatcher("abcaaaaaabc", "abc"))
            {
                Console.WriteLine(r);
            }
            Console.WriteLine();
            foreach (var r in KnutMorrisPratt.KmpSearch("abcaaaaaabc", "abc"))
            {
                Console.WriteLine(r);
            }
        }
    }
}
