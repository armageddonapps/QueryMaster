
#region License
/*
Copyright (c) 2015 Betson Roy

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.
*/
#endregion
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
namespace QueryMaster.Utils
{
    /// <summary>
    /// Represents Steam Id.
    /// </summary>
    public class SteamId
    {
        private static Regex LegacyRegex = new Regex(@"^STEAM_([0-4]):([0-1]):(\d+)$", RegexOptions.IgnoreCase);
        private static Regex SteamId3Regex = new Regex(@"^\[?([IUMGAPCgTcLa])?:([0-4]):(\d+)(:(\d+))?\]?$");

        /// <summary>
        /// what Steam system this Steam ID comes from.
        /// </summary>
        public Universe Universe { get; internal set; }
        /// <summary>
        /// what type of account this is.
        /// </summary>
        public AccountType AccountType { get; internal set; }
        /// <summary>
        /// Instance.
        /// </summary>
        public Instance Instance { get; internal set; }
        /// <summary>
        /// Account Id.
        /// </summary>
        public uint AccountId { get; internal set; }
        /// <summary>
        /// Indicates whether the passed steam id is valid.
        /// </summary>
        public bool IsValid { get; internal set; }

        private SteamId()
        {

        }

        /// <summary>
        /// Create Steam id from account id,accounttype,universe and optioanl instance.
        /// </summary>
        /// <param name="accountId">Account Id.</param>
        /// <param name="accountType">Account Type.</param>
        /// <param name="universe"> Universe.</param>
        /// <param name="instance">Instance.</param>
        public SteamId(uint accountId, AccountType accountType, Universe universe, Instance instance = Instance.Desktop)
        {
            AccountId = accountId;
            if (Enum.IsDefined(typeof(AccountType), accountType))
                AccountType = accountType;
            else
               AccountType = AccountType.Invalid;

            if (Enum.IsDefined(typeof(Universe), Universe))
                Universe = universe;
            else
                Universe = Universe.Invalid;
            if (Enum.IsDefined(typeof(Instance), Instance))
                Instance = instance;
            else
                Instance = Instance.Invalid;
            IsValid = Validate();
        }

        /// <summary>
        /// Creates <see cref="SteamId"/> instance from steam id in legacy format(STEAM_X:Y:Z).
        /// </summary>
        /// <param name="id">Steam id of legacy format(STEAM_X:Y:Z).</param>
        /// <returns>instance of <see cref="SteamId"/>.</returns>
        public static SteamId FromLegacyFormat(string id)
        {
            SteamId steamId = new SteamId();
            Match match = null;
            if ((match = LegacyRegex.Match(id)).Success)
            {
                steamId.Universe = (Universe)Int32.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
                if (!Enum.IsDefined(typeof(Universe), steamId.Universe))
                    steamId.Universe = Universe.Invalid;
                if (steamId.Universe == Universe.Invalid)
                    steamId.Universe = Universe.Public;
                uint accountID = UInt32.Parse(match.Groups[3].Value, CultureInfo.InvariantCulture);
                steamId.AccountId = match.Groups[2].Value == "0" ? (accountID << 1) : ((accountID << 1) + 1);
                steamId.Instance = Instance.Desktop;
                steamId.AccountType = AccountType.Individual;
                steamId.IsValid = steamId.Validate();
            }
            else
            {
                steamId.Universe = Universe.Invalid;
                steamId.AccountId = 0;
                steamId.Instance = Instance.Invalid;
                steamId.AccountType = AccountType.Invalid;
                steamId.IsValid = false;
            }
            return steamId;
        }

