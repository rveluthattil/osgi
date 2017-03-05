using System;
using System.Windows.Forms;

namespace UIShell.PresentationCore
{
    public abstract class AbstractViewProvider : IViewProvider
    {
        private readonly bool _createViewEachTime;
        private Control _view;

        public AbstractViewProvider() : this(false)
        {
        }

        /// <summary>
        /// 实例化AbstractViewProvider对象
        /// </summary>
        /// <param name="createViewEachTime">是否每次都创建一个新的View实例</param>
        public AbstractViewProvider(bool createViewEachTime)
        {
            _createViewEachTime = createViewEachTime;
        }

        #region IViewProvider 成员

        private object _viewInfo;

        public Control View
        {
            get
            {
                if (_createViewEachTime)
                {
                    return CreateControl();
                }
                if (_view == null)
                {
                    _view = CreateControl();
                    _view.Disposed += _view_Disposed;
                }
                return _view;
            }
        }

        /// <summary>
        /// 指定如何显示View，如DockStyle等
        /// </summary>
        public virtual object ViewInfo
        {
            get
            {
                if (_viewInfo == null)
                {
                    _viewInfo = CreateViewInfo();
                }
                return _viewInfo;
            }
        }

        private void _view_Disposed(object sender, EventArgs e)
        {
        }

        protected virtual object CreateViewInfo()
        {
            return null;
        }

        #endregion

        protected abstract Control CreateControl();
    }
}