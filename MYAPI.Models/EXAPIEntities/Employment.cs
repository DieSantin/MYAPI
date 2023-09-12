﻿using Newtonsoft.Json;

namespace MYAPI.Models.EXAPIEntities;

public class Employment
{
    public int Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("key_skill")]
    public string KeySkill { get; set; }

}
