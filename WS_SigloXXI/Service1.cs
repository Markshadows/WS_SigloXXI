using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WS_SigloXXI
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código y en el archivo de configuración a la vez.
    public class Service1 : IService1
    {
        OracleConnection connection = null;
        OracleCommand command = null;
        OracleDataReader reader = null;

        private string oradb = "Data Source=localhost:1521/xe;User Id=sigloxxi;Password=sigloxxi";

        public void GenerarBoleta(int IdReserva)
        {
            object[] parametros = new object[2];
            int ejecutar = 0;
            parametros[0] = IdReserva.ToString();
            ejecutar = ExecSP("SP_BOLETA", parametros);
        }

        public int ExecSP(string SP, params object[] parametros)
        {
            try
            {
                connection = new OracleConnection(oradb);
                command = new OracleCommand(SP, connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                OracleCommandBuilder.DeriveParameters(command);
                int cuenta = 0;
                bool parametroRetorno = false;
                int retorno = 0;

                foreach (OracleParameter pr in command.Parameters)
                {
                    if (pr.ParameterName != "P_RETORNO")
                    {
                        pr.Value = parametros[cuenta];
                        cuenta++;
                    }
                    else
                    {
                        pr.ParameterName.Equals("P_RETORNO");
                        pr.Direction = ParameterDirection.Output;
                        parametroRetorno = true;
                    }
                }
                command.ExecuteNonQuery();
                if (parametroRetorno)
                {
                    retorno = Convert.ToInt32(command.Parameters["P_RETORNO"].Value);
                }
                connection.Close();
                command.Dispose();
                return retorno;
            }
            catch (Exception)
            {

                return 0;
            }
        }
    }
}
