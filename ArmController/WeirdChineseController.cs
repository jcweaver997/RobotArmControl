﻿using System;
using System.Diagnostics.Contracts;
using System.Threading;

namespace ArmControl
{
    public class WeirdChineseController : Joystick
    {
        public WeirdChineseController(string devfile) : base(devfile)
        {
            while (!Axis.ContainsKey(0)) Thread.Sleep(10);
            while (!Axis.ContainsKey(1)) Thread.Sleep(10);
            while (!Axis.ContainsKey(3)) Thread.Sleep(10);
            while (!Axis.ContainsKey(4)) Thread.Sleep(10);
            while (!Axis.ContainsKey(5)) Thread.Sleep(10);
            while (!Axis.ContainsKey(6)) Thread.Sleep(10);

            for (byte i = 0; i <= 9; i++){
                while (!Button.ContainsKey(i)) Thread.Sleep(10);
            }
        }

        public override Vector2 GetThumbstickLeft(){
            return new Vector2(Axis[0] / 32768f, Axis[1] / 32768f);
        }


        public override Vector2 GetThumbstickRight() => 
        new Vector2(Axis[3] / 32768f, Axis[4] / 32768f);

        public override Vector2 GetHAT() => 
        new Vector2(Axis[5], Axis[6]);

        public override bool GetButtonColorTop() => Button[0];
        public override bool GetButtonColorRight() => Button[1];
        public override bool GetButtonColorBottom() => Button[2];
        public override bool GetButtonColorLeft() => Button[3];

        public override bool GetButtonBumperLeft() => Button[4];
        public override bool GetButtonBumperRight() => Button[5];

        public override float GetTriggerLeft() => Button[6]?1:0;
        public override float GetTriggerRight() => Button[7]?1:0;

        public override bool GetButtonSelect() => Button[8];
        public override bool GetButtonStart() => Button[9];

    }
}
