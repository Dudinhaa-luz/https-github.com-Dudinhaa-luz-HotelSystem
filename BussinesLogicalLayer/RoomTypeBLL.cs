﻿using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccessObject;
using DataAccessObject.Infrastructure;
using Common.Infrastructure;

namespace BussinesLogicalLayer {
    public class RoomTypeBLL : BaseValidator<RoomType> {

        RoomTypeDAO roomTypeDAO = new RoomTypeDAO();

        public Response Insert(RoomType item) {
            Response response = Validate(item);
            if (response.Success) {
                return roomTypeDAO.Insert(item);
            }
            return response;
        }
        public Response Update(RoomType item) {
            Response response = new Response();
            if (response.Success) {
                return roomTypeDAO.Update(item);
            }
            return response;
        }

        public QueryResponse<RoomType> GetAllRoomsType() {
            QueryResponse<RoomType> responseProducts = roomTypeDAO.GetAllRoomsType();
            List<RoomType> temp = responseProducts.Data;
            foreach (RoomType item in temp) {
                item.Value.ToString("C2");
                item.DailyValue.ToString("C2");
            }
            return responseProducts;
        }

        public QueryResponse<RoomType> GetAllRoomsTypeByDescription(SearchObject search) {
            QueryResponse<RoomType> responseProducts = roomTypeDAO.GetAllRoomsTypeByDescription(search);
            List<RoomType> temp = responseProducts.Data;
            foreach (RoomType item in temp) {
                item.Value.ToString("C2");
                item.DailyValue.ToString("C2");
            }
            return responseProducts;
        }

        public QueryResponse<RoomType> GetAllRoomsTypeByGuestNumber(SearchObject search) {
            QueryResponse<RoomType> responseProducts = roomTypeDAO.GetAllRoomsTypeByGuestNumber(search);
            List<RoomType> temp = responseProducts.Data;
            foreach (RoomType item in temp) {
                item.Value.ToString("C2");
                item.DailyValue.ToString("C2");
            }
            return responseProducts;
        }

        public QueryResponse<RoomType> GetById(int id) {
            QueryResponse<RoomType> responseProducts = roomTypeDAO.GetById(id);

            List<RoomType> temp = responseProducts.Data;
            foreach (RoomType item in temp)
            {
                item.Value.ToString("C2");
                item.DailyValue.ToString("C2");
            }
            return responseProducts;
        }

        public SingleResponse<RoomType> GetDailyValueByRoomTypeID(int id)
        {
            SingleResponse<RoomType> responseProducts = roomTypeDAO.GetDailyValueByRoomTypeID(id);

            RoomType room = new RoomType();

            room.DailyValue.ToString("C2");

            return responseProducts;
        }

        public override Response Validate(RoomType item)
        {

            if (string.IsNullOrWhiteSpace(Convert.ToString(item.Value)))
            {
                AddError("O valor do quarto deve ser informado.");
            }
            else if (string.IsNullOrWhiteSpace(Convert.ToString(item.DailyValue)))
            {
                AddError("O valor da diária do quarto deve ser informado.");
            }
            if (string.IsNullOrWhiteSpace(item.Description))
            {
                AddError("A descrição do quarto deve ser informada.");
            }
            else if (item.Description.Length > 100)
            {
                AddError("A descrição não pode conter mais de 100 caracteres");
            }
            else if (string.IsNullOrWhiteSpace(Convert.ToString(item.GuestNumber)))
            {
                AddError("A quantidade de hóspedes deve ser informado.");
            }
            return base.Validate(item);
        }

    }
}
