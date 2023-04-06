using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Avaliação
{
    internal class Conexao
    {
        private static string server = "localhost";
        private static string database = "bd_avaliacao";
        private static string usuario = "root";
        private static string senha = "1234";

        string strCoon = $" server={server}; User id={usuario}; database={database}; password={senha}";

        MySqlConnection cn;


        private bool Conectar()
        {
            bool result;
            try
            {
                cn = new MySqlConnection(strCoon);
                cn.Open();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }
        private void Desconectar()
        {
            if (cn.State == System.Data.ConnectionState.Open)
            {
                cn.Close();
            }
        }
        public bool Executar(string sql)
        {
            bool resultato = false;

            if (Conectar())
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(sql, cn);
                    cmd.ExecuteNonQuery();
                    resultato = true;
                }
                catch
                {
                    resultato = false;
                }
                finally
                {
                    Desconectar();
                }

            }
            return resultato;
        }
        public DataTable Retorna(string sql)
        {
            Conectar();
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, cn);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable data = new DataTable();
                da.Fill(data);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Desconectar();
            }
        }

    }
}
