using System;
using System.Threading.Tasks;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            //var formated = Task.Run(() => 6).ContinueWith(antecedent => Format(antecedent.Result)).ConfigureAwait(false).GetAwaiter().GetResult().Result;
            //Console.WriteLine(formated);

            //new UI().Run();
        }

        //public static async Task<int> Sum(int x, int y) {
        //    return x + y;
        //}

        //public static async Task<string> Format(int input)
        //{
        //    return $"{input} = {input}";
        //}

    }

    //public class TestFormatter : IFormatter<string, string>
    //{
    //    public string Format(string value)
    //    {
    //        return Reverse(value);
    //    }
    //    public static string Reverse(string s)
    //    {
    //        char[] charArray = s.ToCharArray();
    //        Array.Reverse(charArray);
    //        return new string(charArray);
    //    }
    //}

   
}

