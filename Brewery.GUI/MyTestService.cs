using Elia.Core.Attributes;

namespace Brewery.GUI
{

    [Injectable]
    public class MyTestService
    {
        public int MyProperty { get; set; }
        public MyTestService()
        {
            MyProperty = 1;
        }
    }
}
