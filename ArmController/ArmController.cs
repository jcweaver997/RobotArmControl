using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmControl
{
    class ArmController
    {

        private bool _running;
        private string _joystickName;
        private string _portname;
        private SerialPort _port;
        private Joystick _js;

        private int[] _motors;
        private float[] _percents;
        private int _time;
        private int _timeOverlap;
        private float _controllerGain;
        private const float speed0 = .5f;
        private const float speed1 = .75f;
        private const float speed2 = 1;
        private const float speed3 = 1.5f;
        private const float speed4 = 2;
        private const float speed5 = 2.5f;

        public ArmController()
        {

            Console.WriteLine("Which serial _port would you like?");


            foreach (string name in SerialPort.GetPortNames())
            {
               if(name.Contains("USB"))
                    Console.WriteLine(name.Substring(5));
            }
            if (SerialPort.GetPortNames().Length == 1)
            {
                _portname = SerialPort.GetPortNames()[0];
            }
            else
            {
                Console.Write("Your choice: ");
                _portname = Console.ReadLine();
                _portname = "/dev/"+_portname;
            }
            
            Console.WriteLine("Choose a gampad:");
            Joystick.PrintJoysticks();
            _joystickName = Console.ReadLine();
            _joystickName = "/dev/input/" + _joystickName;


        }

        public void Instantiate()
        {
            _port = new SerialPort(_portname,115200, Parity.None, 8, StopBits.One);
            _port.Open();
            _js = new WeirdChineseController(_joystickName);
            _running = true;
            _time = 50;
            _timeOverlap = 2;
            _controllerGain = .005f;
            _motors = new int[6];
            _percents = new float[6];
            for (int i = 0; i < _motors.Length; i++)
            {
                _motors[i] = i;
                _percents[i] = .5f;
            }
            SetPositions(_motors, _percents, _time);
            

        }

        public void Start()
        {

            while (_running)
            {
                
                Update();
                System.Threading.Thread.Sleep(_time-_timeOverlap);
            }
        }

        private void Update()
        {
            AddValue(0, _js.GetThumbstickLeft().X * _controllerGain * speed0);
            AddValue(1, _js.GetThumbstickLeft().Y * _controllerGain * speed1);
            AddValue(2, (_js.GetTriggerRight() - _js.GetTriggerLeft()) * _controllerGain * speed2);

            AddValue(3, _js.GetThumbstickRight().Y * _controllerGain * speed3);
            AddValue(4, _js.GetThumbstickRight().X * _controllerGain * speed4);

            AddValue(5, (((_js.GetButtonBumperRight())? 1 :0) 
                         + ((_js.GetButtonBumperLeft())? -1 : 0)) * _controllerGain * speed5);
            SetPositions(_motors, _percents, _time);
        }

        private void AddValue(int index, float value)
        {
            _percents[index] += value;
            _percents[index] = minmax(_percents[index], 0, 1);
        }

        private float minmax(float value, float min, float max)
        {
            if (value < min)
            {
                return min;
            }
            if (value>max)
            {
                return max;
            }
            return value;
        }

        private void SetPosition(int motor, float percent, int time)
        {
            _port.Write("#"+motor+"P"+(int)(2000*percent+500)+"T"+time+"\r");
        }

        private void SetPositions(int[] motors, float[] percents, int time)
        {
            string message = "";
            for (int i = 0; i < motors.Length; i++)
            {
                message += "#" + motors[i] + "P" + (int) (2000 * percents[i] + 500);
            }
            message += "T" + time;
            _port.Write(message+"\r");
        }


    }
}
