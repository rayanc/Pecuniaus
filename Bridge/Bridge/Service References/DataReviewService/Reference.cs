﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bridge.DataReviewService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://dvtransaction.com/", ConfigurationName="DataReviewService.DataviewServiceSoap")]
    public interface DataviewServiceSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://dvtransaction.com/Ping", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string Ping();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://dvtransaction.com/Ping", ReplyAction="*")]
        System.Threading.Tasks.Task<string> PingAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://dvtransaction.com/LoadSystem", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string LoadSystem();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://dvtransaction.com/LoadSystem", ReplyAction="*")]
        System.Threading.Tasks.Task<string> LoadSystemAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://dvtransaction.com/ProcessApplication", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string ProcessApplication(string XmlRequest);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://dvtransaction.com/ProcessApplication", ReplyAction="*")]
        System.Threading.Tasks.Task<string> ProcessApplicationAsync(string XmlRequest);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://dvtransaction.com/ProcessApplicationXML", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode ProcessApplicationXML(System.Xml.XmlElement XmlRequest);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://dvtransaction.com/ProcessApplicationXML", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Xml.XmlNode> ProcessApplicationXMLAsync(System.Xml.XmlElement XmlRequest);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface DataviewServiceSoapChannel : Bridge.DataReviewService.DataviewServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DataviewServiceSoapClient : System.ServiceModel.ClientBase<Bridge.DataReviewService.DataviewServiceSoap>, Bridge.DataReviewService.DataviewServiceSoap {
        
        public DataviewServiceSoapClient() {
        }
        
        public DataviewServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DataviewServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DataviewServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DataviewServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string Ping() {
            return base.Channel.Ping();
        }
        
        public System.Threading.Tasks.Task<string> PingAsync() {
            return base.Channel.PingAsync();
        }
        
        public string LoadSystem() {
            return base.Channel.LoadSystem();
        }
        
        public System.Threading.Tasks.Task<string> LoadSystemAsync() {
            return base.Channel.LoadSystemAsync();
        }
        
        public string ProcessApplication(string XmlRequest) {
            return base.Channel.ProcessApplication(XmlRequest);
        }
        
        public System.Threading.Tasks.Task<string> ProcessApplicationAsync(string XmlRequest) {
            return base.Channel.ProcessApplicationAsync(XmlRequest);
        }
        
        public System.Xml.XmlNode ProcessApplicationXML(System.Xml.XmlElement XmlRequest) {
            return base.Channel.ProcessApplicationXML(XmlRequest);
        }
        
        public System.Threading.Tasks.Task<System.Xml.XmlNode> ProcessApplicationXMLAsync(System.Xml.XmlElement XmlRequest) {
            return base.Channel.ProcessApplicationXMLAsync(XmlRequest);
        }
    }
}