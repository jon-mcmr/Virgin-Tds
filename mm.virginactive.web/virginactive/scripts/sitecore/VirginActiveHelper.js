/*!
* Virgin Active Helper -Library
* McCormack and Morrison
* Date: Tue 27 July 2011
*/

//Substitute Facility URL -convert from Shared Content url to main site url
function ConvertFacilityUrl(urlString) {
    if (urlString.indexOf("sitecore/content/shared-club-content/Facilities") > -1) {
        var urlRoot = urlString.substr(0, urlString.indexOf("sitecore"));
        var subdirectories = urlString.split("/");
        if (subdirectories.length > 3) {
            var module = subdirectories[subdirectories.length - 1].replace(".aspx", "");
            var section = subdirectories[subdirectories.length - 2];
            urlString = urlRoot.concat("facilities-and-classes/facilities/facility.aspx?section=", section, "#", module);
        }
    }
    return urlString;
}

//Substitute Class URL -convert from Shared Content url to main site url
function ConvertClassUrl(urlString) {
    if (urlString.indexOf("sitecore/content/shared-club-content/Classes") > -1) {
        var urlRoot = urlString.substr(0, urlString.indexOf("sitecore"));
        var subdirectories = urlString.split("/");
        if (subdirectories.length > 3) {
            var subsection = subdirectories[subdirectories.length - 2];
            var section = subdirectories[subdirectories.length - 3];
            urlString = urlRoot.concat("facilities-and-classes/classes/class-listing.aspx?section=", section, "#", subsection);
        }
    }
    return urlString;
}