        /// <summary>
        /// Creates <see cref="SteamId"/> instance from steam id in legacy format('[C:U:A]' or '[C:U:A:I]').
        /// </summary>
        /// <param name="id">Steam id of SteamID3 format('[C:U:A]' or '[C:U:A:I]').</param>
        /// <returns>instance of <see cref="SteamId"/>.</returns>
        public static SteamId FromSteamId3(string id)
        {
            SteamId steamId = new SteamId();
            Match match = null;
            if ((match = SteamId3Regex.Match(id)).Success)
            {
                steamId.AccountType = AccountTypeMapper.Instance[match.Groups[1].Value[0]];
                steamId.Universe = (Universe)Int32.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture);
                if (!Enum.IsDefined(typeof(Universe), steamId.Universe))
                    steamId.Universe = Universe.Invalid;
                steamId.AccountId = UInt32.Parse(match.Groups[3].Value, CultureInfo.InvariantCulture);
                if (!String.IsNullOrEmpty(match.Groups[5].Value))
                {
                    steamId.Instance = (Instance)Int32.Parse(match.Groups[5].Value, CultureInfo.InvariantCulture);
                    if (!Enum.IsDefined(typeof(Instance), steamId.Instance))
                        steamId.Instance = Instance.Invalid;
                }
                else
                    if (steamId.AccountType == AccountType.Clan)
                        steamId.Instance = Instance.All;
                    else
                        steamId.Instance = Instance.Desktop;
                steamId.IsValid = steamId.Validate();
            }
            else
            {
                steamId.Universe = Universe.Invalid;
                steamId.AccountId = 0;
                steamId.Instance = Instance.Invalid;
                steamId.AccountType = AccountType.Invalid;
                steamId.IsValid = false;
            }
            return steamId;

        }

