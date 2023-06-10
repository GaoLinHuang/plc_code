using PipettingCode.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Base;

namespace PipettingCode.ViewModel
{
    // 单例模式
    public class CheckViewModel : NotifyBase
    {
        private static CheckViewModel _instance = new();

        #region 选中的一项

        private int selectedIndex;

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                RaisePropertyChanged("SelectedIndex");
                if (SelectedIndex != -1 && MyCheck.Count > SelectedIndex)
                {
                    this.Numbers = this.MyCheck[this.SelectedIndex].Numbers;
                }
            }
        }

        #endregion 选中的一项

        #region 质控选项，创建即选中

        private ObservableCollection<CheckModel> _myCheck;

        public ObservableCollection<CheckModel> MyCheck
        {
            get { return _myCheck; }
            set
            {
                _myCheck = value;
                RaisePropertyChanged("MyCheck");
                Update();
            }
        }

        #endregion 质控选项，创建即选中

        #region 勾选的质控，去掉重复的

        public HashSet<int> SelectedItems { get; set; }

        #endregion 勾选的质控，去掉重复的

        #region 当前选择的质控是哪一个

        private string _numbers;

        public string Numbers
        {
            get { return _numbers; }
            set
            {
                _numbers = value;
                RaisePropertyChanged("Numbers");
                if (Numbers == null || Numbers == "")
                {
                    this.CheckSave = false;
                }
                else
                {
                    this.CheckSave = true;
                }
            }
        }

        #endregion 当前选择的质控是哪一个

        #region 当前保存按钮是否可以按

        private bool _CheckSave;

        public bool CheckSave
        {
            get { return _CheckSave; }
            set
            {
                _CheckSave = value;
                RaisePropertyChanged("CheckSave");
            }
        }

        #endregion 当前保存按钮是否可以按

        #region 修改、删除按钮是否能按

        private bool _checkDelete = false;

        public bool CheckDelete
        {
            get { return _checkDelete; }
            set
            {
                _checkDelete = value;
                RaisePropertyChanged("CheckDelete");
            }
        }

        #endregion 修改、删除按钮是否能按

        #region 新建按钮是否可按

        private bool _CheckNew;

        public bool CheckNew
        {
            get { return _CheckNew; }
            set
            {
                _CheckNew = value;
                RaisePropertyChanged("CheckNew");
            }
        }

        #endregion 新建按钮是否可按

        #region 命令

        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }
        public DelegateCommand ModifyCommand { get; set; }

        #endregion 命令

        #region 更新质控

        private void Update()
        {
            //#region 能否开始实验、能否删除、修改
            //if (MainWindowViewModel.GetInstance().TabIndex == 0)
            //{
            //    MainWindowViewModel.GetInstance().CanStartExperiment = ShiGuanViewModel.GetInstance().MyShiGuan.Count > 0
            //        && ShiJiViewModel.GetInstance().MyShiJi.Count > 0 && this.MyCheck.Count > 0;
            //}
            //this.CheckDelete = this.MyCheck.Count > 0;
            //#endregion

            //#region 步骤三显示的内容
            //if (this.MyCheck.Count > 0)
            //{
            //    ExperimentRightContentViewModel.GetInstance().Step3Content = new Step3Content();
            //}
            //else
            //{
            //    ExperimentRightContentViewModel.GetInstance().Step3Content = new Step3ContentOri();
            //}
            //#endregion

            //#region 选中的质控
            //if (this.SelectedItems == null)
            //{
            //    this.SelectedItems = new();
            //}
            //this.SelectedItems.Clear();
            //foreach (CheckModel checkModel in this.MyCheck)
            //{
            //    this.SelectedItems.Add(int.Parse(checkModel.Numbers) - 1);      // 记得-1
            //}
            //this.CheckNew = this.SelectedItems.Count < 10 ? true : false;
            //#endregion
        }

        #endregion 更新质控

        #region 保存质控

        private void Save(object parameter)
        {
            #region 输入的质控是否有误

            try
            {
                int n = int.Parse(this.Numbers);
                if (n < 1 || n > 96)
                {
                    throw new Exception("质控参数只可输入1至96之间的整数！");
                }
                // 质控已经存在
                if (this.SelectedItems.Contains(n - 1))
                {
                    throw new Exception("质控已存在！");
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (!ex.Message.Contains("质控"))
                {
                    msg = "质控参数只可输入1至96之间的整数！";
                }
                //new UserPasswordNoneErrorWindow("输入质控错误", msg).ShowDialog();
                MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());     // 保存错误日志
                return;
            }

            #endregion 输入的质控是否有误

            #region 添加质控

            this.MyCheck.Add(new CheckModel()
            {
                Numbers = Numbers
            });
            this.SelectedIndex = this.MyCheck.Count - 1;
            Update();

            #endregion 添加质控

            #region 保存配置到文件

            //MyConfig.SaveConfigFile();

            #endregion 保存配置到文件
        }

        #endregion 保存质控

        #region 删除质控

        private void Delete(object parameter)
        {
            try
            {
                this.MyCheck.Remove(this.MyCheck[this.SelectedIndex]);
            }
            catch (Exception ex)
            {
                MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());     // 保存错误日志
            }
            if (this.MyCheck.Count == 0)
            {
                this.SelectedIndex = -1;
            }
            else
            {
                this.SelectedIndex = 0;
            }
            Update();
            // 保存配置
            //MyConfig.SaveConfigFile();
        }

        #endregion 删除质控

        #region 修改质控

        private void Modify(object parameter)
        {
            #region 输入的质控是否有误

            try
            {
                int n = int.Parse(this.Numbers);
                if (n < 1 || n > 96)
                {
                    throw new Exception("质控参数只可输入1至96之间的整数！");
                }
                // 质控已经存在
                if (this.SelectedItems.Contains(n - 1))
                {
                    throw new Exception("质控已存在！");
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (!ex.Message.Contains("质控"))
                {
                    msg = "质控参数只可输入1至96之间的整数！";
                }
                //new UserPasswordNoneErrorWindow("输入质控错误", msg).ShowDialog();
                MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());     // 保存错误日志
                return;
            }

            #endregion 输入的质控是否有误

            int tmp = this.SelectedIndex;

            try
            {
                this.MyCheck[this.SelectedIndex] = new CheckModel()
                {
                    Numbers = Numbers
                };
            }
            catch (Exception ex)
            {
                MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());     // 保存错误日志
            }
            this.SelectedIndex = tmp;
            Update();
            // 保存配置
            //MyConfig.SaveConfigFile();
        }

        #endregion 修改质控

        #region 构造函数

        private CheckViewModel()
        {
            this.SaveCommand = new DelegateCommand();
            this.SaveCommand.ExecuteAction = new Action<object>(this.Save);

            this.DeleteCommand = new DelegateCommand();
            this.DeleteCommand.ExecuteAction = new Action<object>(this.Delete);

            this.ModifyCommand = new DelegateCommand();
            this.ModifyCommand.ExecuteAction = new Action<object>(this.Modify);

            MyCheck = new ObservableCollection<CheckModel>();
            this.SelectedItems = new();
        }

        #endregion 构造函数

        public static CheckViewModel GetInstance()
        {
            return _instance;
        }
    }
}