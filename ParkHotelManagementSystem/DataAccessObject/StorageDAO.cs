﻿using Common;
using DataAccessObject.Infrastructure;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    class StorageDAO
    {
        public Response Insert(Storage storage)
        {
            Response response = new Response();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();

            SqlCommand command = new SqlCommand();

            command.CommandText =
            "INSERT INTO STORAGE (IDPRODUCTS,QUANTIDADE) VALUES (@IDPRODUCTS,@QUANTIDADE)";
            command.Parameters.AddWithValue("@IDPRODUCTS", storage.ProductsID);
            command.Parameters.AddWithValue("@QUANTIDADE", storage.Quantity);

            command.Connection = connection;

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                response.Success = true;
                response.Message = "Cadastrado com sucesso.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Erro no banco de dados, contate o administrador.";
                response.StackTrace = ex.StackTrace;
                response.ExceptionError = ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return response;
        }

        public Response UpdateAdd(Storage client)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();

            SqlCommand command = new SqlCommand();
            command.CommandText =
                "UPDATE STORAGE SET NOME = @NOME, TELEFONE1 = @TELEFONE1, TELEFONE2 = @TELEFONE2, EMAIL = @EMAIL WHERE ID = @ID";
            command.Parameters.AddWithValue("@NOME", client.Name);
            command.Parameters.AddWithValue("@TELEFONE1", client.PhoneNumber1);
            command.Parameters.AddWithValue("@TELEFONE2", client.PhoneNumber2);
            command.Parameters.AddWithValue("@EMAIL", client.Email);
            command.Parameters.AddWithValue("@ID", client.ID);

            command.Connection = connection;

            try
            {
                connection.Open();
                int nLinhasAfetadas = command.ExecuteNonQuery();
                if (nLinhasAfetadas != 1)
                {
                    response.Success = false;
                    response.Message = "Registro não encontrado!";
                    return response;
                }

                response.Success = true;
                response.Message = "Atualizado com sucesso.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Erro no banco de dados, contate o administrador.";
                response.StackTrace = ex.StackTrace;
                response.ExceptionError = ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return response;
        }

        public Response UpdateActiveClient(Storage client)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();

            SqlCommand command = new SqlCommand();
            command.CommandText =
                "UPDATE CLIENTS SET ISATIVO = @ISATIVO WHERE ID = @ID";
            command.Parameters.AddWithValue("@ISATIVO", client.IsActive);
            command.Parameters.AddWithValue("@ID", client.ID);

            command.Connection = connection;

            try
            {
                connection.Open();
                int nLinhasAfetadas = command.ExecuteNonQuery();
                if (nLinhasAfetadas != 1)
                {
                    response.Success = false;
                    response.Message = "Registro não encontrado!";
                    return response;
                }

                response.Success = true;
                response.Message = "Atualizado com sucesso.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Erro no banco de dados, contate o administrador.";
                response.StackTrace = ex.StackTrace;
                response.ExceptionError = ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return response;
        }

        public Response Delete(Storage client)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();

            SqlCommand command = new SqlCommand();
            command.CommandText =
                "DELETE FROM CLIENTS WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", client.ID);

            command.Connection = connection;

            try
            {
                connection.Open();
                int nLinhasAfetadas = command.ExecuteNonQuery();
                if (nLinhasAfetadas != 1)
                {
                    response.Success = false;
                    response.Message = "Registro não encontrado!";
                    return response;
                }

                response.Success = true;
                response.Message = "Excluído com sucesso.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Erro no banco de dados, contate o administrador.";
                response.StackTrace = ex.StackTrace;
                response.ExceptionError = ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return response;
        }

        public QueryResponse<Storage> GetAllClientsByActive()
        {
            QueryResponse<Client> response = new QueryResponse<Client>();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();

            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM CLIENTS WHERE ATIVO = 1";

            command.Connection = connection;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                List<Client> clients = new List<Client>();

                while (reader.Read())
                {
                    Client client = new Client();
                    client.ID = (int)reader["ID"];
                    client.Name = (string)reader["NOME"];
                    client.CPF = (string)reader["CPF"];
                    client.RG = (string)reader["RG"];
                    client.PhoneNumber1 = (string)reader["TELEFONE1"];
                    client.PhoneNumber2 = (string)reader["TELEFONE2"];
                    client.Email = (string)reader["EMAIL"];
                    client.IsActive = (bool)reader["ISATIVO"];
                    clients.Add(client);
                }
                response.Success = true;
                response.Message = "Dados selecionados com sucesso.";
                response.Data = clients;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Erro no banco de dados contate o adm.";
                response.ExceptionError = ex.Message;
                response.StackTrace = ex.StackTrace;
                return response;
            }
            finally
            {
                connection.Close();
            }

        }

        public QueryResponse<Storage> GetAllClientsByInactive()
        {
            QueryResponse<Client> response = new QueryResponse<Client>();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();

            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM CLIENTS WHERE ATIVO = 0";

            command.Connection = connection;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                List<Client> clients = new List<Client>();

                while (reader.Read())
                {
                    Client client = new Client();
                    client.ID = (int)reader["ID"];
                    client.Name = (string)reader["NOME"];
                    client.CPF = (string)reader["CPF"];
                    client.RG = (string)reader["RG"];
                    client.PhoneNumber1 = (string)reader["TELEFONE1"];
                    client.PhoneNumber2 = (string)reader["TELEFONE2"];
                    client.Email = (string)reader["EMAIL"];
                    client.IsActive = (bool)reader["ISATIVO"];
                    clients.Add(client);
                }
                response.Success = true;
                response.Message = "Dados selecionados com sucesso.";
                response.Data = clients;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Erro no banco de dados contate o adm.";
                response.ExceptionError = ex.Message;
                response.StackTrace = ex.StackTrace;
                return response;
            }
            finally
            {
                connection.Close();
            }

        }

        public QueryResponse<Storage> GetAllEmployeesByName(SearchObject search)
        {

            QueryResponse<Client> response = new QueryResponse<Client>();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();
            SqlCommand command = new SqlCommand();
            command.CommandText =
                "SELECT * FROM CLIENTS WHERE NOME LIKE %NOME% = @NOME";
            command.Parameters.AddWithValue("@NOME", search.SearchName);
            command.Connection = connection;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                List<Client> clients = new List<Client>();

                while (reader.Read())
                {
                    Client client = new Client();
                    client.ID = (int)reader["ID"];
                    client.Name = (string)reader["NOME"];
                    client.CPF = (string)reader["CPF"];
                    client.RG = (string)reader["RG"];
                    client.PhoneNumber1 = (string)reader["TELEFONE1"];
                    client.PhoneNumber2 = (string)reader["TELEFONE2"];
                    client.Email = (string)reader["EMAIL"];
                    client.IsActive = (bool)reader["ISATIVO"];

                    clients.Add(client);
                }
                response.Success = true;
                response.Message = "Dados selecionados com sucesso";
                response.Data = clients;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Erro no banco de dados, contate o adm.";
                response.ExceptionError = ex.Message;
                response.StackTrace = ex.StackTrace;
                return response;
            }
            finally
            {
                connection.Close();
            }
        }

        public QueryResponse<Storage> GetAllEmployeesByCPF(SearchObject search)
        {

            QueryResponse<Client> response = new QueryResponse<Client>();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();
            SqlCommand command = new SqlCommand();
            command.CommandText =
                "SELECT FROM CLIENTS WHERE CPF = @CPF";
            command.Parameters.AddWithValue("@CPF", search.SearchCPF);
            command.Connection = connection;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                List<Client> clients = new List<Client>();

                while (reader.Read())
                {
                    Client client = new Client();
                    client.ID = (int)reader["ID"];
                    client.Name = (string)reader["NOME"];
                    client.CPF = (string)reader["CPF"];
                    client.RG = (string)reader["RG"];
                    client.PhoneNumber1 = (string)reader["TELEFONE1"];
                    client.PhoneNumber2 = (string)reader["TELEFONE2"];
                    client.Email = (string)reader["EMAIL"];
                    client.IsActive = (bool)reader["ISATIVO"];

                    clients.Add(client);
                }
                response.Success = true;
                response.Message = "Dados selecionados com sucesso";
                response.Data = clients;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Erro no banco de dados, contate o adm.";
                response.ExceptionError = ex.Message;
                response.StackTrace = ex.StackTrace;
                return response;
            }
            finally
            {
                connection.Close();
            }
        }

        public SingleResponse<Storage> GetById(int id)
        {
            SingleResponse<Client> response = new SingleResponse<Client>();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();
            SqlCommand command = new SqlCommand();
            command.CommandText =
                "SELECT * FROM  CLIENTS WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", id);
            command.Connection = connection;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Client client = new Client();
                    client.ID = (int)reader["ID"];
                    client.Name = (string)reader["NOME"];
                    client.CPF = (string)reader["CPF"];
                    client.RG = (string)reader["RG"];
                    client.PhoneNumber1 = (string)reader["TELEFONE1"];
                    client.PhoneNumber2 = (string)reader["TELEFONE2"];
                    client.Email = (string)reader["EMAIL"];
                    client.IsActive = (bool)reader["ISATIVO"];
                    response.Message = "Dados selecionados com sucesso.";
                    response.Success = true;
                    response.Data = client;
                    return response;
                }
                response.Message = "Funcionário não encontrado.";
                response.Success = false;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Erro no banco de dados, contate o adm.";
                response.ExceptionError = ex.Message;
                response.StackTrace = ex.StackTrace;
                return response;
            }
            finally
            {
                connection.Close();
            }
        }

        public Response IsCPFUnique(string cpf)
        {
            QueryResponse<Client> response = new QueryResponse<Client>();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();

            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT ID FROM CLIENTS WHERE CPF = @CPF";
            command.Parameters.AddWithValue("@CPF", cpf);

            command.Connection = connection;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    response.Success = false;
                    response.Message = "CPF já cadastrado.";
                }
                else
                {
                    response.Success = true;
                    response.Message = "CPF unico";
                }

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Erro no banco de dados contate o adm.";
                response.ExceptionError = ex.Message;
                response.StackTrace = ex.StackTrace;
                return response;
            }
            finally
            {
                connection.Close();
            }

        }
    }
}
