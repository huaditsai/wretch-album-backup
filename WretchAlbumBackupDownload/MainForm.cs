using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Net;
using System.Security.Policy;
using System.Threading;

namespace WretchAlbumBackupDownload
{
    public partial class MainForm : Form
    {
        int progress = 0;

        public Thread mainThread = null; //抓圖Thread

        public MainForm()
        {
            InitializeComponent();
            btnGo.Enabled = true;
            btnStop.Enabled = false;
        }

        /// <summary>
        /// 瀏覽檔案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.ShowDialog();
            txtDetailHtmlPath.Text = file.FileName; //檔名+路徑
        }


        /// <summary>
        /// 開始下載
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGo_Click(object sender, EventArgs e)
        {
            txtLog.Text = "";
            mainThread = new Thread(new ParameterizedThreadStart((o) =>
            {
                try
                {
                    //detail.html檔路徑
                    string detailHtmlPath = txtDetailHtmlPath.Text.Trim();

                    if (File.Exists(detailHtmlPath))
                    { //detail.html檔有存在的話
                        btnGo.Invoke(new Action(() =>
                        {
                            btnGo.Enabled = false;
                        }));
                        btnStop.Invoke(new Action(() =>
                        {
                            btnStop.Enabled = true;
                        }));
                        progressBar1.Invoke(new Action(() =>
                        {
                            progressBar1.Maximum = 1;
                        }));


                        //detail.html的資料夾位置
                        string detailHtmlDir = Path.GetDirectoryName(detailHtmlPath);

                        //打開detail.html解析有哪些.txt檔
                        HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();

                        using (FileStream fs = new FileStream(detailHtmlPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                        {
                            //撈html資料進HtmlDocument物件
                            htmlDoc.Load(fs, Encoding.UTF8);
                            IEnumerable<HtmlNode> tableNodes = htmlDoc.DocumentNode.Descendants("table");
                            //儲存相簿名稱及其圖片的超連結
                            Dictionary<string, List<string>> albums = new Dictionary<string, List<string>>();
                            foreach (HtmlNode table in tableNodes)
                            {
                                //取得相簿名稱
                                string albumTitle = table.SelectSingleNode("tr[2]/td[1]").InnerText;
                                //圖片超連結儲存
                                List<string> picUrls = new List<string>();
                                //取得有.txt檔名
                                string txtFileName = table.SelectSingleNode("tr[6]/td[1]").InnerText;
                                //txt檔的路徑
                                string txtFilePath = Path.Combine(detailHtmlDir, txtFileName);
                                //檢查該.txt存在
                                if (File.Exists(txtFilePath))
                                {
                                    //打開.txt檔
                                    using (FileStream fsTxt = new FileStream(txtFilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                                    {
                                        using (StreamReader sr = new StreamReader(fsTxt))
                                        {
                                            string picUrl = string.Empty;
                                            //一列一列地讀取照片Url
                                            while ((picUrl = sr.ReadLine()) != null)
                                            {
                                                picUrls.Add(picUrl);

                                                progressBar1.Invoke(new Action(() =>
                                                {
                                                    progressBar1.Maximum ++;
                                                }));
                                            }
                                            //加入一本相簿
                                            albums.Add(albumTitle, picUrls);

                                        }
                                    }//End using
                                }//End if
                            }//End foreach

                            //有相簿資料待下載
                            if (albums.Count > 0)
                            {
                                //走訪每本相簿
                                foreach (KeyValuePair<string, List<string>> album in albums)
                                {
                                    //將相簿名稱置換成檔案總管可接受的名稱
                                    string albumTitle = this.ReplaceUrlToOK(album.Key);
                                    //相簿預計存放的位置
                                    string albumDirPath = Path.Combine(detailHtmlDir, albumTitle);
                                    if (!Directory.Exists(albumDirPath))
                                    {//建立相簿資料夾
                                        Directory.CreateDirectory(albumDirPath);
                                    }
                                    txtLog.Invoke(new Action(() =>
                                    {
                                        txtLog.AppendText("正在下載相簿：" + albumTitle + Environment.NewLine);
                                    }));

                                    //下載此相簿的圖片
                                    foreach (string picHttpUrl in album.Value)
                                    {
                                        if (picHttpUrl != "無資料")
                                        {
                                            string picFileName = Path.GetFileName(picHttpUrl).Split('?')[0];
                                            txtLog.Invoke(new Action(() =>
                                            {
                                                txtLog.AppendText("正在下載：" + albumTitle + " \\ " + picFileName + " ....");
                                            }));

                                            this.DonwloadOnePic(picHttpUrl, albumDirPath);

                                            txtLog.Invoke(new Action(() =>
                                            {
                                                txtLog.AppendText("  完成" + Environment.NewLine); //"保存至:" + albumDirPath + Environment.NewLine);
                                                progress++;
                                            }));
                                        }
                                        else
                                        {
                                            txtLog.Invoke(new Action(() =>
                                            {
                                                txtLog.AppendText("沒有圖片" + Environment.NewLine); //"保存至:" + albumDirPath + Environment.NewLine);
                                                progress++;
                                            }));
                                        }

                                        progressBar1.Invoke(new Action(() =>
                                        {
                                            progressBar1.Value = progress;
                                        }));

                                    }
                                }
                            }

                            //下載完成
                            txtLog.Invoke(new Action(() =>
                            {
                                txtLog.AppendText(Environment.NewLine + "檔案已全部下載完成" + Environment.NewLine);
                            }));
                            progressBar1.Invoke(new Action(() =>
                            {
                                progressBar1.Value = progressBar1.Maximum;
                            }));
                            btnGo.Invoke(new Action(() =>
                            {
                                btnGo.Enabled = true;

                            }));
                            btnStop.Invoke(new Action(() =>
                            {
                                btnStop.Enabled = false;
                            }));

                        }//End using

                    }//End if
                    else
                    {
                        MessageBox.Show("檔案不存在");
                    }
                }
                catch (Exception ex)
                {

                    txtLog.Invoke(new Action(() =>
                    {
                        txtLog.AppendText(ex.Message + Environment.NewLine);
                    }));

                    this.AbortMainThread();
                }
            }));
            //啟動執行緒
            mainThread.Start();

        }

        private void SynchProgressBar()
        {
            this.progressBar1.Value = progress;
        }

        /// <summary>
        /// 中止下載
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                //中止執行緒
                this.AbortMainThread();

                btnGo.Enabled = true;
                btnStop.Enabled = false;
            }
            catch (Exception ex)
            {
                // txtLog.AppendText("程式發生錯誤：" + ex.Message + Environment.NewLine);
            }
        }
        /// <summary>
        /// 中止執行緒
        /// </summary>
        private void AbortMainThread()
        {
            if (mainThread != null)
            {
                try
                {
                }
                finally
                {
                    mainThread.Abort();
                }
            }
        }


        /// <summary>
        /// 存放目錄用
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string ReplaceUrlToOK(string url)
        {
            url = url.Replace("|", "").Replace("\"", "”");
            url = url.Replace("`", "‘");//.Replace("-", "－");
            //url = url.Replace("=", "＝")

            url = url.Replace("[", "〔");
            url = url.Replace("]", "〕").Replace("\\", "＼");
            url = url.Replace(";", "；").Replace("'", "’");
            url = url.Replace(",", "，");//.Replace(".", "．");
            url = url.Replace("/", "／").Replace("~", "～");
            url = url.Replace("!", "！").Replace("@", "＠");
            url = url.Replace("#", "＃").Replace("$", "＄");
            url = url.Replace("%", "％").Replace("^", "＾");
            url = url.Replace("&", "＆").Replace("*", "＊");
            // url = url.Replace("(", "（").Replace(")", "）");
            url = url.Replace("_", "＿").Replace("+", "＋");
            url = url.Replace(":", "：").Replace("\"", "”");
            url = url.Replace("<", "＜").Replace(">", "＞");
            url = url.Replace("?", "？");
            return url;
        }
        /// <summary>
        /// 下載一張圖片
        /// </summary>
        /// <param name="PicUrl">只支援Http開頭的Url或檔名，相對路徑不算</param>
        /// <param name="SaveLocalPath"></param>
        /// <returns></returns>
        private void DonwloadOnePic(string picHttpUrl, string albumDirPath)
        {
            string picFileName = Path.GetFileName(picHttpUrl).Split('?')[0];

            //預計存放圖片位置
            string picFilePath = Path.Combine(albumDirPath, picFileName);
            ////解決檔名重覆
            //if (File.Exists(picFilePath))
            //{
            //    string newFileName = Path.GetFileNameWithoutExtension(picFileName) + "(" + Guid.NewGuid().ToString() + ")" + Path.GetExtension(picFileName);
            //    //新的存放圖檔路徑名稱
            //    picFilePath = Path.Combine(albumDirPath, newFileName);
            //}             

            try
            {
                int maxRetryCount = 5;//下載重試次數最多5次
                int retryCount = 1;
                do
                {
                    //下載
                    long[] totalSize = this.DownloadFile_WebRequest(picHttpUrl, picFilePath);

                    Thread.Sleep(100);

                    //驗證檔案是否下載完全
                    if (totalSize[0] != totalSize[1])
                    {//下載不完全
                        if (retryCount >= maxRetryCount)
                        {
                            txtLog.Invoke(new Action(() =>
                            {
                                txtLog.AppendText("錯誤" + Environment.NewLine + "檔案：" + picHttpUrl + " 已下載" + maxRetryCount + "次仍不完全，存放位置：" + picFilePath + Environment.NewLine);

                            }));

                            break;//離開迴圈
                        }
                        else
                        {
                            retryCount++;
                        }
                    }
                    else
                    {//下載完全

                        break;//離開迴圈
                    }

                } while (true);

            }
            catch (Exception ex)
            {
                txtLog.Invoke(new Action(() =>
                {
                    txtLog.AppendText("下載一張圖片發生例外(" + picHttpUrl + "。保存位置:" + picFilePath + ")：" + ex.Message + Environment.NewLine);
                }));
            }

        }


        /// <summary>
        /// 使用HttpWebRequest 下載檔案
        /// </summary>
        /// <param name="httpUrlHaveFileSpot"></param>
        /// <param name="httpFileUrl"></param>
        /// <param name="localFilePath"></param>
        /// <returns></returns>
        private long[] DownloadFile_WebRequest(string httpFileUrl, string localFilePath)
        {
            //檔案總下載大小
            long bytesProcessed = 0;
            //Web上紀錄的檔案大小
            long WebContentLength = 0;

            Stream remoteStream = null;
            Stream localStream = null;
            HttpWebResponse response = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(httpFileUrl);
                if (request != null)
                {
                    request.Method = "GET";
                    request.Accept = "text/html, application/xhtml+xml, */*";
                    request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                    request.Headers.Add("Accept-Language:zh-TW");
                    request.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; WOW64; Trident/6.0)";
                    request.KeepAlive = true;
                    request.Credentials = System.Net.CredentialCache.DefaultCredentials;

                    //發出request
                    response = (HttpWebResponse)request.GetResponse();
                    if (response != null)
                    {
                        remoteStream = response.GetResponseStream();

                        //取得Web檔案大小
                        long.TryParse(response.Headers.Get("Content-Length"), out WebContentLength);

                        localStream = new FileStream(localFilePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);

                        byte[] buffer = new byte[1024];
                        int bytesRead;

                        while ((bytesRead = remoteStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            //一點一點地抓下圖片
                            localStream.Write(buffer, 0, bytesRead);

                            //累加下載檔案大小
                            bytesProcessed += bytesRead;
                        }

                    }
                }
            }
            catch (Exception e)
            {
                txtLog.Invoke(new Action(() =>
                {
                    txtLog.AppendText("下載一張圖片發生例外(httpFileUrl:" + httpFileUrl + "。保存位置:" + localFilePath + ")：" + e.Message + Environment.NewLine);
                }));
            }
            finally
            {
                if (response != null) response.Close();
                if (remoteStream != null) remoteStream.Close();
                if (localStream != null) localStream.Close();
            }

            return new long[] { bytesProcessed, WebContentLength };
        }


    }
}
