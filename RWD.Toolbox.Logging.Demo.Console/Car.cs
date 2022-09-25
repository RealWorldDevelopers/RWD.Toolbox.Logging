using Serilog;
using System;

namespace RWD.Toolbox.Logging.Demo.Console
{
    /// <summary>
    /// Class for Demo
    /// </summary>
    class Car
    {
        public string Model { get; set; }
        public int YearReleased { get; set; }
        public Person Owner { get; set; }

        public Car(string model, int yearReleased, Person owner)
        {
            Model = model;
            YearReleased = yearReleased;
            Owner = owner;

            Log.Debug("Created a car {@person} at {now}", this, DateTime.Now);
        }

    }

}
