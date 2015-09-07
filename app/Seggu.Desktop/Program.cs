﻿using Seggu.Desktop.Forms;
using Seggu.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Seggu.Desktop
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var form = (Layout)DependencyResolver.Instance.Resolve(typeof(Layout));
            Application.Run(form);
            //nnnnnnn
        }
    }
}
