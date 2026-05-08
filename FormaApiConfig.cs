namespace Formicae
{
    /// <summary>
    /// Centralized configuration for Autodesk Forma API endpoints and region support.
    /// Updated to align with Autodesk Platform Services (APS) Forma API v1 (2024-2025).
    /// </summary>
    public static class FormaApiConfig
    {
        /// <summary>
        /// Supported Forma data regions.
        /// US maps to developer.api.autodesk.com; EU maps to developer.api.eu.autodesk.com.
        /// </summary>
        public enum Region
        {
            US,
            EU
        }

        // Authentication (APS OAuth v2) - shared across regions
        public const string AuthBaseUrl = "https://developer.api.autodesk.com";
        public const string AuthorizePath = "/authentication/v2/authorize";
        public const string TokenPath = "/authentication/v2/token";

        // Forma API base URLs per region
        private const string ApiBaseUs = "https://developer.api.autodesk.com";
        private const string ApiBaseEu = "https://developer.api.eu.autodesk.com";

        // Forma-specific path prefixes (APS Forma Site Design API v1)
        public const string FormaApiPath = "/forma/v1";

        // Forma site design element endpoints (Forma Site Design API)
        public const string ElementsPath = FormaApiPath + "/elements";

        // Forma Integrate API (Beta) - import external geometry into Forma
        public const string IntegrateElementsPath = "/integrate/v1/elements";
        public const string IntegrateUploadPath = "/integrate/v1/upload";
        public const string IntegrateBatchElementsPath = "/integrate/v1/batch/elements";

        // Wind analysis endpoint
        public const string WindAnalysisPath = FormaApiPath + "/analyses/wind";

        // Required header name for region routing
        public const string RegionHeaderName = "x-ads-region";

        /// <summary>
        /// Returns the Forma API base URL for the given region.
        /// </summary>
        public static string GetApiBaseUrl(Region region)
        {
            return region == Region.EU ? ApiBaseEu : ApiBaseUs;
        }

        /// <summary>
        /// Returns the x-ads-region header value for the given region.
        /// </summary>
        public static string GetRegionHeaderValue(Region region)
        {
            return region == Region.EU ? "EMEA" : "US";
        }

        /// <summary>
        /// Builds the full URL for a Forma API endpoint given a region and path.
        /// </summary>
        public static string BuildUrl(Region region, string path)
        {
            return GetApiBaseUrl(region) + path;
        }
    }
}
