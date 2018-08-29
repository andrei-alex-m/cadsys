using System;
using System.Linq;
using System.Threading.Tasks;
using CS.Data.Entities;
using CS.EF;
using CS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CS.Services
{
    public class Repo : IRepo
    {
        private CadSysContext context;

        public Repo(CadSysContext _context)
        {
            context = _context;
        }

        public void ClearDatabase()
        {
            var sql = @"delete from Inscrieri;
                        delete from InscrieriDetaliu;
                        delete from ActeProprietate;
                        delete from Proprietari;
                        delete from Parcele;
                        delete from Imobil;";

            context.Database.ExecuteSqlCommand(sql);
        }
    }
}
