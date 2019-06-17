//------------------------------------------------------------------------------
// <自动生成>
//     此代码由工具生成。
//     //
//     对此文件的更改可能导致不正确的行为，并在以下条件下丢失:
//     代码重新生成。
// </自动生成>
//------------------------------------------------------------------------------

namespace FoodBornWebService
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="", ConfigurationName="FoodBornWebService.ReportService")]
    public interface ReportService
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:ReportService/WEBRequest", ReplyAction="urn:ReportService/WEBRequestResponse")]
        System.Threading.Tasks.Task<string> WEBRequestAsync(string strXMLParm);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:ReportService/GetDicts", ReplyAction="urn:ReportService/GetDictsResponse")]
        System.Threading.Tasks.Task<string> GetDictsAsync(string dictName, string key);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:ReportService/GetState", ReplyAction="urn:ReportService/GetStateResponse")]
        System.Threading.Tasks.Task<string> GetStateAsync(string key, string objectGuid, int dataType);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:ReportService/GetCase", ReplyAction="urn:ReportService/GetCaseResponse")]
        System.Threading.Tasks.Task<string> GetCaseAsync(string BNusername, string StrainNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:ReportService/TraNetGetCase", ReplyAction="urn:ReportService/TraNetGetCaseResponse")]
        System.Threading.Tasks.Task<string> TraNetGetCaseAsync(string BNusername, string StrainNumber);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    public interface ReportServiceChannel : FoodBornWebService.ReportService, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    public partial class ReportServiceClient : System.ServiceModel.ClientBase<FoodBornWebService.ReportService>, FoodBornWebService.ReportService
    {
        
    /// <summary>
    /// 实现此分部方法，配置服务终结点。
    /// </summary>
    /// <param name="serviceEndpoint">要配置的终结点</param>
    /// <param name="clientCredentials">客户端凭据</param>
    static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public ReportServiceClient() : 
                base(ReportServiceClient.GetDefaultBinding(), ReportServiceClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.BasicHttpBinding_ReportService.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ReportServiceClient(EndpointConfiguration endpointConfiguration) : 
                base(ReportServiceClient.GetBindingForEndpoint(endpointConfiguration), ReportServiceClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ReportServiceClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(ReportServiceClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ReportServiceClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(ReportServiceClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ReportServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<string> WEBRequestAsync(string strXMLParm)
        {
            return base.Channel.WEBRequestAsync(strXMLParm);
        }
        
        public System.Threading.Tasks.Task<string> GetDictsAsync(string dictName, string key)
        {
            return base.Channel.GetDictsAsync(dictName, key);
        }
        
        public System.Threading.Tasks.Task<string> GetStateAsync(string key, string objectGuid, int dataType)
        {
            return base.Channel.GetStateAsync(key, objectGuid, dataType);
        }
        
        public System.Threading.Tasks.Task<string> GetCaseAsync(string BNusername, string StrainNumber)
        {
            return base.Channel.GetCaseAsync(BNusername, StrainNumber);
        }
        
        public System.Threading.Tasks.Task<string> TraNetGetCaseAsync(string BNusername, string StrainNumber)
        {
            return base.Channel.TraNetGetCaseAsync(BNusername, StrainNumber);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_ReportService))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("找不到名称为“{0}”的终结点。", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_ReportService))
            {
                return new System.ServiceModel.EndpointAddress("http://111.203.7.140/ReportService.svc");
            }
            throw new System.InvalidOperationException(string.Format("找不到名称为“{0}”的终结点。", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return ReportServiceClient.GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_ReportService);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return ReportServiceClient.GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_ReportService);
        }
        
        public enum EndpointConfiguration
        {
            
            BasicHttpBinding_ReportService,
        }
    }
}
