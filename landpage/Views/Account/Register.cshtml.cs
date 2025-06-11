using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

public class RegisterModel : PageModel
{
    [BindProperty]
    public ConsumerModel Consumer { get; set; } = new ConsumerModel();

    [BindProperty]
    public SellerModel Seller { get; set; } = new SellerModel();

    public void OnGet()
    {
        // Inicialização redundante para garantir
        Consumer ??= new ConsumerModel();
        Seller ??= new SellerModel();
    }

    public IActionResult OnPostConsumer()
    {
        Consumer ??= new ConsumerModel(); // Garante que não seja nulo
        
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        // Lógica de registro do consumidor
        return RedirectToPage("/Index");
    }

    public IActionResult OnPostSeller()
    {
        Seller ??= new SellerModel(); // Garante que não seja nulo
        
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        // Lógica de registro do vendedor
        return RedirectToPage("/Index");
    }
}

public class ConsumerModel
{
    [Required(ErrorMessage = "Nome completo é obrigatório")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Senha é obrigatória")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}

public class SellerModel
{
    [Required(ErrorMessage = "Nome da empresa é obrigatório")]
    public string CompanyName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email empresarial é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Senha é obrigatória")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}