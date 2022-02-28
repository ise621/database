namespace Database.Tests
{
    public class Program
    {
        public static int Main()
        {
            return Xunit.ConsoleClient.Program.Main(
                new[] {
                    typeof(Program).Assembly.Location
                }
              );
        }
    }
}
