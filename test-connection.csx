using System;
using System.Data.SqlClient;

string connectionString = "Server=localhost,1433;Database=Sistemas_Turnos;User Id=sa;Password=SqlPassword2025!;TrustServerCertificate=True;";

try
{
    using (SqlConnection conn = new SqlConnection(connectionString))
    {
        conn.Open();
        Console.WriteLine("✅ Conexión a SQL Server exitosa!");
        
        using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM usuarios", conn))
        {
            int count = (int)cmd.ExecuteScalar();
            Console.WriteLine($"📊 Usuarios en la BD: {count}");
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Error: {ex.Message}");
}
