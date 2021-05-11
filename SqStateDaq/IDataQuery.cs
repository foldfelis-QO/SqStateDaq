using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SqStateDaq
{
    [ServiceContract(Name = "DataQueryServices")]
    interface IDataQuery
    {
        [OperationContract]
        [WebGet(UriTemplate = "DataQuery", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        SqData GetSqData();
    }

    [DataContract]
    public class SqData
    {
        [DataMember]
        public double[] Data;
    }
}
