﻿namespace LetsTravelCoolPlaces.Services.Interfaces;

public interface IHttpService<T> where T : class
{
    Task<T?> GetAsync(string url);
    Task<T?> PostAsync(string url, T data);
}