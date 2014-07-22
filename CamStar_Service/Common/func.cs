using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Camstar.XMLClient.API;
using System.IO;


namespace CamStar_Service.Common
{
    public class func
    {
        public csiService gService { get; set; }
        public csiDocument gDocument { get; set; }

        static public string gHost = System.Web.Configuration.WebConfigurationManager.AppSettings["gHost"];
        static public int gPort = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings["gPort"]);
        static public string gUserName = System.Web.Configuration.WebConfigurationManager.AppSettings["gUserName"];
        static public string gPassword = System.Web.Configuration.WebConfigurationManager.AppSettings["gPassword"];
        
        public enum equipmentKind : int
        {
            Item = 1, WafterSort = 2, BackGrind = 3, Assembly = 4, Test = 5, Packing = 6
        }

        //Constructor
        public func(string DocumentName, string ServiceName)
        {
            csiSession gSession;
            csiConnection gConnection;
            Guid gSessionID = Guid.NewGuid();
            csiClient gClient = new csiClient();

            //=====================================================================================
            //initial parameters
            //=====================================================================================
            //Configure connection information for InSite XML Server
            gConnection = gClient.createConnection(gHost, gPort);

            //Create session information for InSite XML Server
            gSession = gConnection.createSession(gUserName, gPassword, gSessionID.ToString());


            //=====================================================================================
            //Create Document and Service
            //=====================================================================================

            //If a document already exist remove the document
            if ((DocumentName.Length) > 0) { gSession.removeDocument(DocumentName); }

            //clear service if it is not nothing
            if (gService != null) { gService = null; }

            //Create a new document
            gDocument = gSession.createDocument(DocumentName);

            //you cannot create a service if you have no name to assign it
            if (ServiceName.Length > 0) { gService = gDocument.createService(ServiceName); }
        }

        //check error or exception
        public string CheckForErrors(csiDocument _ResponseDocument)
        {
            csiExceptionData csiexceptiondata;
            csiField CompletionMsg;
            csiService csiService;

            if (_ResponseDocument.checkErrors() == true)
            {

                //Get the error from the response document and throw an exception.
                csiexceptiondata = _ResponseDocument.exceptionData();

                //Throw a new exception for the calling object
                string ErrorMsg = "Severity: " + csiexceptiondata.getSeverity();
                ErrorMsg = ErrorMsg + "Error Code: ";
                ErrorMsg = ErrorMsg + csiexceptiondata.getErrorCode() + "\n";
                ErrorMsg = ErrorMsg + "Description: " + csiexceptiondata.getDescription();
                return ErrorMsg;
            }
            
            // Get completion message and display
            csiService = _ResponseDocument.getService();
            if (csiService != null)
            {
                CompletionMsg = csiService.responseData().getResponseFieldByName("CompletionMsg");
                return "completion";
            }
            return string.Empty;
        }

        //save xml
        public void PrintDoc(string documentContent, bool isInputDoc)
        {
            try
            {
                string strAppPath = HttpContext.Current.Server.MapPath(".") + "\\xml\\";
                string strDocPath = (isInputDoc == true) ? "inputDoc.xml" : "responseDoc.xml";

                strDocPath = strAppPath + strDocPath;

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