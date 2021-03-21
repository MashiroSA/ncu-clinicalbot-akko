using System;
using System.Data.SQLite;
using ncu_clinicalbot_akko.Core;

namespace ncu_clinicalbot_akko.Extensions
{
    public class MedicalQuestionBasicExtension
    {
        /*
         * 数据库查询内容的参数
         */
        private int ID;
        private string Question;
        private string A;
        private string B;
        private string C;
        private string D;
        private string Answer;
        private string Message;
        
        public MedicalQuestionBasicExtension(){} // 构造方法，为空

        public string GetRandomMedicalQuestion()
        {
            string message = GetRandomMedicalQuestionInfoDB();
            return message;
        }
        
        public string GetMedicalQuestion(int tID)
        {
            string message = GetMedicalQuestionInfoDB(tID);
            return message;
        }
        
        public string GetMedicalAnswer(int tID)
        {
            string message = GetMedicalAnswerInfoDB(tID);
            return message;
        }
        
        private string GetRandomMedicalQuestionInfoDB() // 巡查database中的随机题目及相关信息
        {
            // 基本信息
            string dbPath = AppDomain.CurrentDomain.BaseDirectory + @"/db/";
            string dbQAFile = dbPath + @"QA.db";
            
            SQLiteConnection sqliteConn = new SQLiteConnection("data source=" + dbQAFile);
            if (sqliteConn.State != System.Data.ConnectionState.Open)
            {
                sqliteConn.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = sqliteConn;
                
                // 巡查数据库中的题目数量
                cmd.CommandText = "SELECT count(*) FROM QA";
                SQLiteDataReader c = cmd.ExecuteReader();
                c.Read();
                int totalNumber = c.GetInt32(0);
                c.Close();
                
                //生成范围随机数
                Random rand = new Random();
                int targetID = rand.Next(1, totalNumber);

                cmd.CommandText = "SELECT * FROM QA WHERE ID= 1"; // 查询数据
                
                SQLiteDataReader sr = cmd.ExecuteReader(); // 读取结果集
                while (sr.Read())
                {
                    this.ID = sr.GetInt32(0);
                    this.Question = sr.GetString(1);
                    this.A = sr.GetString(2);
                    this.B = sr.GetString(3);
                    this.C = sr.GetString(4);
                    this.D = sr.GetString(5);
                    this.Answer = sr.GetString(6);
                    this.Message =
                        $"已从数据库中获取到信息，题组：常规，模式：非答题模式，题目ID:{this.ID}\n" +
                        $"{this.Question}:\n" +
                        $"A:{this.A}\n" +
                        $"B:{this.B}\n" +
                        $"C:{this.C}\n" +
                        $"D:{this.D}\n" +
                        $"输入'查答案 id'获取相关题目的正确答案";
                }

                TraceLog.Log("", "医学题目:有人调用了随机常规题并从数据库中获取信息");
                sr.Close();
                cmd.ExecuteNonQuery();
            }
            sqliteConn.Close();

            return Message;
        }

        private string GetMedicalAnswerInfoDB(int tID)
        {
            tID = 1;
            // 基本信息
            string dbPath = AppDomain.CurrentDomain.BaseDirectory + @"/db/";
            string dbQAFile = dbPath + @"QA.db";
            
            SQLiteConnection sqliteConn = new SQLiteConnection("data source=" + dbQAFile);
            if (sqliteConn.State != System.Data.ConnectionState.Open)
            {
                sqliteConn.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = sqliteConn;
                
                // 巡查数据库中的题目数量
                cmd.CommandText = "SELECT count(*) FROM QA";
                SQLiteDataReader c = cmd.ExecuteReader();
                c.Read();
                int totalNumber = c.GetInt32(0);
                c.Close();
                
                // 确权输入的id是否合法，否则返回警告信息，todo:尚未设计异常 @Author MashiroSA
                if (tID <= 0 || tID > totalNumber)
                {
                    return "不合法的输入，ID越界";
                }
                
                // 查询目的题目的答案
                cmd.CommandText = $"SELECT * FROM QA WHERE ID= {tID}";
                
                SQLiteDataReader sr = cmd.ExecuteReader();
                while (sr.Read())
                {
                    this.ID = sr.GetInt32(0);
                    this.Answer = sr.GetString(6);
                    this.Message =
                        $"已从数据库中获取到信息，题组：常规，模式：非答题模式\n" +
                        $"题目ID:{this.ID} 的 答案为:{this.Answer}";
                }

                TraceLog.Log("", "医学题目:有人调用了查答案并从数据库中获取信息");
                sr.Close();
                cmd.ExecuteNonQuery();
            }
            sqliteConn.Close();

            return Message;
        } // 获取database中的题目答案信息
        
        private string GetMedicalQuestionInfoDB(int tID) // 巡查database中的指定题目及相关信息
        {
            // 基本信息
            string dbPath = AppDomain.CurrentDomain.BaseDirectory + @"/db/";
            string dbQAFile = dbPath + @"QA.db";
            
            SQLiteConnection sqliteConn = new SQLiteConnection("data source=" + dbQAFile);
            if (sqliteConn.State != System.Data.ConnectionState.Open)
            {
                sqliteConn.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = sqliteConn;
                
                // 巡查数据库中的题目数量
                cmd.CommandText = "SELECT count(*) FROM QA";
                SQLiteDataReader c = cmd.ExecuteReader();
                c.Read();
                int totalNumber = c.GetInt32(0);
                c.Close();
                
                // 确权输入的id是否合法，否则返回警告信息，todo:尚未设计异常 @Author MashiroSA
                if (tID <= 0 || tID > totalNumber)
                {
                    return "不合法的输入，ID越界";
                }
                else
                {
                    cmd.CommandText = "SELECT * FROM QA WHERE ID= 1";
                
                    SQLiteDataReader sr = cmd.ExecuteReader();
                    while (sr.Read())
                    {
                        this.ID = sr.GetInt32(0);
                        this.Question = sr.GetString(1);
                        this.A = sr.GetString(2);
                        this.B = sr.GetString(3);
                        this.C = sr.GetString(4);
                        this.D = sr.GetString(5);
                        this.Answer = sr.GetString(6);
                        this.Message =
                            $"已从数据库中获取到信息，题组：常规，模式：非答题模式（查询模式），题目ID:{this.ID}\n" +
                            $"{this.Question}:\n" +
                            $"A:{this.A}\n" +
                            $"B:{this.B}\n" +
                            $"C:{this.C}\n" +
                            $"D:{this.D}\n" +
                            $"输入'查答案 id'获取相关题目的正确答案";
                    }
                    
                    TraceLog.Log("", "医学题目:有人调用了查题目并从数据库中获取信息");
                    sr.Close();
                }
                cmd.ExecuteNonQuery();
            }
            sqliteConn.Close();

            return Message;
        }
    }
}