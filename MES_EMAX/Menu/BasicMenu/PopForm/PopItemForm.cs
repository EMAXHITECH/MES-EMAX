using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;

namespace MES
{
    public partial class PopItemForm : BaseForm
    {
        public string Item_Code { get; set; }
        ReturnStruct ret = new ReturnStruct();
        DataSet ds = new DataSet();

        public PopItemForm()
        {
            InitializeComponent();
        }

        private void PopItemForm_Load(object sender, EventArgs e)
        {
            txt_Stock_Qty.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txt_Stock_Qty.Properties.Mask.EditMask = "n" + ForMat.SetDecimal("regItem", "Qty1").ToString();
            txt_Stock_Qty.Properties.Mask.UseMaskAsDisplayFormat = true;

            txt_LeadTime.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txt_LeadTime.Properties.Mask.EditMask = "n" + ForMat.SetDecimal("regItem", "Price1").ToString();
            txt_LeadTime.Properties.Mask.UseMaskAsDisplayFormat = true;

            pictureEdit_Item.Properties.ContextMenuStrip = new ContextMenuStrip();

            ForMat.sBasic_Set("regItem", txt_ItemCode);
            FileIF.Set_URL();
            Grid_Set();

            if (!string.IsNullOrWhiteSpace(Item_Code))
            {
                Search_Data(Item_Code);
                Change_State("S");
            }
            else
            {
                Search_Data(" ");
                Set_Default();
                Change_State("Bind");
            }
            

            if (txt_ItemCode.Enabled)
                txt_ItemCode.Focus();
            else
                txt_ItemPart.Focus();
        }

        private void Grid_Set()
        {
            DbHelp.GridSet(Grid_In, View_In, "Cus_Pt", "", "", false, true, false);
            DbHelp.GridSet(Grid_In, View_In, "Custom_Name", "매입처", "150", false, true, true);
            DbHelp.GridSet(Grid_In, View_In, "Custom_Code", "거래처코드", "100", false, true, false);
            DbHelp.GridSet(Grid_In, View_In, "CItem_Code", "거래처품번", "150", false, true, true);
            DbHelp.GridSet(Grid_In, View_In, "Price", "구매단가", "130", false, true, true);
            DbHelp.GridSet(Grid_In, View_In, "Def_Ck", "주거래처", "80", false, true, true);
            DbHelp.GridSet(Grid_In, View_In, "Moq_Qty", "최소구매량", "100", false, true, true);

            DbHelp.GridSet(Grid_Out, View_Out, "Cus_Pt", "", "", false, true, false);
            DbHelp.GridSet(Grid_Out, View_Out, "Custom_Name", "매출처", "150", false, true, true);
            DbHelp.GridSet(Grid_Out, View_Out, "Custom_Code", "거래처코드", "100", false, true, false);
            DbHelp.GridSet(Grid_Out, View_Out, "CItem_Code", "거래처품번", "150", false, true, true);
            DbHelp.GridSet(Grid_Out, View_Out, "Price", "매출단가", "130", false, true, true);
            DbHelp.GridSet(Grid_Out, View_Out, "Def_Ck", "주거래처", "80", false, true, true);

            DbHelp.GridColumn_CheckBox(View_In, "Def_Ck");
            DbHelp.GridColumn_CheckBox(View_Out, "Def_Ck");

            DbHelp.GridColumn_Help(View_In, "Custom_Name", "Y");
            DbHelp.GridColumn_Help(View_Out, "Custom_Name", "Y");

            DbHelp.GridColumn_NumSet(View_Out, "Price", ForMat.SetDecimal("regItem", "Price1"));
            DbHelp.GridColumn_NumSet(View_In, "Price", ForMat.SetDecimal("regItem", "Price1"));
            DbHelp.GridColumn_NumSet(View_In, "Moq_Qty", ForMat.SetDecimal("regItem", "Qty1"));

            RepositoryItemButtonEdit Custom_Help = (RepositoryItemButtonEdit)View_In.Columns["Custom_Name"].ColumnEdit;
            Custom_Help.Buttons[0].Click += new EventHandler(Help_Custom);
            View_In.Columns["Custom_Name"].ColumnEdit = Custom_Help;
            View_Out.Columns["Custom_Name"].ColumnEdit = Custom_Help;

            Grid_In.DeleteRowEventHandler += new EventHandler(Delete_Row);
            Grid_Out.DeleteRowEventHandler += new EventHandler(Delete_Row);

            Grid_In.AddRowYN = true;
            Grid_In.PopMenuChk = true;
            Grid_In.MouseWheelChk = true;
            View_In.OptionsView.ShowAutoFilterRow = false;
            View_In.OptionsCustomization.AllowSort = false;

            Grid_Out.AddRowYN = true;
            Grid_Out.PopMenuChk = true;
            Grid_Out.MouseWheelChk = true;
            View_Out.OptionsView.ShowAutoFilterRow = false;
            View_Out.OptionsCustomization.AllowSort = false;

            View_Out.OptionsView.RowAutoHeight = true;
        }

