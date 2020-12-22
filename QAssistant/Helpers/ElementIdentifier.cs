using System;

namespace QAssistant.Helpers
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ElementIdentifier : Attribute
    {
        private readonly string identifier;

        public ElementIdentifier(string identifier)
        {
            this.identifier = identifier;
        }
    }
}
