using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using Camstar.XMLClient.API;
using System.IO;

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
        

        [WebMethod]
        public string waferSortEquipmentSetStatus(string _Resource, string _ResourceStatusCode, string _ResourceStatusReason)
        {
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
                //gDocument.saveRequestData(String.Format("{0}_Request.xml", "WaferSortEquipmentSetStatus"), false);
                //gDocument.saveResponseData(String.Format("{0}_Response.xml", "WaferSortEquipmentSetStatus"), true);
                PrintDoc(gDocument.asXML(), true);
                PrintDoc(ResponseDocument.asXML(), false);

                if (CheckForErrors(ResponseDocument) == "completion")
                    return "ok";
                else
                    return CheckForErrors(ResponseDocument);
            }
            catch (Exception e) { return "fail:" + e.ToString(); }
        }

        [WebMethod]
        public string waferSortEquipmentSetup(string _Resource, string _Tool)
        {
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
                //gDocument.saveRequestData(String.Format("{0}_Request.xml", "WaferSortEquipmentSetup"), false);
                //gDocument.saveResponseData(String.Format("{0}_Response.xml", "WaferSortEquipmentSetup"), true);
                PrintDoc(gDocument.asXML(), true);
                PrintDoc(ResponseDocument.asXML(), false);

                if (CheckForErrors(ResponseDocument) == "completion")
                    return "ok";
                else
                    return CheckForErrors(ResponseDocument);
            }
            catch (Exception e) { return "fail:" + e.ToString(); }
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