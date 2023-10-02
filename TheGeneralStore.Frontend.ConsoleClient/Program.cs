using System.Runtime.CompilerServices;

namespace TheGeneralStore.Frontend.ConsoleClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            var test = program.GetLogin();
            Console.WriteLine(test.Item2.Name);
        }

        public Tuple<string, LoginDTO> GetLogin()
        {
            var data = new Login { Id = 1, Name = "Test" };

            if (data == null)
            {
                return new Tuple<string, LoginDTO>("Theres no data bro", null);
            }

            return new Tuple<string, LoginDTO>("Damn there is data bro", data.ToDTO());
        }
    }
}