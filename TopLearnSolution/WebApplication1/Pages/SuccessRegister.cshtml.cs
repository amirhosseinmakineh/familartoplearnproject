using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Toplearn.Domain.Models;
using Toplearn.InfraStructure.Context;

namespace WebApplication1.Pages
{
    public class SuccessRegisterModel : PageModel
    {

        [BindProperty(SupportsGet =true)]
        public  string? UserName { get; set; }
        public void OnGet(string userName)
        {
            ViewData["UserName"] = UserName;
        }
    }
}
