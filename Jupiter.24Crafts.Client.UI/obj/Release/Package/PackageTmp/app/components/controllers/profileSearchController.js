'use strict';

app.controller('profileSearchController', ['$scope', 'authService', '$filter', function ($scope, authService, profileSearchService, $filter, $timeout, $q, $log) {
    var self = this;
    self.selectedItem = null;
    self.searchText = null;

    self.simulateQuery = false;
    self.isDisabled = false;
    $scope.isfullProfile = false;
    self.repos = loadPrrofiles();
    self.querySearch = querySearch;
    self.selectedItemChange = selectedItemChange;
    self.searchTextChange = searchTextChange;
    self.newState = newState;

    function newState(text) {
        alert("Sorry! You'll need to create a Profile for " + text + " first!");
    }
    function querySearch(query) {
        var results = query ? self.repos.filter(createFilterFor(query)) : self.repos,
            deferred;
        if (self.simulateQuery) {
            deferred = $q.defer();
            $timeout(function () { deferred.resolve(results); }, Math.random() * 1000, false);
            return deferred.promise;
        } else {
            return results;
        }
    }


    function searchTextChange(text) {
        $log.info('Text changed to ' + text);
    }

    function selectedItemChange(item) {
        $log.info('Item changed to ' + JSON.stringify(item));
    }

    /**
     * Build `components` list of key/value pairs
     */
    function loadPrrofiles() {
        var repos = [
           { name: 'Nancy sailand', city: 'Pune', src: "https://www.cheme.cornell.edu/engineering2/customcf/iws_ai_faculty_display/ai_images/mjp31-profile.jpg", Profession: "Teacher" },
           { name: 'Neminath', city: 'Solapur', src: "https://www.bme.cornell.edu/engineering2/customcf/iws_ai_faculty_display/ai_images/mrk93-profile.jpg", Profession: "Film Hero" },
           { name: 'Kishor', city: 'Mumbai', src: "https://www.inuvator.com/images/profile-pic.jpg", Profession: "Film Producer" },
           { name: 'Sainath', city: 'Mumbai', src: "https://www.cloudboost.io/images/profilepic1.png", Profession: "Director" },
           { name: 'Avinash', city: 'Mumbai', src: "https://upload.wikimedia.org/wikipedia/en/7/70/Shawn_Tok_Profile.jpg", Profession: "Test Search" },
           { name: 'Mahaveer', city: 'Solapur', src: "https://i.vimeocdn.com/portrait/4900311_640x640", Profession: "Test Search" },
           { name: 'Sreemuki', city: 'Solapur', src: "http://4.bp.blogspot.com/-k_Iglc7r5aU/VhJwo8rIdlI/AAAAAAABoLY/IgLYWM-UH14/s1600/Srimukhi-new-glam-pics-030.jpg", Profession: "Tv Anchor" },
           { name: 'Samantha', city: 'Solapur', src: "http://www.bharatstudent.com/ng7uvideo/bs/news/1013/7826-Samantha.jpg", Profession: "Heroine" },
           { name: 'Kajal Agarwal', city: 'Solapur', src: "http://cdn2.stylecraze.com/wp-content/uploads/2013/07/kajal-agarwal-navel.jpg", Profession: "Film Producre" },
           { name: 'All Arjun', city: 'Solapur', src: "http://www.123telugu.com/content/wp-content/uploads/2015/10/allu-arjun1.jpg", Profession: "Top Hero" },
           { name: 'Sachin', city: 'Solapur', src: "http://i.dailymail.co.uk/i/pix/2013/08/28/article-2404712-1B815F79000005DC-228_306x423.jpg", Profession: "Cricketer" },
           { name: 'Virat Kohli', city: 'Solapur', src: "http://www.khaskhabar.com/images/picture_image/sports-team-india-retain-top-spot-in-odi-rankings-virat-kohli-2nd-in-batting-chart-1-60405-60405-virat-kohli-2.jpg", Profession: "cricket Captain" },
           { name: 'Pradeep Machiraju', city: 'Solapur', src: "https://www.cinealerts.com/images/news/4342.jpg", Profession: "TV Anchor" },
           { name: 'Ravi Lasya', city: 'Solapur', src: "http://timesofindia.indiatimes.com/photo/49660259.cms", Profession: "TV Anchor" }

        ];
        return repos.map(function (repo) {
            repo.value = repo.name.toLowerCase();
            return repo;
        });
    }

    /**
     * Create filter function for a query string
     */
    function createFilterFor(query) {
        var lowercaseQuery = angular.lowercase(query);

        return function filterFn(item) {
            return (item.value.indexOf(lowercaseQuery) === 0);
        };

    }

    $scope.seeFullProfile = function () {
        $('.tile').css('display', 'none');
        $scope.isfullProfile = true;
    }

}]);