        /// <summary>
        /// Creates <see cref="SteamId"/> instance from SteamID64 format steam id.
        /// </summary>
        /// <param name="id">64-bit steam id.</param>
        /// <returns>instance of <see cref="SteamId"/>.</returns>
        public static SteamId FromSteamId64(ulong id)
        {
            SteamId steamId = new SteamId();
            byte[] bytes = BitConverter.GetBytes(id);
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            byte[] accountIdBytes = new byte[] { bytes[0], bytes[1], bytes[2], bytes[3] };
            byte[] instanceBytes = new byte[] { bytes[4], bytes[5], (Byte)(bytes[6] & 0x0F), 0 };
            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(accountIdBytes);
                Array.Reverse(instanceBytes);
            }
            steamId.AccountId = BitConverter.ToUInt32(accountIdBytes, 0);
            steamId.Instance = (Instance)Enum.Parse(typeof(Instance), BitConverter.ToUInt32(instanceBytes, 0).ToString(CultureInfo.InvariantCulture));
            if (!Enum.IsDefined(typeof(Instance), steamId.Instance))
                steamId.Instance = Instance.Invalid;
            steamId.AccountType = (AccountType)(int)(bytes[6] >> 4);
            if (!Enum.IsDefined(typeof(AccountType), steamId.AccountType))
                steamId.AccountType = AccountType.Invalid;
            steamId.Universe = (Universe)bytes[7];
            if (!Enum.IsDefined(typeof(Universe), steamId.Universe))
                steamId.Universe = Universe.Invalid;
            steamId.IsValid = steamId.Validate();
            return steamId;
        }

        /// <summary>
        /// Creates <see cref="SteamId"/> instance from Community Url(Profile Url) of player.
        /// </summary>
        /// <param name="url">Player's Url.</param>
        /// <param name="webApiKey">Steam web api key.</param>
        /// <returns>instance of <see cref="SteamId"/>.</returns>
        public static SteamId FromCommunityUrl(string url,string webApiKey)
        {
            SteamId steamId = null;
            if (Uri.IsWellFormedUriString(url, UriKind.Relative))
            {
                Steam.SteamQuery query = new Steam.SteamQuery(webApiKey);
                ulong? id = query.ISteamUser.ResolveVanityURL(url).ParsedResponse.SteamId;
                if (id == null)
                {
                    steamId = new SteamId();
                    steamId.Universe = Universe.Invalid;
                    steamId.AccountId = 0;
                    steamId.Instance = Instance.Invalid;
                    steamId.AccountType = AccountType.Invalid;
                    steamId.IsValid = false;
                }
                else
                {
                    steamId = SteamId.FromSteamId64((ulong)id);
                }
            }
            else
                throw new FormatException("Url is not in correct format.");
            return steamId;
        }

        private bool Validate()
        {
            if (Universe == Universe.Invalid || AccountType == AccountType.Invalid || Instance == Instance.Invalid)
                return false;
            if (AccountType == AccountType.Individual && AccountId == 0)
                return false;
            if (AccountType == AccountType.Clan && (AccountId == 0 || Instance != Instance.All))
                return false;
            if (AccountType == AccountType.GameServer && AccountId == 0)
                return false;
            return true;
        }

        /// <summary>
        /// Converts to format : STEAM_X:Y:Z.
        /// </summary>
        /// <returns>Returns steam id in the format : STEAM_X:Y:Z.</returns>
        public string ToLegacyFormat()
        {
            return String.Format(CultureInfo.InvariantCulture,"STEAM_0:{0}:{1}", (AccountId & 1), (AccountId >> 1));
        }
        /// <summary>
        /// Converts to its ID3 format :'[C:U:A]' or '[C:U:A:I]'
        /// </summary>
        /// <param name="includeInstanceId">Whether to include instance id.</param>
        /// <returns>Returns steam id in ID3 format('[C:U:A]' or '[C:U:A:I]').</returns>
        public string ToSteamId3(bool includeInstanceId = false)
        {
            string id = string.Empty;
            if (includeInstanceId)
                id = String.Format(CultureInfo.InvariantCulture,"[{0}:{1}:{2}:{3}]", AccountTypeMapper.Instance[AccountType], (int)Universe, AccountId, (int)Instance);
            else
                id = String.Format(CultureInfo.InvariantCulture,"[{0}:{1}:{2}]", AccountTypeMapper.Instance[AccountType], (int)Universe, AccountId);
            return id;
        }
        /// <summary>
        /// Converts Steam id to its 64 bit format.
        /// </summary>
        /// <returns>Returns steam id in 64 bit format.</returns>
        public ulong ToSteamId64()
        {
            ulong id = 0;
            if (AccountType == AccountType.Individual)
                id = (ulong)76561197960265728 + AccountId;
            else
                id = ((ulong)Universe << 56) | ((ulong)AccountType << 52) | ((ulong)Instance <<32) | (ulong)AccountId;
            return id;
        }
        /// <summary>
        /// Converts Steam id to Community Url.
        /// </summary>
        /// <returns>Returns player's community Url(Profile Url).</returns>
        public Uri ToCommunityUrl()
        {
            string url = string.Empty;
            if (AccountType == AccountType.Individual)
                url = "http://steamcommunity.com/profiles/" + ToSteamId64();
            if(AccountType==AccountType.Clan)
                url = "http://steamcommunity.com/gid/" + ToSteamId64();
            return new Uri(url);
        }
        /// <summary>
        /// Fetches the Vanity url.
        /// </summary>
        /// <returns>Vanity url.</returns>
        public Uri GetVanityUrl()
        {
            string vanityUrl=string.Empty;
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(ToCommunityUrl());
            webRequest.AllowAutoRedirect = false;
            webRequest.Timeout = 10000;
            using (HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse())
            {
                if ((int)webResponse.StatusCode >= 300 && (int)webResponse.StatusCode <= 399)
                {
                    string url = webResponse.Headers["Location"];
                    if (!string.IsNullOrWhiteSpace(url))
                    {
                        if (url.EndsWith("/",StringComparison.OrdinalIgnoreCase))
                            url = url.Remove(url.Length - 1);
                        if (url.Contains('/'))
                        {
                            vanityUrl = url.Split('/').Last();
                            if (vanityUrl == ToSteamId64().ToString(CultureInfo.InvariantCulture))
                            {
                                vanityUrl = string.Empty;
                            }
                        }
                    }
                }                 
            }
            return new Uri(vanityUrl);
        }



    }
}
