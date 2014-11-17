using System;
using System.Reflection;
namespace Core.Attributes
{
    /// <summary>
    /// Attribute to denote valid assembly for consumption by army builder
    /// </summary>
    public class ArmyCompatibleDLLAttribute : System.Attribute
    {
        string ArmyName;
        string ReflectionName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ArmyName">User-friendly name for the army list</param>
        /// <param name="ReflectionName">Valid assembly specific name for the class that inherits from Core.Army</param>
        public ArmyCompatibleDLLAttribute(string ArmyName, string ReflectionName)
        {
            this.ArmyName = ArmyName;
            this.ReflectionName = ReflectionName;
        }
    }
}