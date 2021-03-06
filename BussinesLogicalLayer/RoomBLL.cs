﻿using Common;
using Common.Infrastructure;
using DataAccessObject;
using DataAccessObject.Infrastructure;
using Entities;
using Entities.QueryModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinesLogicalLayer {
    public class RoomBLL : BaseValidator<Room> {

        private RoomDAO roomDAO = new RoomDAO();
        public Response Insert(Room item, SearchObject search) {
            Response response = Validate(item);
            if (response.Success) {
                {
                    SingleResponse<Room> responseRooms = GetNumbersRooms(search);
                    if (responseRooms.Success)
                    {
                        response.Message = "Número do quarto já cadastrado!";
                    }
                    else
                    {
                        return roomDAO.Insert(item);
                    }
                }
            }
            return response;
        }
        public Response UpdateOcuppyRoom(Room item) {
            Response response = new Response();
            if (response.Success) {
                return roomDAO.UpdateOcuppyRoom(item);
            }
            return response;
        }
        public QueryResponse<RoomQueryModel> GetAllRoomsAvailable() {
            QueryResponse<RoomQueryModel> responseRooms = roomDAO.GetAllRoomsAvailable();
            List<RoomQueryModel> temp = responseRooms.Data;

            return responseRooms;
        }
        public QueryResponse<RoomQueryModel> GetAllRoomsOccupy() {
            QueryResponse<RoomQueryModel> responseRooms = roomDAO.GetAllRoomsOccupy();
            List<RoomQueryModel> temp = responseRooms.Data;

            return responseRooms;
        }
        public QueryResponse<RoomQueryModel> GetAllRoomsByNumberRoom(SearchObject search) {
            QueryResponse<RoomQueryModel> responseRooms = roomDAO.GetAllRoomsByNumberRoom(search);
            List<RoomQueryModel> temp = responseRooms.Data;

            return responseRooms;
        }
        public QueryResponse<RoomQueryModel> GetAllOccuppyRoomsByNumberRoom(SearchObject search) {
            QueryResponse<RoomQueryModel> responseRooms = roomDAO.GetAllOccuppyRoomsByNumberRoom(search);
            List<RoomQueryModel> temp = responseRooms.Data;

            return responseRooms;
        }
        public QueryResponse<RoomType> GetRoomTypeDescription()
        {
            QueryResponse<RoomType> responseRooms = roomDAO.GetRoomTypeDescription();
            List<RoomType> temp = responseRooms.Data;
            
            return responseRooms;
        }
        public SingleResponse<RoomQueryModel> GetById(int id) {

            SingleResponse<RoomQueryModel> responseRooms = roomDAO.GetById(id);

            return responseRooms;
        }
        public SingleResponse<Room> GetRoomTypeIDByRoomID(int id)
        {

            SingleResponse<Room> responseRooms = roomDAO.GetRoomTypeIDByRoomID(id);

            return responseRooms;
        }
        public SingleResponse<Room> GetRoomTypeIDByDescription(string description)
        {

            SingleResponse<Room> responseRooms = roomDAO.GetRoomTypeIDByDescription(description);

            return responseRooms;
        }
        public SingleResponse<Room> GetNumbersRooms(SearchObject search)
        {
            SingleResponse<Room> responseRooms = roomDAO.GetNumbersRooms(search);
            return responseRooms;
        }

        public override Response Validate(Room item) {

            if (string.IsNullOrWhiteSpace(item.NumberRoom)) {
                AddError("O número do quarto deve ser informado.");
            } else if (item.NumberRoom.Length < 1 || item.NumberRoom.Length > 5) {
                AddError("O número do quarto deve conter entre 1 e 5 caracteres");
            }
            if (string.IsNullOrWhiteSpace(item.Description)) {
                AddError("A descrição do quarto deve ser informada.");
            } else if (item.Description.Length < 3 || item.Description.Length > 100) {
                AddError("A descrição deve conter entre 3 e 100 caracteres");
            }
            if (item.IDRoomType == 0) {
                AddError("O tipo do quarto deve ser informado");
            }
            
            return base.Validate(item);
        }

    }
}
