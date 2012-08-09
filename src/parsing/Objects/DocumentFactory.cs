using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using DynamicRest;
using Puddle.parsing.Objects.Entities;

namespace Puddle.parsing.Objects
{
    internal class DocumentFactory
    {
        private readonly RestOperation _response;

        public DocumentFactory(RestOperation response)
        {
            _response = response;
        }

        public static implicit operator Document(DocumentFactory factory)
        {
            return factory.Build();
        }

        private Document Build()
        {
            Document document;
            try
            {

                document = new Document(_response.Result.title, DateTime.Parse(_response.Result.created),
                                        DateTime.Parse(_response.Result.updated), BuildLinks(_response.Result.link), _response.Result.description, BuildActors(),
                                        Int32.Parse(_response.Result.size), _response.Result.version);
            }
            catch (XmlException ex)
            {
                return null;
            }
            return document;
        }

        
        private IEnumerable<Links> BuildLinks(dynamic result)
        {
            foreach(dynamic li in result)
            {
                yield return new Links(li.rel, li.href);
            }
        }

        private IEnumerable<Actor> BuildActors()
        {
            foreach (dynamic ac in _response.Result.actor)
            {
                yield return new Actor(ac.name,ac.email,ac.rel, BuildLinks(ac.link));
            }
        }
    }
}                                 