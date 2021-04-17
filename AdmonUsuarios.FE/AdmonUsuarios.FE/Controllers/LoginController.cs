using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AdmonUsuarios.FE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AdmonUsuarios.FE.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            return View(new UsuarioModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(UsuarioModel model)
        {
            if (model.Usuario_Login == null || model.Usuario_Login.Equals("") ||
                model.Usuario_Password == null || model.Usuario_Password.Equals(""))
            {
                ModelState.AddModelError("", "Ingresar los datos solictiados");
            }
            else
            {
                UsuarioModel usuario = new UsuarioModel();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("https://localhost:44376/api/Usuario/ConsultaUsuario/" + model.Usuario_Login))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        RespuestaServicio respuesta = JsonConvert.DeserializeObject<RespuestaServicio>(apiResponse);

                        JObject juser = respuesta.Data as JObject;
                        usuario = juser.ToObject<UsuarioModel>();
                    }

                    if (usuario.Usuario_Password == model.Usuario_Password)
                    {
                        using (var responsePermisos = await httpClient.GetAsync("https://localhost:44376/api/Usuario/ObtenerPermisos/" + model.Usuario_Login))
                        {
                            string apiResponse = await responsePermisos.Content.ReadAsStringAsync();
                            RespuestaServicio respuesta = JsonConvert.DeserializeObject<RespuestaServicio>(apiResponse);

                            JObject juser = respuesta.Data as JObject;
                            PermisosUsuario permisos = juser.ToObject<PermisosUsuario>();
                            HttpContext.Session.SetObject("permisos", permisos);
                        }

                        HttpContext.Session.SetString("User", model.Usuario_Login);
                        return RedirectToAction("Index", "Usuario");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Datos ingresado no válido.");
                    }
                }
            }

            return View(model);
        }
    }
}
