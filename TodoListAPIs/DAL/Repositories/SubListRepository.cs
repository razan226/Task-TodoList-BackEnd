using System.Threading.Tasks;
using System;
using TodoListAPIs.Models.Dtos;
using AutoMapper;
using Dbcontext;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace TodoListAPIs.DAL.Repositories
{

    public interface ISubListRepository : IDisposable
    {

        Task<SubListData> CreateSubList(SubListData SubData);
        Task<List<SubListData>> GetSubLists();
        Task DeleteSublist(Guid id);
        Task<SubListData> GetSubListById(Guid id);
    }
    public class SubListRepository : ISubListRepository
    {
        #region Inject Services
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        #endregion

        public SubListRepository(AppDbContext appDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }
      
        public async Task<SubListData> CreateSubList(SubListData SubListData)
        {
            if (SubListData == null)
                throw new ArgumentNullException(nameof(SubListData));

            try
            {

                var List = _mapper.Map<SubList>(SubListData);

                _appDbContext.SubLists.Add(List);
                await _appDbContext.SaveChangesAsync();
                return _mapper.Map<SubListData>(List);
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<SubListData>> GetSubLists()
        {
            try
            {
                var activeItems = await _appDbContext.SubLists.Where(s => s.Status == true && s.pending != true).ToListAsync();
                return _mapper.Map<List<SubListData>>(activeItems);
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteSubList(Guid id)
        {
            try
            {

                var SubList = await _appDbContext.SubLists.FindAsync(id);

                if (SubList == null || SubList.Status == false)
                {
                    throw new NullReferenceException("SubList with id=" + id + " is not found");
                }
                SubList.Status = false;
                _appDbContext.Update(SubList);
                await _appDbContext.SaveChangesAsync();

            }
            catch
            {
                throw;
            }

           
        }

        

        public async Task<SubListData> GetSubListById(Guid id)
        {
            try
            {

                var SubList = await _appDbContext.SubLists.Where(s => s.Id == id && s.Status == true).FirstOrDefaultAsync();

                if (SubList == null)
                {
                    throw new NullReferenceException("AubList with id=" + id + " is not found");
                }
                return _mapper.Map<SubListData>(SubList);
            }
            catch
            {
                throw;
            }
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        Task ISubListRepository.DeleteSublist(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
