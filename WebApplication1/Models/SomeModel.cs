using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class SomeModel
    {
        [PandaPropertyBaseConverter]
        public long NotNullProperty { get; set; }
        [PandaPropertyBaseConverter]
        public long? NullableProperty { get; set; }
    }
}
