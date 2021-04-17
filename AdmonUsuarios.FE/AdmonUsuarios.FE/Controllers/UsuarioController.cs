using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AdmonUsuarios.FE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AdmonUsuarios.FE.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: UsuarioController
        public async Task<IActionResult> Index()
        {
            List<UsuarioModel> listaUsuarios = new List<UsuarioModel>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44376/api/Usuario"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    RespuestaServicio respuesta = JsonConvert.DeserializeObject<RespuestaServicio>(apiResponse);
                    List<UsuarioModel> items = ((JArray)respuesta.Data).Select(x => new UsuarioModel
                    {
                        Usuario_Login = (string)x["Usuario_Login"],
                        Usuario_Nombre = (string)x["Usuario_Nombre"],
                        Usuario_Direccion = (string)x["Usuario_Direccion"],
                        Usuario_Email = (string)x["Usuario_Email"],
                        Usuario_Telefono = (string)x["Usuario_Telefono"],
                        Rol_Id = (int)x["Rol_Id"]
                    }).ToList();

                    listaUsuarios = items;
                }
            }
            return View(listaUsuarios);
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioModel userModel)
        {
            try
            {
                UsuarioModel usuario = new UsuarioModel();
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(userModel), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("https://localhost:44376/api/Usuario", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        RespuestaServicio respuesta = JsonConvert.DeserializeObject<RespuestaServicio>(apiResponse);
                        JObject juser = respuesta.Data as JObject;
                        var usuarioRespuesta = juser.ToObject<UsuarioModel>();
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioController/Edit/5
        public async Task<IActionResult> Edit(string login)
        {
            UsuarioModel usuario = new UsuarioModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44376/api/Usuario/ConsultaUsuario/" + login))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    RespuestaServicio respuesta = JsonConvert.DeserializeObject<RespuestaServicio>(apiResponse);

                    JObject juser = respuesta.Data as JObject;
                    usuario = juser.ToObject<UsuarioModel>();
                }
            }
            return View(usuario);
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string login, UsuarioModel userModel)
        {
            try
            {
                UsuarioModel usuario = new UsuarioModel();
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(userModel), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PutAsync("https://localhost:44376/api/Usuario/", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ViewBag.Result = "Success";
                        RespuestaServicio respuesta = JsonConvert.DeserializeObject<RespuestaServicio>(apiResponse);
                        JObject juser = respuesta.Data as JObject;
                        var usuarioRespuesta = respuesta.Data != null ? juser.ToObject<UsuarioModel>() : null;
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string login)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44376/api/Usuario/" + login))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index");
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
