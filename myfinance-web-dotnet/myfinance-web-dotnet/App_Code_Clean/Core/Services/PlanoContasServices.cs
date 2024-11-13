using myfinance_web_dotnet.App_Code_Clean.Core.DataAcess;
using myfinance_web_dotnet.App_Code_Clean.Core.Entities;
using myfinance_web_dotnet.App_Code_Clean.Core.Services.Interfaces;

namespace myfinance_web_dotnet.App_Code_Clean.Core.Services
{
    public class PlanoContasServices: IPlanoContasServices
    {
        private readonly PlanoContasRepository _planoContasRepository;
        
        public PlanoContasServices(PlanoContasRepository planoContasRepository)
        {
            _planoContasRepository = planoContasRepository;
        }

        public async Task<List<PlanoContas>> ObterTodosPlanosContasAsync()
        {
            return await _planoContasRepository.GetAllAsync();
        }

        //public async Task<PlanoContas> ObterPlanoContaPorIdAsync(int id)
        //{
        //    return await _planoContasRepository.GetByIdAsync(id);
        //}
        //public async Task<List<PlanoContas>> BuscarPorNomeAsync(string nome)
        //{
        //    return await _planoContasRepository.BuscarPorNomeAsync(nome);
        //}

        //public async Task AdicionarPlanoContaAsync(PlanoContasDto planoContasDto)
        //{

        //    var novoPlanoContaEntidade = new PlanoContas
        //    {
        //        Id = 0,
        //        Ativo = true,
        //        Descricao = planoContasDto.Descricao,
        //        TipoPlanoConta = planoContasDto.Equals("D") ? PlanoContasEnum.TipoPlanoConta.Despesa : PlanoContasEnum.TipoPlanoConta.Receita
        //    };

        //    await _planoContasRepository.AddAsync(novoPlanoContaEntidade);
        //}

        //public async Task AtualizarPlanoContaAsync(PlanoContas planoContas)
        //{
        //    await _planoContasRepository.UpdateAsync(planoContas);
        //}

        //public async Task RemoverPlanoContaAsync(PlanoContas planoContas)
        //{
        //    await _planoContasRepository.DeleteAsync(planoContas);
        //}
    }
}
