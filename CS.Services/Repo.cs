using System;
using System.Linq;
using System.Threading.Tasks;
using CS.Data.Entities;
using CS.EF;
using CS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
/*
namespace CS.Services.Repo

    public class Repo:IRepo
    {
        private CadSysContext _context;

        public Repo(CadSysContext context)
        {
            _context = context;
        }

        public Task<IQueryable<ActProprietate>> GetActeProprietate()
        {
            throw new NotImplementedException();
        }

        public Task<Parcela> GetParcelaByIndex(int index, bool includeProprietari = false, bool includeActe = false)
        {
            throw new NotImplementedException();
        }

        public async Task<Proprietar> GetProprietarByIndex(int index)
        {
            return await _context.Proprietari.Include(x=>x.Inscrieri).Where(x => x.Index == index).FirstOrDefaultAsync();
        }
    }
} */
