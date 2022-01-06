﻿using Magitui.Extensions;
using Octokit;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json;

namespace Magitui.Services.File
{
    public class FileServiceBase
    {
        protected GitHubClient _gitHubClient;
        protected string _repoName;
        protected string _branchName;
        protected string _gitHubUserName;
        protected string _personalAccessToken;
        protected string _appName;
        protected string _savingsDataFile;
        protected IRepositoryContentsClient _repoContent;


        protected async Task<RepositoryContent> GetFileAsync(string fileName)
        {
            var contentsByRef = await _repoContent.GetAllContentsByRef(_gitHubUserName, _repoName, _branchName);
            return contentsByRef.FirstOrDefault(x => x.Name == fileName);
        }

        protected async Task UpdateFileAsync<T>(T item, string dataFile, FileOperation operation) where T : IHaveGuidId
        {
            try
            {
                var file = await GetFileAsync(dataFile);
                var json = JsonSerializer.Serialize(item);
                var commitMessage = $"commit-{DateTime.Now.ToUniversalTime().ToString(CultureInfo.InvariantCulture)}";
                if (file == null && operation == FileOperation.Add)
                {
                    var createFileRequest = new CreateFileRequest(commitMessage, $"[{json}]", _branchName);
                    await _repoContent.CreateFile(_gitHubUserName, _repoName, _savingsDataFile, createFileRequest);
                }
                else
                {
                    var _ = await _repoContent.GetAllContentsByRef(_gitHubUserName, _repoName, _savingsDataFile, _branchName);
                    var content = _.FirstOrDefault()?.Content;
                    if (content == null) return;
                    var listOfType = content.ConvertToType<List<T>>();
                    if (listOfType == null) return;

                    if (operation == FileOperation.Add)
                    {
                        listOfType?.Add(item);
                        json = listOfType.ConvertToJson();
                    }
                    else if (operation == FileOperation.Delete)
                    {
                        var itemToDelete = listOfType.FirstOrDefault(x => x.Id == item.Id);
                        if (itemToDelete != null) listOfType.Remove(itemToDelete);
                        json = listOfType.ConvertToJson();
                    }
                    else if (operation == FileOperation.Edit)
                    {
                        var itemToDelete = listOfType.FirstOrDefault(x => x.Id == item.Id);
                        if (itemToDelete != null) listOfType.Remove(itemToDelete);
                        listOfType?.Add(item);
                        json = listOfType.ConvertToJson();
                    }

                    var updateFileRequest = new UpdateFileRequest(commitMessage, json, file.Sha, _branchName);
                    await _repoContent.UpdateFile(_gitHubUserName, _repoName, _savingsDataFile, updateFileRequest);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                throw;
            }
        }

    }

    public enum FileOperation
    {
        Add = 1,
        Edit = 2,
        Delete = 3,
    }

}