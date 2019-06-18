
// Application main
lefarm.appMain.controller('mainController', function ($scope, $rootScope, $location) {
    lefarm.appMain.controller('baseController', { $scope: $scope });
    $scope.slide = true;
    $scope.slideTo = function () {
        $scope.slide = false;
        setTimeout(function () {
            $scope.$apply(function () {
                $scope.slide = true;
            });
        }, 200);
    }
    $rootScope.$on("CallAnimationSlide", function (e) {
        $scope.slideTo();
    });

    $rootScope.$on("countItemsInCart", function () {
        var cartIds = lefarm.getCookie(lefarm.cartinfo.cartIds);
        var ids = cartIds ? cartIds.split(/,/) : new Array();
        $('.icon-cart #count').html(ids.length);
    });

    $rootScope.$on("addToMeta", function (eve, agrs) {
        var post = agrs[0];
        var path = agrs[1];
        lefarm.updateMeta(window.location.href, post.Name, post.Description, window.location.origin + path + post.Thumbnail);
    });
    //eve,agrs
    $rootScope.$on("addProductToCart", function (eve, agrs) {
        var id = agrs[0];
        var image = agrs[1];
        var name = agrs[2];
        var title = agrs[3];
        var quantity = agrs[4];
        var price = agrs[5];
        var goToCart = agrs[6];

        var cartIds = lefarm.getCookie(lefarm.cartinfo.cartIds);
        var ids = cartIds ? cartIds.split(/,/) : new Array();
        var cartQuantities = lefarm.getCookie(lefarm.cartinfo.cartQuantities);
        var quantities = cartQuantities ? cartQuantities.split(/,/) : new Array();

        //update item
        if (ids) {
            for (i = 0; i < ids.length; i++) {
                if (ids[i] == id) {
                    quantities[i] = parseInt(quantities[i], 10) + quantity;
                    lefarm.setCookie(lefarm.cartinfo.cartQuantities, quantities.join(','), lefarm.cartinfo.expired);

                    if (goToCart)
                    {
                        $location.path('/cart');
                        lefarm.scrollToTop();
                    }
                    return false;
                }
            }
        }
        //add new item
        lefarm.pushCookie(lefarm.cartinfo.cartIds, id, lefarm.cartinfo.expired);
        lefarm.pushCookie(lefarm.cartinfo.cartImages, image, lefarm.cartinfo.expired);
        lefarm.pushCookie(lefarm.cartinfo.cartNames, name, lefarm.cartinfo.expired);
        lefarm.pushCookie(lefarm.cartinfo.carttitles, title, lefarm.cartinfo.expired);
        lefarm.pushCookie(lefarm.cartinfo.cartQuantities, quantity, lefarm.cartinfo.expired);
        lefarm.pushCookie(lefarm.cartinfo.cartPrices, price, lefarm.cartinfo.expired);

        //count item in cart
        $rootScope.$emit("countItemsInCart", {});

        if (goToCart) {
            $location.path('/cart');
            lefarm.scrollToTop();
        }

    });
    $rootScope.$on("scrollToTop", function (eve, agrs) {
        lefarm.scrollToTop();
    })
    $rootScope.$emit("countItemsInCart");
});

lefarm.appMain.controller('headerController', function ($scope, $rootScope) {
    $scope.visible = true;
    $scope.hideHeader = function (e) {
        $scope.visible = e;
    }
    $rootScope.$on("CallHideHeader", function (e) {
        $scope.hideHeader(false);
    });
    $rootScope.$on("CallShowHeader", function (e) {
        $scope.hideHeader(true);
    });
});

