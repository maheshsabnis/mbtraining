using System;
using System.ComponentModel.DataAnnotations;

namespace mbaspnetcore6.Models
{
    public class ProfileInfo
    {
        public string FileName { get; set; }
        public string UploadStatus { get; set; }
        [Display(Name ="Image")]
        public IFormFile Picture { get; set; }
    }
}

