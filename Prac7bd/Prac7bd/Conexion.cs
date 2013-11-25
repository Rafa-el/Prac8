using System;
using MySql.Data.MySqlClient;

namespace Prac7bd
{
	public class Conexion
	{
		protected MySqlConnection myConnection;
		public Conexion ()
		{
		}
		
		protected void abrirConexion(){
			string connectionString =
          		"Server=localhost;" +
	          	"Database=ejemplo;" +
	          	"User ID=root;" +
	          	"Password=ralph930322_;" +
	          	"Pooling=false;";
	       this.myConnection = new MySqlConnection(connectionString);
	       this.myConnection.Open();
		}
		
		protected void cerrarConexion(){
			this.myConnection.Close(); 
			this.myConnection = null;
		}
	}
}