lefarm.appMain.controller('breadCrumbController', function ($scope, $http, $routeParams, $rootScope, $location) {
    $scope.visible = true;
    $scope.hideHeader = function (e) {
        $scope.visible = e;
    }
    $rootScope.$on("CallHideBreadCrumb", function (eve) {
        $(".empty-div").addClass("hidden");
        $scope.hideHeader(false);
        setActiveMenu();
    });
    $rootScope.$on("CallShowBreadCrumb", function (eve, agrs) {

        $(".empty-div").addClass("hidden");
        $scope.hideHeader(true);
        $scope.items.getdata(agrs[0], agrs[1]);
        setActiveMenu();
    });

    $scope.items = {
        data: [],
        getdata: function (id, type) {
            //console.log($location.search());
            var url = "/sitemap/GetBreadCrumb/" + id + "/" + type;
            $Ajax.Post(url, null, function (result) {
                $scope.$apply(function () {
                    $scope.items.data = result;
                });
            });
        }
    };
});
lefarm.appMain.controller("bannerController", function ($scope, $http, $rootScope) {
    $scope.getImageBanner = function () {
        var url = "/image/GetImageBanner/-1";
        $Ajax.Get(url, null, function (result) {
            $scope.$apply(function () {
                //$scope.images = result;
                $scope.image = result[0];
            });
        });
    };
    $scope.getImageBanner();
});
lefarm.appMain.controller("homeController", function ($scope, $http, $rootScope) {
    $rootScope.$emit("CallShowHeader", {});
    $rootScope.$emit("CallHideBreadCrumb", {});
    $(".empty-div").removeClass("hidden");
    $scope.items = {
        data: [],
        category:[],
        //getImageBanner: function () {
        //    var url = "/image/GetImageBanner/-1";
        //    $Ajax.Post(url, null, function (result) {
        //        $scope.$apply(function () {
        //            $scope.images = result;
        //            setTimeout(function () {
        //                $('.bxslider').removeClass('hide');
        //                $('.bxslider').bxSlider();
        //            }, 500)
        //        });
        //    });
        //},
        getdata: function (currentPage, pageSize) {

            //Animation slide
            //$rootScope.$emit("CallAnimationSlide", {});
            var id='';
            var url = "/home/GetData";
            $Ajax.Post(url, null, function (result) {
                $scope.$apply(function () {
                    $scope.items.category = result;

                    for (i = 0; i < result.length; i++) {
                        id += "," + result[i].Id;
                    }

                    var filterValue = {
                        CurrentPage: currentPage,
                        PageSize: pageSize,
                        SortBy: 'CreateDTS',
                        status: 1 // item is active
                    }
                    var url = "/product/GetAllByCategory/" + id;
                    $Ajax.ShowLoading();
                    $Ajax.Post(url, filterValue, function (result) {
                        $scope.$apply(function () {
                            $scope.items.data = result.data;
                        });
                    });
                });
            });

            
        }
    };
    $scope.addToCart = function (id, image, name, title, quantity, price, goToCart) {
        //count item in cart
        $rootScope.$emit("addProductToCart", [id, image, name, title, quantity, price, goToCart]);

    };
    //$scope.items.getImageBanner();
    $scope.items.getdata(1, 8);
})

//lefarm.appMain.controller("newItemsController", function ($scope, $http, $rootScope) {
    
//    $scope.items = {
//        cat: null,
//        getdata: function (currentPage, pageSize) {
//            var filterValue = {
//                CurrentPage: currentPage,
//                PageSize: pageSize,
//                SortBy: 'CreateDTS',
//                status: 1 // item is active
//            }
//            var url = "/product/GetAllByCategory/0";
//            $Ajax.Post(url, filterValue, function (result) {
//                $scope.$apply(function () {
//                    $scope.items.data = result.data;
//                });
//            });

//        }
//    };
//    $scope.items.getdata(1, 4);
//})

