

namespace EKReportsemp.WinForms.Classes
{
    #region Libraries (librerias) 
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.Core.EntityClient;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using EDsemp.Classes;
    using EKReportsemp.WinForms.Context;
    using EKReportsemp.WinForms.Properties;

    #endregion
    public class LocationConexion
    {


        #region Atributes (atributos)
        public string[] archivos = Directory.GetFiles(@"C:\SEMP2013\AudSemp\AudSemp\cdbs", "*.txt");
        // string key = "ABCDEFG54669525PQRSTUVWXYZabcdef852846opqrstuvwxyz";
        string suc, tienda, dir, server, database, user, pass;
        string Suc, Tienda, Dir, Server, Database, User, Pass;


        #endregion

        #region Methods (Metodos)





        public String[] Scan()
        {

            

            bool IsCorrect;
            String[] array = new string[7];

            foreach (var file in archivos)
            {
                //anow chek if connection is correct
                StreamReader reader = new StreamReader(file.ToString(), true);
                if (System.IO.File.Exists(file.ToString()))
                {
                    suc = reader.ReadLine();//suc
                    tienda = reader.ReadLine();//tienda
                    dir = reader.ReadLine();//Direccion
                    server = reader.ReadLine();//server
                    database = reader.ReadLine();//database
                    user = reader.ReadLine();//user
                    pass = reader.ReadLine();//password


                    Suc = Encriptar_Desencriptar.DecryptKeyMD5(suc);
                    Tienda = Encriptar_Desencriptar.DecryptKeyMD5(tienda);
                    Dir = Encriptar_Desencriptar.DecryptKeyMD5(dir);
                    Server = Encriptar_Desencriptar.DecryptKeyMD5(server);
                    Database = Encriptar_Desencriptar.DecryptKeyMD5(database);
                    User = Encriptar_Desencriptar.DecryptKeyMD5(user);
                    Pass = Encriptar_Desencriptar.DecryptKeyMD5(pass);
                    //check connectio
                    IsCorrect = checkConnection(Server, Database, User, Pass);
                    if (IsCorrect == true)
                    {
                        array[0] = Suc;
                        array[1] = Tienda;
                        array[2] = Dir;
                        array[3] = Server;
                        array[4] = Database;
                        array[5] = User;
                        array[6] = Pass;
                        //entiyFramework
                        SaveConnectionString("SEMP2013_Context", "", Server, Database, User, Pass, 1);
                        //sqlconnection client
                        SaveConnectionString("SEMP2013_CNX", "", Server, Database, User, Pass, 2);
                        return array;

                    }

                }

            }

            array[0] = "Error in Directory";
            return array;



        }

        private bool checkConnection(string server, string database, string user, string pass)
        {

            var canConnect = false;

            var connectionString = "DATA SOURCE=" + server + ";initial catalog=" + database +
                ";Persist Security Info=True;User ID=" + user + ";Password=" + pass + "";

            var connection = new SqlConnection(connectionString);
            //first test conection NORMAL
            try
            {
                using (connection)
                {
                    connection.Open();
                    //  db.Database.Connection.Open();
                    canConnect = true;
                }


            }
            catch (Exception exception)
            {

            }
            finally
            {
                connection.Close();
                // db.Database.Connection.Close();
            }

            return canConnect;

        }

        public static string GetConnectionString(string connectionStringName)
        {
            Configuration appconfig =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConnectionStringSettings connStringSettings = appconfig.ConnectionStrings.ConnectionStrings[connectionStringName];

            return connStringSettings.ConnectionString;
        }


        public static void SaveConnectionString(string connectionStringName, string connectionString,
            string server, string database, string user, string pass, int opcion)
        {
            string data;
            SqlConnectionStringBuilder sqlString = new SqlConnectionStringBuilder()
            {
                DataSource = server, // Server name
                InitialCatalog = database,  //Database
                UserID = user,         //Username
                Password = pass,  //Password
                PersistSecurityInfo = true,
                MultipleActiveResultSets = true,

            };
            if (opcion == 1)
            {

                //Build an Entity Framework connection string

                EntityConnectionStringBuilder entityString = new EntityConnectionStringBuilder()
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = "res://*/Context.Context.csdl|res://*/Context.Context.ssdl|res://*/Context.Context.msl",
                    ProviderConnectionString = sqlString.ToString() + ";App=EntityFramework;"

                };
                data = entityString.ConnectionString;

                // assumes a connectionString name in .config of MyDbEntities
                var db = new SEMP2013_Context();
                // so only reference the changed properties
                // using the object parameters by name
                db.ChangeDatabase
                    (
                        initialCatalog: database,
                        userId: user,
                        password: pass,
                        dataSource: server // could be ip address 120.273.435.167 etc
                    );

                Configuration appconfig =
              ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                appconfig.ConnectionStrings.ConnectionStrings[connectionStringName].ConnectionString = data;
                appconfig.Save(ConfigurationSaveMode.Modified, true);
                Properties.Settings.Default.Save();

            }
            else
            {

                data = sqlString.ConnectionString;
                Configuration appconfig =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                appconfig.ConnectionStrings.ConnectionStrings[connectionStringName].ConnectionString = data;
                appconfig.Save(ConfigurationSaveMode.Modified, true);
                Properties.Settings.Default.Save();

            }





        }

        //create ist of app strings
        public static List<string> GetConnectionStringNames()
        {
            List<string> cns = new List<string>();
            Configuration appconfig =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            foreach (ConnectionStringSettings cn in appconfig.ConnectionStrings.ConnectionStrings)//
            {
                cns.Add(cn.Name);
            }
            return cns;
        }

        //return first element in list of connectionString
        public static string GetFirstConnectionStringName()
        {
            return GetConnectionStringNames().FirstOrDefault();
        }



        #endregion


    }

}
