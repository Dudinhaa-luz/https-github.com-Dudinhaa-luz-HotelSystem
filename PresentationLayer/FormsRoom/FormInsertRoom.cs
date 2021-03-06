﻿using BussinesLogicalLayer;
using Common;
using Common.Infrastructure;
using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer {
    public partial class FormInsertRoom : Form {
        public FormInsertRoom() {
            InitializeComponent();
        }

        Room room = new Room();
        RoomType roomType = new RoomType();
        RoomTypeBLL roomTypeBLL = new RoomTypeBLL();
        RoomBLL rommBLL = new RoomBLL();
        SearchObject searchObject = new SearchObject();

        private void btnInsert_Click(object sender, EventArgs e) {
            room.NumberRoom = txtNumber.Text;
            searchObject.SearchNumberRoom = room.NumberRoom;
            Response response = rommBLL.Insert(room, searchObject);
            MessageBox.Show(response.Message);
        }
        private void FormInsertRoom_Load(object sender, EventArgs e)
        {
            dgvTypesRoom.DataSource = roomTypeBLL.GetAllRoomsType().Data;
        }
        private void txtSource_TextChanged(object sender, EventArgs e)
        {
            if (txtSource.Text == "")
            {
                dgvTypesRoom.DataSource = roomTypeBLL.GetAllRoomsType().Data;
                return;
            }
            else
            {
                searchObject.SearchDescription = txtSource.Text;
                dgvTypesRoom.DataSource = roomTypeBLL.GetAllRoomsTypeByDescription(searchObject).Data;
            }
        }

        private void dgvTypesRoom_SelectionChanged(object sender, EventArgs e)
        {
            this.txtDescription.Text = Convert.ToString(this.dgvTypesRoom.CurrentRow.Cells["Description"].Value);
            this.room.Description = Convert.ToString(this.dgvTypesRoom.CurrentRow.Cells["Description"].Value);
            this.room.IDRoomType= Convert.ToInt32(this.dgvTypesRoom.CurrentRow.Cells["ID"].Value);
            this.roomType.DailyValue = Convert.ToInt32(this.dgvTypesRoom.CurrentRow.Cells["DailyValue"].Value);
            this.roomType.GuestNumber = Convert.ToInt32(this.dgvTypesRoom.CurrentRow.Cells["GuestNumber"].Value);
            this.roomType.Value = Convert.ToInt32(this.dgvTypesRoom.CurrentRow.Cells["Value"].Value);
        }
    }
}