lefarm.appMain.controller("categoryController", function ($scope, $http,$routeParams, $rootScope) {
    $rootScope.$emit("CallShowHeader", {});
    $rootScope.$emit("CallShowBreadCrumb", [$routeParams.id,"category"]);
    //lefarm.mainId = $routeParams.id;
    $scope.items = {
        data: [],
        getdata: function (id,currentPage, pageSize) {

            //Animation slide
            //$rootScope.$emit("CallAnimationSlide", {});
            id = $routeParams.id;
            currentPage = $scope.paging.currentPage;
            pageSize = $scope.paging.pageSize;

            var url = "/category/GetInfo/" + id;
            $Ajax.Post(url, filterValue, function (result) {
                $scope.$apply(function () {
                    $scope.cat = result;
                });
            });

            var filterValue = {
                CurrentPage: currentPage,
                PageSize: pageSize,
                SortBy: 'CreateDTS',
                status:1 // item is active
            }

            var url = "/product/GetAllByCategory/" + id;
            $Ajax.ShowLoading();
            $Ajax.Post(url, filterValue, function (result) {
                $scope.$apply(function () {
                    $scope.items.data = result.data;
                    //$scope.items.total = result.total;
                    $scope.paging.setPage($scope.paging.currentPage, result.total[id], $scope.paging.pageSize);
                });
            });
        }
    };
    //$scope.items.getdata($routeParams.id, lefarm.paging.currentPage, lefarm.paging.pageSize);
    //Get related items
    lefarm.paging.register($scope, $scope.items.getdata);
    //$scope.paging.setPage(1, 1, 4);
    $scope.paging.goTo(1);
})

lefarm.appMain.controller("adminManagementController", function ($scope, $http, $routeParams, $rootScope) {
    $scope.items = {
        getData: function () {

            var filterValue = {
                CurrentPage: $scope.paging.currentPage,
                PageSize: $scope.paging.pageSize,
                SortBy: 'CreateDTS'
            }
            var url = "/admin/GetAll/";
            $Ajax.Post(url, null, function (result) {
                $scope.$apply(function () {
                    $scope.items.data = result.data;
                    $scope.paging.setPage($scope.paging.currentPage, result.total, $scope.paging.pageSize);
                });
            });

        }
    };
    $scope.doDelete = function (id) {
        if (confirm("Bạn chắc chắn muốn xóa bản ghi này?")) {
            var url = "/admin/delete/" + id;
            $Ajax.Post(url, null, function (result) {
                $scope.items.getData();
            });
        }
        else
            return false;
    }
    //Register event
    lefarm.paging.register($scope, $scope.items.getData);
    //Auto get data
    $scope.paging.goTo(1);
})


lefarm.appMain.controller("categoryManagementController", function ($scope, $http, $routeParams, $rootScope) {
    $rootScope.$emit("CallShowHeader", {});
    $rootScope.$emit("CallShowBreadCrumb", [$routeParams.id, "category"]);
    //lefarm.mainId = $routeParams.id;
    $scope.items = {
        data: [],
        getData: function () {

            var filterValue = {
                CurrentPage: $scope.paging.currentPage,
                PageSize: $scope.paging.pageSize,
                SortBy: 'CreateDTS'
            }
            var url = "/category/GetAll";
            $Ajax.Post(url, null, function (result) {
                $scope.$apply(function () {
                    $scope.items.data = result.data;
                    $scope.paging.setPage($scope.paging.currentPage, result.total, $scope.paging.pageSize);
                });
            });
            
        }
    };
    $scope.doDelete = function (id) {
        if (confirm("Bạn chắc chắn muốn xóa bản ghi này?")) {
            var url = "/category/delete/" + id;
            $Ajax.Post(url, null, function (result) {
                $scope.items.getData();
            });
        }
        else
            return false;
    }
    //Register event
    lefarm.paging.register($scope, $scope.items.getData);
    //Auto get data
    $scope.paging.goTo(1);
})

lefarm.appMain.controller("searchController", function ($scope, $location, $rootScope) {
    $scope.doSearch = function () {
        $("#uiIconSearch").click();
        var value = $scope.value.replace(/\ /g, '-');
        $location.path('/search/' + value);
    }
});
lefarm.appMain.controller("searchResultController", function ($scope, $http, $routeParams, $rootScope) {
    $rootScope.$emit("scrollToTop");
    $rootScope.$emit("CallHideBreadCrumb", {});

    var realValue = $routeParams.value.replace(/\-/g, ' ');
    $(".search").val(realValue);
    $scope.items = {
        data: [],
        value: realValue,
        getdata: function (currentPage, pageSize) {
            currentPage = $scope.paging.currentPage;
            pageSize = $scope.paging.pageSize;
            var filterValue = {
                Value: realValue,
                CurrentPage: currentPage,
                PageSize: pageSize,
                SortBy: 'CreateDTS'

            }
            var url = "/product/SearchBy";
            $Ajax.ShowLoading();
            $Ajax.Post(url, filterValue, function (result) {
                $scope.$apply(function () {
                    $scope.items.data = result.data;
                    $scope.paging.setPage($scope.paging.currentPage, result.total, $scope.paging.pageSize);
                });

            });
        }

    };

    lefarm.paging.register($scope, $scope.items.getdata);
    $scope.paging.goTo(1);
})


