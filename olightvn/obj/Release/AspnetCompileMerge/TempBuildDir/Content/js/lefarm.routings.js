
//Showing Routing  
lefarm.appMain.config(['$routeProvider', '$locationProvider',
function ($routeProvider, $locationProvider) {

    //$routerProvider code here
    $routeProvider.when('/',
                        {
                            templateUrl: 'Home/main'
                        });
    $routeProvider.when('/lv/detail/:id/:title',
                        {
                            templateUrl: function(params){
                                return '/Product/Detail/' + params.id
                            },
                            controller: 'detailController'
                        });
    $routeProvider.when('/detail/:id/:title',
                        {
                            templateUrl: function (params) {
                                return '/Product/Detail/' + params.id
                            },
                            controller: 'detailController'
                        });
    $routeProvider.when('/product/add',
                        {
                            templateUrl: 'Product/Add',
                            controller: 'addProductController'
                        });
    $routeProvider.when('/lv/cat/:id/:title',
                        {
                            templateUrl: 'Category/Index',
                            controller: 'categoryController'
                        });
    $routeProvider.when('/cat/:id/:title',
                        {
                            templateUrl: 'Category/Index',
                            controller: 'categoryController'
                        });
    $routeProvider.when('/search/:value',
                        {
                            templateUrl: 'Product/Search',
                            controller: 'searchResultController'
                        });
    $routeProvider.when('/attr/category',
						{
						    templateUrl: 'Filter/Category',
						    controller: 'attrCategoryController'
						});
    $routeProvider.when('/attr/location',
						{
						    templateUrl: 'Filter/Location',
						    controller: 'attrLocationController'
						});
    $routeProvider.when('/attr/filtering',
						{
						    templateUrl: 'Filter/Filtering',
						    controller: 'attrFilteringController'
						});
    $routeProvider.when('/login',
						{
						    templateUrl: 'Account/Signin',
						    controller: 'loginController'
						});
    $routeProvider.when('/register',
						{
						    templateUrl: 'Account/Register',
						    controller: 'registerController'
						});
    $routeProvider.when('/lien-he',
						{
						    templateUrl: 'ContactUs/index',
						    controller: 'contactController'
						});
    $routeProvider.when('/gioi-thieu',
						{
						    templateUrl: 'Article/aboutus',
						    controller: 'aboutController'
						});
    $routeProvider.when('/admin',
						{
						    templateUrl: 'Admin/login',
						    //controller: 'registerController'

						});
    $routeProvider.when('/cart',
						{
						    templateUrl: 'cart/index',
						    controller: 'cartController'
						});
    $routeProvider.otherwise(
                        {
                            redirectTo: '/'
                        });

    $locationProvider.html5Mode(true);
    //$locationProvider.hashPrefix('');

}]);