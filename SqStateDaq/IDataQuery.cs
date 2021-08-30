using System.ServiceModel;
using System.ServiceModel.Web;
using System.Runtime.Serialization;

namespace SqStateDaq
{
    [ServiceContract(Name = "DataQueryServices")]
    interface IDataQuery
    {
        [OperationContract]
        [WebGet(UriTemplate = "/", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        SqData GetSqData();
    }

    [DataContract]
    public class SqData
    {
        [DataMember]
        public double[] Data;
    }
}
