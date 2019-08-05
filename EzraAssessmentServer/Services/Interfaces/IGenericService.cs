
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IGenericService<TCreateDTO, TGetDTO, TUpdateDTO, TEntity>{

    Task<TGetDTO> Create(TCreateDTO model);
	Task<TGetDTO> GetById(int id);
	Task<IEnumerable<TGetDTO>> GetAll();
    Task<IEnumerable<TGetDTO>> GetAll(int quantityPerPage, int pageNumber, string searchQuery);
    Task<TGetDTO> Update(int id, TUpdateDTO model);
    Task<bool> HardDelete(int id);
    Task<bool> SoftDelete(int id);

}