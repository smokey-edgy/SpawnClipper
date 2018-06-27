using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;
using System.IO;

[assembly: Rage.Attributes.Plugin("Spawn Clipper", Description = "A tool that helps generate spawning code for map design", Author = "Smoke")]
namespace SpawnClipper
{
    public class EntryPoint
    {

        private static StreamWriter sw;

        public static void Main()
        {
            Game.Console.Print("***** SpawnClipper has been loaded.");

            Vehicle hash1171614426_636656533070630982 = new Vehicle(new Model(1171614426), new Vector3(585.2133f, 1196.997f, 305.7668f), 291.2856f);

            string path = @"spawns.txt";
            if (!File.Exists(path))
                sw = File.CreateText(path);
            else
                sw = File.AppendText(path);

            while (true)
                GameFiber.Yield();
        }

        [Rage.Attributes.ConsoleCommand(Description = "Spawn a vehicle and paste generated code into the clipboard", Name = "ClipV")]
        public static void ClipV(uint hash)
        {           
            float heading = Game.LocalPlayer.Character.Heading;
            Model model = new Model(hash);
            float depth = model.Dimensions.Z;
            Vector3 position = Game.LocalPlayer.Character.GetOffsetPositionFront(depth + 2.0f);
            float X = position.X;
            float Y = position.Y;
            float Z = position.Z;
            Vehicle vehicle = new Vehicle(model, position, heading);
            long ticks = DateTime.Now.Ticks;
            sw.WriteLine("Vehicle hash" + hash + "_" + ticks + " = new Vehicle(new Model(" + hash + "), new Vector3(" + X + "f, " + Y + "f, " + Z + "f), " + heading + "f);");
            sw.Flush();
        }        
    }
}
