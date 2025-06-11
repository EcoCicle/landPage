using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;


namespace EcoCicle.Pages
{

    public class LoginModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public class InputModel
        {
            [Required(ErrorMessage = "O email é obrigatório")]
            [EmailAddress(ErrorMessage = "Email inválido")]
            public string? Email { get; set; }

            [Required(ErrorMessage = "A senha é obrigatória")]
            public string? Password { get; set; }

            public bool RememberMe { get; set; }
        }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // TODO: Implementar autenticação
            return RedirectToPage("/Index");
        }
    }
}