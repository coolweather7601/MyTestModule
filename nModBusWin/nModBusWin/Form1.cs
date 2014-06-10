using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//step1. reference nmodbuspc.dll, and using the namespaces.
using Modbus.Device;      //for modbus master
using System.IO.Ports;    //for controlling serial ports
using System.Net.Sockets;

namespace nModBusWin
{
    public partial class Form1 : Form
    {
        private SerialPort serialPort;
        private ModbusSerialMaster master;
        private ushort param;
        public Form1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            // create a new SerialPort object with default settings.
            serialPort = new SerialPort();

            // set the appropriate properties.
            serialPort.PortName = "COM11";   //for serial server the COM port connected to WISE COM10(RS-232)
            //serialPort.PortName = "COM3";   //for RS232 to USB the COM port connected to WISE COM4(RS-232)

            serialPort.BaudRate = 9600;
            serialPort.Parity = Parity.Even;//偶數
            //serialPort.Parity = Parity.None;//無
            serialPort.StopBits = StopBits.One;
            serialPort.DataBits = 8;
            
            #region ddl data bind
            string[] arrFuc = new string[] { "ReadCoilStatus(0x01)", "ReadHoldingRegs(0x03)", "PresetSingleReg(0x06)", "WriteSingleCoil" };
            foreach (string func in arrFuc)
            {
                cbxFunc.Items.Add(func);
            }
            #endregion
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort.Open();
                // create Modbus RTU Master by the comport client
                //document->Modbus.Device.Namespace->ModbusSerialMaster Class->CreateRtu Method
                master = ModbusSerialMaster.CreateRtu(serialPort);

                btnOpen.Enabled = false;
                btnClose.Enabled = true;
                btnRequest.Enabled = true;
                button1.Enabled = true;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            serialPort.Close();
            master.Dispose();

