using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotArmControl
{
    class RobotArmController
    {
        private string comPortName;
        private SerialPort serialport;

        public RobotArmController()
        {
            Console.WriteLine("Please choose a COM port:");
            foreach(string name in SerialPort.GetPortNames())
            {
                Console.WriteLine(name);
            }
            Console.Write("Name: ");
            comPortName = Console.ReadLine().Trim();
        }

        public void Start()
        {
            serialport = new SerialPort(comPortName, 115200);
            serialport.Open();
            serialport.WriteLine("VER");
            //serialport.WriteLine("QP2");
            Console.WriteLine(serialport.IsOpen);
            Console.WriteLine(serialport.ReadLine());
            //serialport.
        }


    }
}
