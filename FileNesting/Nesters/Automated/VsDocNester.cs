﻿using EnvDTE;
using Microsoft.VisualStudio.Utilities;
using System;
using System.ComponentModel.Composition;

namespace MadsKristensen.FileNesting
{
    [Export(typeof(IFileNester))]
    [Name("VsDoc Nester")]
    [Order(After = "Known File Type Nester")]
    internal class VsDocNester : IFileNester
    {
        public NestingResult Nest(string fileName)
        {
            if (!fileName.EndsWith("-vsdoc.js", StringComparison.OrdinalIgnoreCase))
                return NestingResult.Continue;

            string parent = fileName.Replace("-vsdoc.js", ".js");
            ProjectItem item = FileNestingPackage.DTE.Solution.FindProjectItem(parent);

            if (item != null)
            {
                item.ProjectItems.AddFromFile(fileName);
                return NestingResult.StopProcessing;
            }

            return NestingResult.Continue;
        }


        public bool IsEnabled()
        {
            return FileNestingPackage.Options.EnableVsDocRule;
        }
    }
}