        private void Grid_EditorKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && ((GridView)((GridControlEx)sender).MainView).FocusedColumn.FieldName == "Custom_Name")
                Help_Custom(sender, null);
        }

        private void Help_Custom(object sender, EventArgs e)
        {
            GridControlEx Grid = (Grid_In.IsFocused) ? Grid_In : Grid_Out;
            GridView View = (Grid_In.IsFocused) ? View_In : View_Out;
            string code = (View.ActiveEditor == null) ? "" : View.ActiveEditor.EditValue.NullString();
            string Split_Val = (Grid_In.IsFocused) ? "1" : "2";

            PopHelpForm Help_Form = new PopHelpForm("Custom", "sp_Help_Item_Custom", code, "Y");
            Help_Form.sNotReturn = "Y";
            Help_Form.Set_Value(Split_Val, "", "", "", "");

            if (Help_Form.ShowDialog() == DialogResult.OK)
            {
                if (Help_Form.drReturn != null)
                {
                    int Index;

                    Index = View.FocusedRowHandle;

                    foreach (DataRow Row in Help_Form.drReturn)
                    {
                        if ((Grid.DataSource as DataTable).Select("Custom_Code = '" + Row["Custom_Code"].NullString() + "'").Count() > 0)
                            continue;
                        View.SetRowCellValue(Index, "Custom_Code", Row["Custom_Code"].NullString());
                        View.SetRowCellValue(Index, "Custom_Name", Row["Short_Name"].NullString());

                        View.AddNewRow();
                        View.UpdateCurrentRow();

                        Index++;
                    }
                }
            }
        }

        private void Delete_Row(object sender, EventArgs e)
        {
            GridView View = (Grid_In.IsFocused) ? View_In : View_Out;
            string Cus_Pt = (Grid_In.IsFocused) ? "1" : "2";
            string Custom_Code = View.GetFocusedRowCellValue("Custom_Code").NullString();

            SqlParam sp = new SqlParam("sp_regItem");
            sp.AddParam("Kind", "E");
            sp.AddParam("Cus_Pt", Cus_Pt);
            sp.AddParam("Custom_Code", Custom_Code);

            ret = DbHelp.Proc_Save(sp);

            if (ret.ReturnChk != 0)
            {
                XtraMessageBox.Show(ret.ReturnMessage);
                return;
            }

            View.DeleteRow(View.FocusedRowHandle);
        }

        private void Set_Default()
        {
            string[] kind_codes = DbHelp.Set_Default("10040");
            string[] group_codes = DbHelp.Set_Default("10041");
            string[] unit_codes = DbHelp.Set_Default("10042");
            string[] sup_codes = DbHelp.Set_Default("10060");
            string[] po_codes = DbHelp.Set_Default("10061");
            string[] qc_k_codes = DbHelp.Set_Default("10080");
            string[] qc_p_codes = DbHelp.Set_Default("10081");

            if (kind_codes != null)
            {
                txt_ItemBpart.Text = kind_codes[0];
                txt_ItemBpartNM.Text = kind_codes[1];
            }

            if (group_codes != null)
            {
                txt_ItemPart.Text = group_codes[0];
                txt_ItemPartNM.Text = group_codes[1];
            }

            if (unit_codes != null)
            {
                txt_QUnit.Text = unit_codes[0];
                txt_QUnitNM.Text = unit_codes[1];
            }
        }

        // 수정 여부 반영
        protected override void Control_TextChange(object sender, EventArgs e)
        {
            base.Control_TextChange(sender, e);

            btn_Insert.sUpdate = "Y";
            btn_Close.sUpdate = "Y";
        }
        

        #region 버튼 이벤트
        private void btn_Insert_Click(object sender, EventArgs e)
        {
            if (btn_Insert.Result_Update == DialogResult.Yes)
            {
                if (!Save_Data())
                    return;
            }
            txt_ItemCode.Enabled = true;

            DbHelp.Clear_Panel(panel_H);
            DbHelp.Clear_Panel(panelControl3);

            Change_State("I");
            Search_Data(" ");
            txt_ItemCode.Focus();
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            SqlParam sp = new SqlParam("sp_regItem");
            sp.AddParam("Kind", "D");
            sp.AddParam("Item_Code", txt_ItemCode.Text);

            ret = DbHelp.Proc_Save(sp);

            if (ret.ReturnChk == 0)
            {
                Search_Data(" ");
                Change_State("Delete");
            }
            else
            {
                XtraMessageBox.Show(ret.ReturnMessage);
                return;
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (Save_Data())
            {
                Change_State("S");
                btn_Save.sCHK = "Y";
            }
        }

        private bool Save_Data()
        {
            if (Check_Values())
            {
                if (string.IsNullOrWhiteSpace(Item_Code) && Check_Overlap())
                {
                    XtraMessageBox.Show("해당 품목 데이터는 이미 존재합니다.");
                    return false;
                }
                else if (Check_Overlap(Item_Code))
                {
                    XtraMessageBox.Show("해당 품목 데이터는 이미 존재합니다.");
                    return false;
                }

                DataRow Row_In = DbHelp.Summary_Data(View_In, "Custom_Name", new string[] { "Custom_Code", "CItem_Code", "Price", "Def_Ck", "Moq_Qty" });
                DataRow Row_Out = DbHelp.Summary_Data(View_Out, "Custom_Name", new string[] { "Custom_Code", "CItem_Code", "Price", "Def_Ck" });

                SqlParam sp = new SqlParam("sp_regItem");


                if (string.IsNullOrWhiteSpace(Item_Code))
                {
                    if (!string.IsNullOrWhiteSpace(txt_QUnit.Text) && string.IsNullOrWhiteSpace(txt_QUnitNM.Text))
                    {
                        XtraMessageBox.Show("존재하지않는 수량 단위입니다.\n다시 시도해주시길 바랍니다.");
                        return false;
                    }

                    sp.AddParam("Kind", "I");
                }
                else
                    sp.AddParam("Kind", "U");

                sp.AddParam("Item_Code", txt_ItemCode.Text);

                if (!string.IsNullOrWhiteSpace(txt_ItemBpartNM.Text))
                    sp.AddParam("Item_BPart", txt_ItemBpart.Text);
                if (!string.IsNullOrWhiteSpace(txt_ItemPartNM.Text))
                    sp.AddParam("Item_Part", txt_ItemPart.Text);

                sp.AddParam("Item_Name", txt_ItemName.Text);
                sp.AddParam("Ssize", txt_Ssize.Text);
                sp.AddParam("Stock_Qty", txt_Stock_Qty.EditValue.NumString());

                if (!string.IsNullOrWhiteSpace(txt_QUnitNM.Text))
                    sp.AddParam("Q_Unit", txt_QUnit.Text);
                if (!string.IsNullOrWhiteSpace(txt_Supply_MethodNM.Text))
                    sp.AddParam("Supply_Method", txt_Supply_Method.Text);
                if (!string.IsNullOrWhiteSpace(txt_Po_MethodNM.Text))
                    sp.AddParam("Po_Method", txt_Po_Method.Text);
                if (!string.IsNullOrWhiteSpace(txt_Qc_KindNM.Text))
                    sp.AddParam("Qc_Kind", txt_Qc_Kind.Text);
                if (!string.IsNullOrWhiteSpace(txt_Qc_PartNM.Text))
                    sp.AddParam("Qc_Part", txt_Qc_Part.Text);

                sp.AddParam("Stock_Ck", check_Stock.EditValue);
                sp.AddParam("Item_Memo", txt_ItemMemo.Text);
                sp.AddParam("Use_Ck", check_Use.EditValue);
                sp.AddParam("Lead_Time", txt_LeadTime.EditValue.NumString());
                sp.AddParam("Reg_User", GlobalValue.sUserID);

                sp.AddParam("Pack_Ck", check_Pack.EditValue);
                sp.AddParam("Loc_Code", txt_Location.Text);

                //이미지 저장
                if(pictureEdit_Item.Image != null)
                    sp.AddParam("Image_URL",  Image_Help.ImageToByte(pictureEdit_Item.Image));

                ret = DbHelp.Proc_Save(sp);

                if (ret.ReturnChk == 0)
                {
                    if (ret.ReturnDataSet != null && ret.ReturnDataSet.Tables.Count > 0)
                    {
                        Item_Code = Convert.ToString((DbHelp.Fill_Table(ret.ReturnDataSet.Tables[0])).Rows[0]["Item_Code"]);
                        Search_Data(Item_Code);
                    }
                }
                else
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return false;
                }

                sp = new SqlParam("sp_regItem");
                sp.AddParam("Kind", "P");
                sp.AddParam("Cus_Pt", "1");
                sp.AddParam("Item_Code", Item_Code);
                sp.AddParam("Custom_Code", Row_In["Custom_Code"].NullString());
                sp.AddParam("CItem_Code", Row_In["CItem_Code"].NullString());
                sp.AddParam("Price", Row_In["Price"].NullString());
                sp.AddParam("Def_Ck", Row_In["Def_Ck"].NullString());
                sp.AddParam("Moq_Qty", Row_In["Moq_Qty"].NullString());

                ret = DbHelp.Proc_Save(sp);

                sp = new SqlParam("sp_regItem");

                sp.AddParam("Kind", "P");
                sp.AddParam("Cus_Pt", "2");
                sp.AddParam("Item_Code", Item_Code);
                sp.AddParam("Custom_Code", Row_Out["Custom_Code"].NullString());
                sp.AddParam("CItem_Code", Row_Out["CItem_Code"].NullString());
                sp.AddParam("Price", Row_Out["Price"].NullString());
                sp.AddParam("Def_Ck", Row_Out["Def_Ck"].NullString());

                ret = DbHelp.Proc_Save(sp);

                Search_Data(Item_Code);
                

                return true;
            }
            else
                return false;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            if (btn_Close.Result_Update == DialogResult.Yes)
            {
                if (!Save_Data())
                    return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion


        #region 버튼 상속
        protected override void btnInsert()
        {
            btn_Insert.PerformClick();
        }

        protected override void btnDelete()
        {
            btn_Delete.PerformClick();
        }

        protected override void btnSave()
        {
            btn_Save.PerformClick();
        }

        protected override void btnClose()
        {
            btn_Close.PerformClick();
        }

        #endregion


        #region 코드값 이벤트
        private void txt_EditValueChanged(object sender, EventArgs e)
        {
            string Gen_Code = "";
            TextEdit Return_Name = txt_ItemBpartNM;

            switch (((ButtonEdit)sender).Name)
            {
                case "txt_ItemBpart":
                    Gen_Code = "10040";
                    Return_Name = txt_ItemBpartNM;
                    break;
                case "txt_ItemPart":
                    Gen_Code = "10041";
                    Return_Name = txt_ItemPartNM;
                    break;
                case "txt_QUnit":
                    Gen_Code = "10042";
                    Return_Name = txt_QUnitNM;
                    break;
                case "txt_Supply_Method":
                    Gen_Code = "10060";
                    Return_Name = txt_Supply_MethodNM;
                    break;
                case "txt_Po_Method":
                    Gen_Code = "10061";
                    Return_Name = txt_Po_MethodNM;
                    break;
                case "txt_Qc_Kind":
                    Gen_Code = "10080";
                    Return_Name = txt_Qc_KindNM;
                    break;
                case "txt_Qc_Part":
                    Gen_Code = "10081";
                    Return_Name = txt_Qc_PartNM;
                    break;
                case "txt_Location":
                    Gen_Code = "10046";
                    Return_Name = txt_LocationName;
                    break;
            }

            Return_Name.Text = PopHelpForm.Return_Help("sp_Help_General", ((ButtonEdit)sender).Text, Gen_Code);
        }

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txt_Properties_ButtonClick(sender, null);
        }

        private void txt_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string Gen_Code = "";
            TextEdit Return_Name = txt_ItemBpartNM;

            switch (((ButtonEdit)sender).Name)
            {
                case "txt_ItemBpart":
                    Gen_Code = "10040";
                    Return_Name = txt_ItemBpartNM;
                    break;
                case "txt_ItemPart":
                    Gen_Code = "10041";
                    Return_Name = txt_ItemPartNM;
                    break;
                case "txt_QUnit":
                    Gen_Code = "10042";
                    Return_Name = txt_QUnitNM;
                    break;
                case "txt_Supply_Method":
                    Gen_Code = "10060";
                    Return_Name = txt_Supply_MethodNM;
                    break;
                case "txt_Po_Method":
                    Gen_Code = "10061";
                    Return_Name = txt_Po_MethodNM;
                    break;
                case "txt_Qc_Kind":
                    Gen_Code = "10080";
                    Return_Name = txt_Qc_KindNM;
                    break;
                case "txt_Qc_Part":
                    Gen_Code = "10081";
                    Return_Name = txt_Qc_PartNM;
                    break;
                case "txt_Location":
                    Gen_Code = "10046";
                    Return_Name = txt_LocationName;
                    break;

            }

            if (string.IsNullOrWhiteSpace(Return_Name.Text))
            {
                PopHelpForm HelpForm = new PopHelpForm("General", "sp_Help_General", Gen_Code, ((ButtonEdit)sender).Text, "N");
                if (HelpForm.ShowDialog() == DialogResult.OK)
                {
                    ((ButtonEdit)sender).Text = HelpForm.sRtCode;
                    Return_Name.Text = HelpForm.sRtCodeNm;
                }
            }
        }
        #endregion


        #region 기타 이벤트 및 메소드
        private void Change_State(string Kind)
        {
            if (Kind.Contains("S"))
            {
                Item_Code = txt_ItemCode.Text;
                txt_ItemCode.Enabled = false;
                txt_ItemPart.Focus();
            }

            else if (Kind.Contains("D"))
            {
                txt_ItemCode.Enabled = true;
                btn_Delete.sCHK = "Y";

                DbHelp.Clear_Panel(panel_H);
                DbHelp.Clear_Panel(panelControl3);

                Set_Default();
                txt_ItemCode.Focus();
            }
            else if (Kind.Contains("I"))
            {
                txt_ItemCode.Enabled = true;
                btn_Insert.sCHK = "Y";

                DbHelp.Clear_Panel(panel_H);
                DbHelp.Clear_Panel(panelControl3);

                Set_Default();
                txt_ItemCode.Focus();
                Item_Code = "";
            }

            btn_Insert.sUpdate = "N";
            btn_Close.sUpdate = "N";
        }

        private bool Check_Values()
        {
            if (string.IsNullOrWhiteSpace(txt_ItemBpartNM.Text))
            {
                XtraMessageBox.Show("품목 구분 입력해주시길 바랍니다.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txt_ItemPartNM.Text))
            {
                XtraMessageBox.Show("품목그룹을 입력해주시길 바랍니다.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txt_ItemName.Text))
            {
                XtraMessageBox.Show("품목명을 입력해주시길 바랍니다.");
                return false;
            }

            return true;
        }

        private bool Check_Overlap(string compare_Item = "")
        {
            // 중복 체크 => 품목명 + 규격 + 색상
            SqlParam sp = new SqlParam("sp_regItem");
            sp.AddParam("Kind", "C");
            sp.AddParam("Item_Name", txt_ItemName.Text);
            sp.AddParam("Ssize", txt_Ssize.Text);

            ret = DbHelp.Proc_Search(sp);

            int check = 0;
            string temp_item_code = "";
            if (ret.ReturnDataSet.Tables[0] != null && ret.ReturnDataSet.Tables[0].Rows.Count > 0)
            {
                check = Convert.ToInt32(ret.ReturnDataSet.Tables[0].Rows[0][0].ToString());
                temp_item_code = ret.ReturnDataSet.Tables[0].Rows[0]["Item_Code"].ToString();
            }

            if (!string.IsNullOrWhiteSpace(compare_Item))
            {
                if (string.IsNullOrWhiteSpace(temp_item_code))
                    return false;
                if (temp_item_code != compare_Item)
                    return true;        // 수정하는 품목이 다른 거래처와 명칭 등이 중복될 경우
                else
                    return false;       // 조회된 중복 품목이 수정하고자 하는 품목일 경우 (수정시 중복 체크)
            }


            if (check > 0)
                return true;
            else return false;
        }

        private void Search_Data(string code)
        {
            code = (code == " ") ? "@" : code.Replace("@", "");

            SqlParam sp = new SqlParam("sp_regItem");
            sp.AddParam("Kind", "S");
            sp.AddParam("Item_Code", code);

            ret = DbHelp.Proc_Search(sp);

            if (ret.ReturnChk != 0)
            {
                XtraMessageBox.Show(ret.ReturnMessage);
            }

            ds = ret.ReturnDataSet;

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow Row = ds.Tables[0].Rows[0];

                    txt_ItemCode.Text = Row["Item_Code"].NullString();
                    txt_ItemBpart.Text = Row["Item_BPart"].NullString();
                    txt_ItemBpartNM.Text = Row["Item_BPartNM"].NullString();
                    txt_ItemPart.Text = Row["Item_Part"].NullString();
                    txt_ItemPartNM.Text = Row["Item_PartNM"].NullString();
                    txt_ItemName.Text = Row["Item_Name"].NullString();
                    txt_Ssize.Text = Row["Ssize"].NullString();
                    txt_Stock_Qty.Text = Row["Stock_Qty"].NullString();
                    txt_QUnit.Text = Row["Q_Unit"].NullString();
                    txt_QUnitNM.Text = Row["Q_UnitNM"].NullString();
                    txt_Supply_Method.Text = Row["Supply_Method"].NullString();
                    txt_Supply_MethodNM.Text = Row["Supply_MethodNM"].NullString();
                    txt_Po_Method.Text = Row["Po_Method"].NullString();
                    txt_Po_MethodNM.Text = Row["Po_MethodNM"].NullString();
                    txt_Qc_Kind.Text = Row["Qc_Kind"].NullString();
                    txt_Qc_KindNM.Text = Row["Qc_KindNM"].NullString();
                    txt_Qc_Part.Text = Row["Qc_Part"].NullString();
                    txt_Qc_PartNM.Text = Row["Qc_PartNM"].NullString();
                    check_Stock.EditValue = Row["Stock_Ck"].NullString();
                    txt_LeadTime.Text = Row["Lead_Time"].NullString();
                    txt_ItemMemo.Text = Row["Item_Memo"].NullString();
                    check_Use.EditValue = Row["Use_Ck"].NullString();
                    txt_RegDate.Text = Row["Reg_Date"].NullString();
                    txt_RegUser.Text = Row["Reg_User_Name"].NullString();
                    txt_UpDate.Text = Row["Up_Date"].NullString();
                    txt_UpUser.Text = Row["Up_User_Name"].NullString();

                    check_Pack.EditValue = Row["Pack_Ck"].NullString();
                    txt_Location.Text = Row["Loc_Code"].NullString();
                    txt_LocationName.Text = Row["Loc_Name"].NullString();


                    if (!string.IsNullOrWhiteSpace(Row["Image_URL"].ToString()))
                    {
                        MemoryStream ms = new MemoryStream((byte[])Row["Image_URL"]);
                        pictureEdit_Item.Image = Image.FromStream(ms);
                    }

                    txt_ItemCode.Enabled = false;
                    txt_ItemBpart.Focus();

                }

                Grid_In.DataSource = ds.Tables[1];
                Grid_In.RefreshDataSource();

                Grid_Out.DataSource = ds.Tables[2];
                Grid_Out.RefreshDataSource();
            }       
            else
            {
                Item_Code = "";
            }

            btn_Insert.sUpdate = "N";
            btn_Close.sUpdate = "N";
        }

        #endregion

        private void txt_ItemCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Search_Data(txt_ItemCode.Text);
            }
        }

        //이미지
        private void pictureEdit_Item_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                Image_Help.Image_Right_Click(sender, e);
            }
        }

        private void View_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Def_Ck")
            {
                int Index = e.RowHandle;

                for (int i = 0; i < ((GridView)sender).RowCount; i++)
                {
                    ((GridView)sender).CellValueChanging -= View_CellValueChanging;
                    ((GridView)sender).SetRowCellValue(i, "Def_Ck", "N");
                    ((GridView)sender).CellValueChanging += View_CellValueChanging;
                }

                ((GridView)sender).CellValueChanging -= View_CellValueChanging;
                ((GridView)sender).SetRowCellValue(Index, "Def_Ck", "Y");
                ((GridView)sender).CellValueChanging += View_CellValueChanging;
            }
        }

        private void Grid_In_NewRowAdd(object sender, InitNewRowEventArgs e)
        {
            if (e.RowHandle == 0)
                ((GridView)sender).SetRowCellValue(e.RowHandle, "Def_Ck", "Y");
        }
    }
}
