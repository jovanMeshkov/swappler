namespace Swappler.Models
{
    public partial class SwapItem
    {
        public const string ImagesPath = @"Public\Storage\Images\";

        public string PhotoUrl
        {
            get
            {
                string imagesPath = ImagesPath;
                return "/"+imagesPath.Replace("\\", "/") + PhotoFilename;
            }
        }
    }
}