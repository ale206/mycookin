using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using System.Collections;
using System.Web.Script.Serialization;
using System.Collections.ObjectModel;

namespace MyCookin.Common
{
    public class JsonDeserialization
    {
        #region CompleteJsonDeserializationExample
        //http: //www.drowningintechnicaldebt.com/ShawnWeisfeld/archive/2010/08/22/using-c-4.0-and-dynamic-to-parse-json.aspx

        //string json = "{ " +
        //                   " \"glossary\": { " +
        //                   "     \"title\": \"example glossary\", " +
        //                   "     \"GlossDiv\": { " +
        //                   "         \"title\": \"S\", " +
        //                   "         \"GlossList\": { " +
        //                   "             \"GlossEntry\": { " +
        //                   "                 \"ID\": \"SGML\", " +
        //                   "                 \"SortAs\": \"SGML\", " +
        //                   "                 \"GlossTerm\": \"Standard Generalized Markup Language\", " +
        //                   "                 \"Acronym\": \"SGML\", " +
        //                   "                 \"Abbrev\": \"ISO 8879:1986\", " +
        //                   "                 \"GlossDef\": { " +
        //                   "                     \"para\": \"A meta-markup language, used to create markup languages such as DocBook.\", " +
        //                   "                     \"GlossSeeAlso\": [\"GML\", \"XML\"] " +
        //                   "                 }, " +
        //                   "                 \"GlossSee\": \"markup\" " +
        //                   "             } " +
        //                   "         } " +
        //                   "     } " +
        //                   " } " +
        //                   "}";

        //dynamic glossaryEntry = jss.Deserialize(json1, typeof(object)) as dynamic;

        //string Title = "glossaryEntry.glossary.title: " + glossaryEntry.glossary.title;
        //string Title2 = "glossaryEntry.glossary.GlossDiv.title: " + glossaryEntry.glossary.GlossDiv.title;
        //string ID = "glossaryEntry.glossary.GlossDiv.GlossList.GlossEntry.ID: " + glossaryEntry.glossary.GlossDiv.GlossList.GlossEntry.ID;
        //string Para = "glossaryEntry.glossary.GlossDiv.GlossList.GlossEntry.GlossDef.para: " + glossaryEntry.glossary.GlossDiv.GlossList.GlossEntry.GlossDef.para;

        //string GlossSeeAlso = "";
        //foreach (var also in glossaryEntry.glossary.GlossDiv.GlossList.GlossEntry.GlossDef.GlossSeeAlso)
        //{
        //    GlossSeeAlso += "glossaryEntry.glossary.GlossDiv.GlossList.GlossEntry.GlossDef.GlossSeeAlso: " + also;
        //}
        #endregion
    }

    public class DynamicJsonObject : DynamicObject
    {
        private IDictionary<string, object> Dictionary { get; set; }

        public DynamicJsonObject(IDictionary<string, object> dictionary)
        {
            this.Dictionary = dictionary;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = this.Dictionary[binder.Name];

            if (result is IDictionary<string, object>)
            {
                result = new DynamicJsonObject(result as IDictionary<string, object>);
            }
            else if (result is ArrayList && (result as ArrayList) is IDictionary<string, object>)
            {
                result = new List<DynamicJsonObject>((result as ArrayList).ToArray().Select(x => new DynamicJsonObject(x as IDictionary<string, object>)));
            }
            else if (result is ArrayList)
            {
                result = new List<object>((result as ArrayList).ToArray());
            }

            return this.Dictionary.ContainsKey(binder.Name);
        }
    }

    public class DynamicJsonConverter : JavaScriptConverter
    {
        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");

            if (type == typeof(object))
            {
                return new DynamicJsonObject(dictionary);
            }

            return null;
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get { return new ReadOnlyCollection<Type>(new List<Type>(new Type[] { typeof(object) })); }
        }
    }
}
