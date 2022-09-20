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
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;

namespace MES
{
    public partial class regWorkPlan : BaseReg
    {
        private string sDeptCode = "";

        ReturnStruct ret = new ReturnStruct();
        DataSet ds = new DataSet();

        private DataTable dt_Rout = new DataTable();

        public regWorkPlan()
        {
            InitializeComponent();
        }

        private void regWorkPlan_Load(object sender, EventArgs e)
        {
            Tree_Set();

            Search();

            txt_Comp.Text = Comp_Def();

            txt_UserCode.Text = GlobalValue.sUserID;

            dt_Plan.Focus();

            btn_Insert.sUpdate = "N";
            btn_Close.sUpdate = "N";
        }

        private void Tree_Set()
        {
            SetUp_Tree.Tree_Columns_Set(tree_Plan, "SIdx_No", "SIdx_No", 0, false);
            SetUp_Tree.Tree_Columns_Set(tree_Plan, "관리 번호(영업/PI)", "Order_No", 150, true, true);
            SetUp_Tree.Tree_Columns_Set(tree_Plan, "품목코드", "Item_Code", 0, false);
            SetUp_Tree.Tree_Columns_Set(tree_Plan, "모품목코드", "MItem_Code", 0, false);
            SetUp_Tree.Tree_Columns_Set(tree_Plan, "자품목코드", "SItem_Code", 150, true, false, true);
            SetUp_Tree.Tree_Columns_Set(tree_Plan, "품목명", "Item_Name", 200, true, false, true);
            SetUp_Tree.Tree_Columns_Set(tree_Plan, "규격", "Ssize", 200, true, false, true);
            SetUp_Tree.Tree_Columns_Set(tree_Plan, "주문 수량", "Order_Qty", 80, true, false, true);
            SetUp_Tree.Tree_Columns_Set(tree_Plan, "계획 수량", "Plan_Qty", 80, true, true);
            SetUp_Tree.Tree_Columns_Set(tree_Plan, "실제 계획 수량", "Real_PlanQty", 0, false); // 실제 작업 수량
            SetUp_Tree.Tree_Columns_Set(tree_Plan, "리드 타임", "Lead_Time", 80, true, false, true);
            SetUp_Tree.Tree_Columns_Set(tree_Plan, "작업 완료 예정일", "End_Date", 110, true, true);
            SetUp_Tree.Tree_Columns_Set(tree_Plan, "작업 시작일", "Start_Date", 110, true, false, true);
            SetUp_Tree.Tree_Columns_Set(tree_Plan, "MCode", "MCode", 0, false);
            SetUp_Tree.Tree_Columns_Set(tree_Plan, "SCode", "SCode", 0, false);
            SetUp_Tree.Tree_Columns_Set(tree_Plan, "Item_BPart", "Item_BPart", 0, false);
            SetUp_Tree.Tree_Columns_Set(tree_Plan, "외주 체크", "Out_Ck", 80, true, true); //추가

            SetUp_Tree.Tree_Columns_Key(tree_Plan, "SCode", "MCode");

            //헬프창 설정
            SetUp_Tree.Tree_Column_Help(tree_Plan, "Order_No", "Y");
            RepositoryItemButtonEdit button_Help = (RepositoryItemButtonEdit)tree_Plan.Columns["Order_No"].ColumnEdit;
            button_Help.Buttons[0].Click += new EventHandler(OrderNo_Help);
            tree_Plan.Columns["Order_No"].ColumnEdit = button_Help;

            //체크 박스
            SetUp_Tree.Tree_Column_Check(tree_Plan, "Out_Ck");

            //일자
            SetUp_Tree.Tree_Column_Date(tree_Plan, "Start_Date");
            SetUp_Tree.Tree_Column_Date(tree_Plan, "End_Date");

            //숫자 자릿수 설정
            SetUp_Tree.Tree_Column_NumSet(tree_Plan, "Order_Qty", 0);
            SetUp_Tree.Tree_Column_NumSet(tree_Plan, "Plan_Qty", 0);
            SetUp_Tree.Tree_Column_NumSet(tree_Plan, "Lead_Time", 0);

            //이미지 설정
            tree_Plan.ImageIndexFieldName = "Order_No";
            tree_Plan.StateImageList = imageCollection_BOM;
            tree_Plan.OptionsView.RowImagesShowMode = RowImagesShowMode.InIndent;

            //*****라우팅 정보*****
            gc_Rout.AddRowYN = false;
            gc_Rout.MouseWheelChk = false;

            DbHelp.GridSet(gc_Rout, gv_Rout, "Sort_No", "Sort_No", "0", false, false, false);
            DbHelp.GridSet(gc_Rout, gv_Rout, "RIdx_No", "Idx_No", "0", false, false, false);
            DbHelp.GridSet(gc_Rout, gv_Rout, "SIdx_No", "SIdx_No", "0", false, false, false);
            DbHelp.GridSet(gc_Rout, gv_Rout, "Item_Code", "품목코드", "120", false, false, true, true);
            DbHelp.GridSet(gc_Rout, gv_Rout, "Item_Name", "품목명", "120", false, false, true, true);
            DbHelp.GridSet(gc_Rout, gv_Rout, "SSize", "구격", "100", false, false, true, true);
            DbHelp.GridSet(gc_Rout, gv_Rout, "Process_Code", "공정코드", "100", false, false, true, true);
            DbHelp.GridSet(gc_Rout, gv_Rout, "Process_Name", "공정명", "100", false, false, true, true);
            DbHelp.GridSet(gc_Rout, gv_Rout, "Qty", "지시수량", "80", false, true, true);
            DbHelp.GridSet(gc_Rout, gv_Rout, "Custom_Name", "작업처", "100", false, true, true, true);
            DbHelp.GridSet(gc_Rout, gv_Rout, "Custom_Code", "작업처코드", "100", false, false, false, true);

            DbHelp.GridColumn_Help(gv_Rout, "Custom_Name", "Y");
            RepositoryItemButtonEdit btn_Custom = (RepositoryItemButtonEdit)gv_Rout.Columns["Custom_Name"].ColumnEdit;
            btn_Custom.Buttons[0].Click += new EventHandler(Custom_Help);
            gv_Rout.Columns["Custom_Name"].ColumnEdit = btn_Custom;

            DbHelp.GridColumn_NumSet(gv_Rout, "Sort_No", 0);
            DbHelp.GridColumn_NumSet(gv_Rout, "Qty", ForMat.SetDecimal(this.Name, "Qty1"));

            gv_Rout.OptionsView.ShowAutoFilterRow = false;

            //BOM 조회
            gc_Bom.AddRowYN = false;
            gc_Bom.PopMenuChk = false;
            gc_Bom.MouseWheelChk = false;

            DbHelp.GridSet(gc_Bom, gv_Bom, "SItem_Code", "자품목코드", "120", false, false, true);
            DbHelp.GridSet(gc_Bom, gv_Bom, "SItem_Name", "자품목명", "150", false, false, true);
            DbHelp.GridSet(gc_Bom, gv_Bom, "SSize", "규격", "120", false, false, true);
            DbHelp.GridSet(gc_Bom, gv_Bom, "Item_BPart", "품목구분", "80", false, false, true);
            DbHelp.GridSet(gc_Bom, gv_Bom, "Qty", "수량", "80", false, false, true);
            DbHelp.GridSet(gc_Bom, gv_Bom, "Loss_Per", "Loss", "80", false, false, true);
            DbHelp.GridSet(gc_Bom, gv_Bom, "Real_Qty", "실소요량", "80", false, false, true);

            DbHelp.GridColumn_NumSet(gv_Bom, "Qty", ForMat.SetDecimal(this.Name, "Qty1"));
            DbHelp.GridColumn_NumSet(gv_Bom, "Loss_Per", ForMat.SetDecimal(this.Name, "Qty1"));
            DbHelp.GridColumn_NumSet(gv_Bom, "Real_Qty", ForMat.SetDecimal(this.Name, "Qty1"));

            gv_Bom.OptionsView.ShowAutoFilterRow = false;
        }

