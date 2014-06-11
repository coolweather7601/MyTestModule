using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using Camstar.XMLClient.API;
using System.IO;
using System.Xml;

namespace CamStar_Service
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class Service1 : System.Web.Services.WebService
    {
        public Guid gSessionID = Guid.NewGuid();
        public csiClient gClient = new csiClient();
        public csiConnection gConnection;
        public csiSession gSession;
        public csiService gService;
        public csiDocument gDocument;
        public string gHost = "twgkhhpsk1ms049";
        public int gPort= 2881;
        public string gUserName = "APKTEST14";
        public string gPassword= "Training!";

        [WebMethod(Description = "Equipment set status in WaferSort part(ex. WST001, PRD, DOWN)")]
        public XmlNode waferSortEquipmentSetStatus(string _Resource, string _ResourceStatusCode, string _ResourceStatusReason)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            try
            {                
                //KAL05, PRD, DOWN
                init();
                CreateDocumentandService("WaferSortEquipmentSetStatusDoc", "WaferSortEquipmentSetStatus");

                //Create a input object
                csiObject InputData = gService.inputData();

                //Set the Container field.
                csiNamedObject objectChanges = InputData.namedObjectField("Resource");
                objectChanges.dataField("__name").setValue(_Resource);

                csiNamedObject objectChanges_2 = InputData.namedObjectField("ResourceStatusCode");
                objectChanges_2.dataField("__name").setValue(_ResourceStatusCode);

                csiNamedObject objectChanges_3 = InputData.namedObjectField("ResourceStatusReason");
                objectChanges_3.dataField("__name").setValue(_ResourceStatusReason);
                
                //Execute the service.
                gService.setExecute();

                //Request the completion message from the XML Application Server.
                gService.requestData().requestField("CompletionMsg");

                //Submit the input document to the XML Application Server.
                //Submit the changes to the InSite Server
                csiDocument ResponseDocument = gDocument.submit();

                //save xml
                //local path (C:\Program Files (x86)\Common Files\microsoft shared\DevServer\10.0)
                if (System.Configuration.ConfigurationManager.AppSettings["PROD"].ToString().Equals("N"))
                {
                    gDocument.saveRequestData(String.Format("{0}_Request.xml", "WaferSortEquipmentSetStatus"), false);
                    gDocument.saveResponseData(String.Format("{0}_Response.xml", "WaferSortEquipmentSetStatus"), true);
                }
                PrintDoc(gDocument.asXML(), true);
                PrintDoc(ResponseDocument.asXML(), false);

                if (CheckForErrors(ResponseDocument) == "completion")
                {
                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>0</Code> 
                                                              </ResultMessage>"));
                    return (xmlDocumentObject);
                }
                else
                {
                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>-1</Code> 
                                                                <Message>{0}</Message> 
                                                              </ResultMessage>", CheckForErrors(ResponseDocument)));
                    return (xmlDocumentObject);
                }
                    
            }
            catch (Exception e) 
            {
                xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>-1</Code> 
                                                                <Message>{0}</Message> 
                                                              </ResultMessage>", e.ToString()));
                return (xmlDocumentObject);
            }
        }

        [WebMethod(Description = "Equipment setup in WaferSort part(ex. WST001, 190007002)")]
        public XmlNode waferSortEquipmentSetup(string _Resource, string _Tool)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            try
            {
                //KAL05, PRD, DOWN
                init();
                CreateDocumentandService("WaferSortEquipmentSetupDoc", "WaferSortEquipmentSetup");

                //Create a input object
                csiObject InputData = gService.inputData();

                //Set the Container field.
                csiNamedObject objectChanges = InputData.namedObjectField("Resource");
                objectChanges.dataField("__name").setValue(_Resource);

                string[] tools = _Tool.Split('|');
                foreach (string t in tools)
                {
                    InputData.containerList("Tools").appendItem(t, "");    
                }

                //Execute the service.
                gService.setExecute();

                //Request the completion message from the XML Application Server.
                gService.requestData().requestField("CompletionMsg");

                //Submit the input document to the XML Application Server.
                //Submit the changes to the InSite Server
                csiDocument ResponseDocument = gDocument.submit();
                
                //save xml
                //local path (C:\Program Files (x86)\Common Files\microsoft shared\DevServer\10.0)
                if (System.Configuration.ConfigurationManager.AppSettings["PROD"].ToString().Equals("N"))
                {
                    gDocument.saveRequestData(String.Format("{0}_Request.xml", "WaferSortEquipmentSetup"), false);
                    gDocument.saveResponseData(String.Format("{0}_Response.xml", "WaferSortEquipmentSetup"), true);
                }
                PrintDoc(gDocument.asXML(), true);
                PrintDoc(ResponseDocument.asXML(), false);

                if (CheckForErrors(ResponseDocument) == "completion")
                {
                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>0</Code> 
                                                              </ResultMessage>"));
                    return (xmlDocumentObject);
                }
                else
                {
                    string check = CheckForErrors(ResponseDocument);
                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>-1</Code> 
                                                                <Message>{0}</Message> 
                                                              </ResultMessage>", check));
                    return (xmlDocumentObject);
                }
            }
            catch (Exception e) 
            {
                xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>-1</Code> 
                                                                <Message>{0}</Message> 
                                                              </ResultMessage>", e.ToString()));
                return (xmlDocumentObject);   
            }
        }

        [WebMethod(Description = "Equipment set status in Test part(ex. AGIL-01, PRD, DOWN)")]
        public XmlNode TestEquipmentSetStatus(string _Resource, string _ResourceStatusCode, string _ResourceStatusReason)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            try
            {
                init();
                CreateDocumentandService("TestEquipmentSetStatusDoc", "TestEquipmentSetStatus");

                //Create a input object
                csiObject InputData = gService.inputData();

                //Set the Container field.
                csiNamedObject objectChanges = InputData.namedObjectField("Resource");
                objectChanges.dataField("__name").setValue(_Resource);

                csiNamedObject objectChanges_2 = InputData.namedObjectField("ResourceStatusCode");
                objectChanges_2.dataField("__name").setValue(_ResourceStatusCode);

                csiNamedObject objectChanges_3 = InputData.namedObjectField("ResourceStatusReason");
                objectChanges_3.dataField("__name").setValue(_ResourceStatusReason);

                //Execute the service.
                gService.setExecute();

                //Request the completion message from the XML Application Server.
                gService.requestData().requestField("CompletionMsg");

                //Submit the input document to the XML Application Server.
                //Submit the changes to the InSite Server
                csiDocument ResponseDocument = gDocument.submit();

                //save xml
                //local path (C:\Program Files (x86)\Common Files\microsoft shared\DevServer\10.0)
                if (System.Configuration.ConfigurationManager.AppSettings["PROD"].ToString().Equals("N"))
                {
                    gDocument.saveRequestData(String.Format("{0}_Request.xml", "WaferSortEquipmentSetStatus"), false);
                    gDocument.saveResponseData(String.Format("{0}_Response.xml", "WaferSortEquipmentSetStatus"), true);
                }
                PrintDoc(gDocument.asXML(), true);
                PrintDoc(ResponseDocument.asXML(), false);

                if (CheckForErrors(ResponseDocument) == "completion")
                {
                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>0</Code> 
                                                              </ResultMessage>"));
                    return (xmlDocumentObject);
                }
                else
                {
                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>-1</Code> 
                                                                <Message>{0}</Message> 
                                                              </ResultMessage>", CheckForErrors(ResponseDocument)));
                    return (xmlDocumentObject);
                }

            }
            catch (Exception e)
            {
                xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>-1</Code> 
                                                                <Message>{0}</Message> 
                                                              </ResultMessage>", e.ToString()));
                return (xmlDocumentObject);
            }
        }

        [WebMethod(Description = "Equipment set status in Assy part(ex. APK-DB001, SBY, DOWN)")]
        public XmlNode AssemblyEquipmentSetStatus(string _Resource, string _ResourceStatusCode, string _ResourceStatusReason)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            try
            {
                init();
                CreateDocumentandService("AssemblyEquipmentSetStatusDoc", "AssemblyEquipmentSetStatus");

                //Create a input object
                csiObject InputData = gService.inputData();

                //Set the Container field.
                csiNamedObject objectChanges = InputData.namedObjectField("Resource");
                objectChanges.dataField("__name").setValue(_Resource);

                csiNamedObject objectChanges_2 = InputData.namedObjectField("ResourceStatusCode");
                objectChanges_2.dataField("__name").setValue(_ResourceStatusCode);

                csiNamedObject objectChanges_3 = InputData.namedObjectField("ResourceStatusReason");
                objectChanges_3.dataField("__name").setValue(_ResourceStatusReason);

                //Execute the service.
                gService.setExecute();

                //Request the completion message from the XML Application Server.
                gService.requestData().requestField("CompletionMsg");

                //Submit the input document to the XML Application Server.
                //Submit the changes to the InSite Server
                csiDocument ResponseDocument = gDocument.submit();

                //save xml
                //local path (C:\Program Files (x86)\Common Files\microsoft shared\DevServer\10.0)
                if (System.Configuration.ConfigurationManager.AppSettings["PROD"].ToString().Equals("N"))
                {
                    gDocument.saveRequestData(String.Format("{0}_Request.xml", "WaferSortEquipmentSetStatus"), false);
                    gDocument.saveResponseData(String.Format("{0}_Response.xml", "WaferSortEquipmentSetStatus"), true);
                }
                PrintDoc(gDocument.asXML(), true);
                PrintDoc(ResponseDocument.asXML(), false);

                if (CheckForErrors(ResponseDocument) == "completion")
                {
                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>0</Code> 
                                                              </ResultMessage>"));
                    return (xmlDocumentObject);
                }
                else
                {
                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>-1</Code> 
                                                                <Message>{0}</Message> 
                                                              </ResultMessage>", CheckForErrors(ResponseDocument)));
                    return (xmlDocumentObject);
                }

            }
            catch (Exception e)
            {
                xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>-1</Code> 
                                                                <Message>{0}</Message> 
                                                              </ResultMessage>", e.ToString()));
                return (xmlDocumentObject);
            }
        }

        [WebMethod(Description = "Puts given Container on hold(ex. N25_AP01, HOLDLOC001, AA, TEST)")]
        public XmlNode LotHold(string _Container, string _HoldLocation,string _HoldReason,string _Comment)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            try
            {
                init();
                CreateDocumentandService("LotHoldDoc", "LotHold");

                //Create a input object
                csiObject InputData = gService.inputData();

                //Set the Container field.
                InputData.dataField("Comments").setValue(_Comment);
                csiContainerList csiList = InputData.containerList("Details");
                csiContainer csiCon = csiList.appendItem("", "");
                csiCon.dataField("ApplyToChildLots").setValue("false");
                csiCon.namedObjectField("Container").dataField("__name").setValue(_Container);
                csiCon.namedObjectField("HoldLocation").dataField("__name").setValue(_HoldLocation);
                csiCon.namedObjectField("HoldReason").dataField("__name").setValue(_HoldReason);

                //Execute the service.
                gService.setExecute();

                //Request the completion message from the XML Application Server.
                gService.requestData().requestField("CompletionMsg");

                //Submit the input document to the XML Application Server.
                //Submit the changes to the InSite Server
                csiDocument ResponseDocument = gDocument.submit();

                //save xml
                //local path (C:\Program Files (x86)\Common Files\microsoft shared\DevServer\10.0)
                if (System.Configuration.ConfigurationManager.AppSettings["PROD"].ToString().Equals("N"))
                {
                    gDocument.saveRequestData(String.Format("{0}_Request.xml", "LotHold"), false);
                    gDocument.saveResponseData(String.Format("{0}_Response.xml", "LotHold"), true);
                }
                PrintDoc(gDocument.asXML(), true);
                PrintDoc(ResponseDocument.asXML(), false);
                

                if (CheckForErrors(ResponseDocument) == "completion")
                {
                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>0</Code> 
                                                              </ResultMessage>"));
                    return (xmlDocumentObject);
                }
                else
                {
                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>-1</Code> 
                                                                <Message>{0}</Message> 
                                                              </ResultMessage>", CheckForErrors(ResponseDocument)));
                    return (xmlDocumentObject);
                }

            }
            catch (Exception e)
            {
                xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>-1</Code> 
                                                                <Message>{0}</Message> 
                                                              </ResultMessage>", e.ToString()));
                return (xmlDocumentObject);
            }
        }

        [WebMethod(Description = "Create a new equipment in Wafer Sort part.")]
        public XmlNode NewWaferSortEquipment(string _EquipmentName)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            try
            {
                init();
                CreateDocumentandService("WaferSortEquipmentMaintDoc", "WaferSortEquipmentMaint");
                gService.perform("New");

                //Create a input object
                csiObject InputData = gService.inputData();

                //Set the Container field.

                csiNamedObject csiObj = InputData.namedObjectField("ObjectChanges");
                csiObj.dataField("Notes").setValue("TEST_NOTE");
                csiObj.dataField("Description").setValue("TEST_DESC");
                csiObj.dataField("Name").setValue(_EquipmentName);

                csiObj.namedObjectField("SetupAccess").dataField("__name").setValue("Approved");
                csiObj.namedObjectField("BOM").dataField("__useROR").setValue("false");
                csiObj.namedObjectField("DocumentSet").dataField("__name").setValue("Camstar");
                csiObj.namedObjectField("InitialStatus").dataField("__name").setValue("PRD");
                csiObj.namedObjectField("ResourceFamily").dataField("__name").setValue("WST_OVEN");
                csiObj.namedObjectField("ResourceIcon").dataField("__name").setValue("T1");
                csiObj.namedObjectField("Vendor").dataField("__name").setValue("V01");
                csiObj.namedObjectField("Factory").dataField("__name").setValue("APK");

                //<Location>
                //  <__name />
                //  <__parent __CDOTypeName="Factory">
                //    <__name><![CDATA[APK]]></__name>
                //  </__parent>
                //</Location>
                
                //csiObj.namedObjectField("Location").dataField("__name").setValue("T1");
                

                //Execute the service.
                gService.setExecute();

                //Request the completion message from the XML Application Server.
                gService.requestData().requestField("CompletionMsg");

                //Submit the input document to the XML Application Server.
                //Submit the changes to the InSite Server
                csiDocument ResponseDocument = gDocument.submit();

                //save xml
                //local path (C:\Program Files (x86)\Common Files\microsoft shared\DevServer\10.0)
                if (System.Configuration.ConfigurationManager.AppSettings["PROD"].ToString().Equals("N"))
                {
                    gDocument.saveRequestData(String.Format("{0}_Request.xml", "NewWaferSortEquipment"), false);
                    gDocument.saveResponseData(String.Format("{0}_Response.xml", "NewWaferSortEquipment"), true);
                }
                PrintDoc(gDocument.asXML(), true);
                PrintDoc(ResponseDocument.asXML(), false);


                if (CheckForErrors(ResponseDocument) == "completion")
                {
                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>0</Code> 
                                                              </ResultMessage>"));
                    return (xmlDocumentObject);
                }
                else
                {
                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>-1</Code> 
                                                                <Message>{0}</Message> 
                                                              </ResultMessage>", CheckForErrors(ResponseDocument)));
                    return (xmlDocumentObject);
                }

            }
            catch (Exception e)
            {
                xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>-1</Code> 
                                                                <Message>{0}</Message> 
                                                              </ResultMessage>", e.ToString()));
                return (xmlDocumentObject);
            }
        }






        private void init()
        {
            //Configure connection information for InSite XML Server
            gConnection = gClient.createConnection(gHost,gPort);
            //Create session information for InSite XML Server
            gSession = gConnection.createSession(gUserName, gPassword, gSessionID.ToString());
        }

        private void CreateDocumentandService(string DocumentName, string ServiceName)
        {
            //If a document already exist remove the document
            if ((DocumentName.Length) > 0) 
            {
                gSession.removeDocument(DocumentName);
            }

            //clear service if it is not nothing
            if (gService!=null)
            {
               gService = null;
            }

            //Create a new document
            gDocument = gSession.createDocument(DocumentName);

            //you cannot create a service if you have no name to assign it
            if (ServiceName.Length > 0) 
            {
                gService = gDocument.createService(ServiceName); //Create a service
            }
        }

        //Check
        private string CheckForErrors(csiDocument _ResponseDocument)
        {
            csiExceptionData csiexceptiondata;
            csiField CompletionMsg;
            csiService csiService;
            string ErrorMsg;

            if (_ResponseDocument.checkErrors() == true)
            {

                //Get the error from the response document and throw an exception.
                csiexceptiondata = _ResponseDocument.exceptionData();

                //Throw a new exception for the calling object
                ErrorMsg = "Severity: " + csiexceptiondata.getSeverity();
                ErrorMsg = ErrorMsg + "Error Code: ";
                ErrorMsg = ErrorMsg + csiexceptiondata.getErrorCode() + "\n";
                ErrorMsg = ErrorMsg + "Description: " + csiexceptiondata.getDescription();
                return ErrorMsg;
                //Throw New System.Exception(ErrorMsg)
            }
            else
            {
                // Get completion message and display
                csiService = _ResponseDocument.getService();
                if (csiService != null)
                {
                    CompletionMsg = csiService.responseData().getResponseFieldByName("CompletionMsg");
                    return "completion";
                    //MsgBox(CompletionMsg.getValue, MsgBoxStyle.OkOnly, "CompletionMessage")
                }
            }
            return "";
        }

        public void PrintDoc(string documentContent, bool isInputDoc)
        {
            try
            {
                //string strAppPath = Directory.GetCurrentDirectory();
                string strAppPath = Server.MapPath(".") + "\\xml\\";
                string strDocPath = "";

                if (isInputDoc)
                    strDocPath = "inputDoc.xml";
                else
                    strDocPath = "responseDoc.xml";

                strDocPath = strAppPath + "\\" + strDocPath;

                StreamWriter objSW = File.CreateText(strDocPath);
                objSW.Write(documentContent);
                objSW.Flush();
                objSW.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}