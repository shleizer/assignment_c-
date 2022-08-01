using BL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IService
{
    public string GetMaxAdvanceAmountAgent(int year)
    {
        string con = ConfigurationManager.ConnectionStrings["test"].ConnectionString;
        return SQLReader.GetMaxAdvanceAmountAgent(year, con);
    }

    public OrderData[] GetOrdersInSelectedPosition(int position, List<string> agentCodes)
    {
        List<OrderData> orders = new List<OrderData>();

        string con = ConfigurationManager.ConnectionStrings["test"].ConnectionString;
        var list = SQLReader.GetOrdersInSelectedPosition(position, agentCodes, con);

        foreach (object[] item in list)
        {
            orders.Add(new OrderData
            {
                Num = GetIntData(item[0]),
                Amount = GetFloatData(item[1]),
                AdvenceAmount = GetFloatData(item[2]),
                Date = GetDateData(item[3]),
                CustCode = GetStringData(item[4]),
                AgentCode = GetStringData(item[5]),
                Description = GetStringData(item[6]),
            });
        }

        return orders.ToArray();
    }

    public AgentData[] GetOrdersInYear(int year, int orders)
    {
        List<AgentData> agents = new List<AgentData>();

        string con = ConfigurationManager.ConnectionStrings["test"].ConnectionString;
        var list = SQLReader.GetOrdersInYear(year, orders, con);

        foreach (object[] item in list)
        {
            agents.Add(new AgentData { Code = GetStringData(item[0]), Name = GetStringData(item[1]), Phone = GetStringData(item[2]) });
        }

        return agents.ToArray();
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
