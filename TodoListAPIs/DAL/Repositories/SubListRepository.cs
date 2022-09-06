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
        
        Task<SubListData> GetSubListById(Guid id);
        Task<List<SubListData>> GetSubListByMainListId(Guid id);
        Task DeleteSubList(Guid id);

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

                var SubList = _mapper.Map<SubList>(SubListData);
                SubList.Status = true;

                _appDbContext.SubLists.Add(SubList);
                await _appDbContext.SaveChangesAsync();
                return _mapper.Map<SubListData>(SubList);
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
                var SubList = await _appDbContext.SubLists.Where(s => s.Status == true ).ToListAsync();
                return _mapper.Map<List<SubListData>>(SubList);
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<SubListData>> GetSubListByMainListId(Guid id)
        {
            try
            {

                var SubList = await _appDbContext.SubLists.Where(s => s.MainListId== id && s.Status == true).ToListAsync();

                if (SubList == null)
                {
                    throw new NullReferenceException("SubList with id=" + id + " is not found");
                }
                return _mapper.Map<List<SubListData>>(SubList);
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
                    throw new NullReferenceException("SubList with id=" + id + " is not found");
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
            _appDbContext.Dispose();
        }

        
    }
}
