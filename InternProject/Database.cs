using System.Data.SqlClient;

namespace InternProject
{
    class Database
    {
        public static SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Leaderboard;Integrated Security=True");

    }
}
