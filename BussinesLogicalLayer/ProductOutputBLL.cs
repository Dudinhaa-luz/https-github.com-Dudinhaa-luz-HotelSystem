﻿using Common;
using DataAccessObject;
using DataAccessObject.Infrastructure;
using Entities;
using Entities.QueryModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace BussinesLogicalLayer {
    public class ProductOutputBLL : BaseValidator<ProductOutput>{

        ProductOutputDAO productsOutputDAO = new ProductOutputDAO();
        StorageBLL storage = new StorageBLL();


        public Response Insert(ProductOutput item) {
            Response response = Validate(item);
            bool success = true;
            if (response.Success) {
                using (TransactionScope scope = new TransactionScope()) {
                    SingleResponse<int> responseInsert = productsOutputDAO.Insert(item);
                    if (responseInsert.Success) {

                        for (int i = 0; i < item.Items.Count; i++) {

                            item.Items[i].IDProductOutput = responseInsert.Data;
                            Response r = productsOutputDAO.InsertProductOutputDetail(item.Items[i]);

                            storage.DeleteProduct(item.Items[i]);

                            if (!r.Success) {

                                success = false;
                                break;
                            }
                        }

                    }
                    if (success) {
                        scope.Complete();

                    }
                }
            }
            return response;
        }
        public Response Update(ProductOutput item) {
            Response response = new Response();
            if (response.Success) {
                return productsOutputDAO.Update(item);
            }
            return response;
        }
        public QueryResponse<ProductOutputQueryModel> GetAllProductOutput() {

            QueryResponse<ProductOutputQueryModel> responseProductsOutput = productsOutputDAO.GetAllProductOutput();

            List<ProductOutputQueryModel> temp = responseProductsOutput.Data;
            foreach (ProductOutputQueryModel item in temp) {
                item.ProductOutputTotalValue.ToString("C2");
            }

            return responseProductsOutput;
        }
        public QueryResponse<ProductOutputQueryModel> GetAllProductOutputbyExitDate(SearchObject search) {

            QueryResponse<ProductOutputQueryModel> responseProductsOutput = productsOutputDAO.GetAllProductOutputbyExityDate(search);

            List<ProductOutputQueryModel> temp = responseProductsOutput.Data;
            foreach (ProductOutputQueryModel item in temp) {
                item.ProductOutputTotalValue.ToString("C2");
            }

            return responseProductsOutput;
        }
        public QueryResponse<ProductOutputQueryModel> GetAllProductIncomebyEmployeeID(SearchObject search) {

            QueryResponse<ProductOutputQueryModel> responseProductsOutput = productsOutputDAO.GetAllProductOutputbyEmployeeID(search);

            List<ProductOutputQueryModel> temp = responseProductsOutput.Data;
            foreach (ProductOutputQueryModel item in temp) {
                item.ProductOutputTotalValue.ToString("C2");
            }

            return responseProductsOutput;
        }
        public QueryResponse<ProductOutputQueryModel> GetAllProductIncomebySupplierID(SearchObject search) {

            QueryResponse<ProductOutputQueryModel> responseProductsOutput = productsOutputDAO.GetAllProductOutputbyClientID(search);

            List<ProductOutputQueryModel> temp = responseProductsOutput.Data;
            foreach (ProductOutputQueryModel item in temp) {
                item.ProductOutputTotalValue.ToString("C2");
            }

            return responseProductsOutput;
        }

        public override Response Validate(ProductOutput item) {
            if (string.IsNullOrWhiteSpace(item.Items.ToString())) {
                AddError("Insira um produto.");
            }
            return base.Validate(item);
        }
    }
}