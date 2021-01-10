using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace customer_action
{
    public partial class CustomerExport : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Convert.ToBoolean(Session["AdminFlag"]))
            {
                // セッション変数をクリアする
                Session.Clear();
                // 管理者として認証されていないため、［ログオン］フォームに戻る
                Response.Redirect("Logon.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // このWebページをキャッシュしないように設定する
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        }

        protected void ExportButton_Click(object sender, EventArgs e)
        {
            // カンマ区切りの文字列を作成して、変数csvStringに割り当てる
            string csvString = MakeCSVData();

            // BOMデータの作成
            byte[] bom = new byte[3] { 0xef, 0xbb, 0xbf };
            
            // 出力する既定のファイル名
            const string csvFile = "customer.csv";
            
            // クライアントに出力するデータをクリアする
            Response.Clear();
            
            // コンテンツタイプを指定する
            Response.ContentType = "application/octet-stream";
            
            // HTTPヘッダーを指定する
            Response.AddHeader("Content-Disposition", "attachment; filename=" + csvFile);

            // BOMを出力後、UTF-8のテキストファイルで出力する
            Response.BinaryWrite(bom);
            Response.BinaryWrite(System.Text.Encoding.GetEncoding("UTF-8").GetBytes(csvString));
            
            // 出力するファイルを終了する
            Response.End();
        }

        // 出力データをカンマ区切りのテキストファイルに生成するメソッド
        private string MakeCSVData()
        {
            // CSVファイルの内容を蓄積するためのStringBuilder型の変数を定義する
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            // ヘッダー行の列名を出力する
            sb.AppendLine("\"顧客ID\",\"顧客名\",\"顧客名カナ\",\"会社名\",\"部署\",\"役職\",\"郵便番号\",\"住所\",\"電話番号\",\"担当者名\"");

            string queryString = "SELECT c.*, co.company_name, st.staff_name" +
                " FROM tbl_customer AS c LEFT OUTER JOIN" +
                " tbl_staff AS st ON c.staffID = st.staffID LEFT OUTER JOIN" +
                " tbl_company AS co ON c.companyID = co.companyID" +
                " WHERE c.delete_flag = 0";

            // 担当者が選択されているときには、WHERE句の抽出条件を追加する
            if (StaffDropDownList.SelectedValue != "-1")
            {
                queryString += " AND c.staffID=" + StaffDropDownList.SelectedValue;
            }

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
                    
                    // コマンドからデータリーダーを定義する
                    SqlDataReader reader = command.ExecuteReader();

                    // データ分だけ処理を繰り返す
                    while (reader.Read())
                    {
                        // データリーダーから読み出したデータを変数sbに追加する
                        sb.Append("\"" + ReplaceDoubleQuotes(reader["customerID"]) + "\",");
                        sb.Append("\"" + ReplaceDoubleQuotes(reader["customer_name"]) + "\",");
                        sb.Append("\"" + ReplaceDoubleQuotes(reader["customer_kana"]) + "\",");
                        sb.Append("\"" + ReplaceDoubleQuotes(reader["company_name"])  + "\",");
                        sb.Append("\"" + ReplaceDoubleQuotes(reader["section"]) + "\",");
                        sb.Append("\"" + ReplaceDoubleQuotes(reader["post"]) + "\",");
                        sb.Append("\"" + ReplaceDoubleQuotes(reader["zipcode"]) + "\",");
                        sb.Append("\"" + ReplaceDoubleQuotes(reader["address"]) + "\",");
                        sb.Append("\"" + ReplaceDoubleQuotes(reader["tel"]) + "\",");
                        sb.Append("\"" + ReplaceDoubleQuotes(reader["staff_name"]) + "\"");
                        // 改行コードを追加する
                        sb.Append("\r\n");
                    }
                }
            }
            catch (Exception)
            {
                // 何らかのエラーが発生した
            }
            // 文字列に変換して、メソッドの戻り値として返す
            return sb.ToString();
        }

        // ダブルクォーテーションの置換処理
        string ReplaceDoubleQuotes(object apdata)
        {
            string tmp = apdata.ToString();
            return tmp.Replace("\"", "\\\"");
        }
    }
}