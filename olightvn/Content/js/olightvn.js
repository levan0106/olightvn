
// create the module and name it scotchApp
// also include ngRoute for all our routing needs
var olightvn = jQuery.extend(olightvn, {
    appMain: angular.module('appMain', ['ngTouch', 'ngRoute']),// 'ngAnimate'
    paging: {
        pageSize: 16,
        currentPage: 1,
        isPageEnd: false,
        pagingUseScrolling: function (callBackFunc) {
            $(window).off("scroll");
            $(window).scroll(function () {

                olightvn.fixedMenuOnTop(this);

                if ($(window).scrollTop() == $(document).height() - $(window).height()) {

                    if (!olightvn.isLoading && !olightvn.paging.isPageEnd) {
                        olightvn.paging.currentPage++;
                        var pSize = olightvn.paging.pageSize * olightvn.paging.currentPage;
                        if (callBackFunc)
                            callBackFunc(olightvn.paging.currentPage, pSize);
                    }
                }
            });
            //this.resetPaging();
        },
        resetPaging: function () {
            olightvn.paging.pageSize = 16, olightvn.paging.currentPage = 1, olightvn.paging.isPageEnd = false
        },
        register: function ($scope, $callBackfunction) {
            $scope.paging = {
                currentPage: olightvn.paging.currentPage,
                totalPage: 1,
                pageSize: olightvn.paging.pageSize,
                range: [],
                isScrollToTop: true,
                next: function () {
                    $scope.paging.currentPage += 1;
                    $scope.paging.goTo($scope.paging.currentPage);
                },
                previous: function () {
                    $scope.paging.currentPage -= 1;
                    $scope.paging.goTo($scope.paging.currentPage);
                },
                goTo: function (index) {
                    if ($scope.paging.isScrollToTop) {
                        olightvn.scrollToTop();
                    }
                    $scope.paging.currentPage = index;
                    $callBackfunction($scope.paging.currentPage, $scope.paging.pageSize);
                },
                setPage: function (index, total, size, scrollToTop) {
                    $scope.paging.isScrollToTop = scrollToTop == null ? true : scrollToTop;
                    var d = total % size;
                    var totalPage = Math.floor(total / size);
                    if (d > 0)
                        totalPage += 1;

                    $scope.paging.currentPage = index;
                    $scope.paging.totalPage = totalPage;
                    $scope.paging.pageSize = size;
                    $scope.paging.range = [];
                    for (i = 1; i <= totalPage; i++) {
                        $scope.paging.range.push(i);
                    }

                }
            }
        }
    },
    cartinfo: {
        expired: 40,
        cartIds: "cartIds",
        cartImages: "cartImages",
        cartNames: "cartNames",
        cartTitles: "cartTitles",
        cartQuantities: "cartQuantities",
        cartPrices: "cartPrices",
        yourName: "yourName",
        yourAddress: "yourAddress",
        yourPhone: "yourPhone",
        yourEmail: "yourEmail",
    },
});

// Setup the filter
olightvn.appMain.filter('myDate', function ($filter) {

    // Create the return function and set the required parameter name to **input**
    // setup optional parameters for the currency symbol and location (left or right of the amount)
    return function (input) {
        // Ensure that we are working with a number
        if (input == undefined || input == null || input == '') {
            return input;
        } else {
            var dateParts = input.split("/");

            var dateObject = $filter('date')(new Date(dateParts[2].substring(0, 4), dateParts[1] - 1, dateParts[0]), 'dd/MM/yyyy'); // month is 0-based
            return dateObject;

        }
    }

});
// Convert value
olightvn.appMain.filter('convertTrueFalse', function ($filter) {
    return function (data, valTrue, valFalse) {
        if (parseInt(data) == 1)
            return valTrue;
        return valFalse
    }
})

olightvn.appMain.filter("trust", ['$sce', function ($sce) {
    return function (htmlCode) {
        return $sce.trustAsHtml(htmlCode);
    }
}]);
olightvn.appMain.filter('totalSumPriceQty', function () {
    return function (data, key1, key2) {
        if (angular.isUndefined(data) || angular.isUndefined(key1) || angular.isUndefined(key2))
            return 0;
        var sum = 0;
        angular.forEach(data, function (value) {
            sum = sum + (parseInt(value[key1] == '' ? 0 : value[key1]) * parseInt(value[key2] == '' ? 0 : value[key2]));
        });
        return sum;
    }
})
olightvn.paging.resetPaging();


