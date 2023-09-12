﻿using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace LetsTravelCoolPlaces.Services;

public class HttpService<T> : IHttpService<T> where T : class
{
    public async Task<T?> GetAsync(string url)
    {
        var client = GetClient(url);
        var response = await client.GetAsync("");
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(result);
        }
        else
        {
            return null;
        }
    }

    public async Task<T?> PostAsync(string url, T data)
    {
        var client = GetClient(url);
        var response = await client.PostAsJsonAsync("", data);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(result);
        }
        else
        {
            return null;
        }
    }

    private HttpClient GetClient(string url)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri(url);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        return client;
    }
}