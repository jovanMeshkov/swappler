using System.Web;

namespace Swappler.ViewModels
{
    public class PublishSwapItemViewModel
    {
        public HttpPostedFileBase Photo { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}