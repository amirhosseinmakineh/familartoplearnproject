using Microsoft.AspNetCore.Http;
using System.Drawing;

namespace Toplearn.ApplicationService.Contract
{
    public static class ImageValidator
    {
        public static bool IsImage(this IFormFile imageUpload)
        {
            try
            {
                var image = Image.FromStream(imageUpload.OpenReadStream());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
