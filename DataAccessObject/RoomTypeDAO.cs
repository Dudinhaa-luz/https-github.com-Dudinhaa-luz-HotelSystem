﻿using Common;
using Common.Infrastructure;
using DataAccessObject.Infrastructure;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject {
    public class RoomTypeDAO {

        public Response Insert(RoomType roomType) {
            Response response = new Response();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();

            SqlCommand command = new SqlCommand();
            command.CommandText =
                "INSERT INTO ROOMS_TYPE (DESCRICAO, VALOR, VALORDIARIA, QTDHOSPEDES, ISATIVO) VALUES(@DESCRICAO, @VALOR, @VALORDIARIA, @QTDHOSPEDES, @ISATIVO)";
            command.Parameters.AddWithValue("@DESCRICAO", roomType.Description);
            command.Parameters.AddWithValue("@VALOR", roomType.Value);
            command.Parameters.AddWithValue("@VALORDIARIA", roomType.DailyValue);
            command.Parameters.AddWithValue("@QTDHOSPEDES", roomType.GuestNumber);
            command.Parameters.AddWithValue("@ISATIVO", true);

            command.Connection = connection;

            try {
                connection.Open();
                command.ExecuteNonQuery();
                response.Success = true;
                response.Message = "Adicionado com sucesso.";
            } catch (Exception ex) {
                response.Success = false;
                response.Message = "Erro no banco de dados, contate o administrador.";
                response.StackTrace = ex.StackTrace;
                response.ExceptionError = ex.Message;
            } finally {
                connection.Close();
            }
            return response;
        }

        public Response Update(RoomType roomType) {
            Response response = new Response();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();

            SqlCommand command = new SqlCommand();
            command.CommandText =
                "UPDATE ROOMS_TYPE SET DESCRICAO = @DESCRICAO, VALOR = @VALOR, VALORDIARIA = @VALORDIARIA, QTDHOSPEDES = @QTDHOSPEDES, ISATIVO = @ISATIVO WHERE ID = @ID";
            command.Parameters.AddWithValue("@DESCRICAO", roomType.Description);
            command.Parameters.AddWithValue("@VALOR", roomType.Value);
            command.Parameters.AddWithValue("@VALORDIARIA", roomType.DailyValue);
            command.Parameters.AddWithValue("@QTDHOSPEDES", roomType.GuestNumber);
            command.Parameters.AddWithValue("@ISATIVO", roomType.Active);
            command.Parameters.AddWithValue("@ID", roomType.ID);

            command.Connection = connection;

            try {
                connection.Open();
                command.ExecuteNonQuery();
                response.Success = true;
                response.Message = "Atualizado com sucesso.";
            } catch (Exception ex) {
                response.Success = false;
                response.Message = "Erro no banco de dados, contate o administrador.";
                response.StackTrace = ex.StackTrace;
                response.ExceptionError = ex.Message;
            } finally {
                connection.Close();
            }
            return response;
        }

        public QueryResponse<RoomType> GetAllRoomsType() {
            QueryResponse<RoomType> response = new QueryResponse<RoomType>();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();
            SqlCommand command = new SqlCommand();
            command.CommandText =
                "SELECT * FROM ROOMS_TYPE WHERE ISATIVO = 1";
            command.Connection = connection;
            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                List<RoomType> roomsType = new List<RoomType>();

                while (reader.Read()) {
                    RoomType roomType = new RoomType();
                    roomType.ID = (int)reader["ID"];
                    roomType.Description = (string)reader["DESCRICAO"];
                    roomType.Value = (double)reader["VALOR"];
                    roomType.DailyValue = (double)reader["VALORDIARIA"];
                    roomType.GuestNumber = (int)reader["QTDHOSPEDES"];
                    roomType.Active = (bool)reader["ISATIVO"];

                    roomsType.Add(roomType);
                }
                response.Success = true;
                response.Message = "Dados selecionados com sucesso";
                response.Data = roomsType;
                return response;
            } catch (Exception ex) {
                response.Success = false;
                response.Message = "Erro no banco de dados, contate o adm.";
                response.ExceptionError = ex.Message;
                response.StackTrace = ex.StackTrace;
                return response;
            } finally {
                connection.Close();
            }
        }

        public QueryResponse<RoomType> GetAllRoomsTypeByDescription(SearchObject search) {
            QueryResponse<RoomType> response = new QueryResponse<RoomType>();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();
            SqlCommand command = new SqlCommand();
            command.CommandText =
                "SELECT * FROM ROOMS_TYPE WHERE DESCRICAO LIKE @DESCRICAO";
            command.Parameters.AddWithValue("@DESCRICAO", "%" + search.SearchDescription + "%");
            command.Connection = connection;
            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                List<RoomType> roomsType = new List<RoomType>();

                while (reader.Read()) {
                    RoomType roomType = new RoomType();
                    roomType.ID = (int)reader["ID"];
                    roomType.Description = (string)reader["DESCRICAO"];
                    roomType.Value = (double)reader["VALOR"];
                    roomType.DailyValue = (double)reader["VALORDIARIA"];
                    roomType.GuestNumber = (int)reader["QTDHOSPEDES"];
                    roomType.Active = (bool)reader["ISATIVO"];


                    roomsType.Add(roomType);
                }
                response.Success = true;
                response.Message = "Dados selecionados com sucesso";
                response.Data = roomsType;
                return response;
            } catch (Exception ex) {
                response.Success = false;
                response.Message = "Erro no banco de dados, contate o adm.";
                response.ExceptionError = ex.Message;
                response.StackTrace = ex.StackTrace;
                return response;
            } finally {
                connection.Close();
            }
        }

        public QueryResponse<RoomType> GetAllRoomsTypeByGuestNumber(SearchObject search) {
            QueryResponse<RoomType> response = new QueryResponse<RoomType>();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();
            SqlCommand command = new SqlCommand();
            command.CommandText =
                "SELECT * FROM ROOMS_TYPE WHERE QTDHOSPEDES = @QTDHOSPEDES";
            command.Parameters.AddWithValue("@QTDHOSPEDES", search.SearchGuestNumber);
            command.Connection = connection;
            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                List<RoomType> roomsType = new List<RoomType>();

                while (reader.Read()) {
                    RoomType roomType = new RoomType();
                    roomType.ID = (int)reader["ID"];
                    roomType.Description = (string)reader["DESCRICAO"];
                    roomType.Value = (double)reader["VALOR"];
                    roomType.DailyValue = (double)reader["VALORDIARIA"];
                    roomType.GuestNumber = (int)reader["QTDHOSPEDES"];
                    roomType.Active = (bool)reader["ISATIVO"];

                    roomsType.Add(roomType);
                }
                response.Success = true;
                response.Message = "Dados selecionados com sucesso";
                response.Data = roomsType;
                return response;
            } catch (Exception ex) {
                response.Success = false;
                response.Message = "Erro no banco de dados, contate o adm.";
                response.ExceptionError = ex.Message;
                response.StackTrace = ex.StackTrace;
                return response;
            } finally {
                connection.Close();
            }
        }

        public QueryResponse<RoomType> GetById(int id) {
            QueryResponse<RoomType> response = new QueryResponse<RoomType>();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();
            SqlCommand command = new SqlCommand();
            command.CommandText =
                "SELECT * FROM ROOMS_TYPE WHERE ID LIKE @ID";
            command.Parameters.AddWithValue("@ID","%" + id + "%");
            command.Connection = connection;
            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<RoomType> roomsType = new List<RoomType>();

                while (reader.Read())
                {
                    RoomType roomType = new RoomType();
                    roomType.ID = (int)reader["ID"];
                    roomType.Description = (string)reader["DESCRICAO"];
                    roomType.Value = (double)reader["VALOR"];
                    roomType.DailyValue = (double)reader["VALORDIARIA"];
                    roomType.GuestNumber = (int)reader["QTDHOSPEDES"];
                    roomType.Active = (bool)reader["ISATIVO"];


                    roomsType.Add(roomType);
                }
                response.Success = true;
                response.Message = "Dados selecionados com sucesso";
                response.Data = roomsType;
                return response;
            } catch (Exception ex) {
                response.Success = false;
                response.Message = "Erro no banco de dados, contate o adm.";
                response.ExceptionError = ex.Message;
                response.StackTrace = ex.StackTrace;
                return response;
            } finally {
                connection.Close();
            }
        }

        public SingleResponse<RoomType> GetIDByDescription(string description) {
            SingleResponse<RoomType> response = new SingleResponse<RoomType>();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();
            SqlCommand command = new SqlCommand();
            command.CommandText =
                "SELECT ID FROM ROOMS_TYPE WHERE DESCRICAO = @DESCRICAO";
            command.Parameters.AddWithValue("@DESCRICAO", description);
            command.Connection = connection;
            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read()) {
                    RoomType roomType = new RoomType();
                    roomType.ID = (int)reader["ID"];

                    response.Message = "Dados selecionados com sucesso.";
                    response.Success = true;
                    response.Data = roomType;
                    return response;
                }
                response.Message = "Tipo de quarto não encontrado.";
                response.Success = false;
                return response;
            } catch (Exception ex) {
                response.Success = false;
                response.Message = "Erro no banco de dados, contate o adm.";
                response.ExceptionError = ex.Message;
                response.StackTrace = ex.StackTrace;
                return response;
            } finally {
                connection.Close();
            }
        }

        public SingleResponse<RoomType> GetDailyValueByRoomTypeID(int id)
        {
            SingleResponse<RoomType> response = new SingleResponse<RoomType>();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();
            SqlCommand command = new SqlCommand();
            command.CommandText =
                "SELECT VALORDIARIA FROM ROOMS_TYPE WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", id);
            command.Connection = connection;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    RoomType roomType = new RoomType();
                    
                    roomType.DailyValue = (double)reader["VALORDIARIA"];

                    response.Message = "Dados selecionados com sucesso.";
                    response.Success = true;
                    response.Data = roomType;
                    return response;
                }
                response.Message = "Quarto não encontrado.";
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

    }
}
