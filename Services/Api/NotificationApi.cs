using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using OfficeCore.Client.Models.Dtos;

namespace OfficeCore.Client.Services.Api;

public class NotificationApi
{
    private readonly HttpClient _http;
    private readonly NavigationManager _navigation;

    public NotificationApi(HttpClient http, NavigationManager navigation)
    {
        _http = http;
        _navigation = navigation;
    }

    public async Task<List<NotificationDto>> GetNotificationsAsync(int limit = 50)
    {
        try
        {
            var response = await _http.GetAsync($"api/notification?limit={limit}");
            if (!response.IsSuccessStatusCode)
                return new List<NotificationDto>();

            return await response.Content.ReadFromJsonAsync<List<NotificationDto>>() 
                ?? new List<NotificationDto>();
        }
        catch
        {
            return new List<NotificationDto>();
        }
    }

    public async Task<int> GetUnreadCountAsync()
    {
        try
        {
            var response = await _http.GetAsync("api/notification/unread-count");
            if (!response.IsSuccessStatusCode)
                return 0;

            return await response.Content.ReadFromJsonAsync<int>();
        }
        catch
        {
            return 0;
        }
    }

    public async Task<bool> MarkAsReadAsync(Guid notificationId)
    {
        try
        {
            var response = await _http.PutAsync($"api/notification/{notificationId}/read", null);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> MarkMultipleAsReadAsync(List<Guid> notificationIds)
    {
        try
        {
            var request = new { NotificationIds = notificationIds };
            var response = await _http.PutAsJsonAsync("api/notification/mark-read", request);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> MarkAllAsReadAsync()
    {
        try
        {
            var response = await _http.PutAsync("api/notification/mark-all-read", null);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteNotificationAsync(Guid notificationId)
    {
        try
        {
            var response = await _http.DeleteAsync($"api/notification/{notificationId}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}