            btnOpen.Enabled = true;
            btnClose.Enabled = false;
            btnRequest.Enabled = false;
            button1.Enabled = false;
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {

            App_Code.FunctionCode fc = new App_Code.FunctionCode();

            try
            {
                byte slaveID = 1;
                ushort startAddress = param;
                master.Transport.ReadTimeout = 300;
                ushort numOfPoints = 8;

                if (chk_10.Checked)
                    startAddress = (ushort)Convert.ToInt32(txt_10.Text.Trim());                
                else if(chk_16.Checked)
                    startAddress = (ushort)Convert.ToInt32(txt_16.Text.Trim(), 16);
                
                switch (cbxFunc.Text)
                {
                    #region ReadCoilStatus(0x01)
                    case "ReadCoilStatus(0x01)":
                        bool[] status = master.ReadCoils(slaveID, startAddress, numOfPoints);
                        if (status[0] == true) { MessageBox.Show("ON"); }
                        else { MessageBox.Show("OFF"); }

                        for (int index = 0; index < status.Length; index++)
                            Console.WriteLine(string.Format("DO[{0}] = {1}", index, status[index]));
                        break;
                    #endregion
                    #region ReadHoldingRegs(0x03)
                    case "ReadHoldingRegs(0x03)":     
                        ushort[] register = master.ReadHoldingRegisters(slaveID, startAddress, numOfPoints);
                        MessageBox.Show(string.Format(@"十進位：{0}{1}十六進位：{2}", register[0].ToString(), Environment.NewLine, Convert.ToString(Convert.ToInt32(register[0].ToString()), 16)));

                        float[] floatData2 = new float[register.Length / 2];
                        Buffer.BlockCopy(register, 0, floatData2, 0, register.Length * 2);
                        for (int index = 0; index < floatData2.Length; index++)
                            Console.WriteLine(string.Format("AO[{0}] = {1}", index, floatData2[index]));
                        break;
                    #endregion
                    #region PresetSingleReg(0x06)
                    case "PresetSingleReg(0x06)":
                        if (string.IsNullOrEmpty(txtInput.Text)) { MessageBox.Show("Please text the input value."); }
                        else
                        {
                            master.WriteSingleRegister(slaveID, (ushort)startAddress, (ushort)Convert.ToInt32(txtInput.Text.ToString()));//write
                            ushort[] register_read = master.ReadHoldingRegisters(slaveID, (ushort)startAddress, numOfPoints);//read
                            MessageBox.Show(string.Format(@"十進位：{0}{1}十六進位：{2}", register_read[0].ToString(), Environment.NewLine, Convert.ToString(Convert.ToInt32(register_read[0].ToString()), 16)));
                            //MessageBox.Show(register_read[0].ToString());
                        }                     
                        break;
                    #endregion
                    #region WriteSingleCoil
                    case "WriteSingleCoil":
                        bool inputvalue = false;
                        if (txtInput.Text.Equals("1")) inputvalue = true;
                        master.WriteSingleCoil(slaveID, (ushort)startAddress, inputvalue);//write
                        bool[] Coil_status = master.ReadCoils(slaveID, startAddress, numOfPoints);
                        if (Coil_status[0] == true) { MessageBox.Show("ON"); }
                            else { MessageBox.Show("OFF"); }
                        break;
                    #endregion
                }
            }
            catch (Exception exception)
            {
                //Connection exception
                //No response from server.
                //The server maybe close the com port, or response timeout.
                if (exception.Source.Equals("System"))
                    Console.WriteLine(exception.Message);

                //The server return error code.
                //You can get the function code and exception code.
                if (exception.Source.Equals("nModbusPC"))
                {
                    string str = exception.Message;
                    int FunctionCode;
                    string ExceptionCode;
                    
                    str = str.Remove(0, str.IndexOf("\r\n") + 17);
                    FunctionCode = Convert.ToInt16(str.Remove(str.IndexOf("\r\n")));
                    Console.WriteLine("Function Code: " + FunctionCode.ToString("X"));
                    
                    str = str.Remove(0, str.IndexOf("\r\n") + 17);
                    ExceptionCode = str.Remove(str.IndexOf("-"));
                    switch (ExceptionCode.Trim())
                    {
                        case "1":
                            Console.WriteLine("Exception Code: " + ExceptionCode.Trim() + "----> Illegal function!");
                            break;
                        case "2":
                            Console.WriteLine("Exception Code: " + ExceptionCode.Trim() + "----> Illegal data address!");
                            break;
                        case "3":
                            Console.WriteLine("Exception Code: " + ExceptionCode.Trim() + "----> Illegal data value!");
                            break;
                        case "4":
                            Console.WriteLine("Exception Code: " + ExceptionCode.Trim() + "----> Slave device failure!");
                            break;
                    }
                    
                    /*
                       //Modbus exception codes definition
                            
                       * Code   * Name                                      * Meaning
                         01       ILLEGAL FUNCTION                            The function code received in the query is not an allowable action for the server.
                         
                         02       ILLEGAL DATA ADDRESS                        The data addrdss received in the query is not an allowable address for the server.
                         
                         03       ILLEGAL DATA VALUE                          A value contained in the query data field is not an allowable value for the server.
                           
                         04       SLAVE DEVICE FAILURE                        An unrecoverable error occurred while the server attempting to perform the requested action.
                             
                         05       ACKNOWLEDGE                                 This response is returned to prevent a timeout error from occurring in the client (or master)
                                                                              when the server (or slave) needs a long duration of time to process accepted request.
                          
                         06       SLAVE DEVICE BUSY                           The server (or slave) is engaged in processing a long–duration program command , and the
                                                                              client (or master) should retransmit the message later when the server (or slave) is free.
                             
                         08       MEMORY PARITY ERROR                         The server (or slave) attempted to read record file, but detected a parity error in the memory.
                             
                         0A       GATEWAY PATH UNAVAILABLE                    The gateway is misconfigured or overloaded.
                             
                         0B       GATEWAY TARGET DEVICE FAILED TO RESPOND     No response was obtained from the target device. Usually means that the device is not present on the network.                         
                     */
                }
            }//end catch
        }

        private void cbxTest_SelectedIndexChanged(object sender, EventArgs e)
        {
            App_Code.FunctionCode fc = new App_Code.FunctionCode();

            //ushort param = 0;
            switch (cbxTest.Text)
            {
                case "作業中": param = fc.Working; break;
                case "警報": param = fc.Alarm; break;
                case "作業結束": param = fc.Terminate; break;
                case "最可靠的溫度": param = fc.Temperature; break;
                case "CH1": param = fc.CH1; break;
                case "CH2": param = fc.CH2; break;
                case "壓力": param = fc.Pressure; break;
                case "1-步驟": param = fc.firstProcess; break;
                case "1-時": param = fc.firstHour; break;
                case "1-分": param = fc.firstMin; break;
                case "1-溫度": param = fc.firstTemperature; break;
                case "1-壓力": param = fc.firstPressure; break;
                case "2-步驟": param = fc.secondProcess; break;
                case "2-時": param = fc.secondHour; break;
                case "2-分": param = fc.secondMin; break;
                case "2-溫度": param = fc.secondTemperature; break;
                case "2-壓力": param = fc.secondPressure; break;   
                case "選用爐訊號": param = fc.Furnace; break;
                case "啟動按鈕enable並閃爍": param = fc.OnBtnTwinkle; break;
                case "一般模式紅燈OFF": param = fc.RedLightOff; break;
                case "警報蜂鳴器ON": param = fc.AlarmON; break;
                case "強制機台停止訊號": param = fc.StopMachine; break;
            }

            lblRequest.Text = param.ToString();
        }

