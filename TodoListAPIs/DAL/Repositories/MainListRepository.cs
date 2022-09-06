using Dbcontext;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoListAPIs.Models.Dtos;
using AutoMapper;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace TodoListAPIs.DAL.Repositories
{
    public interface IMainListRepository : IDisposable
    {

        Task<MainListData> CreateMainList(MainListData MainData);
        Task<List<MainListData>> GetMainLists();
        Task<MainListData> GetMainListById(Guid id);
        Task DeleteMainList(Guid id);
    }

    public class MainListRepository : IMainListRepository
    {
        #region Inject Services
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        #endregion

        public MainListRepository(AppDbContext appDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<MainListData> CreateMainList(MainListData MainListData)
        {
            if (MainListData == null)
                throw new ArgumentNullException(nameof(MainListData));

            try
            {
                var MainList = _mapper.Map<MainList>(MainListData);
                MainList.Status = true;

                _appDbContext.MainLists.Add(MainList);
                await _appDbContext.SaveChangesAsync();
                return _mapper.Map<MainListData>(MainList);
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<MainListData>> GetMainLists()
        {
            try
            {
                var activeItems = await _appDbContext.MainLists.Where(m => m.Status == true ).ToListAsync();
                return _mapper.Map<List<MainListData>>(activeItems);
            }
            catch
            {
                throw;
            }
        }


        public async Task<MainListData> GetMainListById(Guid id)
        {


            try
            {

                var MainList = await _appDbContext.MainLists.Where(m => m.Id == id && m.Status == true).FirstOrDefaultAsync();

                if (MainList == null)
                {
                    throw new NullReferenceException("MainList with id=" + id + " is not found");
                }
                return _mapper.Map<MainListData>(MainList);
            }
            catch
            {
                throw;
            }

        }
        public async Task DeleteMainList(Guid id)
        {
            try
            {

                var  MainList = await _appDbContext.MainLists.FindAsync(id);

                if (MainList == null || MainList.Status == false)
                {
                    throw new NullReferenceException("MainList with id=" + id + " is not found");
                }
                MainList.Status = false;
                _appDbContext.Update(MainList);
                await _appDbContext.SaveChangesAsync();

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

