using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using Camstar.XMLClient.API;

namespace CamStar_Service
{
    /// <summary>
    /// Summary description for InquiryTxn
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class InquiryTxn : System.Web.Services.WebService
    {
        CamStar_Service.Common.func com;

        [WebMethod(Description = "View Container details.【Inquiry Txn】")]
        public XmlNode ViewContainerStatus(string lot)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            
            try
            {
                csiDocument ResponseDocument = Txn_ViewContainerStatus(lot);

                if (com.CheckForErrors(ResponseDocument) == "completion")
                {
                    csiResponseData responseData = ResponseDocument.getService().responseData();
                    csiDataField ComputerName = (csiDataField)responseData.getResponseFieldByName("ComputerName");
                    csiDataField LotStatus = (csiDataField)responseData.getResponseFieldByName("LotStatus");
                    csiDataField MainQty = (csiDataField)responseData.getResponseFieldByName("MainQty");
                    csiDataField NextStep = (csiDataField)responseData.getResponseFieldByName("NextStep");

                    csiDataField ProcessSpec = (csiDataField)responseData.getResponseFieldByName("ProcessSpec");
                    csiDataField ProcessSpecObjectCategory = (csiDataField)responseData.getResponseFieldByName("ProcessSpecObjectCategory");
                    csiDataField ProcessSpecObjectType = (csiDataField)responseData.getResponseFieldByName("ProcessSpecObjectType");
                    csiDataField ProcessSpecRevision = (csiDataField)responseData.getResponseFieldByName("ProcessSpecRevision");
                    csiDataField ProductLine = (csiDataField)responseData.getResponseFieldByName("ProductLine");

                    csiDataField ProductRevision = (csiDataField)responseData.getResponseFieldByName("ProductRevision");
                    csiDataField Qty2 = (csiDataField)responseData.getResponseFieldByName("Qty2");
                    csiDataField SS = (csiDataField)responseData.getResponseFieldByName("SS");
                    csiDataField SSObjectCategory = (csiDataField)responseData.getResponseFieldByName("SSObjectCategory");
                    csiDataField SSObjectType = (csiDataField)responseData.getResponseFieldByName("SSObjectType");

                    csiDataField SSRevision = (csiDataField)responseData.getResponseFieldByName("SSRevision");
                    csiDataField StartReason = (csiDataField)responseData.getResponseFieldByName("StartReason");
                    csiDataField WIPStatus = (csiDataField)responseData.getResponseFieldByName("WIPStatus");
                    csiDataField WIPType = (csiDataField)responseData.getResponseFieldByName("WIPType");
                    csiDataField WIPYieldResult = (csiDataField)responseData.getResponseFieldByName("WIPYieldResult");

                    csiDataField Location = (csiDataField)responseData.getResponseFieldByName("Location");
                    csiDataField Product = (csiDataField)responseData.getResponseFieldByName("Product");
                    csiDataField Qty = (csiDataField)responseData.getResponseFieldByName("Qty");
                    csiDataField Step = (csiDataField)responseData.getResponseFieldByName("Step");
                    csiDataField Workflow = (csiDataField)responseData.getResponseFieldByName("Workflow");

                    string factory = responseData.getResponseFieldByName("Factory").asNamedObject().getRef(); // NamedObjRef

                    csiSubentity ContainerStatusDetails = responseData.getResponseFieldByName("ContainerStatusDetails").asSubentity();//SubentityObjRef
                    string HoldReason = ContainerStatusDetails.getField("HoldReason").asNamedObject().getRef();
                    string MfgOrder = ContainerStatusDetails.getField("MfgOrder").asNamedObject().getRef();
                    string Owner = ContainerStatusDetails.getField("Owner").asNamedObject().getRef();
                    string PriorityCode = ContainerStatusDetails.getField("PriorityCode").asNamedObject().getRef();
                    string ReworkReason = ContainerStatusDetails.getField("ReworkReason").asNamedObject().getRef();
                    string WorkflowRev = ContainerStatusDetails.dataField("WorkflowRev").getValue();

                    csiSubentity LotAttributes = responseData.getResponseFieldByName("LotAttributes").asSubentity();//SubentityObjRef
                    string ShelfLocation = LotAttributes.getField("ShelfLocation").asDataField().getValue();
                    string ShipFromFactory = LotAttributes.getField("ShipFromFactory").asNamedObject().getRef();


                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>0</Code> 
                                                                <responseData>
                                                                <ComputerName >{0}</ComputerName>
                                                                <Factory >{1}</Factory>
                                                                <LotStatus >{2}</LotStatus>
                                                                <MainQty >{3}</MainQty>
                                                                <NextStep >{4}</NextStep>
                                                                <ProcessSpec >{5}</ProcessSpec>
                                                                <ProcessSpecObjectCategory >{6}</ProcessSpecObjectCategory>
                                                                <ProcessSpecObjectType >{7}</ProcessSpecObjectType>
                                                                <ProcessSpecRevision >{8}</ProcessSpecRevision>
                                                                <ProductLine >{9}</ProductLine>
                                                                <ProductRevision >{10}</ProductRevision>
                                                                <Qty2 >{11}</Qty2>
                                                                <SS >{12}</SS>
                                                                <SSObjectCategory >{13}</SSObjectCategory>
                                                                <SSObjectType >{14}</SSObjectType>
                                                                <SSRevision >{15}</SSRevision>
                                                                <StartReason >{16}</StartReason>
                                                                <WIPStatus >{17}</WIPStatus>
                                                                <WIPType >{18}</WIPType>
                                                                <WIPYieldResult >{19}</WIPYieldResult>
                                                                <Location >{20}</Location>
                                                                <Product >{21}</Product>
                                                                <Qty >{22}</Qty>
                                                                <Step >{23}</Step>
                                                                <Workflow >{24}</Workflow>
                                                                <ContainerStatusDetails>
                                                                    <HoldReason>{25}</HoldReason>
                                                                    <MfgOrder>{26}</MfgOrder>
                                                                    <Owner>{27}</Owner>
                                                                    <PriorityCode>{28}</PriorityCode>
                                                                    <ReworkReason>{29}</ReworkReason>
                                                                    <WorkflowRev>{30}</WorkflowRev>
                                                                </ContainerStatusDetails>
                                                                <LotAttributes>
                                                                    <ShelfLocation>{31}</ShelfLocation>
                                                                    <ShipFromFactory>{32}</ShipFromFactory>
                                                                </LotAttributes>
                                                                </responseData>
                                                              </ResultMessage>", ComputerName.getValue(),
                                                                                 factory,
                                                                                 LotStatus.getValue(),
                                                                                 MainQty.getValue(),
                                                                                 NextStep.getValue(),
                                                                                 ProcessSpec.getValue(),
                                                                                 ProcessSpecObjectCategory.getValue(),
                                                                                 ProcessSpecObjectType.getValue(),
                                                                                 ProcessSpecRevision.getValue(),
                                                                                 ProductLine.getValue(),
                                                                                 ProductRevision.getValue(),
                                                                                 Qty2.getValue(),
                                                                                 SS.getValue(),
                                                                                 SSObjectCategory.getValue(),
                                                                                 SSObjectType.getValue(),
                                                                                 SSRevision.getValue(),
                                                                                 StartReason.getValue(),
                                                                                 WIPStatus.getValue(),
                                                                                 WIPType.getValue(),
                                                                                 WIPYieldResult.getValue(),
                                                                                 Location.getValue(),
                                                                                 Product.getValue(),
                                                                                 Qty.getValue(),
                                                                                 Step.getValue(),
                                                                                 Workflow.getValue(),
                                                                                 HoldReason,
                                                                                 MfgOrder,
                                                                                 Owner,
                                                                                 PriorityCode,
                                                                                 ReworkReason,
                                                                                 WorkflowRev,
                                                                                 ShelfLocation,
                                                                                 ShipFromFactory
                                                                                 ));
                    return (xmlDocumentObject);
                }
                else
                {
                    xmlDocumentObject.LoadXml(string.Format(@"<ResultMessage>  
                                                                <Code>-1</Code> 
                                                                <Message>{0}</Message> 
                                                              </ResultMessage>", com.CheckForErrors(ResponseDocument)));
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

        //logic


        //Txn
        public csiDocument Txn_ViewContainerStatus(string lot)
        {
            XmlDocument xmlDocumentObject = new XmlDocument();
            try
            {
                //initial
                com = new Common.func("ViewContainerStatusDoc", "ViewContainerStatus");

                //Create a input object
                csiObject InputData = com.gService.inputData();
                InputData.containerField("Container").setRef(lot, "");

                //Create a request object
                csiRequestData requestData = com.gService.requestData();
                requestData.requestField("ComputerName");
                requestData.requestField("Factory");
                requestData.requestField("LotStatus");
                requestData.requestField("MainQty");
                requestData.requestField("NextStep");
                requestData.requestField("ProcessSpec");
                requestData.requestField("ProcessSpecObjectCategory");
                requestData.requestField("ProcessSpecObjectType");
                requestData.requestField("ProcessSpecRevision");
                requestData.requestField("ProductLine");
                requestData.requestField("ProductRevision");
                requestData.requestField("Qty2");
                requestData.requestField("SS");
                requestData.requestField("SSObjectCategory");
                requestData.requestField("SSObjectType");
                requestData.requestField("SSRevision");
                requestData.requestField("StartReason");
                requestData.requestField("WIPStatus");
                requestData.requestField("WIPType");
                requestData.requestField("WIPYieldResult");
                requestData.requestField("Location");
                requestData.requestField("Product");
                requestData.requestField("Qty");
                requestData.requestField("Step");
                requestData.requestField("Workflow");

                //ContainerStatusDetails
                requestData.requestField("ContainerStatusDetails.HoldReason");
                requestData.requestField("ContainerStatusDetails.MfgOrder");
                requestData.requestField("ContainerStatusDetails.Owner");
                requestData.requestField("ContainerStatusDetails.PriorityCode");
                requestData.requestField("ContainerStatusDetails.ReworkReason");
                requestData.requestField("ContainerStatusDetails.WorkflowRev");

                //LotAttributes
                requestData.requestField("LotAttributes.ShelfLocation");
                requestData.requestField("LotAttributes.ShipFromFactory");
                

                //Request the completion message from the XML Application Server.
                com.gService.requestData().requestField("CompletionMsg");

                //Submit the input document to the XML Application Server.
                csiDocument ResponseDocument = com.gDocument.submit();

                //save xml
                //local path (C:\Program Files (x86)\Common Files\microsoft shared\DevServer\10.0)
                if (System.Configuration.ConfigurationManager.AppSettings["PROD"].ToString().Equals("N"))
                {
                    com.gDocument.saveRequestData(String.Format("{0}_Request.xml", "ViewContainerStatus"), false);
                    com.gDocument.saveResponseData(String.Format("{0}_Response.xml", "ViewContainerStatus"), true);
                }
                com.PrintDoc(com.gDocument.asXML(), true);
                com.PrintDoc(ResponseDocument.asXML(), false);

                return ResponseDocument;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
