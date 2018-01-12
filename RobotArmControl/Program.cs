using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotArmControl
{
    class Program
    {
        static void Main(string[] args)
        {
            RobotArmController rac = new RobotArmController();
            rac.Start();
        }
    }
}
