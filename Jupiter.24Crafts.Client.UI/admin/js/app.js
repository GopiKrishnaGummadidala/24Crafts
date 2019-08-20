var app = angular.module('admincraft', ['ui.router']);
app.config(
  ['$stateProvider', '$urlRouterProvider',
    function ($stateProvider,   $urlRouterProvider) {

      $stateProvider
        .state("home", {
          url: "/home",
          views: {
                'home': {
                    templateUrl: '/admin/templates/dashboard.html',
                    controller: 'dashboardCrtl'
                }
            }
        })
        .state("singleuser", {
          url: "/singleuser",
          views: {
                'home': {
                    templateUrl: '/admin/templates/singleuser.html',
                    controller: 'singleuserCrtl'
                }
            }
        })
        .state("bussinesuser", {
          url: "/bussinesuser",
          views: {
                'home': {
                    templateUrl: '/admin/templates/bussinesuser.html',
                    controller: 'bussinesuserCrtl'
                }
            }
        })

        .state("singleuserimage", {
          url: "/singleuserimage",
          views: {
                'home': {
                    templateUrl: '/admin/templates/singleuserimage.html',
                    controller: 'singleuserImageCrtl'
                }
            }
        })
        .state("bussinesuserimage", {
          url: "/bussinesuserimage",
          views: {
                'home': {
                    templateUrl: '/admin/templates/bussinesuserimage.html',
                    controller: 'bussinesuserImageCrtl'
                }
            }
        })
        .state("activeprofiles", {
          url: "/activeprofiles",
          views: {
                'home': {
                    templateUrl: '/admin/templates/activeprofiles.html',
                    controller: 'activeprofilesCrtl'
                }
            }
        })
        .state("statprofiles", {
          url: "/statprofiles",
          views: {
                'home': {
                    templateUrl: '/admin/templates/statprofiles.html',
                    controller: 'statprofilesCrtl'
                }
            }
        })
        
        
        .state("languages", {
          url: "/languages",
          views: {
                'home': {
                    templateUrl: '/admin/templates/languages.html',
                    controller: 'languagesCrtl'
                }
            }
        })
         .state("spam", {
          url: "/spam",
          views: {
                'home': {
                    templateUrl: '/admin/templates/spam.html',
                    controller: 'spamCrtl'
                }
            }
        })
        .state("opportunites", {
          url: "/opportunites",
          views: {
                'home': {
                    templateUrl: '/admin/templates/opportunites.html',
                    controller: 'opportunitesCrtl'
                }
            }
        })
          .state("professions", {
          url: "/professions",
          views: {
                'home': {
                    templateUrl: '/admin/templates/professions.html',
                    controller: 'professionsCrtl'
                }
            }
        })

        

        
    }
  ]
);
app.controller('mainCrtl', function($scope) {
    $scope.appName = '24Crafts';
    $('.nav.menu > li').click(function(){
        $('.active').removeClass('active');
        $(this).addClass('active');
    })
});
app.controller('dashboardCrtl', function($scope,httpService) {
    $scope.dashBoardData = {imageCount:'145',appCount:'25',spamCount:'15'};
    $scope.toCount = [{message:'sampel1'},{message:'sampel1'},{message:'sampel1'},{message:'sampel1'},{message:'sampel1'}];
});
app.controller('singleuserCrtl', function($scope,httpService) {
    $scope.toCount = [];
    httpService.getSingleuser().then(function(response){
       $scope.toCount =  response.data['Response'];
    });
    $scope.currentobject = {};
     $scope.dataChanged = function(item){
        console.log(item);
        $scope.name = item.FirstName+' '+item.LastName;
        $scope.comment = '';
        currentobject = item;
        
    }  
    $scope.updateChanged = function(item){
        var mystatus = $scope.status=='true'?true:false
        var url = '/api/common/ContentApproverApprovesUser?approvedBy=1&userId='+currentobject.UserId+'&approved='+mystatus+'&comments='+$scope.comment;
        httpService.getCall(url).then(function(response) {
            alert('Data Changed Succefully!!');
            var index = $scope.toCount.indexOf(currentobject);
            $scope.toCount.splice(index, 1);  
            $('#myModal .close').click();
           
        });;
    }  
});
app.controller('bussinesuserCrtl', function($scope,httpService) {
    $scope.toCount = [];
    httpService.getBussinesuser().then(function(response){
       $scope.toCount =  response.data['Response'];
    });
    $scope.currentobject = {};
     $scope.dataChanged = function(item){
        console.log(item);
        $scope.name = item.FirstName+' '+item.LastName;
        $scope.comment = '';
        currentobject = item;
        
    }  
    $scope.updateChanged = function(item){
        var mystatus = $scope.status=='true'?true:false
        var url = '/api/common/ContentApproverApprovesUser?approvedBy=1&userId='+currentobject.UserId+'&approved='+mystatus+'&comments='+$scope.comment;
        httpService.getCall(url).then(function(response) {
            alert('Data Changed Succefully!!');
            var index = $scope.toCount.indexOf(currentobject);
            $scope.toCount.splice(index, 1);  
            $('#myModal .close').click();
           
        });;
    }   
});

