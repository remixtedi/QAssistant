using System;

namespace QAssistant.Helpers
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ElementIdentifier : Attribute
    {
        public string Identifier { get; }

        public ElementIdentifier(string identifier)
        {
            Identifier = identifier;
        }
    }
}
