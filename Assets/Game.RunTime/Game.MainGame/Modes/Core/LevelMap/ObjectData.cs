


using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace Game.MainGame
{
    [System.Serializable]
    public struct ObjectResultData
    {
        [JsonProperty("i")] public int id;
        [JsonProperty("b")] public int blockID;
        [JsonProperty("p")] public Vector2 position;
        [JsonProperty("r")] public Vector3 rotation;

        public bool IsValid()
        {
            return id!=0;
        }
    }

    [System.Serializable]
    public struct ObjectData
    {
        [JsonProperty("i")] public int id;
        [JsonProperty("on")] public string objectName;
        [JsonProperty("p")] public Vector3 position;
        [JsonProperty("r")] public Vector3 rotation;
        [JsonProperty("s")] public Vector3 scale;
        [JsonProperty("pr")] public PropertyData properties;
        [JsonProperty("o")] public List<ObjectData> objects;


        public ObjectData(int id,string objectName, Vector3 position, Vector3 rotation, Vector3 scale,PropertyData properties, List<ObjectData> objects)
        {
            this.id = id;
            this.objectName = objectName;
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
            this.properties = properties;
            this.objects = objects;
        }
    }
    [System.Serializable]
    public struct PropertyData
    {
        [JsonProperty("p")] public List< Property> properties ;


        public void Set(string key, string value)
        {
            if (Contains(key))
            {
                throw new System.Exception($"Already contained this key {key}");
            }
            else
            {
                properties.Add(new Property(key, value));
            }
        }

        public string Get(string key, string defaultValue ="")
        {
            if (Contains(key))
            {
                return Get(key).value;
            }
            return defaultValue;
        }
        public bool Contains(string key)
        {
            foreach(var prop in properties)
            {
                if (prop.key.Equals(key))
                {
                    return true;
                }
            }
            return false;
        }
        Property Get(string key)
        {
            foreach (var prop in properties)
            {
                if (prop.key.Equals(key))
                {
                    return prop;
                }
            }
            return default(Property);
        }


        [System.Serializable]
        public struct Property
        {
            public string key;
            public string value;

            public Property(string key, string value)
            {
                this.key = key;
                this.value = value;
            }
        }
    }
}