app.controller('singleuserImageCrtl', function($scope,httpService) {

     $scope.toCount = [];
    httpService.getBussinesuser().then(function(response){
       $scope.toCount =  response.data['Response'];
    });
    $scope.currentobject = {};
     $scope.dataChanged = function(item){
        console.log(item);
        $scope.name = item.FirstName+' '+item.LastName;
        $scope.comment = '';
        currentobject = item;
        
    }  
    $scope.updateChanged = function(item){
        var mystatus = $scope.status=='true'?true:false
        var url = '/api/common/ApprovesUserImages?approvedBy=1&userId='+currentobject.UserId+'&approved='+mystatus+'&comments='+$scope.comment;
        httpService.getCall(url).then(function(response) {
            alert('Data Changed Succefully!!');
            var index = $scope.toCount.indexOf(currentobject);
            $scope.toCount.splice(index, 1);  
            $('#myModal .close').click();
           
        });;
    }
});
app.controller('bussinesuserImageCrtl', function($scope,httpService) {
     $scope.toCount = [];
    httpService.getBussinesuser().then(function(response){
       $scope.toCount =  response.data['Response'];
    });
     $scope.currentobject = {};
     $scope.dataChanged = function(item){
        console.log(item);
        $scope.name = item.FirstName+' '+item.LastName;
        $scope.comment = '';
        currentobject = item;
        
    }  
    $scope.updateChanged = function(item){
        var mystatus = $scope.status=='true'?true:false
        var url = '/api/common/ApprovesUserImages?approvedBy=1&userId='+currentobject.UserId+'&approved='+mystatus+'&comments='+$scope.comment;
        httpService.getCall(url).then(function(response) {
            alert('Data Changed Succefully!!');
            var index = $scope.toCount.indexOf(currentobject);
            $scope.toCount.splice(index, 1);  
            $('#myModal .close').click();
           
        });;
    }
    
});
app.controller('activeprofilesCrtl', function($scope,httpService) {
     $scope.toCount = [];
    httpService.getactiveuser().then(function(response){
       $scope.toCount =  response.data['Response'];
    });
});
app.controller('opportunitesCrtl', function($scope,httpService) {
     $scope.toCount = [];
    httpService.getopportunites().then(function(response){
       $scope.toCount =  response.data['Response'];
    });
});

app.controller('spamCrtl', function($scope,httpService) {
     $scope.toCount = [];
    httpService.getspams().then(function(response){
       $scope.toCount =  response.data['Response'];
    });
});

app.controller('statprofilesCrtl', function($scope,httpService) {
     $scope.toCount = [];
    httpService.getstatprofiles().then(function(response){
       $scope.toCount =  response.data['Response'];
    });
});


