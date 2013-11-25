using System;
using MySql.Data.MySqlClient;
namespace Prac7bd
{
	public class Funciones : Conexion
	{
		public Funciones ()
		{
		}
		
		public void mostrarTodos(){
			this.abrirConexion();
            MySqlCommand myCommand = new MySqlCommand(this.querySelect(), 
			                                          myConnection);
            MySqlDataReader myReader = myCommand.ExecuteReader();	
	        while (myReader.Read()){
	            string id = myReader["id"].ToString();
	            string nombre = myReader["nombre"].ToString();
	            string codigo = myReader["codigo"].ToString();
				string telefono = myReader["telefono"].ToString();
				string email = myReader["email"].ToString();

	            Console.WriteLine("ID: " + id +
				                  " Nombre: " + nombre +
				                  " Código: " + codigo + 
				                  " Telefono: " + telefono +
				                  " Email: " + email);
	       }
			
            myReader.Close();
			myReader = null;
            myCommand.Dispose();
			myCommand = null;
			this.cerrarConexion();
		}
		
		public void insertarRegistroNuevo(string nombre, int codigo, int telefono, string email){
			this.abrirConexion();
			string sql = "INSERT INTO tabla (nombre, codigo, telefono, email) VALUES ('" + nombre + "'," + codigo + "," +
														telefono + ", '" +	email + "')";
			this.ejecutarComando(sql);
			this.cerrarConexion();
		}

		public void editarRegistro (string id)
		{
			string nnombre, nmail, ans;
			int ntel, ncod;

			this.abrirConexion ();
			MySqlCommand myCommand = new MySqlCommand (this.queryContId (id), myConnection);
			int res = Convert.ToInt16 (myCommand.ExecuteScalar ());
			myCommand = null;
			this.cerrarConexion();

			this.mostrarPorId (id);
			if (res == 1) {
				Console.WriteLine ("Seguro que desea editar el registro?");
				Console.WriteLine ("1. Si");
				Console.WriteLine ("2. No");
				ans = (Console.ReadLine ());

				if (ans == "1") {

					Console.WriteLine ("Ingrese el nuevo nombre: ");
					nnombre = Console.ReadLine ();
					Console.WriteLine ("Ingrese el nuevo codigo: ");
					ncod = Convert.ToInt32(Console.ReadLine ());
					Console.WriteLine ("Ingrese el nuevo telefono: ");
					ntel = Convert.ToInt32(Console.ReadLine ());
					Console.WriteLine ("Ingrese el nuevo e-mail: ");
					nmail = Console.ReadLine ();
					this.abrirConexion();
					string sql = "UPDATE tabla SET nombre='" + nnombre + 
						"', codigo= " + ncod + 
						", telefono= " + ntel + 
						", email= '" + nmail + "' WHERE (id='" + id + "')";
					Console.WriteLine ("SE EDITÓ CON EXITO");
					this.ejecutarComando (sql);
					this.cerrarConexion ();
				}
			}
		}

		private void mostrarPorId (string id1)
		{
			try {
				this.abrirConexion ();
				MySqlCommand myCommand;
				myCommand= new MySqlCommand (this.queryContId (id1), myConnection);
				int res = Convert.ToInt16 (myCommand.ExecuteScalar ());
				myCommand=null;
				myCommand= new MySqlCommand (this.queryId (id1), myConnection);
				MySqlDataReader myReader = myCommand.ExecuteReader ();
				if (res == 1) {
					while(myReader.Read()){
						string id = myReader ["id"].ToString ();
						string nombre = myReader ["nombre"].ToString ();
						string codigo = myReader ["codigo"].ToString ();
						string telefono = myReader ["telefono"].ToString ();
						string email = myReader ["email"].ToString ();

						Console.WriteLine ("ID: " + id +
							" Nombre: " + nombre +
							" Código: " + codigo + 
							" Telefono: " + telefono +
							" Email: " + email
						);
					};

				} else {
					Console.WriteLine ("No existe el registro");
				}
				myReader.Close ();
				myReader = null;
				myCommand.Dispose ();
				myCommand = null;
				this.cerrarConexion ();

			} catch(Exception ex) {
			}
		}

		private void borrarRegistro (string id)
		{
			string ans;
			this.abrirConexion();
			MySqlCommand myCommand = new MySqlCommand (this.queryContId (id), myConnection);
			int res = Convert.ToInt16 (myCommand.ExecuteScalar ());
			myCommand = null;
			this.cerrarConexion();
			this.mostrarPorId(id);

			if (res == 1) {
				Console.WriteLine ("Seguro que desea borrar el registro?");
				Console.WriteLine ("1. Si");
				Console.WriteLine ("2. No");
				ans = (Console.ReadLine ());

				if (ans == "1") {
					this.abrirConexion();
					string sql = "DELETE FROM tabla WHERE id='" + id + "'";
					Console.WriteLine ("SE BORRÓ CON EXITO");
					this.ejecutarComando (sql);
					this.cerrarConexion ();
				}
			} else {
				Console.WriteLine("No existe el registro con ese id");
			}

		}

		private int ejecutarComando(string sql){
			MySqlCommand myCommand = new MySqlCommand(sql,this.myConnection);
			int afectadas = myCommand.ExecuteNonQuery();
			myCommand.Dispose();
			myCommand = null;
			return afectadas;
		}
		
		private string querySelect(){
			return "SELECT * " +
	           	"FROM tabla";
		}

		private string queryId (string id){
			return "SELECT * FROM tabla where (id='" + id + "')";
		}
		private string queryContId (string id){
			return "SELECT Count(*) FROM tabla where (id='" + id + "')";
		}
		private string queryDelete (string id){
			return "DELETE WHERE id='" + id + "'";
		}

		public void Menu ()
		{
			int opc = 0;
			string nom, email;
			int cod, tel;
			try{
				do {
					Console.WriteLine ("Presione una tecla...");
					Console.ReadKey ();
					Console.Clear ();
					Console.WriteLine ("Menu");
					Console.WriteLine ("1. Mostrar todos");
					Console.WriteLine ("2. Agregar nuevo registro");
					Console.WriteLine ("3. Editar registro");
					Console.WriteLine ("4. Eliminar registro");
					Console.WriteLine ("5. Salir");
					Console.WriteLine ("Ingrese una opcion: ");
					opc = Convert.ToInt32 (Console.ReadLine ());

					switch (opc) {
					case 1:
						this.mostrarTodos ();
						break;
					case 2:
						Console.WriteLine ("Ingrese Nombre: ");
						nom = Console.ReadLine ();
						Console.WriteLine ("Ingrese Codigo: ");
						cod = Convert.ToInt32 (Console.ReadLine ());
						Console.WriteLine ("Ingrese Telefono: ");
						tel = Convert.ToInt32 (Console.ReadLine ());
						Console.WriteLine ("Ingrese Email: ");
						email = Console.ReadLine ();
						this.insertarRegistroNuevo (nom, cod, tel, email);
						break;
					case 3:
						Console.WriteLine ("Ingrese el id del registro");
						this.editarRegistro (Console.ReadLine ());
						break;
					case 4:
						Console.WriteLine ("Ingrese el id a borrar");
						this.borrarRegistro (Console.ReadLine ());
						break;

					default: 
						Console.WriteLine ("Opcion invalida"); 
						break;
					}
				} while(opc != 5);

		}catch(FormatException e){
					
				};
		}
	}
}