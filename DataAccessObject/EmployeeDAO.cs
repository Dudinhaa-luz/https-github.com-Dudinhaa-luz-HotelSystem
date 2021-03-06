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
    public class EmployeeDAO {

        public Response Insert(Employee employee) {
            Response response = new Response();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();

            SqlCommand command = new SqlCommand();
            command.CommandText =
                "INSERT INTO EMPLOYEES (NOME, CPF, RG, ENDERECO, TELEFONE, EMAIL, SENHA, ISADM, ISATIVO) VALUES(@NOME, @CPF, @RG, @ENDERECO, @TELEFONE, @EMAIL, @SENHA, @ISADM, @ISATIVO )";
            command.Parameters.AddWithValue("@NOME", employee.Name);
            command.Parameters.AddWithValue("@CPF", employee.CPF);
            command.Parameters.AddWithValue("@RG", employee.RG);
            command.Parameters.AddWithValue("@ENDERECO", employee.Address);
            command.Parameters.AddWithValue("@TELEFONE", employee.PhoneNumber);
            command.Parameters.AddWithValue("@EMAIL", employee.Email);
            command.Parameters.AddWithValue("@SENHA", employee.Password);
            command.Parameters.AddWithValue("@ISADM", employee.IsAdm);
            command.Parameters.AddWithValue("@ISATIVO", true);


            command.Connection = connection;

            try {
                connection.Open();
                command.ExecuteNonQuery();
                response.Success = true;
                response.Message = "Cadastrado com sucesso.";
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

        public Response Update(Employee employee) {
            Response response = new Response();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();

            SqlCommand command = new SqlCommand();
            command.CommandText =
                "UPDATE EMPLOYEES SET NOME = @NOME, ENDERECO = @ENDERECO, TELEFONE = @TELEFONE, EMAIL = @EMAIL WHERE ID = @ID";
            command.Parameters.AddWithValue("@NOME", employee.Name);
            command.Parameters.AddWithValue("@ENDERECO", employee.Address);
            command.Parameters.AddWithValue("@TELEFONE", employee.PhoneNumber);
            command.Parameters.AddWithValue("@EMAIL", employee.Email);
            command.Parameters.AddWithValue("@ID", employee.ID);

            command.Connection = connection;

            try {
                connection.Open();
                int nLinhasAfetadas = command.ExecuteNonQuery();
                if (nLinhasAfetadas != 1) {
                    response.Success = false;
                    response.Message = "Registro não encontrado!";
                    return response;
                }
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

        public Response Delete(Employee employee) {
            Response response = new Response();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();

            SqlCommand command = new SqlCommand();
            command.CommandText =
                "DELETE FROM EMPLOYEES WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", employee.ID);

            command.Connection = connection;

            try {
                connection.Open();
                int nLinhasAfetadas = command.ExecuteNonQuery();
                if (nLinhasAfetadas != 1) {
                    response.Success = false;
                    response.Message = "Registro não encontrado!";
                    return response;
                }
                response.Success = true;
                response.Message = "Excluído com sucesso.";
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

        public Response UpdateActiveEmployee(Employee employee) {
            Response response = new Response();

            SqlConnection connection = new SqlConnection();

            connection.ConnectionString = ConnectionHelper.GetConnectionString();

            SqlCommand command = new SqlCommand();

            command.CommandText =
                "UPDATE EMPLOYEES SET ISATIVO = @ISATIVO WHERE ID = @ID";

            command.Parameters.AddWithValue("@ISATIVO", employee.IsActive);
            command.Parameters.AddWithValue("@ID", employee.ID);

            command.Connection = connection;

            try {
                connection.Open();
                int nLinhasAfetadas = command.ExecuteNonQuery();
                if (nLinhasAfetadas != 1) {
                    response.Success = false;
                    response.Message = "Registro não encontrado!";
                    return response;
                }
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

        public QueryResponse<Employee> GetAllEmployeesByActive() {
            QueryResponse<Employee> response = new QueryResponse<Employee>();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();
            SqlCommand command = new SqlCommand();
            command.CommandText =
                "SELECT * FROM EMPLOYEES WHERE ISATIVO = 1";
            command.Connection = connection;
            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                List<Employee> employees = new List<Employee>();

                while (reader.Read()) {
                    Employee employee = new Employee();
                    employee.ID = (int)reader["ID"];
                    employee.Name = (string)reader["NOME"];
                    employee.CPF = (string)reader["CPF"];
                    employee.RG = (string)reader["RG"];
                    employee.PhoneNumber = (string)reader["TELEFONE"];
                    employee.Email = (string)reader["EMAIL"];
                    employee.Address = (string)reader["ENDERECO"];
                    employee.IsActive = (bool)reader["ISATIVO"];

                    employees.Add(employee);
                }
                response.Success = true;
                response.Message = "Dados selecionados com sucesso";
                response.Data = employees;
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

        public QueryResponse<Employee> GetAllEmployeesByInactive() {
            QueryResponse<Employee> response = new QueryResponse<Employee>();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();
            SqlCommand command = new SqlCommand();
            command.CommandText =
                "SELECT * FROM EMPLOYEES WHERE ISATIVO = 0";
            command.Connection = connection;
            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                List<Employee> employees = new List<Employee>();

                while (reader.Read()) {
                    Employee employee = new Employee();
                    employee.ID = (int)reader["ID"];
                    employee.Name = (string)reader["NOME"];
                    employee.CPF = (string)reader["CPF"];
                    employee.RG = (string)reader["RG"];
                    employee.PhoneNumber = (string)reader["PHONENUMBER"];
                    employee.Email = (string)reader["EMAIL"];
                    employee.Address = (string)reader["ENDERECO"];
                    employee.IsActive = (bool)reader["ISATIVO"];

                    employees.Add(employee);
                }
                response.Success = true;
                response.Message = "Dados selecionados com sucesso";
                response.Data = employees;
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

        public QueryResponse<Employee> GetAllEmployeesByName(SearchObject search) {

            QueryResponse<Employee> response = new QueryResponse<Employee>();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();
            SqlCommand command = new SqlCommand();
            command.CommandText =
                 "SELECT * FROM EMPLOYEES WHERE NOME LIKE @NOME";

            command.Parameters.AddWithValue("@NOME", "%" + search.SearchName + "%");
            command.Connection = connection;
            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                List<Employee> employees = new List<Employee>();

                while (reader.Read()) {
                    Employee employee = new Employee();
                    employee.ID = (int)reader["ID"];
                    employee.Name = (string)reader["NOME"];
                    employee.CPF = (string)reader["CPF"];
                    employee.RG = (string)reader["RG"];
                    employee.PhoneNumber = (string)reader["TELEFONE"];
                    employee.Email = (string)reader["EMAIL"];
                    employee.Address = (string)reader["ENDERECO"];
                    employee.IsActive = (bool)reader["ISATIVO"];

                    employees.Add(employee);
                }
                response.Success = true;
                response.Message = "Dados selecionados com sucesso";
                response.Data = employees;
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

        public QueryResponse<Employee> GetAllEmployeesByCPF(SearchObject search) {

            QueryResponse<Employee> response = new QueryResponse<Employee>();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();
            SqlCommand command = new SqlCommand();
            command.CommandText =
                "SELECT * FROM EMPLOYEES WHERE CPF LIKE @CPF";
            command.Parameters.AddWithValue("@CPF", "%" + search.SearchCPF + "%");
            command.Connection = connection;
            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                List<Employee> employees = new List<Employee>();

                while (reader.Read()) {
                    Employee employee = new Employee();
                    employee.ID = (int)reader["ID"];
                    employee.Name = (string)reader["NOME"];
                    employee.CPF = (string)reader["CPF"];
                    employee.RG = (string)reader["RG"];
                    employee.PhoneNumber = (string)reader["TELEFONE"];
                    employee.Email = (string)reader["EMAIL"];
                    employee.Address = (string)reader["ENDERECO"];

                    employee.IsActive = (bool)reader["ISATIVO"];

                    employees.Add(employee);
                }
                response.Success = true;
                response.Message = "Dados selecionados com sucesso";
                response.Data = employees;
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

        public QueryResponse<Employee> GetById(int id) {
            QueryResponse<Employee> response = new QueryResponse<Employee>();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();
            SqlCommand command = new SqlCommand();
            command.CommandText =
                "SELECT * FROM EMPLOYEES WHERE ID LIKE @ID";
            command.Parameters.AddWithValue("@ID", "%" + id + "%");
            command.Connection = connection;
            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                List<Employee> employees = new List<Employee>();

                while (reader.Read())
                {
                    Employee employee = new Employee();
                    employee.ID = (int)reader["ID"];
                    employee.Name = (string)reader["NOME"];
                    employee.CPF = (string)reader["CPF"];
                    employee.RG = (string)reader["RG"];
                    employee.PhoneNumber = (string)reader["TELEFONE"];
                    employee.Email = (string)reader["EMAIL"];
                    employee.Address = (string)reader["ENDERECO"];

                    employee.IsActive = (bool)reader["ISATIVO"];

                    employees.Add(employee);
                }
                response.Message = "Dados selecionados com sucesso";
                response.Success = true;
                response.Data = employees;
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

        public SingleResponse<Employee> GetEmployeeByLogin(string email, string password) {
            SingleResponse<Employee> response = new SingleResponse<Employee>();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();
            SqlCommand command = new SqlCommand();
            command.CommandText =
                "SELECT * FROM  EMPLOYEES WHERE EMAIL = @EMAIL AND SENHA = @SENHA";
            command.Parameters.AddWithValue("@EMAIL", email);
            command.Parameters.AddWithValue("@SENHA", password);
            command.Connection = connection;
            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                Employee employee = new Employee();


                if (reader.Read()) {
                    employee.ID = (int)reader["ID"];
                    employee.Name = (string)reader["NOME"];
                    employee.CPF = (string)reader["CPF"];
                    employee.RG = (string)reader["RG"];
                    employee.PhoneNumber = (string)reader["TELEFONE"];
                    employee.Email = (string)reader["EMAIL"];
                    employee.Address = (string)reader["ENDERECO"];
                    employee.IsAdm = (bool)reader["ISADM"];

                    response.Success = true;
                    response.Message = "Dados selecionados com sucesso";
                    response.Data = employee;
                    return response;
                }
                response.Success = false;
                response.Message = "Senha incorreta!";
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

        public Response IsCPFUnique(string cpf) {
            QueryResponse<Employee> response = new QueryResponse<Employee>();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();
            SqlCommand command = new SqlCommand();
            command.CommandText =
                "SELECT ID FROM EMPLOYEES WHERE CPF = @CPF";
            command.Parameters.AddWithValue("@CPF", cpf);
            command.Connection = connection;
            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read()) {
                    response.Success = false;
                    response.Message = "CPF já cadastrado!";

                } else {
                    response.Success = true;
                    response.Message = "CPF único.";
                }

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

        public Response IsEmailUnique(string cpf) {
            QueryResponse<Employee> response = new QueryResponse<Employee>();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();
            SqlCommand command = new SqlCommand();
            command.CommandText =
                "SELECT ID FROM EMPLOYEES WHERE EMAIL = @EMAIL";
            command.Parameters.AddWithValue("@EMAIL", cpf);
            command.Connection = connection;
            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read()) {
                    response.Success = false;
                    response.Message = "Email já cadastrado!";

                } else {
                    response.Success = true;
                    response.Message = "";
                }

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

        public SingleResponse<Employee> GetNameByEmployeeID(SearchObject search) {

            SingleResponse<Employee> response = new SingleResponse<Employee>();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();
            SqlCommand command = new SqlCommand();
            command.CommandText =
                "SELECT NOME FROM EMPLOYEES WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", search.SearchID);
            command.Connection = connection;
            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read()) {
                    Employee employee = new Employee();

                    employee.Name = (string)reader["NOME"];
                    response.Message = "Dados selecionados com sucesso.";
                    response.Success = true;
                    response.Data = employee;
                    return response;
                }
                response.Success = false;
                response.Message = "Fornecedor não encontrado";
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

        public SingleResponse<Employee> GetIDByEmployeeName(SearchObject search) {

            SingleResponse<Employee> response = new SingleResponse<Employee>();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();
            SqlCommand command = new SqlCommand();
            command.CommandText =
                "SELECT ID FROM EMPLOYEES WHERE NOME = @NOME";
            command.Parameters.AddWithValue("@NOME", search.SearchID);
            command.Connection = connection;
            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read()) {
                    Employee employee = new Employee();

                    employee.Name = (string)reader["ID"];
                    response.Message = "Dados selecionados com sucesso.";
                    response.Success = true;
                    response.Data = employee;
                    return response;
                }
                response.Success = false;
                response.Message = "Fornecedor não encontrado";
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


    }

}
