//===============================================================================
// Microsoft patterns & practices
// Smart Client Software Factory
//===============================================================================
// Copyright  Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===============================================================================

using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using Size = System.Drawing.Size;

namespace DockShell
{
    /// <summary>
    /// Default implementation of the <see cref="IWPFUIElementAdapter"/>
    /// </summary>
    public class WPFUIElementAdapter : IWPFUIElementAdapter
    {
        private readonly WeakDictionary<UIElement, ElementHost> _hosts = new WeakDictionary<UIElement, ElementHost>();

        #region IWPFUIElementAdapter Members

        /// <summary>
        /// See <see cref="IWPFUIElementAdapter.Unwrap"/> for more information.
        /// </summary>
        public UIElement Unwrap(Control control)
        {
            if (control is ElementHost)
            {
                if (_hosts.ContainsValue((ElementHost) control))
                {
                    return ((ElementHost) control).Child;
                }
            }
            return null;
        }

        /// <summary>
        /// See <see cref="IWPFUIElementAdapter.Wrap"/> for more information.
        /// </summary>
        public Control Wrap(UIElement smartPart)
        {
            if (smartPart == null)
                throw new ArgumentNullException("smartPart cannot be null");

            if (!_hosts.ContainsKey(smartPart))
            {
                var host = new ElementHost();
                if (smartPart is FrameworkElement)
                {
                    var typedSmartPart = (FrameworkElement) smartPart;
                    host.Size = new Size((int) typedSmartPart.Width, (int) typedSmartPart.Height);
                }
                host.Child = smartPart;

                host.ParentChanged += HostParentChanged;
                host.VisibleChanged += HostVisibleChanged;
                host.Disposed += HostDisposed;

                _hosts.Add(smartPart, host);
            }

            return _hosts[smartPart];
        }

        #endregion

        private void HostDisposed(object sender, EventArgs e)
        {
            var host = sender as ElementHost;
            if (host != null)
            {
                _hosts.Remove(host.Child);
            }
        }

        private void HostParentChanged(object sender, EventArgs e)
        {
            var host = sender as ElementHost;
            if (host != null && host.Parent != null)
            {
                host.Parent.VisibleChanged += ParentVisibleChanged;
            }
        }

        private void ParentVisibleChanged(object sender, EventArgs e)
        {
            var parent = sender as Control;
            if (parent != null)
            {
                ElementHost host = GetElementHostChild(parent);
                if (host != null)
                {
                    host.Visible = parent.Visible;
                    // This action is not called when Hide method in host is called.
                    //Patching
                    HostVisibleChanged(host, e);
                }
            }
        }

        private void HostVisibleChanged(object sender, EventArgs e)
        {
            var host = sender as ElementHost;
            if (host != null)
            {
                host.Child.Visibility = host.Visible ? Visibility.Visible : Visibility.Hidden;
            }
        }

        private ElementHost GetElementHostChild(Control c)
        {
            for (int i = 0; i < c.Controls.Count; i++)
            {
                if (c.Controls[i] is ElementHost)
                    return c.Controls[i] as ElementHost;
            }
            return null;
        }
    }
}