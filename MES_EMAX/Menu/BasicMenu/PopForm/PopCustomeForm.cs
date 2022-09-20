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
using DevExpress.XtraGrid.Views.Grid;

namespace MES
{
    public partial class PopCustomeForm : BaseForm
    {
        public string Cust_Code { get; set; }
        // Edit ↓
        // 거래처코드 텍스트 에디트에 존재하는 코드를 입력한 뒤 수정한 후 저장하려고 할 때 필터링
        // 존재하는 거래처 코드 입력       -> 데이터 수정 -> 저장 => 수정으로 판단
        // 존재하지 않는 거래처 코드 입력  -> 데이터 입력 -> 저장 => 추가로 판단
        public bool Edit = false; 
        ReturnStruct ret = new ReturnStruct();
        DataSet ds = new DataSet();

        public PopCustomeForm(string Cust_Code = null)
        {
            InitializeComponent();

            ForMat.sBasic_Set("regCustome", txt_CustomCode);

            txt_CustomCode.Text = Cust_Code;
            Grid_Set();
        }

        private void PopCustomeForm_Load(object sender, EventArgs e)
        {
            Search_Data(Cust_Code);

            if (string.IsNullOrWhiteSpace(Cust_Code))
            {
                Set_Default();
            }
        }

        private void Grid_Set()
        {
            DbHelp.GridSet(Grid_User, View_User, "Custom_Sort", "", "100", false, true, false);
            DbHelp.GridSet(Grid_User, View_User, "Attn_Name", "담당자", "80", false, true, true);
            DbHelp.GridSet(Grid_User, View_User, "Dept_Name", "부서명", "80", false, true, true);
            DbHelp.GridSet(Grid_User, View_User, "Tel_No", "전화번호", "120", false, true, true);
            DbHelp.GridSet(Grid_User, View_User, "Fax_No", "팩스번호", "120", false, true, true);
            DbHelp.GridSet(Grid_User, View_User, "E_Mail", "이메일", "125", false, true, true);
            DbHelp.GridSet(Grid_User, View_User, "Mobile_No", "핸드폰번호", "120", false, true, true);
            DbHelp.GridSet(Grid_User, View_User, "Text_Memo", "메모", "90", false, true, true);
            DbHelp.GridSet(Grid_User, View_User, "Def_Ck", "주담당자", "65", false, true, true);
            DbHelp.GridSet(Grid_User, View_User, "Use_Ck", "사용유무", "65", false, true, true);
            DbHelp.GridSet(Grid_User, View_User, "Tax_Ck", "계산서", "65", false, true, true);

            Grid_User.AddRowYN = true;
            Grid_User.PopMenuChk = true;
            Grid_User.MouseWheelChk = true;
            View_User.OptionsCustomization.AllowSort = false;

            DbHelp.GridColumn_CheckBox(View_User, "Def_Ck");
            DbHelp.GridColumn_CheckBox(View_User, "Use_Ck");
            DbHelp.GridColumn_CheckBox(View_User, "Tax_Ck");

            Grid_User.DeleteRowEventHandler += Delete_Row;
        }

        private void Set_Default()
        {
            string[] kind_codes = DbHelp.Set_Default("10020");
            string[] currency_codes = DbHelp.Set_Default("10030");
            string[] pay_codes = DbHelp.Set_Default("10050");
            
            if (kind_codes != null)
            {
                txt_CustomPart.Text = kind_codes[0];
                txt_CustomPartNM.Text = kind_codes[1];
            }
            
            if (currency_codes != null)
            {
                txt_PUnit.Text = currency_codes[0];
                txt_PUnitNM.Text = currency_codes[1];
            }

            if (pay_codes != null)
            {
                txt_PayPart.Text = pay_codes[0];
                txt_PayPartNM.Text = pay_codes[1];
            }

            btn_Insert.sUpdate = "N";
            btn_Close.sUpdate = "N";

            if (txt_CustomCode.Enabled)
                txt_CustomCode.Focus();
            else
                txt_CustomPart.Focus();
        }

        #region 텍스트 에디트 이벤트
        // 수정 여부 반영
        protected override void Control_TextChange(object sender, EventArgs e)
        {
            base.Control_TextChange(sender, e);

            btn_Insert.sUpdate = "Y";
            btn_Close.sUpdate = "Y";
        }

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txt_Properties_ButtonClick(sender, null);
        }

