﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VanillaRat.Forms
{
    public partial class Chat : Form
    {
        public Chat()
        {
            InitializeComponent();
            Update = true;
        }

        public int ConnectionID { get; set; }
        public bool Update { get; set; }

        //Send message by button
        private void btnSend_Click(object sender, EventArgs e)
        {
            Classes.Server.MainServer.Send(ConnectionID, Encoding.ASCII.GetBytes("[<MESSAGE>]" + txtSend.Text));
            if (string.IsNullOrWhiteSpace(txtChat.Text))
            {
                txtChat.Text = "You: " + txtSend.Text;
            }
            else
            {
                txtChat.AppendText(Environment.NewLine + "You: " + txtSend.Text);
            }
            txtSend.Text = "";
        }

        //Send message by enter key
        private void txtSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Classes.Server.MainServer.Send(ConnectionID, Encoding.ASCII.GetBytes("[<MESSAGE>]" + txtSend.Text));
                if (string.IsNullOrWhiteSpace(txtChat.Text))
                {
                    txtChat.Text = "You: " + txtSend.Text;
                }
                else
                {
                    txtChat.AppendText(Environment.NewLine + "You: " + txtSend.Text);
                }
                txtSend.Text = "";
            }
        }

        //On form close
        private void Chat_FormClosing(object sender, FormClosingEventArgs e)
        {
            Classes.Server.MainServer.Send(ConnectionID, Encoding.ASCII.GetBytes("CloseChat"));
            Update = false;
        }

        //On form load
        private void Chat_Load(object sender, EventArgs e)
        {
            Classes.Server.MainServer.Send(ConnectionID, Encoding.ASCII.GetBytes("[<MESSAGE>]Opened chat"));
        }     
    }
}
