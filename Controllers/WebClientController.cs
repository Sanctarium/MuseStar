using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using MuseStar.Models;
using MuseStar;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace MuseStar.Controllers
{
    [Route("playlist")]
    public class WebClientController : Controller
    {
        HttpClient httpClient;
        private Uri baseUri = new Uri(@"https://cloud-api.yandex.net/v1/disk/resources/download?path=");
        private readonly PlaylistContext _context;
        public WebClientController(PlaylistContext context)
        {
            _context = context;
        }
        //GET playlist/3
        [HttpGet("{id}")]
         public IActionResult Index(int id)
         {
             List<Song> UserSongs=new List<Song>();
             //validation
             if (_context.Users.Find(id)!=null)
                //GenerateRequests();
                UserSongs = _context.Songs.Where(s => s.UserId == id).ToList();
                //SendRequestsToDisk();
                string downloadUrl = "";
                Song currentSong = new Song();
                HttpRequestMessage RequestMessage=new HttpRequestMessage();
                foreach(var song in UserSongs)
                    { 
                        httpClient = new HttpClient();
                        RequestMessage.RequestUri =new Uri(baseUri + song.Name);
                        RequestMessage.Method = HttpMethod.Get;
                        RequestMessage.Headers.Clear();
                        RequestMessage.Headers.Add("Authorization", "OAuth AQAAAAAhJOxxAAUoplRt2XN-kE5gkry4Rk2b-vI");
                        downloadUrl = httpClient.SendAsync(RequestMessage).Result.Content.ReadAsStringAsync().Result;
                        downloadUrl = JsonConvert.DeserializeObject<DownloadUrl>(downloadUrl).href;
                        httpClient.Dispose();
                //UpdateDatabase();
                        currentSong = UserSongs.Find(s => s.Id==song.Id);
                        currentSong.DownloadUrl = downloadUrl;
                        _context.Songs.Update(currentSong);
                    }
                _context.SaveChanges();
            
            return View("Index", UserSongs);
         }
    }
}