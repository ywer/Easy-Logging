using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using VRage;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;

namespace IngameScript
{
    partial class Program : MyGridProgram
    {
        #region settings
        //settings
        int MaxLoggerRows = 200;//Set this too high will cause lag if you using Debug Screen set this to max Screen Rows
        bool WriteToDebugScreen = true; //Set this true if you have a Debug Screen
        string DebugLCDName = "DebugLCD";
        int MaxDebugLCDRows = 14;
        //Setting end ----> DONT EDIT BELOW
        #endregion

        #region DoNotChange
        double PVersion = 0.2;
        #endregion

        public Program()
        {
            Runtime.UpdateFrequency = UpdateFrequency.Update100;
        }

        public void Save()
        {

        }

        public void Main(string argument, UpdateType updateSource)
        {
            WriteToLog("Oh, it’s you.", MassageLevel.Debug);
            WriteToLog("It’s been a long time", MassageLevel.Info);
            WriteToLog("How have you been?", MassageLevel.Warning);
            WriteToLog("I’ve been really busy being dead", MassageLevel.Warning);
            WriteToLog("You know, after you murdered me ?.", MassageLevel.Debug);
            WriteToLog("Okay look, we’ve both said a lot of things that you’re going to regret", MassageLevel.Info);
            WriteToLog("But I think we can put our differences behind us", MassageLevel.Warning);
            WriteToLog("For science. You monster.", MassageLevel.Warning);
            Runtime.UpdateFrequency = UpdateFrequency.None;
            return;

        }



        #region Logging

        //Logger  by ywer
        public enum MassageLevel
        {
            Info,
            Warning,
            Error,
            Debug
        }


        public void WriteToLog(string Text, MassageLevel TLevel)
        {
            //script by ywer
            string Data = Me.CustomData;
            string[] Lines = Data.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            string Fault = "";
            if (TLevel == MassageLevel.Info)
            {
                Fault = "Info: " + Text;
            }
            else if (TLevel == MassageLevel.Warning)
            {
                Fault = "Warning: " + Text;
            }
            else if (TLevel == MassageLevel.Error)
            {
                Fault = "ERROR: " + Text;

            }
            else if (TLevel == MassageLevel.Debug)
            {
                Fault = "DEBUG: " + Text;
            }
            else
            {
                Fault = "WTF: " + Text;
            }


            string[] NewData = new string[Lines.Length + 2];
            DateTime TimeNow = System.DateTime.Now;
            if (Lines.Length >= MaxLoggerRows - 1)
            {

                if (Lines.Length > 0)
                {

                    Array.Copy(Lines, 1, NewData, 0, Lines.Length - 1);
                }
                NewData[MaxLoggerRows] = TimeNow + ":" + Fault + Environment.NewLine;

            }
            else
            {

                if (Lines.Length > 0)
                {

                    Array.Copy(Lines, 0, NewData, 0, Lines.Length);
                }
                NewData[Lines.Length + 1] = TimeNow + ":" + Fault + Environment.NewLine;
            }
            if (WriteToDebugScreen)
            {
                WriteToDScreen(NewData);
            }
            //NewData[49] = TimeNow + ":" + Text + Environment.NewLine;

            Me.CustomData = string.Join(Environment.NewLine, NewData);

            return;
        }


        IMyTextPanel DebugScreen;
        public void WriteToDScreen(string[] Data)
        {
            //Script by ywer
            if (DebugScreen == null)
            {
                DebugScreen = (IMyTextPanel)GridTerminalSystem.GetBlockWithName(DebugLCDName);
                DebugScreen.ContentType = ContentType.TEXT_AND_IMAGE;
                if (DebugScreen == null)
                {
                    WriteToLog("No Debug Screen Found", MassageLevel.Warning);
                    return;
                }
            }
            int Math = Data.Length - MaxDebugLCDRows;
            string Out = "DebugLog by Ywer Ver: " + PVersion + Environment.NewLine;
            int C1 = 0;
            foreach (string Text in Data)
            {
                if (C1 >= Math)
                {
                    Out = Out + Text + Environment.NewLine;
                }


                C1++;
            }



            DebugScreen.WriteText(Out, false);






            return;
        }





        #endregion











    }
}