var olightvn = jQuery.extend(olightvn, {
    Ajax: {
        _showLoading: false,
        ShowLoading: function () {
            this._showLoading = true;
        },
        Request: function (type, url, param, callBackSuccess, callBackError) {
            $.ajax({
                type: type,
                url: olightvn.APIUrl + url,
                data: param == null || param.length == 0 ? null : JSON.stringify(param),
                contentType: "application/json",
                cache: true,
                //contentType: "application/json; charset=utf-8",
                beforeSend: function () {

                    ////Force reset menu to default on mobile site
                    //olightvn.reLoadMenu();

                    if (olightvn.Ajax._showLoading) {
                        olightvn.Loading.Show();

                        //Reset show loading = false
                        olightvn.Ajax._showLoading = false
                    }

                },
                statusCode: {
                    404: function () {
                        alert('page not found');
                    },
                },
                success: function (data, textStatus, request) {
                    if (callBackSuccess)
                        callBackSuccess(data, textStatus, request);
                },
                error: function (request, textStatus, errorThrown) {
                    if (callBackError)
                        callBackError(request, textStatus, errorThrown);
                    olightvn.Loading.Hide();
                }
            }).done(function () { olightvn.Loading.Hide(); });
        },
        Post: function (url, param, callBackSuccess, callBackError) {
            olightvn.Ajax.Request('POST', url, param, callBackSuccess, callBackError);
        },
        Get: function (url, param, callBackSuccess, callBackError) {
            olightvn.Ajax.Request('GET', url, param, callBackSuccess, callBackError);
        },
        Put: function (url, param, callBackSuccess, callBackError) {
            olightvn.Ajax.Request('PUT', url, param, callBackSuccess, callBackError);
        },
        Delete: function (url, param, callBackSuccess, callBackError) {
            olightvn.Ajax.Request('DETELE', url, param, callBackSuccess, callBackError);
        }
    }
});

olightvn = jQuery.extend(olightvn, {
    isLoading: false,
    Loading: {
        Show: function () {
            olightvn.isLoading = true;
            $('#loading').addClass('loading');
        },
        Hide: function () {
            $('#loading').removeClass('loading');
            setTimeout(function () {
                olightvn.isLoading = false;
            }, 300);
        }
    },
    reLoadMenu: function () {
        $('.navbar-collapse').removeClass('in');
    },
    fixedMenuOnTop: function (e) {
        $(window).scroll(function () {
            if ($(e).scrollTop() > 60) {
                $('.content-menu').addClass('fixed-menu');
            } else {
                $('.content-menu').removeClass('fixed-menu');
            }
        })
    },
    scrollToTop: function () {
        $('body,html').animate({
            scrollTop: 0
        }, 300);
    },
    disableScrolling: function () {
        $(window).off("scroll");
    },
    enableScrolling: function () {
        $(window).on("scroll");
    },
    // Set cookie
    pushCookie: function (name, value, expires, path, domain, secure) {
        var cookie = olightvn.getCookie(name);
        var array = cookie ? cookie.split(/,/) : new Array();
        array.push(value);

        olightvn.setCookie(name, array.join(','), expires, path, domain, secure);
    },
    setCookie: function (name, value, expires, path, domain, secure) {
        //delete old cookie before insert new cookie
        if (olightvn.getCookie(name))
            olightvn.deleteCookie(name, path, domain);

        //set expire day	
        var d = new Date();
        d.setTime(d.getTime() + (expires * 24 * 60 * 60 * 1000));

        document.cookie = escape(name) + "=" + escape(value) +
        ((expires == null) ? "" : "; expires=" + d.toUTCString()) +
        ((path == null) ? "" : "; path=" + path) +
        ((domain == null) ? "" : "; domain=" + domain) +
        ((secure == null) ? "" : "; secure");
    },

    // Read cookie
    getCookie: function (name) {
        var cname = escape(name) + "=";
        var dc = document.cookie;
        if (dc.length > 0) {
            begin = dc.indexOf(cname);
            if (begin != -1) {
                begin += cname.length;
                end = dc.indexOf(";", begin);
                if (end == -1) end = dc.length;
                return unescape(dc.substring(begin, end));
            }
        }
        return null;
    },

    //delete cookie
    deleteCookie: function (name, path, domain) {
        if (olightvn.getCookie(name)) {
            document.cookie = name + "=" +
            ((path == null) ? "" : "; path=" + path) +
            ((domain == null) ? "" : "; domain=" + domain) +
            "; expires=Thu, 01-Jan-70 00:00:01 GMT";
        }
    },
    deleteAllCookies: function (cookieName) {
        var cookies = document.cookie.split(";");

        for (var i = 0; i < cookies.length; i++) {
            var cookie = cookies[i];
            var eqPos = cookie.indexOf("=");
            var name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
            if (!cookieName || name.indexOf("cart") > -1) {

                document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
            }
        }
    },
});