        #region 함수
        private void OrderNo_Help(object sender, EventArgs e)
        {
            TreeListNode FocusNode = tree_Plan.FocusedNode;

            string sOrder_No = FocusNode["Order_No"].NullString();
            string sItem_Code = FocusNode["Item_Code"].NullString();

            if (string.IsNullOrWhiteSpace(sItem_Code))
            {
                PopHelpForm Help_Form = new PopHelpForm("Plan_Order", "sp_Help_PlanOrder", sOrder_No, "N");
                Help_Form.sNotReturn = "Y";
                if(Help_Form.ShowDialog() == DialogResult.OK)
                {
                    FocusNode["Order_No"] = Help_Form.sRtCode;
                    FocusNode["Item_Code"] = Help_Form.sRtCodeNm;
                    Search_Order(FocusNode, Help_Form.sRtCode, Help_Form.sRtCodeNm);
                }
            }
        }

        private void Tree_Node_Add()
        {
            DataTable dt_Tree = tree_Plan.DataSource as DataTable;
            DataRow dt_Add = dt_Tree.NewRow();
            dt_Tree.Rows.Add(dt_Add);
        }

        private void Search()
        {
            try
            {
                SqlParam sp = new SqlParam("sp_regWorkPlan");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_D", "A");
                sp.AddParam("Plan_No", txt_PlanNo.Text);

                ret = DbHelp.Proc_Search(sp);

                if (ret.ReturnChk == 0)
                {
                    DataTable dt_tree = ret.ReturnDataSet.Tables[1];
                    dt_Rout = ret.ReturnDataSet.Tables[2];

                    if (ret.ReturnDataSet.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt_M = ret.ReturnDataSet.Tables[0];

                        dt_Plan.Text = dt_M.Rows[0]["Plan_Date"].ToString();

                        txt_Comp.Text = dt_M.Rows[0]["Company_Code"].ToString();
                        txt_UserCode.Text = dt_M.Rows[0]["User_Code"].ToString();
                        
                        txt_RegDate.Text = dt_M.Rows[0]["Reg_Date"].ToString();
                        txt_RegUser.Text = dt_M.Rows[0]["Reg_User"].ToString();
                        txt_UpDate.Text = dt_M.Rows[0]["Up_Date"].ToString();
                        txt_UpUser.Text = dt_M.Rows[0]["Up_User"].ToString();
                    }
                    else
                    {
                        DbHelp.Clear_Panel(panel_M);
                        DbHelp.Clear_Panel(panel_H);
                    }

                    tree_Plan.DataSource = dt_tree;
                    Out_Check();
                    tree_Plan.ExpandAll();

                    btn_Insert.sUpdate = "N";
                    btn_Close.sUpdate = "N";
                }
                else
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void Search_Order(TreeListNode listNode, string sOrder_No, string sItem_Code)
        {
            tree_Plan.DeleteNode(listNode);

            TreeListNode[] findNodes = tree_Plan.FindNodes((node) => { return node["Order_No"].ToString() == sOrder_No && node["Item_Code"].ToString() == sItem_Code; });

            if(findNodes.Length > 0)
            {
                XtraMessageBox.Show("동일한 관리번호의 품목정보가 있습니다");
                Tree_Node_Add();
                return;
            }

            try
            {
                SqlParam sp = new SqlParam("sp_regWorkPlan");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_D", "SO");
                sp.AddParam("Order_No", sOrder_No);
                sp.AddParam("Item_Code", sItem_Code);
                sp.AddParam("No_Count", tree_Plan.AllNodesCount);

                ret = DbHelp.Proc_Search(sp);

                if (ret.ReturnChk == 0)
                {
                    DataTable dt_Rout_Add = ret.ReturnDataSet.Tables[1];
                    //S
                    DataTable dt_OrderPlan = (tree_Plan.DataSource as DataTable).Copy();
                    DataRow dr_Add;

                    for (int i = 0; i < ret.ReturnDataSet.Tables[0].Rows.Count; i++)
                    {
                        dr_Add = dt_OrderPlan.NewRow();
                        DataRow dr_Search = ret.ReturnDataSet.Tables[0].Rows[i];
                        dr_Add.BeginEdit();
                        for(int j = 0; j < ret.ReturnDataSet.Tables[0].Columns.Count; j++)
                        {
                            dr_Add[j] = dr_Search[j];
                        }
                        dr_Add.EndEdit();
                        dt_OrderPlan.Rows.Add(dr_Add);
                    }

                    dr_Add = dt_OrderPlan.NewRow();
                    dt_OrderPlan.Rows.Add(dr_Add);

                    tree_Plan.DataSource = dt_OrderPlan;// ret.ReturnDataSet.Tables[0];

                    tree_Plan.ExpandAll();

                    tree_Plan.SetFocusedNode(tree_Plan.Nodes[tree_Plan.Nodes.Count - 1]);

                    //Rout
                    if(dt_Rout.Rows.Count == 0)
                    {
                        dt_Rout = dt_Rout_Add;
                    }
                    else
                    {
                        for (int i = 0; i < dt_Rout_Add.Rows.Count; i++)
                        {
                            DataRow dr_Rout_Add = dt_Rout.NewRow();
                            dr_Rout_Add.BeginEdit();
                            for(int j = 0; j < dt_Rout_Add.Columns.Count; j++)
                            {
                                dr_Rout_Add[j] = dt_Rout_Add.Rows[i][j];
                            }
                            dr_Rout_Add.EndEdit();

                            dt_Rout.Rows.Add(dr_Rout_Add);
                        }
                    }
                }
                else
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void User_Search()
        {
            try
            {
                SqlParam sp = new SqlParam("sp_regWorkPlan");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_D", "U");
                sp.AddParam("User_Code", txt_UserCode.Text);

                ret = DbHelp.Proc_Search(sp);

                if (ret.ReturnChk == 0)
                {
                    DataTable dt = ret.ReturnDataSet.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        dt = DbHelp.Fill_Table(dt);
                        DataRow dr = dt.Rows[0];

                        txt_DeptCode.Text = dr["Dept_Name"].ToString();
                        sDeptCode = dr["Dept_Code"].ToString();
                    }
                    else
                    {
                        txt_DeptCode.Text = "";
                        sDeptCode = "";
                    }
                }
                else
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void Search_Bom(string sItem_Code)
        {
            try
            {
                SqlParam sp = new SqlParam("sp_regWorkPlan");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_D", "IB");
                sp.AddParam("Item_Code", sItem_Code);

                ret = DbHelp.Proc_Search(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                gc_Bom.DataSource = ret.ReturnDataSet.Tables[0];

                gv_Bom.BestFitColumns();
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private string Comp_Def()
        {
            string sComp_Code = "";

            try
            {
                SqlParam sp = new SqlParam("sp_regWorkPlan");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_D", "CD");

                ret = DbHelp.Proc_Search(sp);

                if (ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return sComp_Code;
                }

                if (ret.ReturnDataSet.Tables[0].Rows.Count > 0)
                    sComp_Code = ret.ReturnDataSet.Tables[0].Rows[0]["Company_Code"].ToString();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return sComp_Code;
            }

            return sComp_Code;
        }

        private void Custom_Help(object sender, EventArgs e)
        {
            int iRow = gv_Rout.GetFocusedDataSourceRowIndex();

            if (string.IsNullOrWhiteSpace(gv_Rout.GetRowCellValue(iRow, "Custom_Code").ToString()))
            {
                PopHelpForm Help_Form = new PopHelpForm("Custom", "sp_Help_Custom_Param", gv_Rout.GetRowCellValue(iRow, "Custom_Name").ToString());
                Help_Form.Set_Value("생산,외주", "", "", "", "");
                if (Help_Form.ShowDialog() == DialogResult.OK)
                {
                    gv_Rout.SetRowCellValue(iRow, "Custom_Code", Help_Form.sRtCode);
                    if (!string.IsNullOrWhiteSpace(gv_Rout.GetRowCellValue(iRow, "Custom_Code").ToString()))
                    {
                        gv_Rout.SetRowCellValue(iRow, "Custom_Name", Help_Form.sRtCodeNm);
                    }
                }
            }
        }
        private bool Check_Err()
        {
            if (txt_PlanNo.Enabled)
            {
                if (txt_PlanNo.Text == "")
                {
                    XtraMessageBox.Show("계획번호는 필수 입력값입니다");
                    return false;
                }
            }

            if (dt_Plan == null)
            {
                XtraMessageBox.Show("계획일자 필수 입력값입니다");
                return false;
            }

            if (txt_CompNM.Text == "")
            {
                XtraMessageBox.Show("사업장은 필수 입력값입니다");
                return false;
            }

            if (txt_UserName.Text == "")
            {
                XtraMessageBox.Show("담당자는 필수 입력값입니다");
                return false;
            }

            return true;
        }

        private void Out_Check()
        {
            if (tree_Plan.AllNodesCount < 1)
                return;

            DataTable dt_OutCk = tree_Plan.DataSource as DataTable;

            if(dt_OutCk.Select("Out_Ck = 'Y'").Length > 0)
            {
                DataRow[] dr_OutCk = dt_OutCk.Select("Out_Ck = 'Y'");
                int iIndex = 0;
                for(int i = 0; i < dr_OutCk.Length; i++)
                {
                    iIndex = dt_OutCk.Rows.IndexOf(dr_OutCk[i]);

                    TreeListNode node_Out = tree_Plan.FindNodeByID(iIndex);
                    List<TreeListNode> node_OutList = new List<TreeListNode>();

                    SetUp_Tree.Get_AllNodes(node_Out, node_OutList);

                    for (int j = 0; j < node_OutList.Count; j++)
                        node_OutList[j].Visible = false;
                }
            }
        }

        #endregion

        #region 텍스트 이벤트

        private void txt_UserCode_EditValueChanged(object sender, EventArgs e)
        {
            txt_UserName.Text = PopHelpForm.Return_Help("sp_Help_User", txt_UserCode.Text);
            User_Search();
        }

        private void txt_UserCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.txt_UserCode_Properties_ButtonClick(sender, null);
            }
        }

        private void txt_UserCode_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_UserName.Text))
            {
                PopHelpForm HelpForm = new PopHelpForm("User", "sp_Help_User", txt_UserCode.Text, "N");
                if (HelpForm.ShowDialog() == DialogResult.OK)
                {
                    txt_UserCode.Text = HelpForm.sRtCode;
                    txt_UserName.Text = HelpForm.sRtCodeNm;
                    User_Search();
                }
            }
        }

        private void txt_Comp_EditValueChanged(object sender, EventArgs e)
        {
            txt_CompNM.Text = PopHelpForm.Return_Help("sp_Help_Company", txt_Comp.Text);
        }

        private void txt_Comp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txt_Comp_Properties_ButtonClick(sender, null);
            }
        }

        private void txt_Comp_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_CompNM.Text))
            {
                PopHelpForm HelpForm = new PopHelpForm("Company", "sp_Help_Company", txt_Comp.Text, "N");
                if (HelpForm.ShowDialog() == DialogResult.OK)
                {
                    txt_Comp.Text = HelpForm.sRtCode;
                    txt_CompNM.Text = HelpForm.sRtCodeNm;
                }
            }
        }

        #endregion

        #region 버튼 이벤트
        private void btn_Select_Click(object sender, EventArgs e)
        {
            PopHelpForm HelpForm = new PopHelpForm("WorkPlan", "sp_Help_WorkPlan", "", "N");
            HelpForm.sLevelYN = "Y";
            HelpForm.sNotReturn = "Y";
            btn_Select.clsWait.CloseWait();
            if (HelpForm.ShowDialog() == DialogResult.OK)
            {
                txt_PlanNo.Text = HelpForm.sRtCode;

                btn_Select.clsWait.ShowWait(this.FindForm());
                Search();
                btn_Select.clsWait.CloseWait();

                btn_Insert.sUpdate = "N";
                btn_Close.sUpdate = "N";
            }
        }

        private void btn_Insert_Click(object sender, EventArgs e)
        {
            if (btn_Insert.Result_Update == DialogResult.Yes)
            {
                if (!Check_Err())
                    return;
                btn_Save_Click(null, null);
            }

            tree_Plan.DataSource = null;
            gc_Rout.DataSource = null;
            dt_Rout = null;

            txt_PlanNo.Text = "";

            Search();

            txt_UserCode.Text = GlobalValue.sUserID;
            txt_Comp.Text = Comp_Def();

            dt_Plan.Focus();

            btn_Insert.sUpdate = "N";
            btn_Close.sUpdate = "N";
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_PlanNo.Text))
                return;

            try
            {
                SqlParam sp = new SqlParam("sp_regWorkPlan");
                sp.AddParam("Kind", "D");
                sp.AddParam("Delete_D", "M");
                sp.AddParam("Plan_No", txt_PlanNo.Text);

                ret = DbHelp.Proc_Save(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                tree_Plan.DataSource = null;
                gc_Rout.DataSource = null;
                dt_Rout = null;

                txt_PlanNo.Text = "";

                Search();

                txt_UserCode.Text = GlobalValue.sUserID;
                txt_Comp.Text = Comp_Def();

                dt_Plan.Focus();

                btn_Delete.sCHK = "Y";
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (!Check_Err())
                return;
            try
            {
                string sSIdx_No = "", sOrder_No = "", sItem_Code = "", sMItem_Code = "", sSItem_Code = "", sPlan_Qty = "", sStart_Date = "", sEnd_Date = "", sReal_PlanQty = "", sOut_Ck = "";
                string sRIdx_No = "", sRSIdx_No = "", sRSort_No = "", sProcess_Code = "", sCustom_Code = "", sQty = "";

                DataTable dt_Plan_S = tree_Plan.DataSource as DataTable;

                for(int i = 0; i < dt_Plan_S.Rows.Count; i++)
                {
                    if (!string.IsNullOrWhiteSpace(dt_Plan_S.Rows[i]["Order_No"].ToString()))
                    {
                        sSIdx_No += dt_Plan_S.Rows[i]["SIdx_No"].ToString() + "_/";
                        sOrder_No += dt_Plan_S.Rows[i]["Order_No"].ToString() + "_/";
                        sItem_Code += dt_Plan_S.Rows[i]["Item_Code"].ToString() + "_/";
                        sMItem_Code += dt_Plan_S.Rows[i]["MItem_Code"].ToString() + "_/";
                        sSItem_Code += dt_Plan_S.Rows[i]["SItem_Code"].ToString() + "_/";
                        sPlan_Qty += dt_Plan_S.Rows[i]["Plan_Qty"].NumString() + "_/";
                        sStart_Date += dt_Plan_S.Rows[i]["Start_Date"].ToString() == "" ? "_/" : DateTime.Parse(dt_Plan_S.Rows[i]["Start_Date"].ToString()).ToString("yyyyMMdd") + "_/";
                        sEnd_Date += dt_Plan_S.Rows[i]["End_Date"].ToString() == "" ? "_/" : DateTime.Parse(dt_Plan_S.Rows[i]["End_Date"].ToString()).ToString("yyyyMMdd") + "_/";
                        sReal_PlanQty += dt_Plan_S.Rows[i]["Real_PlanQty"].NumString() + "_/";
                        sOut_Ck += dt_Plan_S.Rows[i]["Out_Ck"].ToString() + "_/";
                    }
                }

                for(int i = 0; i< dt_Rout.Rows.Count; i++)
                {
                    sRIdx_No += dt_Rout.Rows[i]["RIdx_No"].ToString() + "_/";
                    sRSIdx_No += dt_Rout.Rows[i]["SIdx_No"].ToString() + "_/";
                    sRSort_No += dt_Rout.Rows[i]["Sort_No"].ToString() + "_/";
                    sProcess_Code += dt_Rout.Rows[i]["Process_Code"].ToString() + "_/";
                    sCustom_Code += dt_Rout.Rows[i]["Custom_Code"].ToString() + "_/";
                    sQty += dt_Rout.Rows[i]["Qty"].NumString() + "_/";
                }

                SqlParam sp = new SqlParam("sp_regWorkPlan");
                sp.AddParam("Kind", "I");
                sp.AddParam("Plan_No", txt_PlanNo.Text);
                sp.AddParam("Plan_Date", dt_Plan.DateTime.ToString("yyyyMMdd"));
                sp.AddParam("Company_Code", txt_Comp.Text);
                sp.AddParam("User_Code", txt_UserCode.Text);
                sp.AddParam("Plan_Memo", txt_Memo.Text);
                sp.AddParam("Reg_User", GlobalValue.sUserID);

                sp.AddParam("SIdxNo", sSIdx_No);
                sp.AddParam("OrderNo", sOrder_No);
                sp.AddParam("ItemCode", sItem_Code);
                sp.AddParam("MItemCode", sMItem_Code);
                sp.AddParam("SItemCode", sSItem_Code);
                sp.AddParam("Plan_Qty", sPlan_Qty);
                sp.AddParam("Start_Date", sStart_Date);
                sp.AddParam("End_Date", sEnd_Date);
                sp.AddParam("Real_PlanQty", sReal_PlanQty);
                sp.AddParam("Out_Ck", sOut_Ck);

                sp.AddParam("RIdxNo", sRIdx_No);
                sp.AddParam("RSIdxNo", sRSIdx_No);
                sp.AddParam("RSortNo", sRSort_No);
                sp.AddParam("ProcessCode", sProcess_Code);
                sp.AddParam("CustomCode", sCustom_Code);
                sp.AddParam("RQty", sQty);

                ret = DbHelp.Proc_Save(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                txt_PlanNo.Text = ret.ReturnDataSet.Tables[0].Rows[0]["Plan_No"].NullString();

                Search();

                btn_Save.sCHK = "Y";
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            MainForm.MainTab.TabPages.Remove(MainForm.MainTab.SelectedTabPage);
        }
        #endregion

        #region 버튼 상속
        protected override void btnSelect()
        {
            btn_Select.PerformClick();
        }

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

        protected override void btnExcel()
        {
            btn_Excel.PerformClick();
        }

        protected override void btnClose()
        {
            btn_Close.PerformClick();
        }

        protected override void Control_TextChange(object sender, EventArgs e)
        {
            base.Control_TextChange(sender, e);

            btn_Insert.sUpdate = "Y";
            btn_Close.sUpdate = "Y";
        }

        #endregion

        #region Tree 이벤트
        private void tree_Plan_Enter(object sender, EventArgs e)
        {
            if(tree_Plan.AllNodesCount == 0)
                Tree_Node_Add();
        }

        private void tree_Plan_Click(object sender, EventArgs e)
        {
            if (tree_Plan.AllNodesCount == 0)
                Tree_Node_Add();
        }
        #endregion


        #region 트리, 그리드 이벤트
        private void tree_Plan_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Plan_Qty")
            {
                decimal dPlan_Qty = decimal.Parse(e.Value.NumString());

                string sSIdx = e.Node["SIdx_No"].ToString();

                if (dt_Rout.Select("SIdx_No = '" + sSIdx + "'").Length > 0)
                {
                    DataRow[] dr_Select = dt_Rout.Select("SIdx_No = '" + sSIdx + "'");

                    for (int i = 0; i < dr_Select.Length; i++)
                    {
                        dr_Select[i]["Qty"] = dPlan_Qty;
                    }

                    gc_Rout.DataSource = dr_Select.CopyToDataTable();
                }
            }
            else if (e.Column.FieldName == "End_Date")
            {
                decimal dLeadTime = decimal.Parse(e.Node["Lead_Time"].NumString());
                DateTime S_WorkEnd_DT = DateTime.Parse(e.Node["Start_Date"].ToString()).AddDays((double)dLeadTime);

                int iDay = (DateTime.Parse(e.Value.ToString()) - S_WorkEnd_DT).Days;

                e.Node["Start_Date"] = DateTime.Parse(e.Value.ToString()).AddDays(-(double)dLeadTime);
            }
        }


        private void tree_Plan_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Out_Ck")
            {
                List<TreeListNode> list_Node = new List<TreeListNode>();

                SetUp_Tree.Get_AllNodes(e.Node, list_Node);

                if (e.Value.ToString() == "Y")
                {
                    for (int i = 0; i < list_Node.Count; i++)
                    {
                        list_Node[i]["Plan_Qty"] = "0";
                        list_Node[i].Visible = false;
                    }
                }
                else
                {
                    for (int i = 0; i < list_Node.Count; i++)
                    {
                        list_Node[i]["Plan_Qty"] = list_Node[i]["Real_PlanQty"];
                        list_Node[i].Visible = true;
                    }
                }

                tree_Plan.CellValueChanging -= tree_Plan_CellValueChanging;
                e.Node["Out_Ck"] = e.Value.ToString();
                tree_Plan.CellValueChanging += tree_Plan_CellValueChanging;

                tree_Plan_FocusedNodeChanged(sender, new FocusedNodeChangedEventArgs(e.Node, e.Node));
            }
        }

        private void tree_Mat_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Po_Date")
            {
                decimal dLeadTime = decimal.Parse(e.Node["Lead_Time"].NumString());

                e.Node["In_Date"] = DateTime.Parse(e.Node["Po_Date"].ToString()).AddDays((double)dLeadTime);
            }
        }

        private void tree_Plan_GetStateImage(object sender, GetStateImageEventArgs e)
        {
            if (e.Node["Item_BPart"].NullString() == "100")
                e.NodeImageIndex = 0;
            else if (e.Node["Item_BPart"].NullString() == "200")
                e.NodeImageIndex = 1;
            else 
                e.NodeImageIndex = 2;
        }

        private void tree_Plan_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            if (e.Node == null)
                return;

            string sSIdx = e.Node["SIdx_No"].ToString();

            DataTable dt_Select = dt_Rout.Clone();
            int iIdx = 0;

            if (dt_Rout.Select("SIdx_No = '" + sSIdx + "'").Length > 0)
            {
                DataRow[] dr_Select = dt_Rout.Select("SIdx_No = '" + sSIdx + "'");

                for(int i = 0; i < dr_Select.Length; i++)
                {
                    DataRow dr = dt_Select.NewRow();
                    dr.ItemArray = dr_Select[i].ItemArray;

                    iIdx = int.Parse(dr["Sort_No"].NumString()) - 1;

                    dt_Select.Rows.InsertAt(dr, iIdx);
                }
            }

            gc_Rout.DataSource = dt_Select;

            Search_Bom(e.Node["SItem_Code"].ToString());
        }

        //그리드
        private void gv_Rout_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            string sSIdx_No = tree_Plan.FocusedNode["SIdx_No"].NullString();
            string sRIdx_No = gv_Rout.GetRowCellValue(e.RowHandle, "Sort_No").NullString();

            if (e.Column.FieldName == "Custom_Name")
            {
                DataRow dr_Help = PopHelpForm.Return_Help_Row("sp_Help_Custom_Param", e.Value.ToString(), "", "생산,외주");
                if (dr_Help == null)
                {
                    gv_Rout.SetRowCellValue(e.RowHandle, "Custom_Code", "");
                }
                else
                {
                    gv_Rout.SetRowCellValue(e.RowHandle, "Custom_Code", dr_Help["Custom_Code"]);
                    gv_Rout.CellValueChanged -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Rout_CellValueChanged);
                    gv_Rout.SetRowCellValue(e.RowHandle, "Custom_Name", dr_Help["Short_Name"]);
                    gv_Rout.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Rout_CellValueChanged);
                }
            }

            DataTable dt_Rout_Update = gc_Rout.DataSource as DataTable;

            if(dt_Rout.Select("SIdx_No = '" + sSIdx_No + "' AND Sort_No = '" + sRIdx_No + "'").Length > 0)
            {
                DataRow dr_Update = dt_Rout.Select("SIdx_No = '" + sSIdx_No + "' AND Sort_No = '" + sRIdx_No + "'")[0];
                dr_Update[e.Column.FieldName] = gv_Rout.GetDataRow(e.RowHandle)[e.Column.FieldName];
            }
            else
            {
                DataRow dr_Add = dt_Rout.NewRow();
                dr_Add.ItemArray = gv_Rout.GetDataRow(e.RowHandle).ItemArray;

                int iIndex = dt_Rout.Rows.IndexOf(gv_Rout.GetDataRow(e.RowHandle - 1));
                dt_Rout.Rows.InsertAt(dr_Add, iIndex + 1);
            }

            btn_Close.sUpdate = "Y";
            btn_Insert.sUpdate = "Y";
        }

        private void gc_Rout_EditorKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (gv_Rout.FocusedColumn == gv_Rout.Columns["Custom_Name"])
                {
                    Custom_Help(null, null);
                }
            }
        }
        private void gc_Rout_NewRowAdd(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            int iFRow = e.RowHandle - 1;

            int iRIdx = int.Parse(gv_Rout.GetRowCellValue(iFRow, "Sort_No").ToString());

            string sSIdx_No = tree_Plan.FocusedNode["SIdx_No"].ToString();

            gv_Rout.CellValueChanged -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Rout_CellValueChanged);

            //아랫 공정 정보 업데이트
            for (int i = gv_Rout.RowCount - 1; i > e.RowHandle; i--)
            {
                int iRIdx_No = int.Parse(gv_Rout.GetRowCellValue(i, "Sort_No").ToString());

                DataRow dr_Update = dt_Rout.Select("SIdx_No = '" + sSIdx_No + "' AND Sort_No = '" + iRIdx_No.ToString() + "'")[0];
                dr_Update["Sort_No"] = (iRIdx_No + 1).ToString();
                dr_Update["RIdx_No"] = (iRIdx_No + 1).ToString();

                gv_Rout.SetRowCellValue(i, "Sort_No", (iRIdx_No + 1).ToString());
            }

            gv_Rout.SetRowCellValue(e.RowHandle, "SIdx_No", gv_Rout.GetRowCellValue(iFRow, "SIdx_No").ToString());
            gv_Rout.SetRowCellValue(e.RowHandle, "RIdx_No", iRIdx + 1);
            gv_Rout.SetRowCellValue(e.RowHandle, "Sort_No", iRIdx + 1);
            gv_Rout.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Rout_CellValueChanged);
            gv_Rout.SetRowCellValue(e.RowHandle, "Item_Code", gv_Rout.GetRowCellValue(iFRow, "Item_Code").ToString());
            gv_Rout.SetRowCellValue(e.RowHandle, "Item_Name", gv_Rout.GetRowCellValue(iFRow, "Item_Name").ToString());
            gv_Rout.SetRowCellValue(e.RowHandle, "SSize", gv_Rout.GetRowCellValue(iFRow, "SSize").ToString());
            gv_Rout.SetRowCellValue(e.RowHandle, "Process_Code", gv_Rout.GetRowCellValue(iFRow, "Process_Code").ToString());
            gv_Rout.SetRowCellValue(e.RowHandle, "Process_Name", gv_Rout.GetRowCellValue(iFRow, "Process_Name").ToString());

            gv_Rout.UpdateCurrentRow();
        }


        #endregion
    }
}