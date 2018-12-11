using Admin.Server;

namespace Admin
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerAdminConnection.Connect();
        }
    }
}
