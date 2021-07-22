namespace EmployementApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (Page.isRunning)
            {
                Page.CurrentPage();
                Page.Control();
            }
        }
    }
}