lefarm.appMain.controller("productManagementController", function ($scope, $http, $routeParams, $rootScope) {
    $rootScope.$emit("CallShowHeader", {});
    $rootScope.$emit("CallShowBreadCrumb", [$routeParams.id, "product"]);
    //lefarm.mainId = $routeParams.id;
    $scope.selectCategory=0;
    $scope.items = {
        data: [],
        getData: function () {
            
            var filterValue = {
                CurrentPage: $scope.paging.currentPage,
                PageSize: $scope.paging.pageSize,
                SortBy: 'CreateDTS',
                status: 2 // all items
            }
            var catId=$scope.selectCategory;
            var url = "/product/GetAllByCategory/" + catId;
            $Ajax.ShowLoading();
            $Ajax.Post(url, filterValue, function (result) {
                $scope.$apply(function () {
                    $scope.items.data = result.data;
                    $scope.paging.setPage($scope.paging.currentPage, result.total[catId], $scope.paging.pageSize);
                });
            });
        }
    };
    $scope.category = {
        getData: function () {
            var url = "/category/GetAllActiveCategories";
            $Ajax.Post(url, null, function (result) {
                $scope.$apply(function () {
                    $scope.category.data = result;
                });
            });

        },
        onChange: function () {
            $scope.items.getData();
        }
    }
    $scope.doDelete = function (id) {
        if (confirm("Bạn chắc chắn muốn xóa bản ghi này?"))
        {
            var url = "/product/delete/" + id;
            $Ajax.Post(url, null, function (result) {
                $scope.items.getData();
            });
        }
        else
            return false;
    }
    

    $scope.category.getData();
    //Get related items
    lefarm.paging.register($scope, $scope.items.getData);
    //$scope.paging.setPage(1, 1, 4);
    $scope.paging.goTo(1);
})

lefarm.appMain.controller("imageController", function ($scope, $http, $routeParams, $rootScope) {
    $scope.items = {
        data: [],
        proId:0,
        getData: function () {
            var url = "/image/GetImageByProduct/" + $scope.items.proId;
            $Ajax.Post(url, null, function (result) {
                $scope.$apply(function () {
                    $scope.items.data = result;
                });
            });
        }
    };
    $scope.doInitialize = function (id) {
        $scope.items.proId = id;
        $scope.items.getData();
    }
    $scope.doDelete = function (id, name,path, requestFrom) {
        var params = {
            id: id,
            name: path + name
        }
        if (confirm("Bạn chắc chắn muốn xóa bản ghi này?")) {
            var url = "admin/image/delete/";
            $Ajax.Post(url, params, function (result) {
                if (requestFrom=='banner')
                    $scope.sliders.getData();
                else 
                    $scope.items.getData();
            });
        }
        else
            return false;
    }
    $scope.doSelected = function (event, ele, thumbnail) {
        $("p").removeClass("selected");
        $("#"+event).addClass('selected');
        var els = document.getElementsByName(ele)
        els[0].value = thumbnail;
    }
    $scope.sliders = {
        getData: function () {
            var url = "/image/GetImageBanner/-1";
            $Ajax.Post(url, null, function (result) {
                $scope.$apply(function () {
                    $scope.sliders.data = result;
                });
            });
        }
    };
    $scope.doUpdate = function (id, path, title) {
        var params = {
            Id: id,
            Path: path,
            Title:title
        }
        var url = "/admin/image/Update/";
        $Ajax.Post(url, params, function (result) {
            return result;
        });
    }
    //$scope.items.getData($scope.items.proId);
})

