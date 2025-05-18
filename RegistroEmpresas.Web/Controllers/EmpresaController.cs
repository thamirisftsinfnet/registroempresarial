using RegistroEmpresas.Application.AppServices;
using RegistroEmpresas.Application.DTOs;
using RegistroEmpresas.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RegistroEmpresas.Web.Controllers
{
    public class EmpresaController : Controller
    {
        private EmpresaAppService _empresaAppService;

        // Injete a interface no construtor
        public EmpresaController()
        {
            _empresaAppService = new EmpresaAppService();
        }

        // GET: Empresa/Create
        public ActionResult Create()
        {
            var model = new PessoaJuridicaDto
            {
                Endereco = new EnderecoDto(),
                Socios = new System.Collections.Generic.List<SocioDto>()
            };

            return View(model);
        }

        // POST: Empresa/Create
        [HttpPost]
        public ActionResult Create(PessoaJuridicaDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            try
            {
                _empresaAppService.CriarEmpresa(dto);
                TempData["Mensagem"] = "Empresa criada com sucesso!";
                return RedirectToAction("Create");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(dto);
            }
        }
    }
}