'use strict';
app.controller('opportunitiesController', ['$scope', 'authService', 'opportunitiesService', '$filter', function ($scope, authService, opportunitiesService, $filter) {

    $scope.IntialLoad = {
        opportunitiesCollection: {},
        myOpportunitiesCollection: {},
        appliedOpportunitiesCollection: {},
        allProfessions: {}
    }

    $scope.userId = authService.authentication.userId;
    $scope.opportunityId = 0;
    $scope.selectedProfessions = [];
    $scope.selectedProfessionIds = [];
    $scope.personDescription = '';
    $scope.applyoppId = '';
    function validationsSave() {
        var ret = true;
        if ($scope.remuneration === '') {
            $('#txtRemuneration').addClass('bg-error');
            ret = false;
        }
        if ($scope.StartDate === '') {
            $('#drpBranch').addClass('bg-error');
            ret = false;
        }
        if ($scope.EndDate === '') {
            $('#drpStandards').addClass('bg-error');
            ret = false;
        }
        if ($scope.description === '') {
            $('#txtDescription').addClass('bg-error');
            ret = false;
        }

        if ($scope.professionsList === '') {
            $('#drpPeriod').addClass('bg-error');
            ret = false;
        }

        if ($scope.remuneration != '' && $scope.StartDate != '' && $scope.EndDate != '' && $scope.description != '' && $scope.professionsList !='') {
            $('#txtRemuneration').removeClass('bg-error');
            $('#txtDescription').removeClass('bg-error');
            $('#drpDay').removeClass('bg-error');
            $('#drpPeriod').addClass('bg-error');
            $('#drpStandards').removeClass('bg-error');
            $('#drpSubject').removeClass('bg-error');
            $('#drpAcademicYear').removeClass('bg-error');
        }
        return ret;
    }

    $scope.applyHere = function (e) {
        $scope.applyoppId = $(e.currentTarget).attr('data-oppid');
        $scope.personDescription = '';
        $('.valid-desc').css('display', 'none');
        $('.valid-oppStatus').css('display', 'none');
    }

    $scope.getAllProfessions = function (userId) {
        opportunitiesService.getAllProfessions(userId)
            .then(function (result) {
                $scope.IntialLoad.allProfessions = result.Response;
            },
                function (reason) {
                    console.log('Error' + reason);
                });
    }

    $scope.getOpportunities = function () {
        opportunitiesService.getOpportunityData($scope.userId)
            .then(function (result) {
                $scope.IntialLoad.opportunitiesCollection = result.Response;
            },
                function (reason) {
                    console.log('Error' + reason);
                });
    }

    $scope.getMyAppliedOpportunities = function () {
        if ($scope.userId != '' && $scope.userId != 0) {
            opportunitiesService.getMyAppliedOpportunityData($scope.userId)
                .then(function (result) {
                    $scope.IntialLoad.appliedOpportunitiesCollection = result.Response;
                },
                    function (reason) {
                        console.log('Error' + reason);
                    });
        }
    }

    $scope.getMyOpportunities = function () {
        if ($scope.userId != '' && $scope.userId != 0) {
            opportunitiesService.getMyOpportunityData($scope.userId)
                .then(function (result) {
                    $scope.IntialLoad.myOpportunitiesCollection = result.Response;
                },
                    function (reason) {
                        console.log('Error' + reason);
                    });
        }
        $scope.getAllProfessions($scope.userId);
    }

    $scope.getOpportunities();
    $scope.getMyOpportunities();
    $scope.getMyAppliedOpportunities();

    $scope.Save = function () {
        if ($scope.userId != '' && $scope.userId != 0) {
            if (validationsSave()) {
                var data = {
                    OpportunityId: $scope.opportunityId,
                    Remuneration: $scope.remuneration,
                    StartDate: $scope.startDate,
                    EndDate: $scope.endDate,
                    CreatedByUserId: $scope.userId,
                    Description: $scope.description,
                    IsActive: true,
                    ProfessionIds: $scope.selectedProfessionIds.join()
                }
                if ($scope.opportunityId === 0) {
                    opportunitiesService.Save(data)
                        .then(function () {
                            $scope.getMyOpportunities();
                            $scope.getOpportunities();
                            alert('Saved successfully!!');
                            $('#oppAddUpdate').modal('hide');
                        },
                       function (reason) {
                           console.log('ERROR' + reason);
                       });
                } else {
                    opportunitiesService.Update(data)
                    .then(function () {
                        $scope.getMyOpportunities();
                        $scope.getOpportunities();
                        alert('Updated successfully!!');
                        $('#oppAddUpdate').modal('hide');
                    },
                        function (reason) {
                            console.log('ERROR' + reason);
                        });
                }
            }
        }
        else {
            alert('Please Do Login or Register !!');
        }
    }

    $scope.EditSet = function (data) {

        $scope.selectedProfessions = [];
        $scope.selectedProfessionIds = [];

        $scope.opportunityId = data.OpportunityId;
        $scope.remuneration = data.Remuneration;
        $scope.startDate = new Date(data.StartDate);
        $scope.endDate = new Date(data.EndDate);
        $scope.description = data.Description;
        $scope.CreatedByUserId = data.CreatedByUserId;

        angular.forEach(data.Professions, function (item) {
            $scope.selectedProfessions.push(item.ProfessionName);
            $scope.selectedProfessionIds.push(item.Id);
        })
    }
    $("#oppImageUpload").fileinput({
        uploadUrl: '#', // you must set a valid URL here else you will get an error
        allowedFileExtensions: ['jpg', 'png', 'gif'],
        overwriteInitial: false,
        maxFileSize: 5000,
        maxFilesNum: 10,
        width: 120,
        height: 100,
        //allowedFileTypes: ['image', 'video', 'flash'],
        slugCallback: function (filename) {
            console.log(filename.replace('(', '_').replace(']', '_'));
            return filename.replace('(', '_').replace(']', '_');
        }
    });


    $scope.toggleSelection = function toggleSelection(profession) {
        var idx = $scope.selectedProfessions.indexOf(profession.ProfessionName);
        // is currently selected
        if (idx > -1) {
            $scope.selectedProfessions.splice(idx, 1);
            $scope.selectedProfessionIds.splice(idx, 1);
        }
        else {
            $scope.selectedProfessions.push(profession.ProfessionName);
            $scope.selectedProfessionIds.push(profession.ProfessionId);
        }
    };

    $scope.Update = function (opportunityId) {
        if (validationsSave()) {
            var data = {
                OpportunityId: data.OpportunityId,
                Remuneration: $scope.remuneration,
                StartDate: $scope.startDate,
                EndDate: $scope.endDate,
                CreatedByUserId: $scope.userId,
                Description: $scope.description,
                ProfessionIds: ''
            }
            opportunitiesService.Update(data)
                .then(function () {
                    alert('Saved successfully!!');
                },
                    function (reason) {
                        console.log('ERROR' + reason);
                    });
        }
    }

    $scope.Apply = function () {
        if ($scope.userId != '' && $scope.userId != 0) {
            if ($scope.personDescription != '') {
                $('.valid-desc').css('display', 'none');
                var data = {
                    OpportunityId: $scope.applyoppId,
                    UserId: $scope.userId,
                    Description: $scope.personDescription,
                    ImageUrls: ''
                }
                opportunitiesService.ApplyOpportunity(data)
                        .then(function (res) {
                            if (res.Response) {
                                alert('Successfully Applied!!');
                                $('#myModal').modal('hide');
                                $scope.getMyAppliedOpportunities();
                            }
                            else {
                                $('.valid-oppStatus').css('display', 'block');
                            }
                        },
                            function (reason) {
                                console.log('ERROR' + reason);
                            });
            }
            else {
                $('.valid-desc').css('display', 'block');
            }
        }
        else {
            alert('Please Do Login or Register !!');
        }
    }
    $scope.New = function () {
        $scope.opportunityId = 0;
        $scope.remuneration = '';
        $scope.startDate = new Date();
        $scope.endDate = new Date();
        $scope.description = '';
        $scope.selectedProfessions = [];;
    }
    

    $scope.Login = function () {
        $('.close-btn').click();
    }

    $scope.delete = function (opportunityId) {
        if (confirm("Are you sure want to delete??")) {
            opportunitiesService.Delete(opportunityId)
            .then(function (result) {
                alert('Deleted successfully!!');
                $scope.getMyOpportunities();
            },
                function (reason) {
                    console.log('ERROR ' + reason);
                });
        }
    }
    
}]);