// Application sub
lefarm.appMain.controller("detailController", function ($scope, $routeParams, $rootScope, $location) {
    $rootScope.$emit("scrollToTop");
    $rootScope.$emit("CallShowBreadCrumb", [$routeParams.id,"product"]);    
    $scope.searchByHashtag = function (val) {
        $('.search').val(val);
        val = val.replace(/\ /g, '-');
        $location.path('/search/' + val);
    },
    $scope.items = {
        getData: function (id) {

            var url = "/product/GetDetail/" + id;
            $Ajax.ShowLoading();
            $Ajax.Post(url, null, function (result) {
                $scope.$apply(function () {
                    $scope.data = result;
                    //$rootScope.$emit("addToMeta", [result, lefarm.thumbPath]);
                });
            });
        },
        getUser: function (id) {

            //var url = "/user/GetUserByProduct/" + id;
            //$Ajax.Get(url, null, function (result) {
            //    $scope.$apply(function () {
            //        $scope.user = result;
            //    });
            //});
        },
        getImage: function (id) {
            var url = "/image/GetImageByProduct/" + id;
            $Ajax.Post(url, null, function (result) {
                $scope.$apply(function () {
                    $scope.images = result;
                    setTimeout(function () {
                        $(".lv-slider").lvSlider();
                        $(".lv-slider").children().removeClass('hide');
                    }, 300);
                });
            });
        },
        getHashtag: function (id) {
            var url = "/hashtag/GetHashtagByProduct/" + id;
            $Ajax.Post(url, null, function (result) {
                $scope.$apply(function () {
                    $scope.hashtags = result;                    
                });
            });
        },
        getRelatedData: function (currentPage, pageSize) {
            var filterValue = {
                CurrentPage: currentPage,
                PageSize: pageSize
            }
            url = "/product/GetRelatedItems/" + $routeParams.id;
            $Ajax.ShowLoading();
            $Ajax.Post(url, filterValue, function (result) {
                $scope.$apply(function () {
                    $scope.categories = result.data;
                    $scope.paging.setPage($scope.paging.currentPage, result.total, $scope.paging.pageSize,false);
                });
            });
        }

    };
    $scope.isMobile = function () {

        return isMobile();
    };
    $scope.addToCart = function (id, image, name, title, quantity, price, goToCart) {
        //count item in cart
        $rootScope.$emit("addProductToCart", [id, image, name, title, quantity, price, goToCart]);

    };    

    //Swipe action
    $scope.swipeRight = function () {
        $(".lv-slider").find('.lv-prev').each(function () {
            $(this).click();
        });
    }
    $scope.swipeLeft = function () {
        $(".lv-slider").find('.lv-next').each(function () {
            $(this).click();
        });
    }
    //Get data info
    $scope.items.getData($routeParams.id);
    $scope.items.getUser($routeParams.id);
    $scope.items.getImage($routeParams.id);
    $scope.items.getHashtag($routeParams.id);

    //Get related items
    lefarm.paging.register($scope, $scope.items.getRelatedData);
    $scope.paging.setPage(1, 1, 8);
    $scope.paging.goTo(1);
});

// Account controller

lefarm.appMain.controller("loginController", function ($scope, $location, $rootScope) {
    //Animation slide
    $rootScope.$emit("CallAnimationSlide", {});
    $rootScope.$emit("CallHideHeader", {});

    $scope.doLogin = function () {

        return $scope.validateData();
    }
    $scope.validateData = function () {
        return false;
    }
});
lefarm.appMain.controller("registerController", function ($scope, $location, $rootScope) {
    //Animation slide
    $rootScope.$emit("CallAnimationSlide", {});
    $rootScope.$emit("CallHideHeader", {});

    $scope.doRegister = function () {

        return $scope.validateData();
    }
    $scope.validateData = function () {
        return false;
    }
});
//End Account controller