var $Ajax = olightvn.Ajax;
$(document).ready(function () {
    $(document).click(function (e) {
        //Force reset menu to default on mobile site
        olightvn.reLoadMenu();
    });


});

// bootstrap datepicker: vitalets.github.io/bootstrap-datepicker

$().ready(function () {
    $(".content-menu ul li").click(function () {
        var a = $(this).find("a");
        if (a)
            a[0].click();
    })
    // Back history //
    $("#backLink").click(function (event) {
        event.preventDefault();
        history.back(1);
    });
})

// Scroll to top //
$(function () {
    $('#sttop').fadeOut();
    $(window).scroll(function () {
        if ($(this).scrollTop() > 300) {
            $('#sttop').fadeIn();
        } else {
            $('#sttop').fadeOut();
        }

        if (isMobile()) {
            if ($(this).scrollTop() > 120) {
                $('.btn-order').fadeOut();
            } else {
                $('.btn-order').fadeIn();
            }
        }
    });
    $('#sttop').click(function () {
        $('body,html').animate({
            scrollTop: 0
        }, 800);
    });
});


// Select active menu item //////
function setActiveMenu() {
    var pgurl = window.location.hash;
    var baseUrl = window.location.href;
    $("ul.menu li a").each(function () {
        $(this).removeClass("active");
        var urlMenu = $(this).attr("href");
        if (baseUrl == urlMenu || (pgurl == "/#/" & urlMenu == "/#/"))
            $(this).parents("li").find('a').addClass("active");
        else if (pgurl.indexOf(urlMenu) >= 0 && urlMenu != "#/") {
            $(this).parents("li").find('> a').addClass("active");
        }
    })
}

// Check mobile //
function isMobile() {
    if (/Android|webOS|iPhone|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent))
        return true;
    else
        return false;
}

// CKEditor //
function createEditor(languageCode, id, height) {
    CKEDITOR.env.isCompatible = true;
    var editor = CKEDITOR.replace(id, {
        language: languageCode,
        height: height == null ? '400px' : height
    });
    //editor = CKEDITOR.replace('editor1', { htmlEncodeOutput: true });
}
// End CKEditor //

function DeleteConfirm() {
    if (confirm("Bạn chắc chắn muốn xóa bản ghi này?"))
        return true;
    else
        return false;
}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

function goBack() {
    window.history.back();
    return false;
}
// Input floatl
$(document).ready(function () {
    setTimeout(function () {
        $(".floatl-input").each(function () {
            if ($(this).val().length > 0)
                $(this).parents(".floatl").addClass("floatl-active");
        });
    }, 100);

    $(".floatl-input").focusin(function () {
        $(this).parents(".floatl").addClass("floatl-active");
    });
    $(".floatl-input").focusout(function () {
        if ($(this).val().length == 0)
            $(this).parents(".floatl").removeClass("floatl-active");
    })
});
//End input floatl

//Read file from file preview
olightvn = jQuery.extend(olightvn, {
    file: {
        readURL: function (input) {
            var files = input.files;
            if (input.files) {
                var name = [];
                var size = [];
                for (i = 0; i < input.files.length; i++) {
                    if (input.files[i]) {
                        name.push(input.files[i].name);
                        size.push(input.files[i].size);

                        var reader = new FileReader();

                        reader.readAsDataURL(input.files[i]);
                        reader.onload = function (e) {

                            var file = { Name: '', Base64: '', Type: '', Size: 0 };
                            //set file info
                            var id = size.indexOf(e.total);
                            file.Name = name[id];
                            //file.Type = type;
                            file.Size = size[id];
                            file.Base64 = e.target.result;

                            $("img").removeClass("selected");

                            //render image preview
                            var img = "<div class='img-list'> <img class='thumbnail selected' onclick='olightvn.file.deSelect(this)' code='[code]' id='fileN' src='[src]'/><a onclick='olightvn.file.doDelete(this);'>x</a></div>";
                            img = img.replace('[src]', e.target.result).replace('[code]', e.target.result).replace(new RegExp('fileN', 'g'), file.Name);
                            $("#uiImages").append(img);

                        }
                    }
                }
            }
        },
        doDelete: function (id) {
            $(id).parents('.img-list').remove();
        },

        deSelect: function (ele) {
            $("img").removeClass("selected");
            $(ele).addClass('selected');
        },

        getFileCollections: function () {
            var fileCollections = [];
            $("#uiImages").find("img").each(function () {
                var file = { Name: '', Base64: '', Type: '', Size: 0, Selected: false };
                file.Name = $(this).attr("id");
                file.Base64 = $(this).attr("code");
                file.Selected = $(this).hasClass("selected");
                fileCollections.push(file);
            })

            $("#uifileBase64").val(JSON.stringify(fileCollections));
        }
    }
});


//End file preview