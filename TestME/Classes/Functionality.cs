﻿using System.Data;
using System.Windows.Forms;

namespace TestME
{
    public static class Functionality
    {
        public static void RefreshMyQuestions()
        {
            Label lblRegQ = (Label)Utilities.FindControl(Globals.formMain, "lblRegQ");
            Label pnumQ = (Label)Utilities.FindControl(Globals.formMain, "pnumQ");
            DataGridView dgvMyQ = (DataGridView)Utilities.FindControl(Globals.formMain, "dgvMyQ");

            DataTable dt = Utilities.AsyncDB().query("SELECT * FROM questions WHERE uid=" + Globals.logUser.id);
            
            //set all columns to readonly
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dt.Columns[i].ReadOnly = true;
            }

            if (dt.Rows.Count > 0)
            {
                Utilities.InvokeMe(lblRegQ, () =>
                {
                    lblRegQ.Visible = false;
                });
                Utilities.InvokeMe(dgvMyQ, () =>
                {
                    dgvMyQ.DataSource = dt;
                    dgvMyQ.Columns[0].Visible = true;
                    dgvMyQ.Columns[0].HeaderText = "Select";
                    dgvMyQ.Columns[0].Width = 50;
                    dgvMyQ.Columns[1].Visible = false;
                    dgvMyQ.Columns[2].HeaderText = "Questions";
                    dgvMyQ.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvMyQ.Columns[2].Width = 420;
                    dgvMyQ.Columns[3].Visible = false;
                    dgvMyQ.Columns[4].Visible = false;
                    dgvMyQ.Columns[5].HeaderText = "Private";
                    dgvMyQ.Columns[5].Width = 80;
                    dgvMyQ.Columns[6].Visible = false;
                    for (int i = 0; i < dgvMyQ.Rows.Count; i++)
                    {
                        dgvMyQ.Rows[i].Cells[0].Value = "False";
                    }
                });
            }
            else
            {
                Utilities.InvokeMe(lblRegQ, () =>
                {
                    lblRegQ.Visible = true;
                });

                Utilities.InvokeMe(dgvMyQ, () =>
                {
                    dgvMyQ.Columns[0].Visible = false;
                });
            }
            Utilities.InvokeMe(pnumQ, () =>
            {
                pnumQ.Text = dgvMyQ.Rows.Count.ToString();
            });
        }

        public static void LoadTags(AutocompleteMenuNS.AutocompleteMenu autoCMenu = null)
        {
            Globals.colTags = Utilities.AsyncDB().column("SELECT DISTINCT nametag FROM tags");
            if (autoCMenu != null)
            {
                autoCMenu.Items = Globals.colTags.ToArray();
            }
        }
    }
}
