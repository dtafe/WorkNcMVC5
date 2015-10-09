using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WorkNCInfoService.Mvc5.Models
{
    public class ImageGallery
    {
        [Key]
        public int ImageId { get; set; }
        public string FileName { get; set; }
        public int ImageSize { get; set; }
        public byte[] ImageData { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}
