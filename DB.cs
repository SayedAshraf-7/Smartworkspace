// ================================================
// FILE: DB.cs
// ================================================
namespace SmartWorkspace
{
    public static class DB
    {
        // Change the Server value to match YOUR SQL Server instance name.
        // Common values:
        //   localhost\SQLEXPRESS
        //   .\SQLEXPRESS
        //   DESKTOP-XXXXX\SQLEXPRESS
        //   localhost   (full SQL Server, default instance)
        public static readonly string ConnectionString =
           @"Server=localhost;Database=smart_workspace_hub;Integrated Security=True;
TrustServerCertificate=True;";
    
    
    }
}
