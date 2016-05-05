using DLog.IService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Windows.Forms;
using DLog.Entity.CommonBO;
using DLog.Entity;
using Tmac.Frameworks.Log;
using Tmac.Frameworks.Common.Extends;

namespace DLog.Tools
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.ResetColor();
            Console.WriteLine("开始初始化....");

            var commonConfig = new CommonConfigBO
            {
                ErrorLog = new CommonConfigBase
                {
                    IsEnabled = true,
                    MaxReceiveCount = 100,
                    MaxPostCount = 50,
                    InsertCycleTime = 500
                },
                DebugLog = new CommonConfigBase 
                {
                    IsEnabled = true,
                    MaxReceiveCount = 100,
                    MaxPostCount = 50,
                    InsertCycleTime = 500
                },
                PerfLog = new PerfLogConfig 
                {
                    IsEnabled = true,
                    MaxReceiveCount = 100,
                    MaxPostCount = 50,
                    InsertCycleTime = 500,
                    Duration= 10
                }
            };

            using (var factory = new ChannelFactory<IDLogCommonService>("*"))
            {
                var client = factory.CreateChannel();
                var result= client.InitCommonConfig(commonConfig);
            }

            Console.WriteLine("初始化完成!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var list = new List<ErrorLogInfo> 
            {
                new ErrorLogInfo
                {
                    Message= "message1",
                    Detail= "detail1",
                    SystemCode= "DLog",
                    Source= "Offline.Site"
                },
                new ErrorLogInfo
                {
                    Message= "message2",
                    Detail= "detail2",
                    SystemCode= "DLog",
                    Source= "Offline.Site"
                }
            };

            var result = list.ToJson();
        }

        /// <summary>
        /// 刷新SearchInfo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            Console.ResetColor();
            Console.WriteLine("开始刷新searchinfo....");

            using (var factory = new ChannelFactory<IDLogCommonService>("*"))
            {
                var client = factory.CreateChannel();
                var result = client.RefreshSearchInfo();
            }

            Console.WriteLine("刷新完成!");
        }
    }
}