        private void txt_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string Gen_Code = "";
            TextEdit Return_Name = txt_CustomPartNM;

            switch (((ButtonEdit)sender).Name)
            {
                case "txt_CustomPart":
                    Gen_Code = "10020";
                    Return_Name = txt_CustomPartNM;
                    break;
                case "txt_PayCode":
                    Gen_Code = "10051";
                    Return_Name = txt_PayCodeNM;
                    break;
                case "txt_Vat_Code":
                    Gen_Code = "10021";
                    Return_Name = txt_Vat_CodeNM;
                    break;
                case "txt_PayPart":
                    Gen_Code = "10050";
                    Return_Name = txt_PayPartNM;
                    break;
                case "txt_PUnit":
                    Gen_Code = "10030";
                    Return_Name = txt_PUnitNM;
                    break;
                case "txt_Country":
                    Gen_Code = "10070";
                    Return_Name = txt_CountryNM;
                    break;
                case "txt_PayCustom":
                    Return_Name = txt_PayCustomNM;
                    break;
            }

            if (string.IsNullOrWhiteSpace(Return_Name.Text))
            {
                if (Return_Name != txt_PayCustomNM)
                {
                    PopHelpForm HelpForm = new PopHelpForm("General", "sp_Help_General", Gen_Code, ((ButtonEdit)sender).Text, "N");
                    if (HelpForm.ShowDialog() == DialogResult.OK)
                    {
                        ((ButtonEdit)sender).Text = HelpForm.sRtCode;
                        Return_Name.Text = HelpForm.sRtCodeNm;
                    }
                }
                else if (Return_Name == txt_PayCustomNM)
                {
                    PopHelpForm HelpForm = new PopHelpForm("Custom", "sp_Help_Custom", ((ButtonEdit)sender).Text, "N");
                    HelpForm.Set_Value("All", "", "", "", "");
                    if (HelpForm.ShowDialog() == DialogResult.OK)
                    {
                        ((ButtonEdit)sender).Text = HelpForm.sRtCode;
                        Return_Name.Text = HelpForm.sRtCodeNm;
                    }
                }
            }
        }

        private void txt_EditValueChanged(object sender, EventArgs e)
        {
            string Gen_Code = "";
            TextEdit Return_Name = txt_CustomPartNM;

            switch (((ButtonEdit)sender).Name)
            {
                case "txt_CustomPart":
                    Gen_Code = "10020";
                    Return_Name = txt_CustomPartNM;
                    break;
                case "txt_PayCode":
                    Gen_Code = "10051";
                    Return_Name = txt_PayCodeNM;
                    break;
                case "txt_Vat_Code":
                    Gen_Code = "10021";
                    Return_Name = txt_Vat_CodeNM;
                    break;
                case "txt_PayPart":
                    Gen_Code = "10050";
                    Return_Name = txt_PayPartNM;
                    break;
                case "txt_PUnit":
                    Gen_Code = "10030";
                    Return_Name = txt_PUnitNM;
                    break;
                case "txt_Country":
                    Gen_Code = "10070";
                    Return_Name = txt_CountryNM;
                    break;
                case "txt_PayCustom":
                    Return_Name = txt_PayCustomNM;
                    break;
            }

            if (Return_Name != txt_PayCustomNM)
                Return_Name.Text = PopHelpForm.Return_Help("sp_Help_General", ((ButtonEdit)sender).Text, Gen_Code);
            else if (Return_Name == txt_PayCustomNM)
                Return_Name.Text = PopHelpForm.Return_Help("sp_Help_Custom", txt_PayCustom.Text, "", "All");
        }
        #endregion

        // 거래처 코드 중복 조회 -> 존재시 해당 데이터 리턴
        private void Search_Data(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                Cust_Code = "@";
                //return;
            }

            SqlParam sp = new SqlParam("sp_regCustome");
            sp.AddParam("Kind", "S");
            sp.AddParam("Custom_Code", str);

            ret = DbHelp.Proc_Search(sp);
            ds = ret.ReturnDataSet;

            if (ds != null && ds.Tables.Count > 1)
            {
                DataTable Table = ds.Tables[0];

                if (Table.Rows.Count > 0)
                {
                    if (Table.Select("Custom_Code = '" + str + "'") != null &&
                        Table.Select("Custom_Code = '" + str + "'").Count() > 0)
                    {
                        DataRow Row = Table.Select("Custom_Code = '" + str + "'")[0];

                        txt_CustomCode.Text = Row["Custom_Code"].NullString();
                        txt_CustomPart.Text = Row["Custom_Part"].NullString();
                        txt_CustomPartNM.Text = Row["Custom_Part_Name"].NullString();
                        txt_CustomName.Text = Row["Custom_Name"].NullString();
                        txt_ShortName.Text = Row["Short_Name"].NullString();
                        txt_CompanyNo.Text = Row["Company_No"].NullString();
                        txt_OfficeNo.Text = Row["Office_No"].NullString();
                        txt_Owner.Text = Row["Owner"].NullString();
                        txt_UpTai.Text = Row["UpTai"].NullString();
                        txt_UpJong.Text = Row["UpJong"].NullString();
                        txt_TelNo.Text = Row["Tel_No"].NullString();
                        txt_FaxNo.Text = Row["Fax_No"].NullString();
                        txt_EMail.Text = Row["E_Mail"].NullString();
                        txt_HomePage.Text = Row["Home_Page"].NullString();

                        txt_PUnit.Text = Row["P_Unit"].NullString();
                        txt_PUnitNM.Text = Row["P_Unit_Name"].NullString();
                        txt_PayPart.Text = Row["Pay_Part"].NullString();
                        txt_PayPartNM.Text = Row["Pay_Part_Name"].NullString();
                        txt_Country.Text = Row["Country_Code"].NullString();
                        txt_PayCode.Text = Row["Pay_Code"].NullString();
                        txt_PayCodeNM.Text = Row["Pay_CodeNM"].NullString();
                        txt_Vat_Code.Text = Row["Vat_Code"].NullString();
                        txt_Vat_CodeNM.Text = Row["Vat_CodeNM"].NullString();
                        if (!string.IsNullOrWhiteSpace(Row["Pay_CustomNM"].NullString()))
                        {
                            txt_PayCustom.Text = Row["Pay_Custom"].NullString();
                            txt_PayCustomNM.Text = Row["Pay_CustomNM"].NullString();
                        }
                        else
                        {
                            txt_PayCustom.Text = Row["Custom_Code"].NullString();
                            txt_PayCustomNM.Text = Row["Short_Name"].NullString();
                        }
                        txt_BillAddr1.Text = Row["Bill_Addr1"].NullString();
                        txt_BillAddr2.Text = Row["Bill_Addr2"].NullString();

                        txt_RegDate.Text = Row["Reg_Date"].NullString();
                        txt_RegUser.Text = Row["Reg_User_Name"].NullString();
                        txt_UpDate.Text = Row["Up_Date"].NullString();
                        txt_UpUser.Text = Row["Up_User_Name"].NullString();

                        txt_PayMonth.Text = Row["Pay_Month"].NullString();
                        txt_PayDay.Text = Row["Pay_Day"].NullString();
                        check_Use.EditValue = Row["Use_Ck"].NullString();

                        Cust_Code = Row["Custom_Code"].NullString();

                        txt_CustomCode.Enabled = false;
                        txt_CustomPart.Focus();
                    }
                    else
                    {
                        Cust_Code = "";
                    }


                }
                else
                {
                    Cust_Code = "";
                }

                Grid_User.DataSource = ds.Tables[1];
                Grid_User.RefreshDataSource();
                //View_User.BestFitColumns();
            }

            btn_Insert.sUpdate = "N";
            btn_Close.sUpdate = "N";
        }

        #region 거래처 코드 검색 및 중복 체크
        // 필수값 체크 -> 모두 존재시 true
        private bool Check_Values()
        {
            if (string.IsNullOrWhiteSpace(txt_CustomPartNM.Text))
            {
                XtraMessageBox.Show("거래처 구분을 입력해주시길 바랍니다.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txt_CustomName.Text))
            {
                XtraMessageBox.Show("거래처명을 입력해주시길 바랍니다.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txt_ShortName.Text))
            {
                XtraMessageBox.Show("줄임상호를 입력해주시길 바랍니다.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txt_CustomCode.Text))
            {
                if (txt_CustomCode.Enabled == false) // 자동채번의 경우 패스
                    return true;
                XtraMessageBox.Show("거래처 코드를 입력해주시길 바랍니다.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txt_PayPartNM.Text))
            {
                XtraMessageBox.Show("결제조건을 입력해주시길 바랍니다.");
                return false;
            }
            return true;
        }

        // 거래처 중복 체크
        private bool Check_Overlap(string compared_cust_code = "")
        {
            SqlParam sp = new SqlParam("sp_regCustome");
            sp.AddParam("Kind", "C");
            sp.AddParam("Custom_Code", txt_CustomCode.Text);
            sp.AddParam("Custom_Name", txt_CustomName.Text);
            sp.AddParam("Short_Name", txt_ShortName.Text);
            sp.AddParam("Company_No", txt_CompanyNo.Text.Replace(" ", "").Replace("-", ""));

            ret = DbHelp.Proc_Search(sp);

            bool Different = (ret.ReturnDataSet.Tables[0].Rows[0][0].NullString() == "True") ? true : false;

            return Different;
        }

        private void txt_CustomCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Search_Data(txt_CustomCode.Text);

                bool pre = false;

                if (string.IsNullOrWhiteSpace(txt_PayPart.Text))
                {
                    //txt_PayPart.Text = "000";
                    pre = true;
                }

                if (string.IsNullOrWhiteSpace(txt_PUnit.Text))
                {
                    //txt_PUnit.Text = "000";
                    pre = true;
                }


                if (pre)
                {
                    btn_Insert.sUpdate = "N";
                    btn_Close.sUpdate = "N";
                }
                txt_CustomPart.Focus();
            }
        }
        #endregion

        #region 버튼 이벤트
        private void btn_Insert_Click(object sender, EventArgs e)
        {
            if (btn_Insert.Result_Update == DialogResult.Yes)
            {
                if (!Save_Data())
                    return;
            }

            DbHelp.Clear_Panel(panel_H);
            DbHelp.Clear_Panel(panelControl3);
            txt_OfficeNo.Text = "";
            txt_CompanyNo.Text = "";
            Grid_User.DataSource = (Grid_User.DataSource as DataTable).Clone();
            Grid_User.RefreshDataSource();

            Set_Default();

            ForMat.sBasic_Set("regCustome", txt_CustomCode);

            Cust_Code = "";
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            SqlParam sp = new SqlParam("sp_regCustome");
            sp.AddParam("Kind", "D");

            sp.AddParam("Custom_Code", txt_CustomCode.Text);

            ret = DbHelp.Proc_Save(sp);

            if (ret.ReturnChk == 0)
            {
                Cust_Code = "";
                DbHelp.Clear_Panel(panel_H);
                DbHelp.Clear_Panel(panelControl3);
                txt_OfficeNo.Text = "";
                txt_CompanyNo.Text = "";
                Grid_User.DataSource = (Grid_User.DataSource as DataTable).Clone();
                Grid_User.RefreshDataSource();

                txt_CompanyNo.Text = null;
                txt_OfficeNo.Text = null;

                btn_Delete.sCHK = "Y";  // 삭제 메세지 출력
                Set_Default();
            }
            else
            {
                XtraMessageBox.Show(ret.ReturnMessage);
                return;
            }
        }

        private void Delete_Row(object sender, EventArgs e)
        {

        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            Save_Data();
            btn_Save.sCHK = "Y";
        }

        private bool Save_Data()
        {
            if (Check_Values())
            {
                if (string.IsNullOrWhiteSpace(Cust_Code) && Check_Overlap())
                {
                    XtraMessageBox.Show("해당 거래처 데이터는 이미 존재합니다.");
                    return false;
                }
                else if (Check_Overlap(Cust_Code))
                {
                    XtraMessageBox.Show("해당 거래처 데이터는 이미 존재합니다.");
                    return false;
                }

                SqlParam sp = new SqlParam("sp_regCustome");

                if (string.IsNullOrWhiteSpace(Cust_Code))
                    sp.AddParam("Kind", "I");
                else
                    sp.AddParam("Kind", "U");

                sp.AddParam("Custom_Code", txt_CustomCode.Text);
                sp.AddParam("Custom_Part", txt_CustomPart.Text);
                sp.AddParam("Custom_Name", txt_CustomName.Text);
                sp.AddParam("Short_Name", txt_ShortName.Text);
                sp.AddParam("Company_No", txt_CompanyNo.Text.Replace("-", ""));
                sp.AddParam("Office_No", txt_OfficeNo.Text.Replace("-", "").Replace(" ", ""));
                sp.AddParam("Owner", txt_Owner.Text);
                sp.AddParam("UpTai", txt_UpTai.Text);
                sp.AddParam("UpJong", txt_UpJong.Text);
                sp.AddParam("Tel_No", txt_TelNo.Text);
                sp.AddParam("Fax_No", txt_FaxNo.Text);
                sp.AddParam("E_Mail", txt_EMail.Text);          
                sp.AddParam("Home_Page", txt_HomePage.Text);
                if (!string.IsNullOrWhiteSpace(txt_PayCustomNM.Text))
                    sp.AddParam("Pay_Custom", txt_PayCustom.Text);
                if (!string.IsNullOrWhiteSpace(txt_PayCodeNM.Text))
                    sp.AddParam("Pay_Code", txt_PayCode.Text);
                if (!string.IsNullOrWhiteSpace(txt_Vat_CodeNM.Text))
                    sp.AddParam("Vat_Code", txt_Vat_Code.Text);
                if (!string.IsNullOrWhiteSpace(txt_PUnitNM.Text))
                    sp.AddParam("P_Unit", txt_PUnit.Text);
                if (!string.IsNullOrWhiteSpace(txt_PayPartNM.Text))
                    sp.AddParam("Pay_Part", txt_PayPart.Text);
                if (!string.IsNullOrWhiteSpace(txt_CountryNM.Text))
                    sp.AddParam("Country_Code", txt_Country.Text);

                sp.AddParam("Use_Ck", check_Use.EditValue.ToString());
                sp.AddParam("Bill_Addr1", txt_BillAddr1.Text);
                sp.AddParam("Bill_Addr2", txt_BillAddr2.Text);

                sp.AddParam("Pay_Month", txt_PayMonth.Text);
                sp.AddParam("Pay_Day", txt_PayDay.Text);

                sp.AddParam("Reg_User", GlobalValue.sUserID);

                ret = DbHelp.Proc_Save(sp);

                if (!txt_CustomCode.Enabled && string.IsNullOrWhiteSpace(txt_CustomCode.Text))
                {
                    Cust_Code = ret.ReturnDataSet.Tables[0].Rows[0]["Cust_Code"].NullString();
                }
                else
                {
                    Cust_Code = txt_CustomCode.Text;
                }

                // Custom_S 데이터
                DataRow Row = DbHelp.Summary_Data(View_User, "Attn_Name", new string[] { "Custom_Sort", "Attn_Name", "Dept_Name", "Tel_No", "Fax_No", "E_Mail", "Mobile_No", "Text_Memo", "Def_Ck", "Use_Ck", "Tax_Ck" });

                sp = new SqlParam("sp_regCustome");
                sp.AddParam("Kind", "P");
                sp.AddParam("Custom_Code", Cust_Code);
                sp.AddParam("S_Custom_Sort", Row["Custom_Sort"].NullString());
                sp.AddParam("S_Attn_Name", Row["Attn_Name"].NullString());
                sp.AddParam("S_Dept_Name", Row["Dept_Name"].NullString());
                sp.AddParam("S_Tel_No", Row["Tel_No"].NullString());
                sp.AddParam("S_Fax_No", Row["Fax_No"].NullString());
                sp.AddParam("S_E_Mail", Row["E_Mail"].NullString());
                sp.AddParam("S_Mobile_No", Row["Mobile_No"].NullString());
                sp.AddParam("S_Text_Memo", Row["Text_Memo"].NullString());
                sp.AddParam("S_Def_Ck", Row["Def_Ck"].NullString());
                sp.AddParam("S_Use_Ck", Row["Use_Ck"].NullString());
                sp.AddParam("S_Tax_Ck", Row["Tax_Ck"].NullString());

                ret = DbHelp.Proc_Save(sp);

                if (ret.ReturnChk == 0)
                {
                    txt_CustomCode.Enabled = false;

                    Search_Data(Cust_Code);

                    btn_Save.sCHK = "Y";
                    btn_Insert.sUpdate = "N";
                    btn_Close.sUpdate = "N";
                    return true;
                }
                else
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return false;
                }
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

        private void txt_PayDay_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_PayDay.Text))
                return;

            if(int.Parse(txt_PayDay.Text) > 31)
            {
                XtraMessageBox.Show("31일 이상 입력 불가능합니다.");
                return;
            }
        }

        private void Grid_User_NewRowAdd(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            if (e.RowHandle == 0)
                ((GridView)sender).SetRowCellValue(e.RowHandle, "Def_Ck", "Y");
            ((GridView)sender).SetRowCellValue(e.RowHandle, "Use_Ck", "Y");
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
    }
}
