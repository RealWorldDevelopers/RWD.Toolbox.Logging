using Serilog;
using System;

namespace RWD.Toolbox.Logging.Demo.Console
{
    /// <summary>
    /// Class for Demo
    /// </summary>
    class Person
    {
        public string Name { get; set; }
        public string LastName { get; set; }

        public Person(string name, string lastName)
        {
            Name = name;
            LastName = lastName;

            Log.Debug("Created a person {@person} at {now}", this, DateTime.Now);
        }

    }
}
