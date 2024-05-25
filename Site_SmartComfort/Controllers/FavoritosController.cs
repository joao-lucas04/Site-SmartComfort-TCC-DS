using Microsoft.AspNetCore.Mvc;

namespace Site_SmartComfort.Controllers
{
    public class FavoritosController : Controller
    {
        public IActionResult Favoritos()
        {//retornando o repositorio com metodo todosClientes
            return View();
        }
    }
}
