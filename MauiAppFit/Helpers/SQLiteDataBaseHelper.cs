﻿using MauiAppFit.Models;
using SQLite;

namespace MauiAppFit.Helpers
{
    public class SQLiteDataBaseHelper
    {
        readonly SQLiteAsyncConnection _db;

        public SQLiteDataBaseHelper(string dbPath) 
        {
            _db = new SQLiteAsyncConnection(dbPath);
            _db.CreateTableAsync<Atividade>().Wait();
        }

        public Task<List<Atividade>> GetAllRows() 
        {
            return _db.Table<Atividade>().OrderByDescending(i => i.Id).ToListAsync();
        }

        public Task<Atividade> GetById(int id) 
        {
            return _db.Table<Atividade>().FirstAsync(i => i.Id == id);
        }

        public Task<int> Insert(Atividade model) 
        {
            return _db.InsertAsync(model);
        }

        public Task<List<Atividade>> Update(Atividade model) 
        {
            string sql = "UPDATE Atividade SET Descricao=?, Data=?, Peso=? " +
                "Observacoes=? WHERE Id=?";

            return _db.QueryAsync<Atividade>(
                sql,
                model.Descricao,
                model.Data,
                model.Peso,
                model.Observacoes,
                model.Id
                );
        }

        public Task<int> Delete(int id)
        {
            return _db.Table<Atividade>().DeleteAsync(i => i.Id == id);
        }

        public Task<List<Atividade>> search(string q)
        {
            string sql = "SELECT * FROM Atividade WHERE Descricao LIKE '%" + q + "%'";

            return _db.QueryAsync<Atividade>(sql);
        }
    }
}