// Contact controller
lefarm.appMain.controller("contactController", function ($scope, $location, $rootScope, $routeParams) {
    $rootScope.$emit("scrollToTop");
    $rootScope.$emit("CallShowBreadCrumb", [$routeParams.id == null ? 1 : $routeParams.id, "article"]);
    //$scope.getData = function () {
    //    var url = "/contactus/GetContact";
    //    $Ajax.Post(url, null, function (result) {
    //        $scope.$apply(function () {
    //            $scope.data = result;
    //        });
    //    });

    //};
    //$scope.getData();
});

// Footer controller
lefarm.appMain.controller("footerController", function ($scope, $location, $rootScope, $routeParams) {
    $scope.getData = function () {
        var url = "/ContactUs/GetContact";
        $Ajax.Post(url, null, function (result) {
            $scope.$apply(function () {
                $scope.data = result;
            });
        });
    };
    $scope.getMenu = function () {
        var url = "/sitemap/GetSitemap2";
        $Ajax.Post(url, null, function (result) {
            $scope.$apply(function () {
                $scope.sitemaps = result;
            });
        });
    };
    $scope.getData();
});


// About US controller
lefarm.appMain.controller("aboutController", function ($scope, $location, $rootScope, $routeParams) {
    $rootScope.$emit("scrollToTop");
    $rootScope.$emit("CallShowBreadCrumb", [$routeParams.id == null ? 1 : $routeParams.id, "article"]);

});
// End About US

lefarm.appMain.controller("brandManagementController", function ($scope, $location, $rootScope, $routeParams) {
    $scope.items = {
        getData: function () {
            var url = "/brand/GetAll";
            $Ajax.Post(url, null, function (result) {
                $scope.$apply(function () {
                    $scope.items.data = result;
                });
            });

        }
    };
    $scope.doDelete = function (id) {
        if (confirm("Bạn chắc chắn muốn xóa bản ghi này?")) {
            var url = "/brand/delete/" + id;
            $Ajax.Post(url, null, function (result) {
                $scope.items.getData();
            });
        }
        else
            return false;
    }
    $scope.items.getData();
});

lefarm.appMain.controller("originManagementController", function ($scope, $location, $rootScope, $routeParams) {
    $scope.items = {
        getData: function () {
            var url = "/origin/GetAll";
            $Ajax.Post(url, null, function (result) {
                $scope.$apply(function () {
                    $scope.items.data = result;
                });
            });

        }
    };
    $scope.doDelete = function (id) {
        if (confirm("Bạn chắc chắn muốn xóa bản ghi này?")) {
            var url = "/origin/delete/" + id;
            $Ajax.Post(url, null, function (result) {
                $scope.items.getData();
            });
        }
        else
            return false;
    }
    $scope.items.getData();
});

