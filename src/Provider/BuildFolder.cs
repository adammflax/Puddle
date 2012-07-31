using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DynamicRest;

namespace IsolatedStorage.Provider
{
    public class BuildFolder
    {
        private readonly RestOperation _response;

        public BuildFolder(RestOperation response)
        {
            _response = response;
        }

        public static implicit operator Folder(BuildFolder factory)
        {
            return factory.Build();
        }

        private Folder Build()
        {
            var folder = new Folder(
                _response.Result.description,
                _response.Result.title,
                DateTime.Parse(_response.Result.created),
                DateTime.Parse(_response.Result.updated));
            return folder;
        }
    }
}