app.controller('languagesCrtl', function($scope,httpService) {

    $scope.toCount = [];
    httpService.getAllLanguages().then(function(response){
       $scope.toCount =  response.data['Response'];
    });
     $scope.dataChanged = function(item){
        console.log(item);
        item.isActive = !($('#status-'+item.LanguageId).prop('checked'));
        httpService.addData('/api/Portfolio/SaveLanguage',item).then(function(response) {});
    }
    $scope.deleteData = function(item){
        console.log(item)
        var r = confirm("Sure we want to Delted !! "+item.LanguageName);
        if (r == true) {
            httpService.deleteData('/api/Portfolio/DeleteLanguage?languageId='+item.LanguageId);
            var index = $scope.toCount.indexOf(item);
            $scope.toCount.splice(index, 1);  
        }
    }
    $scope.addData = function(){
        data = {
                    LanguageId : 0,
                    LanguageName : $scope.name,
                    isActive : $scope.status=='true'?true:false
                }
        httpService.addData('/api/Portfolio/SaveLanguage',data).then(function(response) {
            alert('Data Inserted Succefully!!');
            httpService.getAllLanguages().then(function(response){
                $scope.toCount =  response.data['Response'];
                $('#myModal .close').click();
            });
        });;
    }
});

app.controller('professionsCrtl', function($scope,httpService) {

    $scope.toCount = [];
    httpService.getAllProfessions().then(function(response){
       $scope.toCount =  response.data['Response'];
    });
    $scope.dataChanged = function(item){
        console.log(item);
    }
    $scope.deleteData = function(item){
        console.log(item)
        var r = confirm("Sure we want to Delted !! "+item.ProfessionName);
        if (r == true) {
            httpService.deleteData('/api/Portfolio/DeleteProfession?professionId='+item.ProfessionId);
            var index = $scope.toCount.indexOf(item);
            $scope.toCount.splice(index, 1);  
        }
    }
    $scope.addData = function(){
        data = {
                ProfessionId : 0,
                ProfessionName : $scope.name,
                isActive : $scope.status=='true'?true:false
            }
        httpService.addData('/api/Portfolio/SaveProfession',data).then(function(response) {
            alert('Data Inserted Succefully!!');
            httpService.getAllProfessions().then(function(response){
                $scope.toCount =  response.data['Response'];
                $('#myModal .close').click();
            });
        });;
       
        
         
    }
});
app.factory('httpService', ['$http', '$q',function ($http, $q) {

    var service ={};

    service.getDashboard = function(){
         $http.get("/api/")
        .then(function(response) {
            $scope.myWelcome = response.data;
        });
    }
    service.getopportunites =  function(){
         return $http.get("/api/Opportunities/AllOpportunitesForApproval");
    }
    service.getspams = function(){
        return $http.get("/API/Common/GetAllSpam");
    }
    service.getactiveuser = function(){
         return $http.get("/api/common/GetAllActiveProfiles");
    }

    service.getstatprofiles = function(){
         return $http.get("/api/common/GetStatsOpportunitiesInfo");
    }
    
    service.getSingleuser = function(){
         return $http.get("/api/common/getAllUsersForContentApproval?profileType=1");
    }

    service.getBussinesuser = function(){
        return $http.get("/api/common/getAllUsersForContentApproval?profileType=2")
    }

    service.getSingleuserimage = function(){
         return $http.get("/api/common/getAllUsersImagesForApproval?profileType=1");
    }

    service.getBussinesuserimage = function(){
        return $http.get("/api/common/getAllUsersImagesForApproval?profileType=")
    }
    
     service.getSingleuserImage = function(){
         $http.get("/api/")
    }
     service.getBussinesuserImage = function(){
         $http.get("/api/")
        .then(function(response) {
            $scope.myWelcome = response.data;
        });
    }
    service.getAllLanguages = function(){
         return $http.get("/api/Portfolio/getAllLanguages?isAdmin=true");
    }
    service.getCall = function(url){
         return $http.get(url); 
    }
    service.deleteData = function(url){
         return $http.get(url).then(function(response) {
            alert('Data Deleted Succefully!!');
        });
    }

     service.addData = function(url,data){
         return $http.post(url,data);
        
    }
    
    service.getAllProfessions = function(){
         return $http.get("/api/Portfolio/getAllProfessions?isAdmin=true");
        
    }
    return service;

}]);