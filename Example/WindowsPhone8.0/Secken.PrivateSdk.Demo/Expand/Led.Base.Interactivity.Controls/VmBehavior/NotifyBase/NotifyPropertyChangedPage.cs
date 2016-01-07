using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace Led.Base.VmBehavior.NotifyBase
{
    public class NotifyPropertyChangedPage : PhoneApplicationPage, System.ComponentModel.INotifyPropertyChanged, IDisposable
    {
        public bool IsBackMain = true;
        /// <summary>
        /// 离开页面时进行的资源清理操作
        /// </summary>
        public virtual void Dispose()
        {

        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                Dispose();
                GC.Collect();
            }
            base.OnNavigatedFrom(e);
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (NavigationService.BackStack != null && !NavigationService.BackStack.Any())
                {
                    if (IsBackMain)
                    {
                        e.Cancel = true;
                        
                    }
                }
            }
            catch { }
            base.OnBackKeyPress(e);
        }

        public NotifyPropertyChangedPage()
        {
            DataContext = this;
        }

        #region NotifyBase
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        // 使用CallerMemberNameAttribute可以获得调用这个方法的成员名称，对于属性的set来说，就是属性名
        public void NotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
