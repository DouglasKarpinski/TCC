﻿using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
namespace Repository.Repository.Categoria
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly UI _ui;

        public CategoriaRepository(UI ui)
        {
            _ui = ui;
        }

        public IDbConnection Connection => new SqlConnection(_ui.DataBase);


        public IEnumerable<Data.Services.Categoria.Categoria> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = "SELECT " +
                                "c.IdCategoria, " +
                                "c.Nome, " +
                                "c.Descricao, " +
                                "c.IdUnidadeNegocio, " +
                                "c.Ativo, " +
                                "un.Nome as nomeUnidadeNegocio " +
                                "FROM [dbo].[Categoria] c " +
                                "INNER JOIN [dbo].[UnidadeNegocio] un " +
                                "ON c.IdUnidadeNegocio = un.IdUnidadeNegocio ";
                dbConnection.Open();
                return dbConnection.Query<Data.Services.Categoria.Categoria>(sQuery);
            }
        }

        public Data.Services.Categoria.Categoria Post(Data.Services.Categoria.Categoria categoria)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = "INSERT INTO [dbo].[Categoria] (Nome, Descricao, IdUnidadeNegocio, Ativo)"
                                + " VALUES(@Nome, @Descricao, @IdUnidadeNegocio, @Ativo)";
                dbConnection.Open();
                dbConnection.Execute(sQuery, categoria);

                return dbConnection.Query<Data.Services.Categoria.Categoria>("SELECT TOP 1 * FROM [dbo].[Categoria] ORDER BY IdCategoria DESC ").FirstOrDefault();
            }
        }

        public Data.Services.Categoria.Categoria GetById(int idCategoria)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = "SELECT * FROM [dbo].[Categoria]"
                                + "WHERE IdCategoria = @IdCategoria";
                dbConnection.Open();
                return dbConnection.Query<Data.Services.Categoria.Categoria>(sQuery, new {IdCategoria = idCategoria})
                    .FirstOrDefault();
            }
        }

        public Data.Services.Categoria.Categoria Put(Data.Services.Categoria.Categoria categoria)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = "UPDATE [dbo].[Categoria]"
                                + " SET Nome = @Nome, " +
                                "Descricao = @Descricao, " +
                                "IdUnidadeNegocio = @IdUnidadeNegocio, " +
                                "Ativo = @Ativo " +
                                "WHERE IdCategoria = @IdCategoria";

                dbConnection.Open();
                dbConnection.Execute(sQuery, categoria);

                return categoria;
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = "DELETE FROM [dbo].[Categoria] WHERE IdCategoria = @IdCategoria";
                dbConnection.Open();
                dbConnection.Execute(sQuery, new{IdCategoria = id});
            }
        }
       
    }
}
