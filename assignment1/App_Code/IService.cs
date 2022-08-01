using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
[ServiceContract]
public interface IService
{
	[WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
	[OperationContract]
	string GetMaxAdvanceAmountAgent(int year);

	[WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
	[OperationContract]
	AgentData[] GetOrdersInYear(int year, int orders);

	[WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
	[OperationContract]
	OrderData[] GetOrdersInSelectedPosition(int position, List<string> agentCodes);



	// TODO: Add your service operations here
}

// Use a data contract as illustrated in the sample below to add composite types to service operations.
[DataContract]
public class AgentData
{
	[DataMember]
	public string Code { get; set; }

	[DataMember]
	public string Name { get; set; }

	[DataMember]
	public string Phone { get; set; }
}

[DataContract]
public class OrderData
{
	[DataMember]
	public int Num { get; set; }

	[DataMember]
	public float Amount { get; set; }

	[DataMember]
	public float AdvenceAmount { get; set; }
	[DataMember]
	public DateTime Date { get; set; }
	[DataMember]
	public string CustCode { get; set; }
	[DataMember]
	public string AgentCode { get; set; }
	[DataMember]
	public string Description { get; set; }


}