        private void cbxFunc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] arrTest = new string[] { };
            if(cbxFunc.Text.Equals("PresetSingleReg(0x06)"))
            {
                txtInput.Enabled=true;
                arrTest = new string[] { "選用爐訊號", "啟動按鈕enable並閃爍", "一般模式紅燈OFF", "警報蜂鳴器ON", "強制機台停止訊號" };
            }
            else if (cbxFunc.Text.Equals("ReadCoilStatus(0x01)"))
            {
                txtInput.Enabled=false;
                arrTest = new string[] { "作業中", "警報", "作業結束" };
            }
            else if (cbxFunc.Text.Equals("ReadHoldingRegs(0x03)"))
            {
                txtInput.Enabled = false;
                arrTest = new string[] { "最可靠的溫度", "CH1", "CH2", "壓力", "1-步驟", "1-時", "1-分", "1-溫度", "1-壓力", "2-步驟", "2-時", "2-分", "2-溫度", "2-壓力" };
            }
            cbxTest.Items.Clear();                                               
            foreach (string Test in arrTest) { cbxTest.Items.Add(Test); }
        }

        private void chk_10_CheckedChanged(object sender, EventArgs e)
        {
            chk_16.Checked = false;
            cbxTest.Enabled = false;
            if (chk_16.Checked == false && chk_10.Checked == false) { cbxTest.Enabled = true; }
        }

        private void chk_16_CheckedChanged(object sender, EventArgs e)
        {
            chk_10.Checked = false;
            cbxTest.Enabled = false;
            if (chk_16.Checked == false && chk_10.Checked == false) { cbxTest.Enabled = true; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //TEST
            //error code check for machine monitor(Uniga offer)
            App_Code.ErrorCode ec = new App_Code.ErrorCode();
            List<ushort[]> lst = new List<ushort[]>
                {
                    new ushort[]{ec.error616,616}, new ushort[]{ec.error617,617}, new ushort[]{ec.error618,618}, new ushort[]{ec.error619,619}, new ushort[]{ec.error620,620},
                    new ushort[]{ec.error621,621}, new ushort[]{ec.error622,622}, new ushort[]{ec.error623,623}, new ushort[]{ec.error624,624}, new ushort[]{ec.error625,625},
                    new ushort[]{ec.error626,626}, new ushort[]{ec.error627,627}, new ushort[]{ec.error628,628}, new ushort[]{ec.error629,629}, new ushort[]{ec.error630,630},
                    new ushort[]{ec.error631,631}, new ushort[]{ec.error632,632}, new ushort[]{ec.error633,633}, new ushort[]{ec.error634,634}, new ushort[]{ec.error635,635},
                    new ushort[]{ec.error636,636}, new ushort[]{ec.error638,638}, new ushort[]{ec.error639,639}, new ushort[]{ec.error640,640}, new ushort[]{ec.error641,641},
                    new ushort[]{ec.error642,642}, new ushort[]{ec.error643,643}, new ushort[]{ec.error644,644}, new ushort[]{ec.error645,645}, new ushort[]{ec.error646,646},
                    new ushort[]{ec.error647,647}, new ushort[]{ec.error648,648}, new ushort[]{ec.error649,649}, new ushort[]{ec.error650,650}, new ushort[]{ec.error651,651},
                    new ushort[]{ec.error652,652}, new ushort[]{ec.error653,653}, new ushort[]{ec.error654,654}, new ushort[]{ec.error655,655}, new ushort[]{ec.error656,656},
                    new ushort[]{ec.error657,657}, new ushort[]{ec.error658,658}, new ushort[]{ec.error659,659}, new ushort[]{ec.error660,660}
                };

            byte slaveID = 1;
            ushort startAddress;
            ushort numOfPoints = 8;

            Console.WriteLine(string.Format(@"BEGIN: {0}", DateTime.Now.ToString()));
            foreach (ushort[] item in lst)
            {
                startAddress = item[0];
                bool[] register_ErrorCode = master.ReadCoils(slaveID, startAddress, numOfPoints);
                if (register_ErrorCode[0] == true)
                {
                    Console.WriteLine(string.Format(@"ERROR: {0} || Address:{1}", DateTime.Now.ToString(), startAddress));
                }
            }
            Console.WriteLine(string.Format(@"END: {0}", DateTime.Now.ToString()));
        }
    }
}
