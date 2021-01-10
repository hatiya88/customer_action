using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace customer_action
{
    public partial class CustomerCard : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["StaffID"] == null)
            {
                // ユーザーが認証されていないため、［ログオン］フォームに戻る
                Response.Redirect("Logon.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // このWebページをキャッシュしないように設定する
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            // ポストバックかどうか判定する（他のWebページから遷移してきたかどうか）
            if (!IsPostBack)
            {
                // 他のフォームから遷移してきたとき
                if (Request.QueryString["id"] == null)
                {
                    // URLにクエリ文字列idが含まれていないときには
                    // フォームビューのデフォルトモードを挿入モードにする
                    FormView1.DefaultMode = FormViewMode.Insert;
                }
            }
        }

        protected void FormView1_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            // 最終更新日時と最終更新者をセットする
            e.NewValues["update_date"] = DateTime.Now;
            e.NewValues["update_staff_name"] = Session["StaffName"];
        }

        protected void FormView1_ItemInserting(object sender, FormViewInsertEventArgs e)
        {
            // 顧客IDを取得する(最大値＋１)
            int customerID = GetNewID();

            if (customerID != -1)
            {
                // 顧客IDが取得できたため、顧客IDをセットする
                e.Values["customerID"] = customerID;
            
                // 初回登録日時と初回登録者をセットする
                e.Values["input_date"] = DateTime.Now;
                e.Values["input_staff_name"] = Session["StaffName"];
                
                // 最終更新日時と最終更新者をセットする
                e.Values["update_date"] = DateTime.Now;
                e.Values["update_staff_name"] = Session["StaffName"];
            }
            else
            {
                // 顧客IDの取得に失敗したため、処理をキャンセルする
                e.Cancel = true;
            }
        }

        private int GetNewID()
        {
            // 戻り値用の変数を定義する（-1は失敗したときの数値として設定）
            int ret = -1;

            try
            {
                // 接続文字列を取得する
                string connectionString = System.Configuration.ConfigurationManager.
                  ConnectionStrings["customer_actionConnectionString"].ConnectionString;
                
                // コネクションを定義する
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // SQLステートメントを定義する（現在の顧客IDの最大値＋１を取得）
                    string queryString = "SELECT ISNULL(MAX(customerID),0)+1 FROM tbl_customer";
                
                    // コマンドを定義する
                    SqlCommand command = new SqlCommand(queryString, connection);
                    
                    // コネクションを開く
                    connection.Open();
                    
                    // SQLステートメントの実行結果を取得する
                    Object result = command.ExecuteScalar();
                    
                    // 結果を正しく取得できたときには、戻り値を設定する
                    if (result != null)
                    {
                        ret = (int)result;
                    }
                }
            }
            catch (Exception)
            {
                // 何らかのエラーが発生した
            }
            
            // 新規データ用の顧客IDを返す
            return ret;
        }

        protected void FormView1_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            // 実行するコマンド名と現在のモードを判定する
            if (e.CommandName == "Cancel" && FormView1.CurrentMode == FormViewMode.Insert)
            {
                // コマンド名が「Cancel」で挿入モードのときにはCustomerListフォームに戻る
                Response.Redirect("CustomerList.aspx");
            }
        }

        protected void NewActionLinkButton_Click(object sender, EventArgs e)
        {
            // 新しいIDを取得する（最大値+1）
            int newID = GetNewActionID();

            if (newID == -1)
            {
                // IDの取得に失敗したとき
                MessageLabel.Text = "IDの取得に失敗しました。データベースを確認してください。";
                return;
            }

            // 新規行挿入用のSQLステートメントを定義する
            string queryString = "INSERT INTO tbl_action" + 
              " (ID, customerID, action_date, action_content, action_staffID) " + 
              " VALUES (" + newID + "," + Request.QueryString["id"] + ",'" + DateTime.Today + 
              "'," + "'★新規営業報告データ★'," + Session["StaffID"] + ")";

            try
            {
                // 接続文字列を取得する
                string connectionString = System.Configuration.ConfigurationManager.
                  ConnectionStrings["customer_actionConnectionString"].ConnectionString;

                // コネクションを定義する
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // コマンドを定義する
                    SqlCommand command = new SqlCommand(queryString, connection);

                    // コネクションを開く
                    connection.Open();

                    // コマンドに定義したSQLステートメントを実行する
                    command.ExecuteNonQuery();

                    // グリッドビューを再バインドしてデータを読み込み直す
                    ActionListGridView.DataBind();

                    // 結果のメッセージを表示する
                    MessageLabel.Text = "新しいデータを追加しました。";
                }
            }
            catch (Exception ex)
            {
                // エラーが発生したとき
                MessageLabel.Text = "エラーが発生したため、処理を中止します。<br />" + ex.Message;
            }
        }

        private int GetNewActionID()
        {
            // 戻り値用の変数を定義する（-1は失敗したときの値として設定）
            int ret = -1;
            try
            {
                // 接続文字列を取得する
                string connectionString = System.Configuration.ConfigurationManager.
                  ConnectionStrings["customer_actionConnectionString"].ConnectionString;

                // コネクションを定義する
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // SQLステートメントを定義する（現在のIDの最大値+1を取得）
                    string queryString = "SELECT ISNULL(MAX(ID), 0)+1 FROM tbl_action";

                    // コマンドを定義する
                    SqlCommand command = new SqlCommand(queryString, connection);

                    // コネクションを開く
                    connection.Open();

                    // SQLステートメントの実行結果を取得する
                    Object result = command.ExecuteScalar();

                    // 結果を正しく取得できたときには、戻り値を設定する
                    if (result != null)
                    {
                        ret = Convert.ToInt32(result.ToString());
                    }
                }
            }
            catch (Exception)
            {
                // 何らかのエラーが発生した
            }
            // 新規のIDを返す
            return ret;
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            // 日付の判定のカスタムエラー
           args.IsValid = false;

            DateTime d;
             if (DateTime.TryParse((string)args.Value, out d))
             {
                 if (d.Year >= 1753 && d.Year <= 9999)
                 {
                     args.IsValid = true;
                 }
             }
        }

        protected void FormView1_PreRender(object sender, EventArgs e)
        {
            // ［新しい営業報告を登録する］リンクと営業報告履歴のグリッドビュー設定
            // 閲覧モードで対象データがあるときにのみ表示する
            if (FormView1.CurrentMode == FormViewMode.ReadOnly && FormView1.DataItemCount == 1)
            {
                ActionListTitleLabel.Visible = true;
                NewActionLinkButton.Visible = true;
                ActionListGridView.Visible = true;
            }
            else
            {
                ActionListTitleLabel.Visible = false;
                NewActionLinkButton.Visible = false;
                ActionListGridView.Visible = false;
            }
        }
    }
}