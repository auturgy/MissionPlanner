﻿
using System.Collections.Concurrent;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GMap.NET.MapProviders
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Security.Cryptography;
    using GMap.NET.Internals;
    using GMap.NET.Projections;
    using System.Text;

    /// <summary>
    /// providers that are already build in
    /// </summary>
    public class GMapProviders
    {
        static GMapProviders()
        {
            list = new List<GMapProvider>();

            Type type = typeof(GMapProviders);
            foreach (var p in type.GetFields())
            {
                var v = p.GetValue(null) as GMapProvider; // static classes cannot be instanced, so use null...
                if (v != null)
                {
                    list.Add(v);
                }
            }

            Hash = new Dictionary<Guid, GMapProvider>();
            foreach (var p in list)
            {
                Hash.Add(p.Id, p);
            }

            DbHash = new Dictionary<int, GMapProvider>();
            foreach (var p in list)
            {
                DbHash.Add(p.DbId, p);
            }
        }

        GMapProviders()
        {
        }

        public static readonly EmptyProvider EmptyProvider = EmptyProvider.Instance;

        public static readonly OpenStreetMapProvider OpenStreetMap = OpenStreetMapProvider.Instance;

        public static readonly OpenStreet4UMapProvider OpenStreet4UMap = OpenStreet4UMapProvider.Instance;

        public static readonly OpenCycleMapProvider OpenCycleMap = OpenCycleMapProvider.Instance;
        public static readonly OpenCycleLandscapeMapProvider OpenCycleLandscapeMap = OpenCycleLandscapeMapProvider.Instance;
        public static readonly OpenCycleTransportMapProvider OpenCycleTransportMap = OpenCycleTransportMapProvider.Instance;

        public static readonly OpenStreetMapQuestProvider OpenStreetMapQuest = OpenStreetMapQuestProvider.Instance;
        public static readonly OpenStreetMapQuestSatteliteProvider OpenStreetMapQuestSattelite = OpenStreetMapQuestSatteliteProvider.Instance;
        public static readonly OpenStreetMapQuestHybridProvider OpenStreetMapQuestHybrid = OpenStreetMapQuestHybridProvider.Instance;

        public static readonly OpenSeaMapHybridProvider OpenSeaMapHybrid = OpenSeaMapHybridProvider.Instance;

#if OpenStreetOsm
      public static readonly OpenStreetOsmProvider OpenStreetOsm = OpenStreetOsmProvider.Instance;
#endif

#if OpenStreetMapSurfer
      public static readonly OpenStreetMapSurferProvider OpenStreetMapSurfer = OpenStreetMapSurferProvider.Instance;
      public static readonly OpenStreetMapSurferTerrainProvider OpenStreetMapSurferTerrain = OpenStreetMapSurferTerrainProvider.Instance;
#endif
        public static readonly WikiMapiaMapProvider WikiMapiaMap = WikiMapiaMapProvider.Instance;

        public static readonly BingMapProvider BingMap = BingMapProvider.Instance;
        public static readonly BingSatelliteMapProvider BingSatelliteMap = BingSatelliteMapProvider.Instance;
        public static readonly BingHybridMapProvider BingHybridMap = BingHybridMapProvider.Instance;

        public static readonly AMapProvider AMap = AMapProvider.Instance;
        public static readonly AMapSateliteProvider AMapStatelite = AMapSateliteProvider.Instance;

        public static readonly GoogleMapProvider GoogleMap = GoogleMapProvider.Instance;
        public static readonly GoogleSatelliteMapProvider GoogleSatelliteMap = GoogleSatelliteMapProvider.Instance;
        public static readonly GoogleHybridMapProvider GoogleHybridMap = GoogleHybridMapProvider.Instance;
        public static readonly GoogleTerrainMapProvider GoogleTerrainMap = GoogleTerrainMapProvider.Instance;

        public static readonly GoogleChinaMapProvider GoogleChinaMap = GoogleChinaMapProvider.Instance;
        public static readonly GoogleChinaSatelliteMapProvider GoogleChinaSatelliteMap = GoogleChinaSatelliteMapProvider.Instance;
        public static readonly GoogleChinaHybridMapProvider GoogleChinaHybridMap = GoogleChinaHybridMapProvider.Instance;
        public static readonly GoogleChinaTerrainMapProvider GoogleChinaTerrainMap = GoogleChinaTerrainMapProvider.Instance;

        public static readonly GoogleKoreaMapProvider GoogleKoreaMap = GoogleKoreaMapProvider.Instance;
        public static readonly GoogleKoreaSatelliteMapProvider GoogleKoreaSatelliteMap = GoogleKoreaSatelliteMapProvider.Instance;
        public static readonly GoogleKoreaHybridMapProvider GoogleKoreaHybridMap = GoogleKoreaHybridMapProvider.Instance;

        public static readonly NearMapProvider NearMap = NearMapProvider.Instance;
        public static readonly NearSatelliteMapProvider NearSatelliteMap = NearSatelliteMapProvider.Instance;
        public static readonly NearHybridMapProvider NearHybridMap = NearHybridMapProvider.Instance;

        public static readonly OviMapProvider OviMap = OviMapProvider.Instance;
        public static readonly OviSatelliteMapProvider OviSatelliteMap = OviSatelliteMapProvider.Instance;
        public static readonly OviHybridMapProvider OviHybridMap = OviHybridMapProvider.Instance;
        public static readonly OviTerrainMapProvider OviTerrainMap = OviTerrainMapProvider.Instance;

        public static readonly YandexMapProvider YandexMap = YandexMapProvider.Instance;
        public static readonly YandexSatelliteMapProvider YandexSatelliteMap = YandexSatelliteMapProvider.Instance;
        public static readonly YandexHybridMapProvider YandexHybridMap = YandexHybridMapProvider.Instance;

        public static readonly LithuaniaMapProvider LithuaniaMap = LithuaniaMapProvider.Instance;
        public static readonly Lithuania3dMapProvider Lithuania3dMap = Lithuania3dMapProvider.Instance;
        public static readonly LithuaniaOrtoFotoMapProvider LithuaniaOrtoFotoMap = LithuaniaOrtoFotoMapProvider.Instance;
        public static readonly LithuaniaOrtoFotoOldMapProvider LithuaniaOrtoFotoOldMap = LithuaniaOrtoFotoOldMapProvider.Instance;
        public static readonly LithuaniaHybridMapProvider LithuaniaHybridMap = LithuaniaHybridMapProvider.Instance;
        public static readonly LithuaniaHybridOldMapProvider LithuaniaHybridOldMap = LithuaniaHybridOldMapProvider.Instance;
        public static readonly LithuaniaTOP50 LithuaniaTOP50Map = LithuaniaTOP50.Instance;

        public static readonly LatviaMapProvider LatviaMap = LatviaMapProvider.Instance;

        public static readonly MapBenderWMSProvider MapBenderWMSdemoMap = MapBenderWMSProvider.Instance;

        public static readonly TurkeyMapProvider TurkeyMap = TurkeyMapProvider.Instance;

        public static readonly CloudMadeMapProvider CloudMadeMap = CloudMadeMapProvider.Instance;

        public static readonly SpainMapProvider SpainMap = SpainMapProvider.Instance;

        public static readonly CzechMapProvider CzechMap = CzechMapProvider.Instance;
        public static readonly CzechSatelliteMapProvider CzechSatelliteMap = CzechSatelliteMapProvider.Instance;
        public static readonly CzechHybridMapProvider CzechHybridMap = CzechHybridMapProvider.Instance;
        public static readonly CzechTuristMapProvider CzechTuristMap = CzechTuristMapProvider.Instance;
        public static readonly CzechHistoryMapProvider CzechHistoryMap = CzechHistoryMapProvider.Instance;

        public static readonly ArcGIS_Imagery_World_2D_MapProvider ArcGIS_Imagery_World_2D_Map = ArcGIS_Imagery_World_2D_MapProvider.Instance;
        public static readonly ArcGIS_ShadedRelief_World_2D_MapProvider ArcGIS_ShadedRelief_World_2D_Map = ArcGIS_ShadedRelief_World_2D_MapProvider.Instance;
        public static readonly ArcGIS_StreetMap_World_2D_MapProvider ArcGIS_StreetMap_World_2D_Map = ArcGIS_StreetMap_World_2D_MapProvider.Instance;
        public static readonly ArcGIS_Topo_US_2D_MapProvider ArcGIS_Topo_US_2D_Map = ArcGIS_Topo_US_2D_MapProvider.Instance;

        public static readonly ArcGIS_World_Physical_MapProvider ArcGIS_World_Physical_Map = ArcGIS_World_Physical_MapProvider.Instance;
        public static readonly ArcGIS_World_Shaded_Relief_MapProvider ArcGIS_World_Shaded_Relief_Map = ArcGIS_World_Shaded_Relief_MapProvider.Instance;
        public static readonly ArcGIS_World_Street_MapProvider ArcGIS_World_Street_Map = ArcGIS_World_Street_MapProvider.Instance;
        public static readonly ArcGIS_World_Terrain_Base_MapProvider ArcGIS_World_Terrain_Base_Map = ArcGIS_World_Terrain_Base_MapProvider.Instance;
        public static readonly ArcGIS_World_Topo_MapProvider ArcGIS_World_Topo_Map = ArcGIS_World_Topo_MapProvider.Instance;

        public static readonly ArcGIS_DarbAE_Q2_2011_NAVTQ_Eng_V5_MapProvider ArcGIS_DarbAE_Q2_2011_NAVTQ_Eng_V5_Map = ArcGIS_DarbAE_Q2_2011_NAVTQ_Eng_V5_MapProvider.Instance;

        static List<GMapProvider> list;

        /// <summary>
        /// get all instances of the supported providers
        /// </summary>
        public static List<GMapProvider> List
        {
            get
            {
                return list;
            }
        }

        static Dictionary<Guid, GMapProvider> Hash;

        public static GMapProvider TryGetProvider(Guid id)
        {
            GMapProvider ret;
            if (Hash.TryGetValue(id, out ret))
            {
                return ret;
            }
            return null;
        }

        static Dictionary<int, GMapProvider> DbHash;

        public static GMapProvider TryGetProvider(int DbId)
        {
            GMapProvider ret;
            if (DbHash.TryGetValue(DbId, out ret))
            {
                return ret;
            }
            return null;
        }
    }

    /// <summary>
    /// base class for each map provider
    /// </summary>
    public abstract class GMapProvider
    {
        /// <summary>
        /// unique provider id
        /// </summary>
        public abstract Guid Id
        {
            get;
        }

        /// <summary>
        /// provider name
        /// </summary>
        public abstract string Name
        {
            get;
        }

        /// <summary>
        /// provider projection
        /// </summary>
        public abstract PureProjection Projection
        {
            get;
        }

        /// <summary>
        /// provider overlays
        /// </summary>
        public abstract GMapProvider[] Overlays
        {
            get;
        }

        /// <summary>
        /// gets tile image using implmented provider
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="zoom"></param>
        /// <returns></returns>
        public abstract PureImage GetTileImage(GPoint pos, int zoom);

        static readonly List<GMapProvider> MapProviders = new List<GMapProvider>();

        protected GMapProvider()
        {
            using (var HashProvider = new SHA1CryptoServiceProvider())
            {
                DbId = Math.Abs(BitConverter.ToInt32(HashProvider.ComputeHash(Id.ToByteArray()), 0));
            }

            if (MapProviders.Exists(p => p.Id == Id || p.DbId == DbId))
            {
                throw new Exception("such provider id already exsists, try regenerate your provider guid...");
            }
            MapProviders.Add(this);
        }

        static GMapProvider()
        {
            WebProxy = EmptyWebProxy.Instance;
        }

        bool isInitialized = false;

        /// <summary>
        /// was provider initialized
        /// </summary>
        public bool IsInitialized
        {
            get
            {
                return isInitialized;
            }
            internal set
            {
                isInitialized = value;
            }
        }

        /// <summary>
        /// called before first use
        /// </summary>
        public virtual void OnInitialized()
        {
            // nice place to detect current provider version
        }

        /// <summary>
        /// id for database, a hash of provider guid
        /// </summary>
        public readonly int DbId;

        /// <summary>
        /// area of map
        /// </summary>
        public RectLatLng? Area;

        /// <summary>
        /// minimum level of zoom
        /// </summary>
        public int MinZoom;

        /// <summary>
        /// maximum level of zoom
        /// </summary>
        public int? MaxZoom = 17;

        /// <summary>
        /// proxy for net access
        /// </summary>
        public static IWebProxy WebProxy;

        /// <summary>
        /// Connect trough a SOCKS 4/5 proxy server
        /// </summary>
        public static bool IsSocksProxy = false;

        /// <summary>
        /// NetworkCredential for tile http access
        /// </summary>
        public static ICredentials Credential;

        /// <summary>
        /// Gets or sets the value of the User-agent HTTP header.
        /// It's pseudo-randomized to avoid blockages...
        /// </summary>                  
        public static string UserAgent =
            string.Format(
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edge/17.17074");

        /// <summary>
        /// timeout for provider connections
        /// </summary>
#if !PocketPC
        public static int TimeoutMs = 5 * 1000;
#else
      public static int TimeoutMs = 44 * 1000; 
#endif
        /// <summary>
        /// Gets or sets the value of the Referer HTTP header.
        /// </summary>
        public string RefererUrl = string.Empty;

        public string Copyright = string.Empty;

        /// <summary>
        /// true if tile origin at BottomLeft, WMS-C
        /// </summary>
        public bool InvertedAxisY = false;

        static string languageStr = "en";
        public static string LanguageStr
        {
            get
            {
                return languageStr;
            }
        }
        static LanguageType language = LanguageType.English;

        /// <summary>
        /// map language
        /// </summary>
        public static LanguageType Language
        {
            get
            {
                return language;
            }
            set
            {
                language = value;
                languageStr = Stuff.EnumToString(Language);
            }
        }

        /// <summary>
        /// to bypass the cache, set to true
        /// </summary>
        public bool BypassCache = false;

        /// <summary>
        /// internal proxy for image managment
        /// </summary>
        public static PureImageProxy TileImageProxy;

        static readonly string requestAccept = "*/*";
        static readonly string responseContentType = "image";

        protected virtual bool CheckTileImageHttpResponse(WebResponse response)
        {
            //Debug.WriteLine(response.StatusCode + "/" + response.StatusDescription + "/" + response.ContentType + " -> " + response.ResponseUri);
            return response.ContentType.Contains(responseContentType);
        }

        protected PureImage GetTileImageUsingHttp(string url)
        {
            PureImage ret = null;

            HttpClient client = new HttpClient();
            if (!string.IsNullOrEmpty(UserAgent))
                client.DefaultRequestHeaders.Add("User-Agent", UserAgent);
            if (!string.IsNullOrEmpty(requestAccept))
                client.DefaultRequestHeaders.Add("Accept", requestAccept);
            if (!string.IsNullOrEmpty(RefererUrl))
                client.DefaultRequestHeaders.Add("Referer", RefererUrl);

            MemoryStream data = Task.Run(async () =>
            {
                using (var response = await client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var stream = await response.Content.ReadAsStreamAsync();
                        return Stuff.CopyStream(stream, false);
                    }

                    throw new WebException((int)response.StatusCode + " " + response.ReasonPhrase);
                }
            }).GetAwaiter().GetResult();

            Debug.WriteLine("Response[" + data.Length + " bytes]: " + url);

            if (data.Length > 0)
            {
                ret = TileImageProxy.FromStream(data);

                if (ret != null)
                {
                    ret.Data = data;
                    ret.Data.Position = 0;
                }
                else
                {
                    data.Dispose();
                }
            }
            data = null;
            return ret;
        }

        protected string GetContentUsingHttp(string url)
        {
            string ret = string.Empty;

            HttpClient client = new HttpClient();
            if (!string.IsNullOrEmpty(UserAgent))
                client.DefaultRequestHeaders.Add("User-Agent", UserAgent);
            if (!string.IsNullOrEmpty(requestAccept))
                client.DefaultRequestHeaders.Add("Accept", requestAccept);
            if (!string.IsNullOrEmpty(RefererUrl))
                client.DefaultRequestHeaders.Add("Referer", RefererUrl);

            ret = Task.Run(async () =>
            {
                return await client.GetStringAsync(url);
            }).GetAwaiter().GetResult();

            return ret;
        }

        protected static int GetServerNum(GPoint pos, int max)
        {
            return (int)(pos.X + 2 * pos.Y) % max;
        }

        public override int GetHashCode()
        {
            return (int)DbId;
        }

        public override bool Equals(object obj)
        {
            if (obj is GMapProvider)
            {
                return Id.Equals((obj as GMapProvider).Id);
            }
            return false;
        }        

        public override string ToString()
        {
            return Name;
        }        
    }

    /// <summary>
    /// represents empty provider
    /// </summary>
    public class EmptyProvider : GMapProvider
    {
        public static readonly EmptyProvider Instance;

        EmptyProvider()
        {
            MaxZoom = null;
        }

        static EmptyProvider()
        {
            Instance = new EmptyProvider();
        }

        #region GMapProvider Members

        public override Guid Id
        {
            get
            {
                return Guid.Empty;
            }
        }

        readonly string name = "None";
        public override string Name
        {
            get
            {
                return name;
            }
        }

        readonly MercatorProjection projection = MercatorProjection.Instance;
        public override PureProjection Projection
        {
            get
            {
                return projection;
            }
        }

        public override GMapProvider[] Overlays
        {
            get
            {
                return null;
            }
        }

        public override PureImage GetTileImage(GPoint pos, int zoom)
        {
            return null;
        }

        #endregion
    }

    public sealed class EmptyWebProxy : IWebProxy
    {
        public static readonly EmptyWebProxy Instance = new EmptyWebProxy();

        private ICredentials m_credentials;
        public ICredentials Credentials
        {
            get
            {
                return this.m_credentials;
            }
            set
            {
                this.m_credentials = value;
            }
        }

        public Uri GetProxy(Uri uri)
        {
            return uri;
        }

        public bool IsBypassed(Uri uri)
        {
            return true;
        }
    }
}
