using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class SQLReader
    {
        public static string GetMaxAdvanceAmountAgent(int year, string connectionString)
        {
            string agentCode = null;

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("GetMaxAdvanceAmountAgent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@year", year);

            con.Open();
            object resultObj = cmd.ExecuteScalar();
            if (resultObj != null && resultObj is string)
            {
                agentCode = (string)resultObj;
            }
            con.Close();

            return agentCode;
        }

        public static List<object[]> GetOrdersInSelectedPosition(int position, List<string> agentCodes, string connectionString)
        {
            //agentCodes = new List<string> { "A002", "A009" };
            List<object[]> orders = new List<object[]>();

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("GetOrdersInSelectedPosition", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@position", position);
            cmd.Parameters.AddWithValue("@list", string.Join(",", agentCodes));

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader != null)
            {
                while (reader.Read())
                {
                    var item = new Object[7];
                    reader.GetValues(item);
                    orders.Add(item);

                }
            }
            con.Close();

            return orders;
        }

        public static List<object[]> GetOrdersInYear(int year, int orders, string connectionString)
        {
            List<object[]> agents = new List<object[]>();

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("GetOrdersInYear", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@year", year);
            cmd.Parameters.AddWithValue("@orders", orders);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader != null)
            {
                while (reader.Read())
                {
                    var item = new Object[3];
                    reader.GetValues(item);
                    agents.Add(item);
                }
            }
            con.Close();

            return agents;
        }

        private string GetStringData(object orgValue)
        {
            return orgValue.ToString().Trim();
        }

        private int GetIntData(object orgValue)
        {
            return int.Parse(orgValue.ToString());
        }

        private float GetFloatData(object orgValue)
        {
            return float.Parse(orgValue.ToString());
        }

        private DateTime GetDateData(object orgValue)
        {
            return DateTime.Parse(orgValue.ToString());
        }
    }
}
