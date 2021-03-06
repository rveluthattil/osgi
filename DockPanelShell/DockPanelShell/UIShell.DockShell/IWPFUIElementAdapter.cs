﻿//===============================================================================
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

namespace DockShell
{
    /// <summary>
    /// Service that adapts WPF <see cref="UIElement"/>s objects to Windows Forms <see cref="System.Windows.Forms.Control"/>s.
    /// </summary>
    /// <remarks>
    /// This service is used by the <see cref="ElementHostWorkspace{TSmartPart, TSmartPartInfo}"/> to manage WPF Controls as Windows Forms Controls.
    /// </remarks>
    public interface IWPFUIElementAdapter
    {
        /// <summary>
        /// Gets the <see cref="UIElement"/> wrapped by the ElementHost <paramref name="control" />.
        /// </summary>
        /// <returns>The <see cref="UIElement"/> object wrapped by the control.
        /// <para>
        /// -or-
        /// </para>
        /// <see langword="null" /> if <paramref name="control" /> is not an ElementHost generated by the Wrap method.</returns>
        UIElement Unwrap(Control control);

        /// <summary>
        /// Gets an <see cref="System.Windows.Forms.Integration.ElementHost"/> control that wraps the <paramref name="smartPart"/> object.
        /// </summary>
        /// <remarks>
        /// If the method is called more than once using the same <paramref name="smartPart"/> instance, 
        /// the same <see cref="System.Windows.Forms.Integration.ElementHost"/> control instance will be returned.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="smartPart" /> is <see langword="null" />.</exception>
        Control Wrap(UIElement smartPart);
    }
}