// Your cart
lefarm.appMain.controller("cartController", function ($scope, $http, $routeParams, $rootScope) {
    $rootScope.$emit("CallHideBreadCrumb", {});
    
    $scope.items = {
        data: [],
        getdata: function () {

            var cartIds = lefarm.getCookie(lefarm.cartinfo.cartIds);
            var cartImages = lefarm.getCookie(lefarm.cartinfo.cartImages);
            var cartNames = lefarm.getCookie(lefarm.cartinfo.cartNames);
            var cartTitles = lefarm.getCookie(lefarm.cartinfo.cartTitles);
            var cartQuantities = lefarm.getCookie(lefarm.cartinfo.cartQuantities);
            var cartPrices = lefarm.getCookie(lefarm.cartinfo.cartPrices);
            var yourName = lefarm.getCookie(lefarm.cartinfo.yourName);
            yourName = yourName != "null" ? yourName : "";
            var yourAddress = lefarm.getCookie(lefarm.cartinfo.yourAddress);
            yourAddress = yourAddress != "null" ? yourAddress : "";
            var yourPhone = lefarm.getCookie(lefarm.cartinfo.yourPhone);
            yourPhone = yourPhone != "null" ? yourPhone : "";
            var yourEmail = lefarm.getCookie(lefarm.cartinfo.yourEmail);
            yourEmail = yourEmail != "null" ? yourEmail : "";

            var ids= cartIds ? cartIds.split(/,/) : new Array();
            var images= cartImages ? cartImages.split(/,/) : new Array();
            var names = cartNames ? cartNames.split(/,/) : new Array
            var titles = cartTitles ? cartTitles.split(/,/) : new Array();
            var quantities = cartQuantities ? cartQuantities.split(/,/) : new Array();
            var prices = cartPrices ? cartPrices.split(/,/) : new Array();

            var carts = [];
            var cartInfo = [];
            for (a = 0; a < ids.length; a++) {
                var array = { "ProId": ids[a], "Image": images[a], "ProName": names[a], "Title": titles[a], "Quantity": quantities[a], "Price": prices[a] };
                carts.push(array);
            }
            cartInfo = { carts: carts, yourName: yourName, yourAddress: yourAddress, yourPhone: yourPhone, yourEmail: yourEmail };
            $scope.items.data = JSON.parse(JSON.stringify(cartInfo));

            //count item in cart
            $rootScope.$emit("countItemsInCart");

            $(window).scrollTop(0);
        }

    };
    $scope.deteteItem = function (number) {
        var cartIds = lefarm.getCookie(lefarm.cartinfo.cartIds);
        var cartImages = lefarm.getCookie(lefarm.cartinfo.cartImages);
        var cartNames = lefarm.getCookie(lefarm.cartinfo.cartNames);
        var cartTitles = lefarm.getCookie(lefarm.cartinfocartTitles);
        var cartQuantities = lefarm.getCookie(lefarm.cartinfo.cartQuantities);
        var cartPrices = lefarm.getCookie(lefarm.cartinfo.cartPrices);

        var ids = cartIds ? cartIds.split(/,/) : new Array();
        var images = cartImages ? cartImages.split(/,/) : new Array();
        var names = cartNames ? cartNames.split(/,/) : new Array
        var titles = cartTitles ? cartTitles.split(/,/) : new Array();
        var quantities = cartQuantities ? cartQuantities.split(/,/) : new Array();
        var prices = cartPrices ? cartPrices.split(/,/) : new Array();

        for (i = 0; i < ids.length; i++) {
            if (ids[i] === number) {

                //delete item in cookie
                ids.splice(i, 1);
                images.splice(i, 1);
                names.splice(i, 1);
                titles.splice(i, 1);
                quantities.splice(i, 1);
                prices.splice(i, 1);

                //delete item in data
                $scope.items.data.carts.splice(i, 1);

                break;
            }
        }

        lefarm.setCookie(lefarm.cartinfo.cartIds, ids.join(','), lefarm.cartinfo.expired);
        lefarm.setCookie(lefarm.cartinfo.cartImages, images.join(','), lefarm.cartinfo.expired);
        lefarm.setCookie(lefarm.cartinfo.cartNames, names.join(','), lefarm.cartinfo.expired);
        lefarm.setCookie(lefarm.cartinfo.cartTitles, titles.join(','), lefarm.cartinfo.expired);
        lefarm.setCookie(lefarm.cartinfo.cartQuantities, quantities.join(','), lefarm.cartinfo.expired);
        lefarm.setCookie(lefarm.cartinfo.cartPrices, prices.join(','), lefarm.cartinfo.expired);

        //count item in cart
        $rootScope.$emit("countItemsInCart");
    };

    $scope.doSubmit = function () {
        if (!$("#formAdd").valid()) {
            $(window).scrollTop(0);
            return false;
        }

        lefarm.setCookie(lefarm.cartinfo.yourName, $scope.items.data.yourName, lefarm.cartinfo.expired);
        lefarm.setCookie(lefarm.cartinfo.yourAddress, $scope.items.data.yourAddress, lefarm.cartinfo.expired);
        lefarm.setCookie(lefarm.cartinfo.yourPhone, $scope.items.data.yourPhone, lefarm.cartinfo.expired);
        lefarm.setCookie(lefarm.cartinfo.yourEmail, $scope.items.data.yourEmail, lefarm.cartinfo.expired);

        var url = "/cart/SubmitToCart";
        $Ajax.ShowLoading();
        $Ajax.Post(url, $scope.items.data, function (result) {
            $scope.$apply(function () {
                if(result=='true')
                {
                    lefarm.deleteAllCookies("cart");
                    //count item in cart
                    $rootScope.$emit("countItemsInCart");

                    $scope.goToStep(3);
                    return false;
                }
            });
        });
    }
    $scope.updatCart = function (index, quantity) {
        var cartQuantities = lefarm.getCookie(lefarm.cartinfo.cartQuantities);
        var quantities = cartQuantities ? cartQuantities.split(/,/) : new Array();

        //update item
        quantities[index] = quantity;
        lefarm.setCookie(lefarm.cartinfo.cartQuantities, quantities.join(','), lefarm.cartinfo.expired);
    };

    $scope.goToStep = function (step) {
        $('.step1').addClass('hidden');
        $('.step2').addClass('hidden');
        var cartIds = lefarm.getCookie(lefarm.cartinfo.cartIds);

        if (step == 3) {
            $('#step-info').html('Hoàn tất: Đơn hàng đang được xử lý. Cảm ơn ' + lefarm.getCookie(lefarm.cartinfo.yourName) + ' !');
        }
        else if (cartIds == null || cartIds == '') {

            $('#step-info').html('Không có sản phẩm nào trong giỏ hàng của bạn.');
        } else {
            if (step == 1) {
                $('#step-info').html('Bước 1: Danh sách sản phẩm');
                $('.step1').removeClass('hidden');
            }
            else {
                $('#step-info').html('Bước 2: Thông tin người nhận');
                $('.step2').removeClass('hidden');
            }
        }
    }
    $scope.goToStep(1);
    $scope.items.getdata();
});
lefarm.appMain.controller("fbCtrl", function ($scope, $http, $routeParams, $rootScope) {
    $scope.share = function (post, path) {
        // remove tags from html string
        var r = /<\/?[^>]+>/gi;
        var des = post.ShortDescription != null ? post.ShortDescription.replace(r, ""): post.Description;

        FB.ui({
            method: 'share',
            name: post.Name,
            title: post.Name,
            href: window.location.href,
            link: window.location.href,// Returns full URL
            picture: window.location.origin + path + post.Thumbnail,
            caption: window.location.host, // Returns path only
            description: des,
            message: 'message'
        });
    }
});

