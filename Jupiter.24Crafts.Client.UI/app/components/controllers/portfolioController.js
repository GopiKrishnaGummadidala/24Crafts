'use strict';
app.controller('portfolioController', ['$scope', '$state', '$rootScope', 'portfolioService','authService','countryStateCityService','notificationService',
function ($scope, $state, $rootScope, portfolioService, authService, countryStateCityService, notificationService) {
    //variable declartion
    $scope.userId = authService.authentication.userId;;
    $scope.date = moment.tz('America/Toronto');
    $scope.datepickerConfig = {
        allowFuture: true,
        dateFormat: "DD/MM/YYYY"
    };
    $scope.nextSlide = function ($event) {
        $($event.target).parent().toggleClass("pass-slide");
        $($event.target).parent().toggleClass("active");
        $($event.target).parent().next().toggleClass("active");
        $($event.target).parent().next().removeClass("prev-slide");
    }
    $scope.prevSlide = function ($event) {
        $($event.target).parent().toggleClass("prev-slide");
        $($event.target).parent().toggleClass("active");
        $($event.target).parent().prev().toggleClass("active");
        $($event.target).parent().prev().toggleClass("pass-slide");
    }

    //empty object declartion
    $scope.pfMyself = {};
    $scope.pfProfession = {};
    $scope.pfRenumeration = {};
    $scope.pfContactDetails = {};
    $scope.pfImagesAndVideos = {};
    $scope.pfBusiness = {};
    $scope.countries = {};
    $scope.states = {};
    $scope.cities = {};
    $scope.phoneCodes = [];
    $scope.businessObj = {
        enableBusiness: false,
        showBusiness: false
    };
    $scope.profilePicObj = {};
    $scope.busProfilePicObj = {};
    $scope.mulipleImagePaths = [];
    $scope.multipleVideoPaths = [];
    $scope.isContentModified = false;
    $scope.validateProfilePic = false;
    $scope.validateOldProfilePicOrNot = '';
    $scope.busValidateProfilePic = false;
    $scope.imageLayoutVisiblity = true;
    $scope.uploadMorePics = false;
    $scope.videosLayoutVisiblity = true;
    $scope.videoUploadMorePics = false;

    $rootScope.$on('busValidateProfilePicCall', function (index, value) {
        $scope.busValidateProfilePic = value;
    });
    $rootScope.$on('validateProfilePicCall', function (index, value) {
        $scope.validateProfilePic = value;
    });
    $rootScope.$on('profileObj', function (index, value) {
        $scope.profilePicObj = value;
    })

    //model object declartion
    $scope.pfMyselfModel = {
        userName: "",
        Gender: 0,
        age:'',
        martialStatus: "",
        language: [],
        hobbies: '',
        description: '',
        userUpload:''
    }; 
    $scope.pfProfessionModel = {
        highestQual: '',
        profileType: '',
        professionType: [],
        chooseMonths: '',
        chooseYears:''
    };
    $scope.pfRenumerationModel = {
        currentAssignment: '',
        lastAssignment: '',
        remuneration: '',
        date: '',
        toDate: '',
        country: '',
        state: '',
        city:'',
    };
    $scope.pfImagesAndVideosModel = {
        images:[],
        videos: [],
        googleDrive: '',
        youtubeChannel: '',
        profileDisable:''
    };
    $scope.pfContactDetailsModel = {
        email: '',
        phoneNumber: '',
        countryCodes :''
    };
    $scope.pfBusinessModel = {
        name: '',
        phoneno: '',
        email:'',
        currentAssignment:'',
        googleDriveUrl: '',
        imagePath: '',
        businessUserId:'',
    };
    $scope.PutResponseData = function (data) {
        if (data.userInfoObj != null) {
            //country binding and calling selected event 
            $scope.pfRenumerationModel.country = parseInt(data.userInfoObj.Country);
            $scope.countryChangeEvent(parseInt(data.userInfoObj.Country));

            //state binding and calling selected event 
            $scope.pfRenumerationModel.state = parseInt(data.userInfoObj.State);
            $scope.stateChangeEvent(parseInt(data.userInfoObj.State));

            $scope.pfMyselfModel.Gender = data.userInfoObj.Gender === null ? 0 : data.userInfoObj.Gender;
            $scope.pfMyselfModel.age = data.userInfoObj.Age;
            $scope.pfMyselfModel.martialStatus = data.userInfoObj.MaritalStatus;
            $scope.pfMyselfModel.hobbies = data.userInfoObj.Hobbies;
            $scope.pfMyselfModel.description = data.userInfoObj.SelfDescription;

            $scope.pfProfessionModel.highestQual = data.userInfoObj.Qualification;
            $scope.pfProfessionModel.profileType = data.userInfoObj.PrimaryProfessionId;
            $scope.pfProfessionModel.chooseMonths = data.userInfoObj.ExpMonth;
            $scope.pfProfessionModel.chooseYears = data.userInfoObj.ExpYear;

            $scope.pfRenumerationModel.currentAssignment = data.userInfoObj.CurrAssignment;
            $scope.pfRenumerationModel.lastAssignment = data.userInfoObj.LastAssignment;
            $scope.pfRenumerationModel.remuneration = data.userInfoObj.Remunaration;
            $scope.pfRenumerationModel.date = convertDateFormat(data.userInfoObj.AvailabilityFrom);
            $scope.pfRenumerationModel.toDate = convertDateFormat(data.userInfoObj.AvailabilityTo);
            $scope.pfRenumerationModel.city = parseInt(data.userInfoObj.City);
            $scope.pfImagesAndVideosModel.googleDrive = data.userInfoObj.GoogleDriveUrl;
            $scope.pfImagesAndVideosModel.youtubeChannel = data.userInfoObj.YputubeChannel;
            $scope.pfImagesAndVideosModel.profileDisable = data.userInfoObj.IsActive;
        }
        if (data.BusinessObj != null)
        {
            //bind data to business model
            $scope.pfBusinessModel.name =data.BusinessObj.Name;
            $scope.pfBusinessModel.phoneno = parseInt(data.BusinessObj.PhoneNum);
            $scope.pfBusinessModel.email = data.BusinessObj.EmailID;
            $scope.pfBusinessModel.currentAssignment = data.BusinessObj.CurrentAssignment;
            $scope.pfBusinessModel.googleDriveUrl = data.BusinessObj.GoogleDriveUrl;
            $scope.pfBusinessModel.businessUserId = data.BusinessObj.BusinessUserId;
        }
        $scope.pfMyselfModel.userName = data.FirstName;
        $scope.pfMyselfModel.language = data.LanguageInfo;
        $scope.pfProfessionModel.professionType = data.ProfessionInfo;
        $scope.pfContactDetailsModel.email = data.EmailID;
        $scope.pfContactDetailsModel.phoneNumber = parseInt(data.PhoneNum);

        if (data.countryCodes != null && data.countryCodes != '')
        {
            $scope.pfContactDetailsModel.countryCodes = data.countryCodes;
          // $('#countryCodeSelect').val(data.countryCodes);
        }
       

        if (data.BusinessObj != null)
        {
            //enable checkbox hide
            $scope.businessObj.enableBusiness = true;
            $scope.businessObj.showBusiness = false;
        }
        else if(data.ProfileType === 1){
            $scope.businessObj.showBusiness = true;
        }
        if (data.ProfilePicPathUrl != '')
        {
            $dropimg.css('background-image', 'url(' + data.ProfilePicPathUrl + ')');
            $dropimg.css('height', '200px');
            $scope.pfMyselfModel.userUpload = data.ProfilePicPathUrl;
            $scope.validateOldProfilePicOrNot = data.ProfilePicPathUrl;
            //if (data.ProfilePicPathUrl.indexOf('DefaultProfile') != -1)
               // $scope.validateProfilePic = true;
            //else
               $scope.validateProfilePic = false;
        }
      
        
        //$scope.pfMyselfModel.userUpload =null;
        if (data.ImagesPathUrls.length > 0)
        {
            $scope.imageLayoutVisiblity = false;
        }
        $scope.mulipleImagePaths = data.ImagesPathUrls;

        if (data.VideoPathUrls.length > 0) {
            $scope.videosLayoutVisiblity = false;
        }
        $scope.multipleVideoPaths = data.VideoPathUrls;
        
    }; 
    $scope.portfolioValidateForm={
        myself: false,
        profession: false,
        renumeration: false,
        contactDetails: false,
        imagesAndVideos: false
    }
    $scope.portfolio = {
        myself: true,
        profession: true,
        renumeration: true,
        contactDetails: true,
        imagesAndVideos: true
    };
    

    //Intialization
    $scope.init = {
        Gender: true,
        language:true
    }
    function convertDateFormat(input) {
        if (input != null) {
            var date = new Date(input);
            var month = (date.getMonth() + 1).toString();
            month = month.length > 1 ? month : '0' + month;
            var day = date.getDate().toString();
            day = day.length > 1 ? day : '0' + day;
            return day + '/' + month + '/' + date.getFullYear();
        } else {
            return "";
        }
    }
    //Events
    $scope.updateSelection = function (position, entities) {
        angular.forEach(entities, function (subscription, index) {
            if (position != index)
                subscription.checked = false;
        });
    };
    $scope.structureTheCommaSeparteStringForLanguage = function () {
        var languageIds = '';
        for (var i in $scope.pfMyselfModel.language) {
            if ($scope.pfMyselfModel.language[i].isSelected === true) {
                languageIds += $scope.pfMyselfModel.language[i].LanguageId + ',';
            }
        }
        return languageIds.replace(/,\s*$/, "");
    };
    $scope.structureTheCommaSeparteStringForProfession = function () {
        var professionIds = '';
        for (var i in $scope.pfProfessionModel.professionType) {
            if ($scope.pfProfessionModel.professionType[i].isSelected === true) {
                professionIds += $scope.pfProfessionModel.professionType[i].ProfessionId + ',';
            }
        }
        return professionIds.replace(/,\s*$/, "");
    };
    $rootScope.$on('portfolioimageCall', function (index, value) {
        $scope.pfImagesAndVideosModel.images = value;
    });
    $rootScope.$on('portfoliovideoCall', function (index, value) {
        $scope.pfImagesAndVideosModel.videos = value;
    });
    $scope.enableIndivudualEvent = function () {
        if ($scope.businessObj.showBusiness)
        {
            $('.individual-tab').trigger('click');
        }
    };

    //City-State-country-calls
    $scope.countryChangeEvent = function (id) {
        if (id != undefined) {
            $scope.getStates(id);
        }
        else {
            $scope.states = {};
            $scope.cities = {};
        }
    }
    $scope.stateChangeEvent = function (id) {
        if (id != undefined)
            $scope.getCities(id);
        else
            $scope.cities = {};
    }

   
    //validate profile pic 
    function validateProfilePicUploadOrNot() {
        $scope.validateProfilePic = $('.image_preview').css('background-image') === 'none' ? true : false;
        return $scope.validateProfilePic;
    }
    function businessValidateProfilePicUploadOrNot() {
        $scope.busValidateProfilePic = ($('#bus_inputFile').val() === '') ? true : false;
        return $scope.busValidateProfilePic;
    }
    

    //myself
    $scope.mySelf = function (valid) {
        
        if (valid && !validateProfilePicUploadOrNot()) {
            //Add upload profile pic object to myself model
            //$scope.pfMyselfModel.userUpload = $('#userUpload').fileinput('getFileStack');
            console.log($scope.pfMyselfModel);

            //diable and enable form functionality
            $scope.portfolioValidateForm.myself = valid;
            $('#profession-fieldset').css("opacity", 1);
            $scope.portfolio.profession = false;

            //next page validation
            $scope.nextClick(pfMyself);
        }
        else {
            //diable and enable form functionality
            $scope.portfolioValidateForm.myself = valid;
            $('#profession-fieldset').css("opacity", 0.5);
            $scope.portfolio.profession = true;
        }
    }

    //profession
    $scope.profession = function (myselfValid,professionValid) {
        if (myselfValid && professionValid && !validateProfilePicUploadOrNot()) {
            console.log($scope.pfProfessionModel);
            $scope.portfolioValidateForm.profession = true;
            $scope.portfolio.renumeration = false;
            $('#remuneration-fieldset').css("opacity", 1);
            $scope.nextClick(pfProfession);
        }
        else {
            $scope.portfolioValidateForm.profession = false;
            $('#remuneration-fieldset').css("opacity", 0.5);
            $scope.portfolio.renumeration = true;
        }
    }
    $scope.previousProfessionClick = function () {
        $scope.prevClick(btnPfProfession);
    }

    //renumeration
    $scope.renumeration = function (myselfValid, professionValid,renumertionValid) {
        if (myselfValid && professionValid && renumertionValid && !validateProfilePicUploadOrNot()) {
            console.log($scope.pfRenumerationModel);
            $scope.portfolioValidateForm.renumeration = true;
            $scope.portfolio.contactDetails = false;
            $('#contact-fieldset').css("opacity", 1);
            $scope.nextClick(pfRenumeration);
        }
        else {
            $scope.portfolioValidateForm.renumeration = false;
            $('#contact-fieldset').css("opacity", 0.5);
            $scope.portfolio.contactDetails = true;
        }
    }
    $scope.previousRenumerationClick = function () {
        $scope.prevClick(btnPfRenumeration);
    }
    
    //contactDetails
    $scope.contactDetails = function (myselfValid, professionValid, renumertionValid, contactValid) {
        if (myselfValid && professionValid && renumertionValid && contactValid && !validateProfilePicUploadOrNot()) {
            console.log($scope.pfContactDetailsModel);
            $scope.portfolioValidateForm.contactDetails = true;
            $scope.portfolio.imagesAndVideos = false;
            $('#image-fieldset').css("opacity", 1);
            $scope.nextClick(pfContactDetails);
        }
        else {
            $scope.portfolioValidateForm.contactDetails = false;
            $('#image-fieldset').css("opacity", 0.5);
            $scope.portfolio.imagesAndVideos = true;
        }
    }
    $scope.previousContactDetailsClick = function () {
        $scope.prevClick(btnPfContactDetails);
    }
   
    //ImagesAndVideos
    $scope.deleteImage = function (path,event) {
        $scope.$emit('dynamic-loading', true);
        portfolioService.DeleteImageByPath(path).then(function (res) {
            $('.delete-image' + event.currentTarget.id).remove();
            for (var item in $scope.mulipleImagePaths) {
                if ($scope.mulipleImagePaths[item] === path) {
                    $scope.mulipleImagePaths.splice(item, 1);
                }
            }
            if ($scope.mulipleImagePaths.length <= 0)
            {
                $scope.imageLayoutVisiblity = true;
            }
            $scope.$emit('dynamic-loading', false);
        }, function (reason) {
            $scope.$emit('dynamic-loading', false);
            notificationService.displayError('Delete Image Failed.');
            console.log('ERROR' + reason);
        });
    }
    $scope.deleteVideo = function (path, event) {
        $scope.$emit('dynamic-loading', true);
        portfolioService.DeleteImageByPath(path).then(function (res) {
            $('.delete-video' + event.currentTarget.id).remove();
            for (var item in $scope.multipleVideoPaths) {
                if ($scope.multipleVideoPaths[item] === path) {
                    $scope.multipleVideoPaths.splice(item, 1);
                }
            }
            if ($scope.multipleVideoPaths.length <= 0) {
                $scope.videosLayoutVisiblity = true;
            }
            $scope.$emit('dynamic-loading', false);
        }, function (reason) {
            $scope.$emit('dynamic-loading', false);
            notificationService.displayError('Delete Video Failed.');
            console.log('ERROR' + reason);
        });
    }
    $scope.showImageUploadLayout = function (event) {
        if ($('#' + event.currentTarget.id).is(':checked')) {
            $scope.imageLayoutVisiblity = true;
        }
        else {
            $scope.imageLayoutVisiblity = false;
        }
    };
    $scope.showVideoUploadLayout = function (event) {
        if ($('#' + event.currentTarget.id).is(':checked')) {
            $scope.videosLayoutVisiblity = true;
        }
        else {
            $scope.videosLayoutVisiblity = false;
        }
    };
    $scope.ImagesAndVideos = function (valid) {
        if (valid) {
            $scope.pfImagesAndVideosModel.images = $('#imageupload').fileinput('getFileStack');
            $scope.pfImagesAndVideosModel.videos = $('#videoupload').fileinput('getFileStack');
            console.log($scope.pfImagesAndVideosModel);
            $scope.portfolioValidateForm.imagesAndVideos = valid;
            var request = {
                FirstName: $scope.pfMyselfModel.userName,
                UserID: parseInt(authService.authentication.userId),
                LanguageIds: $scope.structureTheCommaSeparteStringForLanguage(),
                ProfessionIds: $scope.structureTheCommaSeparteStringForProfession(),
                ProfilePicturePath: $scope.pfMyselfModel.userUpload,
                //ProfilePicturePath :null,
                //MultipleImageUploadPaths: $scope.pfImagesAndVideosModel.images,
                //MultipleVideoUploadPaths: $scope.pfImagesAndVideosModel.videos,
                ImagePathUrls: null,
                VideoPathUrls: null,
                EmailID: $scope.pfContactDetailsModel.email,
                PhoneNum: $scope.pfContactDetailsModel.phoneNumber,
                countryCodes: $scope.pfContactDetailsModel.countryCodes,
                BusinessObj: null,
                IsEmailApproved: false,
                IsBusinessEnabled: false,
                NumOfOpportunitiesCreated: 0,
                Views: 0,
                IsProfileCreated: false,
                IsPaymentUser: false,
                IsAdminApproved: false,
                IsContentApproverApproved: false,
                ProfileType: null,
                IsActive: $scope.pfImagesAndVideosModel.profileDisable,
                IsImageVideoContentModified:true,
                userInfoObj: {
                    Gender: parseInt($scope.pfMyselfModel.Gender),
                    Age: parseInt($scope.pfMyselfModel.age),
                    MaritalStatus: parseInt($scope.pfMyselfModel.martialStatus),
                    Hobbies: $scope.pfMyselfModel.hobbies,
                    SelfDescription: $scope.pfMyselfModel.description,
                    Qualification: $scope.pfProfessionModel.highestQual,
                    ExpYear: parseInt($scope.pfProfessionModel.chooseYears),
                    ExpMonth: parseInt($scope.pfProfessionModel.chooseMonths),
                    Remunaration: $scope.pfRenumerationModel.remuneration,
                    CurrAssignment: $scope.pfRenumerationModel.currentAssignment,
                    LastAssignment: $scope.pfRenumerationModel.lastAssignment,
                    AvailabilityFrom: $scope.pfRenumerationModel.date,
                    AvailabilityTo: $scope.pfRenumerationModel.toDate,
                    City: parseInt($scope.pfRenumerationModel.city),
                    State: parseInt($scope.pfRenumerationModel.state),
                    Country: parseInt($scope.pfRenumerationModel.country),
                    PrimaryProfessionId: parseInt($scope.pfProfessionModel.profileType),
                    GoogleDriveUrl: $scope.pfImagesAndVideosModel.googleDrive,
                    YputubeChannel: $scope.pfImagesAndVideosModel.youtubeChannel,
                }
            };
            //preparing upload data
            var formData = new FormData();
            formData.append('ProfilePicturePath', $scope.profilePicObj);
            formData.append('customerId', authService.authentication.customerId);
            //validating old or new uploads
            if ($scope.mulipleImagePaths.length > 0 || $scope.multipleVideoPaths.length > 0 || $scope.validateOldProfilePicOrNot != '') {
                formData.append('isContentModified', true);
            }
            else {
                formData.append('isContentModified', false);
            }
            //preparing images object
            for (var i in $scope.pfImagesAndVideosModel.images) {
                formData.append('ImagePathUrls'+i, $scope.pfImagesAndVideosModel.images[i]);
            }
            //preparing videos object
            for (var i in $scope.pfImagesAndVideosModel.videos) {
                formData.append('VideoPathUrls'+i, $scope.pfImagesAndVideosModel.videos[i]);
            }
            $scope.CreateProfile(request,formData);
        }
        else {
            $scope.portfolioValidateForm.imagesAndVideos = valid;
        }
        console.log($scope.pfImagesAndVideosModel);
    }
    $scope.previousImagesAndVideos = function () {
        $scope.prevClick(btnPfImagesAndVideos);
    }
    

    //$scope.userUploadCall = function (param) {
    //    $scope.pfMyselfModel.userUpload = param;
    //}
   
   
    //jquery events
    $scope.animating = false;
    $scope.current_fs = '';
    var left, opacity, scale;
    $scope.next_fs = '', $scope.previous_fs;
    $scope.nextClick = function (id) {
        if ($scope.animating) return false;
        $scope.animating = true;

        $scope.current_fs = angular.element(id).parent();
        $scope.next_fs = angular.element(id).parent().next();

        //activate next step on progressbar using the index of next_fs
        $("#progressbar li").eq($("fieldset").index($scope.next_fs)).addClass("active");

        //show the next fieldset
        $scope.next_fs.show();
        //hide the current fieldset with style
        $scope.current_fs.animate({ opacity : 0 }, {
            step: function (now, mx) {
                //as the opacity of current_fs reduces to 0 - stored in "now"
                //1. scale current_fs down to 80%
                scale = 1 - (1 - now) * 0.2;
                //2. bring next_fs from the right(50%)
                left = (now * 50) + "%";
                //3. increase opacity of next_fs to 1 as it moves in
                opacity = 1 - now;
                $scope.current_fs.css({
                    'transform': 'scale(' + scale + ')',
                    'position': 'absolute'
                });
                $scope.next_fs.css({ 'left': left, 'opacity': opacity });
            },
            duration: 800,
            complete: function () {
                $scope.current_fs.hide();
                $scope.animating = false;
            },
            //this comes from the custom easing plugin
            easing: 'easeInOutBack'
        });
    };
    $scope.prevClick = function (id) {
        if ($scope.animating) return false;
        $scope.animating = true;

        $scope.current_fs = angular.element(id).parent().parent();
        $scope.previous_fs = angular.element(id).parent().parent().prev();

        //de-activate current step on progressbar
        $("#progressbar li").eq($("fieldset").index($scope.current_fs)).removeClass("active");

        //show the previous fieldset
        $scope.previous_fs.show();
        //hide the current fieldset with style
        $scope.current_fs.animate({ opacity: 0 }, {
            step: function (now, mx) {
                //as the opacity of current_fs reduces to 0 - stored in "now"
                //1. scale previous_fs from 80% to 100%
                scale = 0.8 + (1 - now) * 0.2;
                //2. take current_fs to the right(50%) - from 0%
                left = ((1 - now) * 50) + "%";
                //3. increase opacity of previous_fs to 1 as it moves in
                opacity = 1 - now;
                $scope.current_fs.css({ 'left': left });
                $scope.previous_fs.css({ 'transform': 'scale(' + scale + ')', 'opacity': opacity });
            },
            duration: 800,
            complete: function () {
                $scope.current_fs.hide();
                $scope.animating = false;
            },
            //this comes from the custom easing plugin
            easing: 'easeInOutBack'
        });
    };
    
    $('#progressbar li .counter-ele').click(function () {
        $.each($('#progressbar li'), function (index, value) {
            $($('#progressbar li')[index]).removeClass('active');
        });
        $(this).parent().addClass('active');
        if ($(this).parent().find('.filedName').html() == "Myself") {
            $('#myself-fieldset').css({
                "display": "block",
                "opacity": 1,
                "left": 0,
                "position": "relative",
                "transform": "none"
            });
            $('#profession-fieldset').css('display', 'none');
            $('#remuneration-fieldset').css('display', 'none');
            $('#contact-fieldset').css('display', 'none');
            $('#image-fieldset').css('display', 'none');
        }
        else if ($(this).parent().find('.filedName').html() == "Profession") {
            $(this).parent().addClass('active');
            $('#profession-fieldset').css({
                "display": "block",
                "opacity": 1,
                "left": 0,
                "position": "relative",
                "transform": "none"
            });
            $('#myself-fieldset').css('display', 'none');
            $('#remuneration-fieldset').css('display', 'none');
            $('#contact-fieldset').css('display', 'none');
            $('#image-fieldset').css('display', 'none');
            if($scope.portfolioValidateForm.myself)
            {
                $('#profession-fieldset').css("opacity", 1);
                $scope.portfolio.profession = false;
            }
            else {
                $('#profession-fieldset').css("opacity", 0.5);
                $scope.portfolio.profession = true;
            }   
        }
        else if ($(this).parent().find('.filedName').html() == "Renumeration") {
            $(this).parent().addClass('active');
            $('#profession-fieldset').css('display', 'none');
            $('#myself-fieldset').css('display', 'none');
            $('#remuneration-fieldset').css({
                "display": "block",
                "opacity": 1,
                "left": 0,
                "position": "relative",
                "transform": "none"
            });
            $('#contact-fieldset').css('display', 'none');
            $('#image-fieldset').css('display', 'none');
            if($scope.portfolioValidateForm.myself && $scope.portfolioValidateForm.profession) {
                $('#remuneration-fieldset').css("opacity", 1);
                $scope.portfolio.renumeration = false;
            }
            else{
                $('#remuneration-fieldset').css("opacity", 0.5);
                $scope.portfolio.renumeration = true;
            }
        }
        else if ($(this).parent().find('.filedName').html() == "Contact Details") {
            $(this).parent().addClass('active');
            $('#profession-fieldset').css('display', 'none');
            $('#myself-fieldset').css('display', 'none');
            $('#remuneration-fieldset').css('display', 'none');
            $('#contact-fieldset').css({
                "display": "block",
                "opacity": 1,
                "left": 0,
                "position": "relative",
                "transform": "none"
            });
            $('#image-fieldset').css('display', 'none');
            if($scope.portfolioValidateForm.myself && $scope.portfolioValidateForm.profession && $scope.portfolioValidateForm.renumeration) {
                $('#contact-fieldset').css("opacity", 1);
                $scope.portfolio.contactDetails = false;
            }
            else{
                $('#contact-fieldset').css("opacity", 0.5);
                $scope.portfolio.contactDetails = true;
            }
        }
        else {
            $(this).parent().addClass('active');
            $('#profession-fieldset').css('display', 'none');
            $('#myself-fieldset').css('display', 'none');
            $('#remuneration-fieldset').css('display', 'none');
            $('#contact-fieldset').css('display', 'none');
            $('#image-fieldset').css({
                "display": "block",
                "opacity": 1,
                "left": 0,
                "position": "relative",
                "transform": "none"
            });
            if($scope.portfolioValidateForm.myself && $scope.portfolioValidateForm.profession && $scope.portfolioValidateForm.renumeration && $scope.portfolioValidateForm.contactDetails) {
                $('#image-fieldset').css("opacity", 1);
                $scope.portfolio.imagesAndVideos = false;
            }
            else{
                $('#image-fieldset').css("opacity", 0.5);
                $scope.portfolio.imagesAndVideos = true;
            }
            
        }
    });
    $("#imageupload").fileinput({
        uploadUrl: '#', // you must set a valid URL here else you will get an error
        allowedFileExtensions: ['jpg', 'png', 'gif'],
        overwriteInitial: false,
        maxFileSize: 5000,
        maxFilesNum: 10,
        width: 120,
        height:100,
        //allowedFileTypes: ['image', 'video', 'flash'],
        slugCallback: function (filename) {
            console.log(filename.replace('(', '_').replace(']', '_'));
            return filename.replace('(', '_').replace(']', '_');
        }
    });
    $("#videoupload").fileinput({
        uploadUrl: '#', // you must set a valid URL here else you will get an error
        allowedFileExtensions: ['m4v', 'avi', 'mpg', 'mp4'],
        overwriteInitial: false,
        maxFileSize: 50000,
        maxFilesNum: 10,
        //allowedFileTypes: ['image', 'video', 'flash'],
        slugCallback: function (filename) {
            console.log(filename.replace('(', '_').replace(']', '_'));
            return filename.replace('(', '_').replace(']', '_');
        }
    });    
    $("#userUpload").fileinput({
        uploadUrl: '#', // you must set a valid URL here else you will get an error
        allowedFileExtensions: ['jpg', 'png', 'gif'],
        overwriteInitial: false,
        maxFileSize: 500,
        maxFilesNum: 1,
        width: 120,
        height:100,
        //allowedFileTypes: ['image', 'video', 'flash'],
        slugCallback: function (filename) {
            console.log(filename.replace('(', '_').replace(']', '_'));
            return filename.replace('(', '_').replace(']', '_');
        }
    });
    $(".submit").click(function () {
        return false;
    })
    $('.textbox input').attr('value', "");
    $('.textbox input').attr('onkeyup', "this.setAttribute('value', this.value);");
    $('.textbox .info').append('<i class="material-icons">info</i>');
    $('.material-textbox').on("focus", function () {
        if ($(this).val() !== '')
            $(this).addClass('textbox-focused')
        else
            $(this).removeClass('textbox-focused')
    });
    $('.material-textbox').on("blur", function () {
        if ($(this).val() === '') {
            $(this).removeClass('textbox-focused')
        }
    });

    
    //service calls
    $scope.getCountries = function () {
         $scope.$emit('dynamic-loading', true);
        countryStateCityService.getCountries().then(
            function (result) {
                $scope.countries = result.Response;
                $scope.$emit('dynamic-loading', false);
            },
            function (reason) {
                $scope.$emit('dynamic-loading', false);
                notificationService.displayError('OOPS Something went wrong. Fetching data failed.');
                console.log('ERROR ' + reason);
            });
    };
    $scope.getStates = function (id) {
        $scope.$emit('dynamic-loading', true);
        countryStateCityService.getStatesByCountryId(id).then(
            function (result) {
                $scope.states = result.Response;
                $scope.$emit('dynamic-loading', false);
            },
            function (reason) {
                $scope.$emit('dynamic-loading', false);
                notificationService.displayError('OOPS Something went wrong. Fetching data failed.');
                console.log('ERROR ' + reason);
            });
    };
    $scope.getCities = function (id) {
        countryStateCityService.getCitiesByStateId(id).then(
            function (result) {
                $scope.cities = result.Response;
            },
            function (reason) {
                console.log('ERROR ' + reason);
            });
    };
    $scope.GetProfileDataByUserId = function (userId) {
        $scope.$emit('dynamic-loading', true);
        portfolioService.GetProfileDataByUserId(userId).then(function (res) {
            $scope.PutResponseData(res.Response);
            $scope.$emit('dynamic-loading', false);
            if ($('.material-textbox').val() !== '') {
                $('.material-textbox').removeClass('textbox-focused');
            }
            else {
                $('.material-textbox').addClass('textbox-focused');
            }
        }, function (reason) {
            $scope.$emit('dynamic-loading', false);
           notificationService.displayError('OOPS Something went wrong. Fetching data failed.');
           console.log('ERROR' + reason);
      });
    }
    $scope.CreateProfile = function (request , filesUpload) {
        $scope.$emit('dynamic-loading', true);
        portfolioService.CreateProfile(request).then(function (result) {
            portfolioService.UploadFiles(filesUpload).then(function (result) {
                portfolioService.GetProfileDataByUserId(request.UserID).then(function (res) {
                    $scope.PutResponseData(res.Response);
                    $scope.$emit('dynamic-loading', false);
                    $('.material-textbox').val() !== '' ? $('.material-textbox').removeClass('textbox-focused') : $('.material-textbox').addClass('textbox-focused');
                    $scope.$emit('dynamic-loading', false);
                    notificationService.displaySuccess('Succesfully Updated!');
                }, function (reason) {
                    $scope.$emit('dynamic-loading', false);
                    notificationService.displayError('OOPS Something went wrong!');
                    console.log('ERROR' + reason);
                });
            }, function (reason) {
                $scope.$emit('dynamic-loading', false);
                notificationService.displayError('OOPS Something went wrong!');
                console.log('ERROR' + reason);
            })
        }, function (reason) {
            $scope.$emit('dynamic-loading', false);
            notificationService.displayError('OOPS Something went wrong!');
            console.log('ERROR' + reason);
        });
    };
    $scope.portfolioBusinessSubmit = function (valid) {
        if (valid)
        {
            $scope.$emit('dynamic-loading', true);
            var requestObj = {
                BusinessObj: {
                    Name: $scope.pfBusinessModel.name,
                    EmailID: $scope.pfBusinessModel.email,
                    PhoneNum: $scope.pfBusinessModel.phoneno,
                    CurrentAssignment: $scope.pfBusinessModel.currentAssignment,
                    GoogleDriveUrl: $scope.pfBusinessModel.googleDriveUrl,
                    ImagePathUrls: $scope.busProfilePicObj,
                    //ImagePathUrls: $scope.pfBusinessModel.imagePath,
                    BusinessUserId: $scope.pfBusinessModel.businessUserId
                }
            };
            var formData = new FormData();
            formData.append('ProfilePicturePath', $scope.busProfilePicObj);
            formData.append('isContentModified', false);
            formData.append('mobileNum', parseInt(authService.authentication.businessUserId));
            portfolioService.CreateBusiness(requestObj).then(function (result) {
                portfolioService.UploadFiles(formData).then(function (reponse) {
                    $scope.$emit('dynamic-loading', false);
                    notificationService.displaySuccess('Succesfully Updated!');
                }, function (reason) {
                    $scope.$emit('dynamic-loading', false);
                    notificationService.displayError('OOPS Something went wrong!');
                    console.log('ERROR' + reason);
                });
            }, function (reason) {
                $scope.$emit('dynamic-loading', false);
                notificationService.displayError('OOPS Something went wrong!');
                console.log('ERROR' + reason);
            });
        }
    };



    $scope.getCountries();
    if (authService.authentication.isAuth) {
        $scope.GetProfileDataByUserId(parseInt(authService.authentication.userId));
    }

    $scope.appendCountryCode = function () {
        var countryCodeData = {
         countries: [{
    "coun_id": 240,
    "coun_name": "Andorra",
    "country_code": "AD",
    "phone_code": 376
  },
  {
    "coun_id": 56,
    "coun_name": "United Arab Emirates",
    "country_code": "AE",
    "phone_code": 971
  },
  {
    "coun_id": 235,
    "coun_name": "Afghanistan",
    "country_code": "AF",
    "phone_code": 93
  },
  {
    "coun_id": 55,
    "coun_name": "Antigua and Barbuda",
    "country_code": "AG",
    "phone_code": "1-268"
  },
  {
    "coun_id": 242,
    "coun_name": "Anguilla",
    "country_code": "AI",
    "phone_code": "1-264"
  },
  {
    "coun_id": 237,
    "coun_name": "Albania",
    "country_code": "AL",
    "phone_code": 355
  },
  {
    "coun_id": 244,
    "coun_name": "Armenia",
    "country_code": "AM",
    "phone_code": 374
  },
  {
    "coun_id": 31,
    "coun_name": "Netherlands Antilles",
    "country_code": "AN",
    "phone_code": 599
  },
  {
    "coun_id": 241,
    "coun_name": "Angola",
    "country_code": "AO",
    "phone_code": 244
  },
  {
    "coun_id": 243,
    "coun_name": "Antarctica",
    "country_code": "AQ",
    "phone_code": 672
  },
  {
    "coun_id": 22,
    "coun_name": "Argentina",
    "country_code": "AR",
    "phone_code": 54
  },
  {
    "coun_id": 239,
    "coun_name": "American Samoa",
    "country_code": "AS",
    "phone_code": "1-684"
  },
  {
    "coun_id": 3,
    "coun_name": "Austria",
    "country_code": "AT",
    "phone_code": 43
  },
  {
    "coun_id": 98,
    "coun_name": "Australia",
    "country_code": "AU",
    "phone_code": 61
  },
  {
    "coun_id": 78,
    "coun_name": "Aruba",
    "country_code": "AW",
    "phone_code": 297
  },
  {
    "coun_id": 236,
    "coun_name": "Åland Islands",
    "country_code": "AX",
    "phone_code": 358
  },
  {
    "coun_id": 245,
    "coun_name": "Azerbaijan",
    "country_code": "AZ",
    "phone_code": 994
  },
  {
    "coun_id": 192,
    "coun_name": "Bosnia And Herzegovina",
    "country_code": "BA",
    "phone_code": 387
  },
  {
    "coun_id": 97,
    "coun_name": "Barbados",
    "country_code": "BB",
    "phone_code": "1-246"
  },
  {
    "coun_id": 23,
    "coun_name": "Bangladesh",
    "country_code": "BD",
    "phone_code": 880
  },
  {
    "coun_id": 24,
    "coun_name": "Belgium",
    "country_code": "BE",
    "phone_code": 32
  },
  {
    "coun_id": 196,
    "coun_name": "Burkina Faso",
    "country_code": "BF",
    "phone_code": 226
  },
  {
    "coun_id": 70,
    "coun_name": "Bulgaria",
    "country_code": "BG",
    "phone_code": 359
  },
  {
    "coun_id": 2,
    "coun_name": "Bahrain",
    "country_code": "BH",
    "phone_code": 973
  },
  {
    "coun_id": 197,
    "coun_name": "Burundi",
    "country_code": "BI",
    "phone_code": 257
  },
  {
    "coun_id": 248,
    "coun_name": "Benin",
    "country_code": "BJ",
    "phone_code": 229
  },
  {
    "coun_id": 77,
    "coun_name": "Saint Barthélemy",
    "country_code": "BL",
    "phone_code": 590
  },
  {
    "coun_id": 26,
    "coun_name": "Bermuda",
    "country_code": "BM",
    "phone_code": "1-441"
  },
  {
    "coun_id": 15,
    "coun_name": "Brunei Darussalam",
    "country_code": "BN",
    "phone_code": 673
  },
  {
    "coun_id": 71,
    "coun_name": "Bolivia",
    "country_code": "BO",
    "phone_code": 591
  },
  {
    "coun_id": 12,
    "coun_name": "Brazil",
    "country_code": "BR",
    "phone_code": 55
  },
  {
    "coun_id": 1,
    "coun_name": "Bahamas",
    "country_code": "BS",
    "phone_code": "1-242"
  },
  {
    "coun_id": 249,
    "coun_name": "Bhutan",
    "country_code": "BT",
    "phone_code": 975
  },
  {
    "coun_id": 194,
    "coun_name": "Bouvet Island",
    "country_code": "BV",
    "phone_code": 47
  },
  {
    "coun_id": 193,
    "coun_name": "Botswana",
    "country_code": "BW",
    "phone_code": 267
  },
  {
    "coun_id": 246,
    "coun_name": "Belarus",
    "country_code": "BY",
    "phone_code": 375
  },
  {
    "coun_id": 247,
    "coun_name": "Belize",
    "country_code": "BZ",
    "phone_code": 501
  },
  {
    "coun_id": 82,
    "coun_name": "Canada",
    "country_code": "CA",
    "phone_code": 1
  },
  {
    "coun_id": 204,
    "coun_name": "Cocos (Keeling) Islands",
    "country_code": "CC",
    "phone_code": 61
  },
  {
    "coun_id": 216,
    "coun_name": "The Democratic Republic Of The Congo",
    "country_code": "CD",
    "phone_code": 243
  },
  {
    "coun_id": 201,
    "coun_name": "Central African Republic",
    "country_code": "CF",
    "phone_code": 236
  },
  {
    "coun_id": 206,
    "coun_name": "Congo",
    "country_code": "CG",
    "phone_code": 242
  },
  {
    "coun_id": 9,
    "coun_name": "Switzerland",
    "country_code": "CH",
    "phone_code": 41
  },
  {
    "coun_id": 208,
    "coun_name": "Côte D'Ivoire",
    "country_code": "CI",
    "phone_code": 225
  },
  {
    "coun_id": 207,
    "coun_name": "Cook Islands",
    "country_code": "CK",
    "phone_code": 682
  },
  {
    "coun_id": 25,
    "coun_name": "Chile",
    "country_code": "CL",
    "phone_code": 56
  },
  {
    "coun_id": 199,
    "coun_name": "Cameroon",
    "country_code": "CM",
    "phone_code": 237
  },
  {
    "coun_id": 115,
    "coun_name": "China",
    "country_code": "CN",
    "phone_code": 86
  },
  {
    "coun_id": 73,
    "coun_name": "Colombia",
    "country_code": "CO",
    "phone_code": 57
  },
  {
    "coun_id": 74,
    "coun_name": "Costa Rica",
    "country_code": "CR",
    "phone_code": 506
  },
  {
    "coun_id": 234,
    "coun_name": "Serbia and Montenegro",
    "country_code": "CS",
    "phone_code": 381
  },
  {
    "coun_id": 117,
    "coun_name": "Cuba",
    "country_code": "CU",
    "phone_code": 53
  },
  {
    "coun_id": 7,
    "coun_name": "Curacao",
    "country_code": "CUR",
    "phone_code": 599
  },
  {
    "coun_id": 200,
    "coun_name": "Cape Verde",
    "country_code": "CV",
    "phone_code": 238
  },
  {
    "coun_id": 203,
    "coun_name": "Christmas Island",
    "country_code": "CX",
    "phone_code": 61
  },
  {
    "coun_id": 100,
    "coun_name": "Cyprus",
    "country_code": "CY",
    "phone_code": 357
  },
  {
    "coun_id": 85,
    "coun_name": "Czech Republic",
    "country_code": "CZ",
    "phone_code": 420
  },
  {
    "coun_id": 72,
    "coun_name": "Germany",
    "country_code": "DE",
    "phone_code": 49
  },
  {
    "coun_id": 119,
    "coun_name": "Djibouti",
    "country_code": "DJ",
    "phone_code": 253
  },
  {
    "coun_id": 86,
    "coun_name": "Denmark",
    "country_code": "DK",
    "phone_code": 45
  },
  {
    "coun_id": 29,
    "coun_name": "Dominica",
    "country_code": "DM",
    "phone_code": "1-767"
  },
  {
    "coun_id": 101,
    "coun_name": "Dominican Republic",
    "country_code": "DO",
    "phone_code": "1-809, 1-829, 1-849"
  },
  {
    "coun_id": 238,
    "coun_name": "Algeria",
    "country_code": "DZ",
    "phone_code": 213
  },
  {
    "coun_id": 28,
    "coun_name": "Ecuador",
    "country_code": "EC",
    "phone_code": 593
  },
  {
    "coun_id": 122,
    "coun_name": "Estonia",
    "country_code": "EE",
    "phone_code": 372
  },
  {
    "coun_id": 19,
    "coun_name": "Egypt",
    "country_code": "EG",
    "phone_code": 20
  },
  {
    "coun_id": 231,
    "coun_name": "Western Sahara",
    "country_code": "EH",
    "phone_code": 212
  },
  {
    "coun_id": 121,
    "coun_name": "Eritrea",
    "country_code": "ER",
    "phone_code": 291
  },
  {
    "coun_id": 16,
    "coun_name": "Spain",
    "country_code": "ES",
    "phone_code": 34
  },
  {
    "coun_id": 123,
    "coun_name": "Ethiopia",
    "country_code": "ET",
    "phone_code": 251
  },
  {
    "coun_id": 13,
    "coun_name": "Finland",
    "country_code": "FI",
    "phone_code": 358
  },
  {
    "coun_id": 109,
    "coun_name": "Fiji",
    "country_code": "FJ",
    "phone_code": 679
  },
  {
    "coun_id": 124,
    "coun_name": "Falkland Islands (Malvinas)",
    "country_code": "FK",
    "phone_code": 500
  },
  {
    "coun_id": 126,
    "coun_name": "Micronesia",
    "country_code": "FM",
    "phone_code": 691
  },
  {
    "coun_id": 125,
    "coun_name": "Faroe Islands",
    "country_code": "FO",
    "phone_code": 298
  },
  {
    "coun_id": 37,
    "coun_name": "France",
    "country_code": "FR",
    "phone_code": 33
  },
  {
    "coun_id": 130,
    "coun_name": "Gabon",
    "country_code": "GA",
    "phone_code": 241
  },
  {
    "coun_id": 112,
    "coun_name": "United Kingdom",
    "country_code": "GB",
    "phone_code": 44
  },
  {
    "coun_id": 136,
    "coun_name": "Grenada",
    "country_code": "GD",
    "phone_code": "1-473"
  },
  {
    "coun_id": 132,
    "coun_name": "Georgia",
    "country_code": "GE",
    "phone_code": 995
  },
  {
    "coun_id": 127,
    "coun_name": "French Guiana",
    "country_code": "GF",
    "phone_code": 594
  },
  {
    "coun_id": 137,
    "coun_name": "Guernsey",
    "country_code": "GG",
    "phone_code": "44-1481"
  },
  {
    "coun_id": 133,
    "coun_name": "Ghana",
    "country_code": "GH",
    "phone_code": 233
  },
  {
    "coun_id": 134,
    "coun_name": "Gibraltar",
    "country_code": "GI",
    "phone_code": 350
  },
  {
    "coun_id": 135,
    "coun_name": "Greenland",
    "country_code": "GL",
    "phone_code": 299
  },
  {
    "coun_id": 131,
    "coun_name": "Gambia",
    "country_code": "GM",
    "phone_code": 220
  },
  {
    "coun_id": 138,
    "coun_name": "Guinea",
    "country_code": "GN",
    "phone_code": 224
  },
  {
    "coun_id": 38,
    "coun_name": "Guadeloupe",
    "country_code": "GP",
    "phone_code": 590
  },
  {
    "coun_id": 120,
    "coun_name": "Equatorial Guinea",
    "country_code": "GQ",
    "phone_code": 240
  },
  {
    "coun_id": 42,
    "coun_name": "Greece",
    "country_code": "GR",
    "phone_code": 30
  },
  {
    "coun_id": 45,
    "coun_name": "Guatemala",
    "country_code": "GT",
    "phone_code": 502
  },
  {
    "coun_id": 6,
    "coun_name": "Guam",
    "country_code": "GU",
    "phone_code": "1-671"
  },
  {
    "coun_id": 139,
    "coun_name": "Guinea-Bissau",
    "country_code": "GW",
    "phone_code": 245
  },
  {
    "coun_id": 140,
    "coun_name": "Guyana",
    "country_code": "GY",
    "phone_code": 592
  },
  {
    "coun_id": 51,
    "coun_name": "Hong Kong",
    "country_code": "HK",
    "phone_code": 852
  },
  {
    "coun_id": 142,
    "coun_name": "Heard Island And Mcdonald Islands",
    "country_code": "HM",
    "phone_code": 672
  },
  {
    "coun_id": 87,
    "coun_name": "Honduras",
    "country_code": "HN",
    "phone_code": 504
  },
  {
    "coun_id": 84,
    "coun_name": "Croatia",
    "country_code": "HR",
    "phone_code": 385
  },
  {
    "coun_id": 141,
    "coun_name": "Haiti",
    "country_code": "HT",
    "phone_code": 509
  },
  {
    "coun_id": 14,
    "coun_name": "Hungary",
    "country_code": "HU",
    "phone_code": 36
  },
  {
    "coun_id": 5,
    "coun_name": "Indonesia",
    "country_code": "ID",
    "phone_code": 62
  },
  {
    "coun_id": 44,
    "coun_name": "Ireland",
    "country_code": "IE",
    "phone_code": 353
  },
  {
    "coun_id": 114,
    "coun_name": "Israel",
    "country_code": "IL",
    "phone_code": 972
  },
  {
    "coun_id": 145,
    "coun_name": "Isle Of Man",
    "country_code": "IM",
    "phone_code": "44-1624"
  },
  {
    "coun_id": 41,
    "coun_name": "India",
    "country_code": "IN",
    "phone_code": 91
  },
  {
    "coun_id": 195,
    "coun_name": "British Indian Ocean Territory",
    "country_code": "IO",
    "phone_code": 246
  },
  {
    "coun_id": 144,
    "coun_name": "Iraq",
    "country_code": "IQ",
    "phone_code": 964
  },
  {
    "coun_id": 43,
    "coun_name": "Iran",
    "country_code": "IR",
    "phone_code": 98
  },
  {
    "coun_id": 40,
    "coun_name": "Iceland",
    "country_code": "IS",
    "phone_code": 354
  },
  {
    "coun_id": 4,
    "coun_name": "Italy",
    "country_code": "IT",
    "phone_code": 39
  },
  {
    "coun_id": 146,
    "coun_name": "Jersey",
    "country_code": "JE",
    "phone_code": "44-1534"
  },
  {
    "coun_id": 57,
    "coun_name": "Jamaica",
    "country_code": "JM",
    "phone_code": "1-876"
  },
  {
    "coun_id": 88,
    "coun_name": "Jordan",
    "country_code": "JO",
    "phone_code": 962
  },
  {
    "coun_id": 75,
    "coun_name": "Japan",
    "country_code": "JP",
    "phone_code": 81
  },
  {
    "coun_id": 11,
    "coun_name": "Kenya",
    "country_code": "KE",
    "phone_code": 254
  },
  {
    "coun_id": 149,
    "coun_name": "Kyrgyzstan",
    "country_code": "KG",
    "phone_code": 996
  },
  {
    "coun_id": 198,
    "coun_name": "Cambodia",
    "country_code": "KH",
    "phone_code": 855
  },
  {
    "coun_id": 148,
    "coun_name": "Kiribati",
    "country_code": "KI",
    "phone_code": 686
  },
  {
    "coun_id": 205,
    "coun_name": "Comoros",
    "country_code": "KM",
    "phone_code": 269
  },
  {
    "coun_id": 8,
    "coun_name": "Saint Kitts and Nevis",
    "country_code": "KN",
    "phone_code": "1-869"
  },
  {
    "coun_id": 118,
    "coun_name": "North Korea",
    "country_code": "KP",
    "phone_code": 850
  },
  {
    "coun_id": 93,
    "coun_name": "South Korea",
    "country_code": "KR",
    "phone_code": 82
  },
  {
    "coun_id": 103,
    "coun_name": "Kuwait",
    "country_code": "KW",
    "phone_code": 965
  },
  {
    "coun_id": 99,
    "coun_name": "Cayman Islands",
    "country_code": "KY",
    "phone_code": "1-345"
  },
  {
    "coun_id": 147,
    "coun_name": "Kazakhstan",
    "country_code": "KZ",
    "phone_code": 7
  },
  {
    "coun_id": 150,
    "coun_name": "Lao People'S Democratic Republic",
    "country_code": "LA",
    "phone_code": 856
  },
  {
    "coun_id": 104,
    "coun_name": "Lebanon",
    "country_code": "LB",
    "phone_code": 961
  },
  {
    "coun_id": 61,
    "coun_name": "Saint Lucia",
    "country_code": "LC",
    "phone_code": "1-758"
  },
  {
    "coun_id": 48,
    "coun_name": "Liechtenstein",
    "country_code": "LI",
    "phone_code": 423
  },
  {
    "coun_id": 63,
    "coun_name": "Sri Lanka",
    "country_code": "LK",
    "phone_code": 94
  },
  {
    "coun_id": 153,
    "coun_name": "Liberia",
    "country_code": "LR",
    "phone_code": 231
  },
  {
    "coun_id": 152,
    "coun_name": "Lesotho",
    "country_code": "LS",
    "phone_code": 266
  },
  {
    "coun_id": 154,
    "coun_name": "Lithuania",
    "country_code": "LT",
    "phone_code": 370
  },
  {
    "coun_id": 30,
    "coun_name": "Luxembourg",
    "country_code": "LU",
    "phone_code": 352
  },
  {
    "coun_id": 151,
    "coun_name": "Latvia",
    "country_code": "LV",
    "phone_code": 371
  },
  {
    "coun_id": 95,
    "coun_name": "Libya",
    "country_code": "LY",
    "phone_code": 218
  },
  {
    "coun_id": 20,
    "coun_name": "Morocco",
    "country_code": "MA",
    "phone_code": 212
  },
  {
    "coun_id": 39,
    "coun_name": "Monaco",
    "country_code": "MC",
    "phone_code": 377
  },
  {
    "coun_id": 178,
    "coun_name": "Republic Of Moldova",
    "country_code": "MD",
    "phone_code": 373
  },
  {
    "coun_id": 164,
    "coun_name": "Montenegro",
    "country_code": "ME",
    "phone_code": 382
  },
  {
    "coun_id": 50,
    "coun_name": "Saint Martin",
    "country_code": "MF",
    "phone_code": 590
  },
  {
    "coun_id": 155,
    "coun_name": "Madagascar",
    "country_code": "MG",
    "phone_code": 261
  },
  {
    "coun_id": 159,
    "coun_name": "Marshall Islands",
    "country_code": "MH",
    "phone_code": 692
  },
  {
    "coun_id": 217,
    "coun_name": "Macedonia",
    "country_code": "MK",
    "phone_code": 389
  },
  {
    "coun_id": 158,
    "coun_name": "Mali",
    "country_code": "ML",
    "phone_code": 223
  },
  {
    "coun_id": 167,
    "coun_name": "Myanmar",
    "country_code": "MM",
    "phone_code": 95
  },
  {
    "coun_id": 163,
    "coun_name": "Mongolia",
    "country_code": "MN",
    "phone_code": 976
  },
  {
    "coun_id": 54,
    "coun_name": "Macau",
    "country_code": "MO",
    "phone_code": 853
  },
  {
    "coun_id": 102,
    "coun_name": "Northern Mariana Islands",
    "country_code": "MP",
    "phone_code": "1-670"
  },
  {
    "coun_id": 160,
    "coun_name": "Martinique",
    "country_code": "MQ",
    "phone_code": 596
  },
  {
    "coun_id": 161,
    "coun_name": "Mauritania",
    "country_code": "MR",
    "phone_code": 222
  },
  {
    "coun_id": 165,
    "coun_name": "Montserrat",
    "country_code": "MS",
    "phone_code": "1-664"
  },
  {
    "coun_id": 21,
    "coun_name": "Malta",
    "country_code": "MT",
    "phone_code": 356
  },
  {
    "coun_id": 52,
    "coun_name": "Mauritius",
    "country_code": "MU",
    "phone_code": 230
  },
  {
    "coun_id": 157,
    "coun_name": "Maldives",
    "country_code": "MV",
    "phone_code": 960
  },
  {
    "coun_id": 156,
    "coun_name": "Malawi",
    "country_code": "MW",
    "phone_code": 265
  },
  {
    "coun_id": 47,
    "coun_name": "Mexico",
    "country_code": "MX",
    "phone_code": 52
  },
  {
    "coun_id": 46,
    "coun_name": "Malaysia",
    "country_code": "MY",
    "phone_code": 60
  },
  {
    "coun_id": 166,
    "coun_name": "Mozambique",
    "country_code": "MZ",
    "phone_code": 258
  },
  {
    "coun_id": 10,
    "coun_name": "Namibia",
    "country_code": "NA",
    "phone_code": 264
  },
  {
    "coun_id": 27,
    "coun_name": "New Caledonia",
    "country_code": "NC",
    "phone_code": 687
  },
  {
    "coun_id": 169,
    "coun_name": "Niger",
    "country_code": "NE",
    "phone_code": 227
  },
  {
    "coun_id": 172,
    "coun_name": "Norfolk Island",
    "country_code": "NF",
    "phone_code": 672
  },
  {
    "coun_id": 170,
    "coun_name": "Nigeria",
    "country_code": "NG",
    "phone_code": 234
  },
  {
    "coun_id": 35,
    "coun_name": "Nicaragua",
    "country_code": "NI",
    "phone_code": 505
  },
  {
    "coun_id": 106,
    "coun_name": "Netherlands",
    "country_code": "NL",
    "phone_code": 31
  },
  {
    "coun_id": 59,
    "coun_name": "Norway",
    "country_code": "NO",
    "phone_code": 47
  },
  {
    "coun_id": 105,
    "coun_name": "Nepal",
    "country_code": "NP",
    "phone_code": 977
  },
  {
    "coun_id": 168,
    "coun_name": "Nauru",
    "country_code": "NR",
    "phone_code": 674
  },
  {
    "coun_id": 171,
    "coun_name": "Niue",
    "country_code": "NU",
    "phone_code": 683
  },
  {
    "coun_id": 32,
    "coun_name": "New Zealand",
    "country_code": "NZ",
    "phone_code": 64
  },
  {
    "coun_id": 60,
    "coun_name": "Oman",
    "country_code": "OM",
    "phone_code": 968
  },
  {
    "coun_id": 111,
    "coun_name": "Panama",
    "country_code": "PA",
    "phone_code": 507
  },
  {
    "coun_id": 17,
    "coun_name": "Peru",
    "country_code": "PE",
    "phone_code": 51
  },
  {
    "coun_id": 128,
    "coun_name": "French Polynesia",
    "country_code": "PF",
    "phone_code": 689
  },
  {
    "coun_id": 175,
    "coun_name": "Papua New Guinea",
    "country_code": "PG",
    "phone_code": 675
  },
  {
    "coun_id": 53,
    "coun_name": "Philippines",
    "country_code": "PH",
    "phone_code": 63
  },
  {
    "coun_id": 36,
    "coun_name": "Pakistan",
    "country_code": "PK",
    "phone_code": 92
  },
  {
    "coun_id": 76,
    "coun_name": "Poland",
    "country_code": "PL",
    "phone_code": 48
  },
  {
    "coun_id": 182,
    "coun_name": "Saint Pierre And Miquelon",
    "country_code": "PM",
    "phone_code": 508
  },
  {
    "coun_id": 177,
    "coun_name": "Pitcairn",
    "country_code": "PN",
    "phone_code": 64
  },
  {
    "coun_id": 81,
    "coun_name": "Puerto Rico",
    "country_code": "PR",
    "phone_code": "1-787, 1-939"
  },
  {
    "coun_id": 173,
    "coun_name": "Occupied Palestinian Territory",
    "country_code": "PS",
    "phone_code": 970
  },
  {
    "coun_id": 89,
    "coun_name": "Portugal",
    "country_code": "PT",
    "phone_code": 351
  },
  {
    "coun_id": 174,
    "coun_name": "Palau",
    "country_code": "PW",
    "phone_code": 680
  },
  {
    "coun_id": 176,
    "coun_name": "Paraguay",
    "country_code": "PY",
    "phone_code": 595
  },
  {
    "coun_id": 58,
    "coun_name": "Qatar",
    "country_code": "QA",
    "phone_code": 974
  },
  {
    "coun_id": 179,
    "coun_name": "Réunion",
    "country_code": "RE",
    "phone_code": 262
  },
  {
    "coun_id": 34,
    "coun_name": "Romania",
    "country_code": "RO",
    "phone_code": 40
  },
  {
    "coun_id": 90,
    "coun_name": "Serbia",
    "country_code": "RS",
    "phone_code": 381
  },
  {
    "coun_id": 49,
    "coun_name": "Russia",
    "country_code": "RU",
    "phone_code": 7
  },
  {
    "coun_id": 180,
    "coun_name": "Rwanda",
    "country_code": "RW",
    "phone_code": 250
  },
  {
    "coun_id": 107,
    "coun_name": "Saudi Arabia",
    "country_code": "SA",
    "phone_code": 966
  },
  {
    "coun_id": 190,
    "coun_name": "Solomon Islands",
    "country_code": "SB",
    "phone_code": 677
  },
  {
    "coun_id": 188,
    "coun_name": "Seychelles",
    "country_code": "SC",
    "phone_code": 248
  },
  {
    "coun_id": 210,
    "coun_name": "Sudan",
    "country_code": "SD",
    "phone_code": 249
  },
  {
    "coun_id": 18,
    "coun_name": "Sweden",
    "country_code": "SE",
    "phone_code": 46
  },
  {
    "coun_id": 108,
    "coun_name": "Singapore",
    "country_code": "SG",
    "phone_code": 65
  },
  {
    "coun_id": 181,
    "coun_name": "Saint Helena",
    "country_code": "SH",
    "phone_code": 290
  },
  {
    "coun_id": 91,
    "coun_name": "Slovenia",
    "country_code": "SI",
    "phone_code": 386
  },
  {
    "coun_id": 212,
    "coun_name": "Svalbard And Jan Mayen",
    "country_code": "SJ",
    "phone_code": 47
  },
  {
    "coun_id": 110,
    "coun_name": "Slovakia",
    "country_code": "SK",
    "phone_code": 421
  },
  {
    "coun_id": 189,
    "coun_name": "Sierra Leone",
    "country_code": "SL",
    "phone_code": 232
  },
  {
    "coun_id": 185,
    "coun_name": "San Marino",
    "country_code": "SM",
    "phone_code": 378
  },
  {
    "coun_id": 187,
    "coun_name": "Senegal",
    "country_code": "SN",
    "phone_code": 221
  },
  {
    "coun_id": 191,
    "coun_name": "Somalia",
    "country_code": "SO",
    "phone_code": 252
  },
  {
    "coun_id": 211,
    "coun_name": "Suriname",
    "country_code": "SR",
    "phone_code": 597
  },
  {
    "coun_id": 186,
    "coun_name": "Sao Tome And Principe",
    "country_code": "ST",
    "phone_code": 239
  },
  {
    "coun_id": 83,
    "coun_name": "El Salvador",
    "country_code": "SV",
    "phone_code": 503
  },
  {
    "coun_id": 214,
    "coun_name": "Syria",
    "country_code": "SY",
    "phone_code": 963
  },
  {
    "coun_id": 213,
    "coun_name": "Swaziland",
    "country_code": "SZ",
    "phone_code": 268
  },
  {
    "coun_id": 113,
    "coun_name": "Turks and Caicos Islands",
    "country_code": "TC",
    "phone_code": "1-649"
  },
  {
    "coun_id": 202,
    "coun_name": "Chad",
    "country_code": "TD",
    "phone_code": 235
  },
  {
    "coun_id": 129,
    "coun_name": "French Southern Territories",
    "country_code": "TF",
    "phone_code": 262
  },
  {
    "coun_id": 219,
    "coun_name": "Togo",
    "country_code": "TG",
    "phone_code": 228
  },
  {
    "coun_id": 62,
    "coun_name": "Thailand",
    "country_code": "TH",
    "phone_code": 66
  },
  {
    "coun_id": 215,
    "coun_name": "Tajikistan",
    "country_code": "TJ",
    "phone_code": 992
  },
  {
    "coun_id": 220,
    "coun_name": "Tokelau",
    "country_code": "TK",
    "phone_code": 690
  },
  {
    "coun_id": 218,
    "coun_name": "East Timor",
    "country_code": "TL",
    "phone_code": 670
  },
  {
    "coun_id": 224,
    "coun_name": "Turkmenistan",
    "country_code": "TM",
    "phone_code": 993
  },
  {
    "coun_id": 223,
    "coun_name": "Tunisia",
    "country_code": "TN",
    "phone_code": 216
  },
  {
    "coun_id": 221,
    "coun_name": "Tonga",
    "country_code": "TO",
    "phone_code": 676
  },
  {
    "coun_id": 66,
    "coun_name": "Turkey",
    "country_code": "TR",
    "phone_code": 90
  },
  {
    "coun_id": 222,
    "coun_name": "Trinidad And Tobago",
    "country_code": "TT",
    "phone_code": "1-868"
  },
  {
    "coun_id": 225,
    "coun_name": "Tuvalu",
    "country_code": "TV",
    "phone_code": 688
  },
  {
    "coun_id": 80,
    "coun_name": "Taiwan",
    "country_code": "TW",
    "phone_code": 886
  },
  {
    "coun_id": 227,
    "coun_name": "United Republic Of Tanzania",
    "country_code": "TZ",
    "phone_code": 255
  },
  {
    "coun_id": 64,
    "coun_name": "Ukraine",
    "country_code": "UA",
    "phone_code": 380
  },
  {
    "coun_id": 226,
    "coun_name": "Uganda",
    "country_code": "UG",
    "phone_code": 256
  },
  {
    "coun_id": 228,
    "coun_name": "United States Minor Outlying Islands",
    "country_code": "UM",
    "phone_code": 268
  },
  {
    "coun_id": 79,
    "coun_name": "United States",
    "country_code": "US",
    "phone_code": 1
  },
  {
    "coun_id": 94,
    "coun_name": "Uruguay",
    "country_code": "UY",
    "phone_code": 598
  },
  {
    "coun_id": 229,
    "coun_name": "Uzbekistan",
    "country_code": "UZ",
    "phone_code": 998
  },
  {
    "coun_id": 143,
    "coun_name": "Holy See (Vatican City State)",
    "country_code": "VA",
    "phone_code": 379
  },
  {
    "coun_id": 183,
    "coun_name": "Saint Vincent And The Grenadines",
    "country_code": "VC",
    "phone_code": "1-784"
  },
  {
    "coun_id": 65,
    "coun_name": "Venezuela",
    "country_code": "VE",
    "phone_code": 58
  },
  {
    "coun_id": 67,
    "coun_name": "British Virgin Islands",
    "country_code": "VG",
    "phone_code": "1-284"
  },
  {
    "coun_id": 68,
    "coun_name": "U.S. Virgin Islands",
    "country_code": "VI",
    "phone_code": "1-340"
  },
  {
    "coun_id": 116,
    "coun_name": "Vietnam",
    "country_code": "VN",
    "phone_code": 84
  },
  {
    "coun_id": 33,
    "coun_name": "Vanuatu",
    "country_code": "VU",
    "phone_code": 678
  },
  {
    "coun_id": 230,
    "coun_name": "Wallis And Futuna",
    "country_code": "WF",
    "phone_code": 681
  },
  {
    "coun_id": 184,
    "coun_name": "Samoa",
    "country_code": "WS",
    "phone_code": 685
  },
  {
    "coun_id": 69,
    "coun_name": "Yemen",
    "country_code": "YE",
    "phone_code": 967
  },
  {
    "coun_id": 162,
    "coun_name": "Mayotte",
    "country_code": "YT",
    "phone_code": 262
  },
  {
    "coun_id": 92,
    "coun_name": "South Africa",
    "country_code": "ZA",
    "phone_code": 27
  },
  {
    "coun_id": 232,
    "coun_name": "Zambia",
    "country_code": "ZM",
    "phone_code": 260
  },
  {
    "coun_id": 233,
    "coun_name": "Zimbabwe",
    "country_code": "ZW",
    "phone_code": 263
  }]
        };
        var countryArray = countryCodeData.countries;
        countryArray.sort(function(a, b) {
         var nameA=a.coun_name, nameB=b.coun_name;
         if (nameA < nameB)
          return -1;
         if (nameA > nameB)
          return 1;
         return 0;
        });     
      for(var country in countryArray) {
          var text = countryArray[country].coun_name + " (" + countryArray[country].phone_code +")";
          var val = countryArray[country].coun_id;
          $scope.phoneCodes.push({ 'Name': text, 'Id': val });
       }
    }
    $scope.appendCountryCode();

    var $dropzone = $('.image_picker'),
    $droptarget = $('.drop_target'),
    $dropinput = $('#inputFile'),
    $dropimg = $('.image_preview'),
    $remover = $('[data-action="remove_current_image"]'),
    //business
    $business_droptarget = $('.bus_drop_target'),
    $business_dropinput = $('#bus_inputFile'),
    $business_dropimg = $('.bus_image_preview');
   

    $dropzone.on('dragover', function() {
       $droptarget.addClass('dropping');
     return false;
    });    
    $dropzone.on('dragend dragleave', function() {
       $droptarget.removeClass('dropping');
      return false;
    });    
    $dropzone.on('drop', function(e) {
              $droptarget.removeClass('dropping');
              $droptarget.addClass('dropped');
              $remover.removeClass('disabled');
              e.preventDefault();
  
              var file = e.originalEvent.dataTransfer.files[0],
                  reader = new FileReader();
             
              reader.onload = function(event) {
                  $dropimg.css('background-image', 'url(' + event.target.result + ')');
                  $dropimg.css('height', '200px');
              };
  
              console.log(file);
              reader.readAsDataURL(file);
              return false;
    });
    $dropinput.change(function(e) {
            $droptarget.addClass('dropped');
            $remover.removeClass('disabled');
            $('.image_title input').val('');
            
            var file = $dropinput.get(0).files[0],
                reader = new FileReader();
            
            reader.onload = function(event) {
                $dropimg.css('background-image', 'url(' + event.target.result + ')');
                $dropimg.css('height', '200px');
            }
  
           reader.readAsDataURL(file);
    });
    $remover.on('click', function() {
         $dropimg.css('background-image', '');
         $dropimg.css('height', '0px');
         $droptarget.removeClass('dropped');
         $remover.addClass('disabled');
         $('.image_title input').val('');
   });
    $('.image_title input').blur(function() {
         if ($(this).val() != '') {
           $droptarget.removeClass('dropped');
         }
    });
    //business
    $business_dropinput.change(function (e) {
        $business_droptarget.addClass('dropped');
        $remover.removeClass('disabled');
        $('.bus_image_title input').val('');

        var file = $business_dropinput.get(0).files[0],
            reader = new FileReader();

        reader.onload = function (event) {
            $business_dropimg.css('background-image', 'url(' + event.target.result + ')');
            $business_dropimg.css('height', '200px');
        }

        reader.readAsDataURL(file);
    });


}]);
app.directive('validFile',['$rootScope' ,function ($rootScope) {
    return {
        require: 'ngModel',
        link: function (scope, el, attrs, ngModel) {
            //change event is fired when file is selected
            el.bind('change', function (event) {
                //validating whether profile upload or not
                if(event.currentTarget.id == 'inputFile')
                {
                    $rootScope.$broadcast('validateProfilePicCall', false);
                    $rootScope.$broadcast('profileObj', this.files[0]);
                }
                else if (event.currentTarget.id == 'bus_inputFile')
                {
                    $rootScope.$broadcast('busValidateProfilePicCall', false);
                    scope.busProfilePicObj =this.files[0];
                }
                
                scope.$apply(function () {
                    ngModel.$setViewValue(el.val());
                    ngModel.$render();
                })
            })
        }
    }
}]);
app.controller('myselfController', ['$scope', '$state', function ($scope, $state) {
    
    $scope.files = [];
   
    $scope.upload = function () {
        console.log("No of Files:" + $scope.files.length);
    };
   
}]);