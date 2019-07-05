﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Library.DAL;
using Library.Models;

namespace Library.Forms
{
    public partial class Report : Form
    {
        private readonly LibraryContext _context;
        public Report()
        {
            InitializeComponent();

            _context = new LibraryContext();

        }


        private void FillOrderList()
        {
            DgvOrderList.Columns[1].DefaultCellStyle.Format = "dd-MM-yyyy";
            DgvOrderList.Columns[5].DefaultCellStyle.Format = "dd-MM-yyyy";

            DgvOrderList.Rows.Clear();

            List<OrderItem> orders = _context.OrderItems.Include("Book").Include("Order").Include("Order.Customer").Where(o=>o.Order.CreatedOn >= DateFrom.Value && o.Order.CreatedOn <= DateTo.Value).ToList();

            foreach (var item in orders)
            {
                DgvOrderList.Rows.Add(item.Id,
                                        item.Order.CreatedOn,
                                        item.Order.Customer.FirstName+" "+ item.Order.Customer.LastName,
                                        item.Book.Name,
                                        item.Count,
                                        item.ReturnDate,
                                        item.Price);
            }

        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            FillOrderList();
            //List<Order> or = _context.Orders.Where(o => o.CreatedOn >= DateFrom.Value && o.CreatedOn <= DateTo.Value).ToList();

            // using (var con = new LibraryContext())
            //{
            //    //var getData = con.or;

            //    foreach (var x in getData)
            //    {
            //        FillOrderList();
            //    }
            //}

        }
    }
}