lefarm.appMain.controller("adminManagementController", function ($scope, $http, $routeParams, $rootScope) {
    $scope.items = {
        getData: function () {

            var filterValue = {
                CurrentPage: $scope.paging.currentPage,
                PageSize: $scope.paging.pageSize,
                SortBy: 'CreateDTS'
            }
            var url = "/admin/user/GetAll/";
            $Ajax.Post(url, null, function (result) {
                $scope.$apply(function () {
                    $scope.items.data = result.data;
                    $scope.paging.setPage($scope.paging.currentPage, result.total, $scope.paging.pageSize);
                });
            });

        }
    };
    $scope.doDelete = function (id) {
        if (confirm("Bạn chắc chắn muốn xóa bản ghi này?")) {
            var url = "/admin/user/delete";
            var params = { id: id };
            $Ajax.Post(url, params, function (result) {
                $scope.items.getData();
            });
        }
        else
            return false;
    }
    //Register event
    lefarm.paging.register($scope, $scope.items.getData);
    //Auto get data
    $scope.paging.goTo(1);
})

lefarm.appMain.directive('lazy', function ($timeout) {

    return {
        restrict: 'C',
        link: function (scope, elm, attrs) {
            $timeout(function () {
                $(elm).lazy({
                    effect: "fadeIn",
                    effectTime: 1000,
                    threshold: 0,
                    beforeLoad: function (element) {
                        //debugger;
                        //element.addClass('lazyloading');
                        //console.log('image "' + element.data('src') + '" is about to be loaded');
                    },
                    afterLoad: function (element) {
                        //element.removeClass('lazyloading');
                        //console.log('image "' + element.data('src') + '" was loaded successfully');
                    },
                    //defaultImage: "http://jquery.eisbehr.de/lazy/images/loading.gif"
                    defaultImage: "/Images/empty.png"
                });
            }, 500);
        }
    }
})