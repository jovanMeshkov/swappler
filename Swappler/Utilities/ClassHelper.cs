namespace Swappler.Utilities
{
    public class ClassHelper
    {
        public static object PropertyValue(object instance, string propertyName)
        {
            return instance.GetType().GetProperty(propertyName).GetValue(instance);
        }
    }
}