
$(function () {

});

$(document).ready(function () {

    var homePageUrl = "";
    var arr1 = document.getElementsByTagName("input");
    for (i = 0; i < arr1.length; i++) {
        if (arr1[i].className == "homePageUrl") {
            homePageUrl = arr1[i].value;
        }
    }
    var a = $("input#search").autocomplete({
        autoFocus: true,
        source: function (request, response) {
            $.ajax({
                url: homePageUrl,
                dataType: "json",
                data: {
                    term: request.term,
                    ajax: 1,
                    cmd: 'SiteSearchResults'
                },

                success: function (data) {
                    if (data) {
                        response(data);
                    }
                }
            })
        },
        minLength: 2,
        select: function (event, ui) {
            //need to stop input control being refreshed
            event.preventDefault(); // or return false;
            //substitute in the main site area url in place of shared item url
            var urlString = ui.item.resultUrl;
            urlString = ConvertFacilityUrl(urlString);
            urlString = ConvertClassUrl(urlString);
            //now append the search term used as querystring to enable save
            if ($("input#search").length) {
                var jumpLink = '';
                if (urlString.indexOf('#') >= 0) {
                    jumpLink = urlString.substr(urlString.indexOf('#'));
                    urlString = urlString.substr(0, urlString.indexOf('#'));
                }
                if (urlString.indexOf('?') >= 0) {
                    urlString = urlString + '&ste=' + $("input#search").val();
                }
                else {
                    urlString = urlString + '?ste=' + $("input#search").val();
                }
                urlString = urlString + jumpLink;
            }

            window.location.href = urlString;
            return false;
        },
        focus: function (event, ui) {
            event.preventDefault(); // or return false;
        },
        keypress: function (event, ui) {
            event.preventDefault(); // or return false;
        },
        open: function (event, ui) {        //swaps out the special characters that jquery puts in to escape html
            $("ul.ui-autocomplete li a").each(function () {
                var htmlString = $(this).html().replace(/&lt;/g, '<');
                htmlString = htmlString.replace(/&gt;/g, '>');
                $(this).html(htmlString);
            });
        }
    })


    var b = $("input#memberloginclub").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: homePage,
                dataType: "json",
                data: {
                    term: request.term,
                    ajax: 1,
                    cmd: 'ClubNameList'
                },

                success: function (data) {
                    if (data) {
                        response(data);
                    }
                }
            })
        },
        minLength: 2,
        select: function (event, ui) {
            //need to stop input control being refreshed
            event.preventDefault(); // or return false;
            var urlString = ui.item.memberLoginUrl;
            window.location.href = urlString;
            return false;
        },
        focus: function (event, ui) {
            event.preventDefault(); // or return false;
        },
        keypress: function (event, ui) {
            event.preventDefault(); // or return false;
        },
        open: function (event, ui) {        //swaps out the special characters that jquery puts in to escape html
            $("ul.ui-autocomplete li a").each(function () {
                var htmlString = $(this).html().replace(/&lt;/g, '<');
                htmlString = htmlString.replace(/&gt;/g, '>');
                $(this).html(htmlString);
            });
        }
    })
});
