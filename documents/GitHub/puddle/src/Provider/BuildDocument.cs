using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DynamicRest;

namespace IsolatedStorage.Provider
{
    public class BuildDocument
    {
        private readonly RestOperation _response;

        public BuildDocument(RestOperation response)
        {
            _response = response;
        }

        public static implicit operator Document(BuildDocument factory)
        {
            return factory.Build();
        }

        private Document Build()
        {
            var document = new Document(
                _response.Result.description,
                _response.Result.title,
                DateTime.Parse(_response.Result.created),
                DateTime.Parse(_response.Result.updated));
            return document;
        }
    }
}
