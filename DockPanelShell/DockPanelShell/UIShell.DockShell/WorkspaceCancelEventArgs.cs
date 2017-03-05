//===============================================================================
// Microsoft patterns & practices
// CompositeUI Application Block
//===============================================================================
// Copyright ?Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

namespace DockShell
{
    /// <summary>
    /// Provides data for the cancellable events exposed by 
    /// <see cref="IWorkspace"/>.
    /// </summary>
    public class WorkspaceCancelEventArgs : WorkspaceEventArgs
    {
        /// <summary>
        /// Initializes a new <see cref="WorkspaceCancelEventArgs"/> using
        /// the specified SmartPart.
        /// </summary>
        /// <param name="smartPart"></param>
        public WorkspaceCancelEventArgs(object smartPart)
            : base(smartPart)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether the event should be canceled.
        /// </summary>
        public bool Cancel { get; set; }
    }
}