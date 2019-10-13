using System.Collections.Generic;
using AutoMapper;
using Fiap.Api.ViewModel;
using Fiap.Domain.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParceriasController : ControllerBase
    {
        private readonly ParceriaRepository _parceriaRepository;
        private readonly ParceiraLikeRepository _parceiraLikeRepository;

        public ParceriasController()
        {
            _parceriaRepository = new ParceriaRepository();
            _parceiraLikeRepository = new ParceiraLikeRepository();
        }

        [HttpGet("RetornaLista")]
        public ActionResult<IEnumerable<ParceriaViewModel>> RetornaLista()
        {
            var parcerias = _parceriaRepository.ObterTodasValidas();
            var viewModel = Mapper.Map<IList<ParceriaViewModel>>(parcerias);
            return Ok(viewModel);
        }

        [HttpGet("PesquisaParceria")]
        public ActionResult<IEnumerable<ParceriaViewModel>> PesquisaParceria([FromQuery] string termoPesquisa)
        {
            var parcerias = _parceriaRepository.ObterParceriaPorNome(termoPesquisa);
            var viewModel = Mapper.Map<IList<ParceriaViewModel>>(parcerias);
            return Ok(viewModel);
        }

        [HttpGet("RetornaParceria/{codigo}")]
        public ActionResult<ParceriaViewModel> RetornaParceria(int codigo)
        {
            var parceria = _parceriaRepository.ObterPorId(codigo);
            var viewModel = Mapper.Map<ParceriaViewModel>(parceria);
            return Ok(viewModel);
        }

        [HttpPost("CadastraLike/{codigo}")]
        public ActionResult CadastraLike(int codigo)
        {
            var totalLikes = _parceiraLikeRepository.InserirLike(codigo);
            return Ok(totalLikes);
        }
    }
}
