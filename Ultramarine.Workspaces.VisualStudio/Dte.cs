﻿using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ultramarine.Workspaces.VisualStudio
{
    public class Dte
    {
        #region Ctor
        private static readonly Lazy<Dte> _instance = new Lazy<Dte>(() => new Dte());
        public static Dte Instance { get { return _instance.Value; } }

        private Dte()
        {

        }
        #endregion

        public DTE Host { get; set; }
        public async Task InitializeAsync(Microsoft.VisualStudio.Shell.IAsyncServiceProvider serviceProvider)
        {
            Host = await serviceProvider.GetServiceAsync(typeof(SDTE)) as DTE;
        }

        public Project GetProject(string projectName)
        {
            for (var i = 1; i < Host.Solution.Projects.Count; i++)
            {
                var project = Host.Solution.Projects.Item(i);
                if (project.Name == projectName)
                    return project;
            }
            return null;
        }

        public ProjectModel GetProjectModel(string projectName)
        {
            var project = GetProject(projectName);
            return project == null ? null : new ProjectModel(project);
        }
        public List<ProjectModel> GetAllProjects(bool includeSolutionFolders)
        {
            Projects projects = Host.Solution.Projects;
            var list = new List<ProjectModel>();
            var item = projects.GetEnumerator();
            while (item.MoveNext())
            {
                var project = item.Current as EnvDTE.Project;
                if (project == null)
                {
                    continue;
                }

                if (project.Kind == EnvDTE.Constants.vsProjectKindSolutionItems)
                {
                    list.AddRange(GetSolutionFolderProjects(project, includeSolutionFolders));
                }
                else
                {
                    list.Add(new ProjectModel(project));
                }
            }

            return list;
        }

        
        public IEnumerable<ProjectModel> GetSolutionFolderProjects(EnvDTE.Project solutionFolder, bool includeSolutionFolders)
        {
            var list = new List<ProjectModel>();
            if (includeSolutionFolders)
                list.Add(new ProjectModel(solutionFolder));
            for (var i = 1; i <= solutionFolder.ProjectItems.Count; i++)
            {
                var subProject = solutionFolder.ProjectItems.Item(i).SubProject;
                if (subProject == null)
                {
                    continue;
                }

                // If this is another solution folder, do a recursive call, otherwise add
                if (subProject.Kind == EnvDTE.Constants.vsProjectKindSolutionItems)
                {
                    list.AddRange(GetSolutionFolderProjects(subProject, includeSolutionFolders));
                }
                else
                {
                    list.Add(new ProjectModel(subProject));
                }
            }
            return list;
        }
    }
}
