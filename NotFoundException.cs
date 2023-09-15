namespace CityInfo
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
        public override string ToString()
        {
            return Message; // Only return the custom message, excluding technical details
        }

    }
}