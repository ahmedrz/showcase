using EManifestClient.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Net.Http.Handlers;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EManifestClient.Core
{
    //public class DataDownloader
    //{
    //    Guid ValidStatusID = new Guid("12f2243e-8545-489d-a854-7ff22fbee723");
    //    Guid InValidStatusID = new Guid("5aa93c9b-65d1-4378-a35c-537db5ab1d19");
    //    Guid PendingStatusID = new Guid("f2fb713c-8986-44a2-bd3a-a0d64dddeef9");
    //    Guid ApprovedStatusID = new Guid("d7b43b80-ea2e-4d6c-a436-b6572ef236ac");
    //    Guid DeclinedStatusID = new Guid("bec15f0a-734f-43a3-a5ad-bb9908a62c87");

    //    string clientUserName = "defaultname";
    //    string clientPassword = "defaultpassword";
    //    HttpClient client;
    //    public string ServiceUrl { get; set; }
    //    public DataDownloader()
    //    {
    //        GetServiceUrl();
    //        InitializeClient();
    //    }
    //    public DataDownloader(string clientUserName, string clientPassword) : this()
    //    {
    //        this.clientUserName = clientUserName;
    //        this.clientPassword = clientPassword;
    //        InitializeClient();
    //    }
    //    private void InitializeClient()
    //    {
    //        client = new HttpClient();
    //        var byteArray = Encoding.ASCII.GetBytes($"{clientUserName}:{clientPassword}");
    //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

    //        // Update port # in the following line.
    //        client.BaseAddress = new Uri(ServiceUrl);
    //        client.DefaultRequestHeaders.Accept.Clear();
    //        client.DefaultRequestHeaders.Accept.Add(
    //            new MediaTypeWithQualityHeaderValue("application/json"));
    //    }
    //    private void GetServiceUrl()
    //    {
    //        ServiceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
    //    }

    //    public async Task<List<ValidVoyageResult>> GetValidVoyages(string portCode, bool showApproved = false, bool showDeclined = false)
    //    {
    //        if (portCode == null)
    //        {
    //            return null;
    //        }
    //        List<ValidVoyageResult> validaVoyages = null;
    //        HttpResponseMessage response = await client.GetAsync(GetValidVoyagesPathResolver(portCode, showApproved, showDeclined)).ConfigureAwait(false);
    //        if (response.IsSuccessStatusCode)
    //        {
    //            validaVoyages = await response.Content.ReadAsAsync<List<ValidVoyageResult>>().ConfigureAwait(false);
    //        }
    //        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
    //        {
    //            throw new Exception("You are not authorized to access the vessel api");
    //        }
    //        return validaVoyages;
    //    }
    //    public async Task<List<Vessels>> GetVessels(Guid carrierId)
    //    {
    //        if (carrierId == Guid.Empty)
    //        {
    //            return null;
    //        }
    //        List<Vessels> vessels = null;
    //        HttpResponseMessage response = await client.GetAsync(GetVesselsPathResolver(carrierId)).ConfigureAwait(false);
    //        if (response.IsSuccessStatusCode)
    //        {
    //            vessels = await response.Content.ReadAsAsync<List<Vessels>>().ConfigureAwait(false);
    //        }
    //        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
    //        {
    //            throw new Exception("You are not authorized to access the vessel api");
    //        }
    //        return vessels;
    //    }

    //    public async Task<Users> GetUser(string userName, string password)
    //    {
    //        if (userName == null || password == null)
    //        {
    //            return null;
    //        }
    //        Users user = null;
    //        HttpResponseMessage response = await client.GetAsync(GetUserPathResolver(userName, password)).ConfigureAwait(false);
    //        if (response.IsSuccessStatusCode)
    //        {
    //            user = await response.Content.ReadAsAsync<Users>().ConfigureAwait(false);
    //        }
    //        return user;
    //    }

    //    public async Task<List<VoyageDetails>> GetVoyages(ValidVoyageResult voyageInfo)
    //    {
    //        if (voyageInfo == null)
    //        {
    //            return null;
    //        }
    //        List<VoyageDetails> voyages = null;
    //        HttpResponseMessage response = await client.GetAsync(GetVoyagesPathResolver(voyageInfo.PortCode, voyageInfo.VesselName, voyageInfo.Voyage)).ConfigureAwait(false);
    //        if (response.IsSuccessStatusCode)
    //        {
    //            voyages = await response.Content.ReadAsAsync<List<VoyageDetails>>().ConfigureAwait(false);
    //        }
    //        return voyages;
    //    }

    //    public async Task<Guid?> GetVoyageStatusId(Guid voyageDetailsId)
    //    {
    //        if (voyageDetailsId == Guid.Empty)
    //        {
    //            return null;
    //        }
    //        Guid result;
    //        HttpResponseMessage response = await client.GetAsync(GetVoyageStatusPathResolver(voyageDetailsId)).ConfigureAwait(false);
    //        if (response.IsSuccessStatusCode)
    //        {
    //            result = await response.Content.ReadAsAsync<Guid>().ConfigureAwait(false);
    //            if (result == Guid.Empty)
    //            {
    //                return null;
    //            }
    //            return result;
    //        }
    //        return null;
    //    }
    //    public async Task<bool> ApproveVoyage(Guid voyageDetailsId)
    //    {
    //        if (voyageDetailsId == Guid.Empty)
    //        {
    //            return false;
    //        }
    //        JObject result = null;
    //        HttpResponseMessage response = await client.GetAsync(GetApproveVoyagePathResolver(voyageDetailsId)).ConfigureAwait(false);
    //        if (response.IsSuccessStatusCode)
    //        {
    //            result = await response.Content.ReadAsAsync<JObject>().ConfigureAwait(false);
    //        }
    //        return result != null && result["Success"].ToObject<bool>() == true;
    //    }
    //    public async Task<bool> DeclineVoyage(Guid voyageDetailsId, string declineReason)
    //    {
    //        if (voyageDetailsId == Guid.Empty)
    //        {
    //            return false;
    //        }
    //        JObject result = null;
    //        HttpResponseMessage response = await client.GetAsync(GetDeclineVoyagePathResolver(voyageDetailsId, declineReason)).ConfigureAwait(false);
    //        if (response.IsSuccessStatusCode)
    //        {
    //            result = await response.Content.ReadAsAsync<JObject>().ConfigureAwait(false);
    //        }
    //        return result != null && result["Success"].ToObject<bool>() == true;
    //    }

    //    private string GetValidVoyagesPathResolver(string portCode, bool showApproved, bool showDeclined)
    //    {
    //        string path = $"/api/voyage/getvalidvoyages?portcode={portCode}";
    //        if (showApproved)
    //        {
    //            path += "&showApproved=true";
    //        }
    //        if (showDeclined)
    //        {
    //            path += "&showDeclined=true";
    //        }
    //        return path;
    //    }
    //    private string GetVoyageStatusPathResolver(Guid voyageDetailsId)
    //    {
    //        string path = $"/api/voyage/GetStatus?voyageDetailsId={voyageDetailsId}";
    //        return path;
    //    }
    //    private string GetApproveVoyagePathResolver(Guid voyageDetailsId)
    //    {
    //        string path = $"/api/voyage/ApproveVoyage?voyageDetailsId={voyageDetailsId}";
    //        return path;
    //    }
    //    private string GetDeclineVoyagePathResolver(Guid voyageDetailsId, string declineReason)
    //    {
    //        string path = $"/api/voyage/DeclineVoyage?voyageDetailsId={voyageDetailsId}&declineReason={declineReason}";
    //        return path;
    //    }
    //    private string GetVoyagesPathResolver(string portCode, string vesselName, string Voyage)
    //    {
    //        string path = $"/api/Voyage/GetVoyage?vesselname={vesselName}&voyage={Voyage}&portcode={portCode}";
    //        return path;
    //    }


    //    private string GetUserPathResolver(string userName, string password)
    //    {
    //        string path = $"/api/public/GetUser?userName={userName}&password={password}";
    //        return path;
    //    }
    //    private string GetVesselsPathResolver(Guid carrierId)
    //    {
    //        string path = $"/api/info/GetVessels?CarrierId={carrierId}";
    //        return path;
    //    }
    //    //info 
    //    private string GetCountryCodesPathResolver(int serial)
    //    {
    //        string path = $"/api/info/GetCountryCodes?serial={serial}";
    //        return path;
    //    }
    //    private string GetLocationCodesPathResolver(int serial)
    //    {
    //        string path = $"/api/info/GetLocationCodes?serial={serial}";
    //        return path;
    //    }
    //    private string GetHSCodesPathResolver(int serial)
    //    {
    //        string path = $"/api/info/GetHSCodes?serial={serial}";
    //        return path;
    //    }
    //    private string GetPackageCodesPathResolver(int serial)
    //    {
    //        string path = $"/api/info/GetPackageCodes?serial={serial}";
    //        return path;
    //    }
    //    private string GetLinesPathResolver(int serial)
    //    {
    //        string path = $"/api/info/GetLines?serial={serial}";
    //        return path;
    //    }
    //    public async Task<List<CountryCodes>> GetCountryCodes(int serial)
    //    {
    //        List<CountryCodes> codes = null;
    //        HttpResponseMessage response = await client.GetAsync(GetCountryCodesPathResolver(serial)).ConfigureAwait(false);
    //        if (response.IsSuccessStatusCode)
    //        {
    //            codes = await response.Content.ReadAsAsync<List<CountryCodes>>().ConfigureAwait(false);
    //        }
    //        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
    //        {
    //            throw new Exception("You are not authorized to access the api");
    //        }
    //        return codes;
    //    }
    //    public async Task<List<LocationCodes>> GetLocationCodes(int serial)
    //    {
    //        List<LocationCodes> codes = null;
    //        HttpResponseMessage response = await client.GetAsync(GetLocationCodesPathResolver(serial)).ConfigureAwait(false);
    //        if (response.IsSuccessStatusCode)
    //        {
    //            codes = await response.Content.ReadAsAsync<List<LocationCodes>>().ConfigureAwait(false);
    //        }
    //        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
    //        {
    //            throw new Exception("You are not authorized to access the api");
    //        }
    //        return codes;
    //    }
    //    public async Task<List<UNHSCodes>> GetHSCodes(int serial)
    //    {
    //        List<UNHSCodes> codes = null;
    //        HttpResponseMessage response = await client.GetAsync(GetHSCodesPathResolver(serial)).ConfigureAwait(false);
    //        if (response.IsSuccessStatusCode)
    //        {
    //            codes = await response.Content.ReadAsAsync<List<UNHSCodes>>().ConfigureAwait(false);
    //        }
    //        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
    //        {
    //            throw new Exception("You are not authorized to access the api");
    //        }
    //        return codes;
    //    }
    //    public async Task<List<PackageCodes>> GetPackageCodes(int serial)
    //    {
    //        List<PackageCodes> codes = null;
    //        HttpResponseMessage response = await client.GetAsync(GetPackageCodesPathResolver(serial)).ConfigureAwait(false);
    //        if (response.IsSuccessStatusCode)
    //        {
    //            codes = await response.Content.ReadAsAsync<List<PackageCodes>>().ConfigureAwait(false);
    //        }
    //        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
    //        {
    //            throw new Exception("You are not authorized to access the api");
    //        }
    //        return codes;
    //    }
    //    public async Task<List<Lines>> GetLines(int serial)
    //    {
    //        List<Lines> lines = null;
    //        HttpResponseMessage response = await client.GetAsync(GetLinesPathResolver(serial)).ConfigureAwait(false);
    //        if (response.IsSuccessStatusCode)
    //        {
    //            lines = await response.Content.ReadAsAsync<List<Lines>>().ConfigureAwait(false);
    //        }
    //        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
    //        {
    //            throw new Exception("You are not authorized to access the api");
    //        }
    //        return lines;
    //    }
    //}
    public delegate void FileProgressEventHandler(string filename, float progress);
    public class DataDownloader<VES, USR, VOY, COUNTRY, LOCATION, UNHSCODE, PACKAGE, LINE, CTRISOCODE, HAZARDCODE>
    {

        public event FileProgressEventHandler FileProgress;
        Guid ValidStatusID = new Guid("12f2243e-8545-489d-a854-7ff22fbee723");
        Guid InValidStatusID = new Guid("5aa93c9b-65d1-4378-a35c-537db5ab1d19");
        Guid PendingStatusID = new Guid("f2fb713c-8986-44a2-bd3a-a0d64dddeef9");
        Guid ApprovedStatusID = new Guid("d7b43b80-ea2e-4d6c-a436-b6572ef236ac");
        Guid DeclinedStatusID = new Guid("bec15f0a-734f-43a3-a5ad-bb9908a62c87");

        string clientUserName = "defaultname";
        string clientPassword = "defaultpassword";
        HttpClient client;
        public string ServiceUrl { get; set; }
        public DataDownloader()
        {
            GetServiceUrl();
            InitializeClient();
        }
        public DataDownloader(string clientUserName, string clientPassword) : this()
        {
            this.clientUserName = clientUserName;
            this.clientPassword = clientPassword;
            InitializeClient();
        }
        private void InitializeClient()
        {
            client = new HttpClient();
            InitializeClient(client);
        }

        private void InitializeClient(HttpClient client)
        {
            var byteArray = Encoding.ASCII.GetBytes($"{clientUserName}:{clientPassword}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            // Update port # in the following line.
            client.BaseAddress = new Uri(ServiceUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        private void GetServiceUrl()
        {
            ServiceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
        }
        private MemoryStream Compress(Stream fileStream)
        {
            var outStream = new MemoryStream();
            var tinyStream = new GZipStream(outStream, CompressionMode.Compress);
            fileStream.CopyTo(tinyStream);
            outStream.Seek(0, SeekOrigin.Begin);
            return outStream;

        }
        public async Task<bool> PostManifestFile(Stream manifestFileStream)
        {
            JObject result;
            var content = new MultipartFormDataContent();
            StreamContent streamContent = new StreamContent(manifestFileStream);
            content.Add(streamContent, "\"file\"", string.Format("\"{0}\"", "ManifestFile.txt")
            );
            HttpResponseMessage response = await client.PostAsync(GetPostManifestFilePathResolver(), content).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<JObject>().ConfigureAwait(false);
                if (result["Success"].ToObject<bool>() == true)
                {
                    return true;
                }
                else
                {
                    throw new Exception(result["Message"].ToObject<string>());
                }
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new Exception("You are not authorized to access the vessel api");
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }

        }
        public async Task<bool> Upload(Stream fileStream, CancellationToken cancelToken)
        {
            JObject result;
            HttpContent fileStreamContent = new StreamContent(fileStream);
            var handler = new ProgressMessageHandler();
            var formData = new MultipartFormDataContent();
            formData.Add(fileStreamContent, "\"file\"", string.Format("\"{0}\"", "ManifestFile.txt"));
            handler.HttpSendProgress += (s, e) =>
            {
                float prog = (float)e.BytesTransferred / fileStream.Length;
                prog = prog > 1 ? 1 : prog;
                if (FileProgress != null)
                    FileProgress?.Invoke("ManifestFile.txt", prog);
            };
            HttpClient client = HttpClientFactory.Create(handler);
            InitializeClient(client);
            var response = await client.PostAsync(GetPostManifestFilePathResolver(), formData, cancelToken).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<JObject>().ConfigureAwait(false);
                if (result["Success"].ToObject<bool>() == true)
                {
                    return true;
                }
                else
                {
                    throw new Exception(result["Message"].ToObject<string>());
                }
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new Exception("You are not authorized to access the post manifest api");
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }

        }
        public async Task<List<ValidVoyageResult>> GetValidVoyages(string portCode, bool showApproved = false, bool showDeclined = false)
        {
            if (portCode == null)
            {
                return null;
            }
            List<ValidVoyageResult> validaVoyages = null;
            HttpResponseMessage response = await client.GetAsync(GetValidVoyagesPathResolver(portCode, showApproved, showDeclined)).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                validaVoyages = await response.Content.ReadAsAsync<List<ValidVoyageResult>>().ConfigureAwait(false);
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new Exception("You are not authorized to access the vessel api");
            }
            return validaVoyages;
        }
        public async Task<List<VES>> GetVessels(Guid carrierId)
        {
            if (carrierId == Guid.Empty)
            {
                return null;
            }
            List<VES> vessels = null;
            HttpResponseMessage response = await client.GetAsync(GetVesselsPathResolver(carrierId)).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                vessels = await response.Content.ReadAsAsync<List<VES>>().ConfigureAwait(false);
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new Exception("You are not authorized to access the vessel api");
            }
            return vessels;
        }

        public async Task<USR> GetUser(string userName, string password)
        {
            if (userName == null || password == null)
            {
                return default(USR);
            }
            USR user = default(USR);
            HttpResponseMessage response = await client.GetAsync(GetUserPathResolver(userName, password)).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadAsAsync<USR>().ConfigureAwait(false);
            }
            return user;
        }

        public async Task<List<VOY>> GetVoyages(ValidVoyageResult voyageInfo)
        {
            if (voyageInfo == null)
            {
                return null;
            }
            List<VOY> voyages = null;
            HttpResponseMessage response = await client.GetAsync(GetVoyagesPathResolver(voyageInfo.PortCode, voyageInfo.VesselName, voyageInfo.Voyage)).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                voyages = await response.Content.ReadAsAsync<List<VOY>>().ConfigureAwait(false);
            }
            return voyages;
        }

        public async Task<Guid?> GetVoyageStatusId(Guid voyageDetailsId)
        {
            if (voyageDetailsId == Guid.Empty)
            {
                return null;
            }
            Guid result;
            HttpResponseMessage response = await client.GetAsync(GetVoyageStatusPathResolver(voyageDetailsId)).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<Guid>().ConfigureAwait(false);
                if (result == Guid.Empty)
                {
                    return null;
                }
                return result;
            }
            return null;
        }
        public async Task<bool> ApproveVoyage(Guid voyageDetailsId)
        {
            if (voyageDetailsId == Guid.Empty)
            {
                return false;
            }
            JObject result = null;
            HttpResponseMessage response = await client.GetAsync(GetApproveVoyagePathResolver(voyageDetailsId)).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<JObject>().ConfigureAwait(false);
            }
            return result != null && result["Success"].ToObject<bool>() == true;
        }
        public async Task<bool> DeclineVoyage(Guid voyageDetailsId, string declineReason)
        {
            if (voyageDetailsId == Guid.Empty)
            {
                return false;
            }
            JObject result = null;
            HttpResponseMessage response = await client.GetAsync(GetDeclineVoyagePathResolver(voyageDetailsId, declineReason)).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<JObject>().ConfigureAwait(false);
            }
            return result != null && result["Success"].ToObject<bool>() == true;
        }

        private string GetValidVoyagesPathResolver(string portCode, bool showApproved, bool showDeclined)
        {
            string path = $"/api/voyage/getvalidvoyages?portcode={portCode}";
            if (showApproved)
            {
                path += "&showApproved=true";
            }
            if (showDeclined)
            {
                path += "&showDeclined=true";
            }
            return path;
        }
        private string GetVoyageStatusPathResolver(Guid voyageDetailsId)
        {
            string path = $"/api/voyage/GetStatus?voyageDetailsId={voyageDetailsId}";
            return path;
        }
        private string GetApproveVoyagePathResolver(Guid voyageDetailsId)
        {
            string path = $"/api/voyage/ApproveVoyage?voyageDetailsId={voyageDetailsId}";
            return path;
        }
        private string GetDeclineVoyagePathResolver(Guid voyageDetailsId, string declineReason)
        {
            string path = $"/api/voyage/DeclineVoyage?voyageDetailsId={voyageDetailsId}&declineReason={declineReason}";
            return path;
        }
        private string GetVoyagesPathResolver(string portCode, string vesselName, string Voyage)
        {
            string path = $"/api/Voyage/GetVoyage?vesselname={vesselName}&voyage={Voyage}&portcode={portCode}";
            return path;
        }

        private string GetPostManifestFilePathResolver()
        {
            string path = $"/api/Voyage/PortManifestFile";
            return path;
        }
        private string GetUserPathResolver(string userName, string password)
        {
            string path = $"/api/public/GetUser?userName={userName}&password={password}";
            return path;
        }
        private string GetVesselsPathResolver(Guid carrierId)
        {
            string path = $"/api/info/GetVessels?CarrierId={carrierId}";
            return path;
        }
        //info 
        private string GetCountryCodesPathResolver(int serial)
        {
            string path = $"/api/info/GetCountryCodes?serial={serial}";
            return path;
        }
        private string GetLocationCodesPathResolver(int serial)
        {
            string path = $"/api/info/GetLocationCodes?serial={serial}";
            return path;
        }
        private string GetHSCodesPathResolver(int serial)
        {
            string path = $"/api/info/GetHSCodes?serial={serial}";
            return path;
        }
        private string GetPackageCodesPathResolver(int serial)
        {
            string path = $"/api/info/GetPackageCodes?serial={serial}";
            return path;
        }
        private string GetLinesPathResolver(int serial)
        {
            string path = $"/api/info/GetLines?serial={serial}";
            return path;
        }
        private string GetContainerIsoCodesPathResolver(int serial)
        {
            string path = $"/api/info/GetContainerIsoCodes?serial={serial}";
            return path;
        }
        private string GetUNHazardousCodesPathResolver(int serial)
        {
            string path = $"/api/info/GetUNHazardousCodes?serial={serial}";
            return path;
        }
        public async Task<List<COUNTRY>> GetCountryCodes(int serial)
        {
            List<COUNTRY> codes = null;
            HttpResponseMessage response = await client.GetAsync(GetCountryCodesPathResolver(serial)).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                codes = await response.Content.ReadAsAsync<List<COUNTRY>>().ConfigureAwait(false);
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new Exception("You are not authorized to access the api");
            }
            return codes;
        }
        public async Task<List<LOCATION>> GetLocationCodes(int serial)
        {
            List<LOCATION> codes = null;
            HttpResponseMessage response = await client.GetAsync(GetLocationCodesPathResolver(serial)).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                codes = await response.Content.ReadAsAsync<List<LOCATION>>().ConfigureAwait(false);
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new Exception("You are not authorized to access the api");
            }
            return codes;
        }
        public async Task<List<UNHSCODE>> GetHSCodes(int serial)
        {
            List<UNHSCODE> codes = null;
            HttpResponseMessage response = await client.GetAsync(GetHSCodesPathResolver(serial)).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                codes = await response.Content.ReadAsAsync<List<UNHSCODE>>().ConfigureAwait(false);
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new Exception("You are not authorized to access the api");
            }
            return codes;
        }
        public async Task<List<PACKAGE>> GetPackageCodes(int serial)
        {
            List<PACKAGE> codes = null;
            HttpResponseMessage response = await client.GetAsync(GetPackageCodesPathResolver(serial)).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                codes = await response.Content.ReadAsAsync<List<PACKAGE>>().ConfigureAwait(false);
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new Exception("You are not authorized to access the api");
            }
            return codes;
        }
        public async Task<List<LINE>> GetLines(int serial)
        {
            List<LINE> lines = null;
            HttpResponseMessage response = await client.GetAsync(GetLinesPathResolver(serial)).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                lines = await response.Content.ReadAsAsync<List<LINE>>().ConfigureAwait(false);
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new Exception("You are not authorized to access the api");
            }
            return lines;
        }
        public async Task<List<CTRISOCODE>> GetContainerIsoCodes(int serial)
        {
            List<CTRISOCODE> containerCodes = null;
            HttpResponseMessage response = await client.GetAsync(GetContainerIsoCodesPathResolver(serial)).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                containerCodes = await response.Content.ReadAsAsync<List<CTRISOCODE>>().ConfigureAwait(false);
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new Exception("You are not authorized to access the api");
            }
            return containerCodes;
        }
        public async Task<List<HAZARDCODE>> GetUNHazardousCodes(int serial)
        {
            List<HAZARDCODE> codes = null;
            HttpResponseMessage response = await client.GetAsync(GetUNHazardousCodesPathResolver(serial)).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                codes = await response.Content.ReadAsAsync<List<HAZARDCODE>>().ConfigureAwait(false);
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new Exception("You are not authorized to access the api");
            }
            return codes;
        }
    }

}
